using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.Service
{
    using Clinic_Management_BLL.AuditWritter;
    using Clinic_Management_BLL.CrudInterface;
    using Clinic_Management_BLL.LoginProcess;
    using Clinic_Management_BLL.ResultWraper;
    using Clinic_Management_DAL.Data;
    using Clinic_Management_Entities.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class PaymentService : BaseCrudService<Payment>
    {
        // =======================
        // PERMISSIONS
        // =======================
        protected override string CreatePermissionCode => "PAYMENT_CREATE";
        protected override string UpdatePermissionCode => "PAYMENT_UPDATE";
        protected override string DeletePermissionCode => "PAYMENT_DELETE";
        protected override string ViewPermissionCode => "PAYMENT_VIEW";

        protected override string EntityName => "Payment";

        // =======================
        // DAL WIRING
        // =======================
        protected override int DalCreate(Payment entity)
            => PaymentData.Insert(entity);

        protected override bool DalUpdate(Payment entity)
            => PaymentData.Update(entity);

        protected override bool DalDelete(int id)
            => PaymentData.GetById(id) != null
               && PaymentData.Delete(id);

        protected override Payment? DalGetById(int id)
            => PaymentData.GetById(id);

        protected override IEnumerable<Payment> DalGetAll()
            => PaymentData.GetAll() ?? Enumerable.Empty<Payment>();

        protected override int GetEntityId(Payment entity)
            => entity.PaymentId;

        // =======================
        // VALIDATION
        // =======================
        protected override ValidationResult.ValidationResult IsValidateData(Payment p)
        {
            var v = ValidationResult.ValidationResult.Success();

            if (p is null)
            {
                v.Add("Payment is required.");
                return v;
            }

            if (p.InvoiceId <= 0) v.Add("InvoiceId is required.");
            if (p.PaymentMethodId <= 0) v.Add("PaymentMethodId is required.");

            // CK_Payments_Amount
            if (p.Amount <= 0) v.Add("Amount must be > 0.");

            // Optional: refund should still be positive amount (your table supports IsRefund)
            // if (p.IsRefund && p.Amount <= 0) v.Add("Refund amount must be > 0.");

            return v;
        }

        // =======================
        // READ OPERATIONS (with Permission + Audit)
        // =======================
        public Result<Payment> GetByIdSafe(int paymentId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<Payment>.Fail("Permission denied.");

            if (paymentId <= 0)
                return Result<Payment>.Fail("Invalid PaymentId.");

            var p = PaymentData.GetById(paymentId);
            if (p is null)
                return Result<Payment>.Fail("Payment not found.");

            AuditWriter.Write(
                action: GetAuditMessage("VIEW", p),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: p.PaymentId.ToString(),
                success: true,
                newEntity: p
            );

            return Result<Payment>.Ok(p);
        }

        public Result<IEnumerable<Payment>> GetAllSafe(DateTime? from = null, DateTime? to = null)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<Payment>>.Fail("Permission denied.");

            var list = PaymentData.GetAll(from, to) ?? Enumerable.Empty<Payment>();

            AuditLogData.Log("View Payments", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<Payment>>.Ok(list);
        }

        public Result<IEnumerable<Payment>> GetByInvoiceId(int invoiceId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<Payment>>.Fail("Permission denied.");

            if (invoiceId <= 0)
                return Result<IEnumerable<Payment>>.Fail("Invalid InvoiceId.");

            var list = PaymentData.GetByInvoiceId(invoiceId) ?? Enumerable.Empty<Payment>();

            AuditLogData.Log("View Payments By Invoice", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<Payment>>.Ok(list);
        }

        public Result<IEnumerable<Payment>> GetByPaymentMethodId(byte paymentMethodId, DateTime? from = null, DateTime? to = null)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<Payment>>.Fail("Permission denied.");

            if (paymentMethodId <= 0)
                return Result<IEnumerable<Payment>>.Fail("Invalid PaymentMethodId.");

            var list = PaymentData.GetByPaymentMethodId(paymentMethodId, from, to) ?? Enumerable.Empty<Payment>();

            AuditLogData.Log("View Payments By Method", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<Payment>>.Ok(list);
        }

        // =======================
        // CREATE / UPDATE (Smart wrappers with Audit)
        // =======================
        public Result<int> CreatePayment(Payment p)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result<int>.Fail("Permission denied.");

            // attach audit fields
            p.CreatedByUserId = SecurityContext.Current.UserId;

            var v = IsValidateData(p);
            if (!v.IsValid)
                return Result<int>.Fail(v.Errors);

            // (Recommended) protect from overpay here (uses InvoiceData)
            var inv = InvoiceData.GetById(p.InvoiceId);
            if (inv is null)
                return Result<int>.Fail("Invoice not found.");

            if (!p.IsRefund)
            {
                decimal newPaid = inv.PaidAmount + p.Amount;
                if (newPaid > inv.TotalAmount)
                    return Result<int>.Fail("Payment exceeds invoice total.");
            }
            else
            {
                // refund means reduce paid amount (optional rule)
                if (p.Amount > inv.PaidAmount)
                    return Result<int>.Fail("Refund exceeds paid amount.");
            }

            int newId = PaymentData.Insert(p);

            bool ok = newId > 0;
            if (ok)
            {
                p.PaymentId = newId;

                // keep invoice totals in sync
                bool invoiceOk = ApplyInvoiceEffect(inv, p);

                AuditWriter.Write(
                    action: GetAuditMessage("CREATE", p),
                    performedBy: SecurityContext.Current.UserId,
                    entityType: EntityName,
                    entityId: newId.ToString(),
                    success: ok && invoiceOk,
                    newEntity: p,
                    failureReason: invoiceOk ? null : "Invoice update failed after payment insert"
                );

                if (ok) AuditLogData.Log("Create Payment", true, SecurityContext.Current.UserId, EntityName);

                return (ok && invoiceOk)
                    ? Result<int>.Ok(newId)
                    : Result<int>.Fail("Payment created but invoice update failed.");
            }

            AuditWriter.Write(
                action: $"{EntityName} CREATE failed",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: "0",
                success: false,
                newEntity: p,
                failureReason: "Insert returned 0"
            );

            return Result<int>.Fail("Failed to create payment.");
        }

        public Result UpdatePayment(Payment p)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (p.PaymentId <= 0)
                return Result.Fail("Invalid PaymentId.");

            var old = PaymentData.GetById(p.PaymentId);
            if (old is null)
                return Result.Fail("Payment not found.");

            var v = IsValidateData(p);
            if (!v.IsValid)
                return Result.Fail(v.Errors);

            // Recommended: do NOT allow changing financial-impact fields after creation
            // If you want to allow, you must reverse old impact and apply new impact (needs transaction support)
            if (p.InvoiceId != old.InvoiceId ||
                p.Amount != old.Amount ||
                p.IsRefund != old.IsRefund)
            {
                return Result.Fail("Updating InvoiceId/Amount/IsRefund is not allowed. Create a new payment or implement reversal workflow.");
            }

            bool ok = PaymentData.Update(p);

            AuditWriter.Write<Payment>(
                action: GetAuditMessage("UPDATE", p),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: p.PaymentId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: p,
                failureReason: ok ? null : "Update returned false"
            );

            if (ok) AuditLogData.Log("Update Payment", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Failed to update payment.");
        }

        // =======================
        // DELETE (Hard)
        // =======================
        public Result DeletePayment(int paymentId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            if (paymentId <= 0)
                return Result.Fail("Invalid PaymentId.");

            var old = PaymentData.GetById(paymentId);
            if (old is null)
                return Result.Fail("Payment not found.");

            // Recommended: do NOT allow delete if it affects invoice
            // Better: create a refund payment instead.
            return Result.Fail("Deleting payments is not allowed. Use a refund payment (IsRefund=1) instead.");
        }

        // =======================
        // HELPERS
        // =======================
        private static bool ApplyInvoiceEffect(Invoice inv, Payment p)
        {
            // DB-side increment exists: InvoiceData.AddPayment()
            // For refund, you may implement InvoiceData.AddPayment with negative OR create a separate DAL method.
            if (!p.IsRefund)
            {
                bool ok = InvoiceData.AddPayment(inv.InvoiceId, p.Amount, SecurityContext.Current.UserId);
                return ok;
            }
            else
            {
                // If you want refunds, implement InvoiceData.AddRefund(...) or allow negative in AddPayment.
                // For now, we keep it explicit to avoid hidden behavior.
                return false;
            }
        }

        // =======================
        // AUDIT MESSAGE
        // =======================
        protected override string GetAuditMessage(string operation, Payment entity)
            => $"{EntityName} [{entity.PaymentId}] {operation} performed.";
    }
}
