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

    public sealed class PaymentMethodService : BaseCrudService<PaymentMethod>
    {
        // =======================
        // PERMISSIONS
        // =======================
        protected override string CreatePermissionCode => "PAYMENT_METHOD_CREATE";
        protected override string UpdatePermissionCode => "PAYMENT_METHOD_UPDATE";
        protected override string DeletePermissionCode => "PAYMENT_METHOD_DELETE";
        protected override string ViewPermissionCode => "PAYMENT_METHOD_VIEW";

        protected override string EntityName => "PaymentMethod";

        // =======================
        // DAL WIRING
        // =======================
        // NOTE: PaymentMethodId is NOT identity (byte key). Your DAL Insert returns bool.
        // BaseCrudService expects int, so we adapt: return PaymentMethodId on success, else 0.
        protected override int DalCreate(PaymentMethod entity)
            => PaymentMethodData.Insert(entity) ? entity.PaymentMethodId : (byte)0;

        protected override bool DalUpdate(PaymentMethod entity)
            => PaymentMethodData.Update(entity);

        protected override bool DalDelete(int id)
        {
            if (id <= 0 || id > byte.MaxValue) return false;

            byte key = (byte)id;
            return PaymentMethodData.GetById(key) != null
                   && PaymentMethodData.Delete(key);
        }

        protected override PaymentMethod? DalGetById(int id)
        {
            if (id <= 0 || id > byte.MaxValue) return null;
            return PaymentMethodData.GetById((byte)id);
        }

        protected override IEnumerable<PaymentMethod> DalGetAll()
            => PaymentMethodData.GetAll() ?? Enumerable.Empty<PaymentMethod>();

        protected override int GetEntityId(PaymentMethod entity)
            => entity.PaymentMethodId;

        // =======================
        // VALIDATION
        // =======================
        protected override ValidationResult.ValidationResult IsValidateData(PaymentMethod m)
        {
            var v = ValidationResult.ValidationResult.Success();

            if (m is null)
            {
                v.Add("PaymentMethod is required.");
                return v;
            }

            if (m.PaymentMethodId <= 0)
                v.Add("PaymentMethodId is required.");

            if (string.IsNullOrWhiteSpace(m.Name))
                v.Add("Name is required.");

            if (!string.IsNullOrWhiteSpace(m.Name))
            {
                bool exists = PaymentMethodData.ExistsByName(
                    m.Name.Trim(),
                    ignoreId: m.PaymentMethodId > 0 ? m.PaymentMethodId : (byte?)null);

                if (exists)
                    v.Add("Payment method name already exists.");
            }

            return v;
        }

        // =======================
        // READ OPERATIONS (with Permission + Audit)
        // =======================
        public Result<PaymentMethod> GetByIdSafe(byte paymentMethodId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<PaymentMethod>.Fail("Permission denied.");

            if (paymentMethodId <= 0)
                return Result<PaymentMethod>.Fail("Invalid PaymentMethodId.");

            var m = PaymentMethodData.GetById(paymentMethodId);
            if (m is null)
                return Result<PaymentMethod>.Fail("Payment method not found.");

            AuditWriter.Write(
                action: GetAuditMessage("VIEW", m),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: m.PaymentMethodId.ToString(),
                success: true,
                newEntity: m
            );

            return Result<PaymentMethod>.Ok(m);
        }

        public Result<IEnumerable<PaymentMethod>> GetAllSafe()
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<PaymentMethod>>.Fail("Permission denied.");

            var list = PaymentMethodData.GetAll() ?? Enumerable.Empty<PaymentMethod>();

            AuditLogData.Log("View Payment Methods", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<PaymentMethod>>.Ok(list);
        }

        // =======================
        // CREATE / UPDATE (Smart wrappers with Audit)
        // =======================
        public Result<byte> CreatePaymentMethod(PaymentMethod m)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result<byte>.Fail("Permission denied.");

            var v = IsValidateData(m);
            if (!v.IsValid)
                return Result<byte>.Fail(v.Errors);

            bool ok = PaymentMethodData.Insert(m);

            AuditWriter.Write(
                action: GetAuditMessage("CREATE", m),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: m.PaymentMethodId.ToString(),
                success: ok,
                newEntity: m,
                failureReason: ok ? null : "Insert returned false"
            );

            if (ok) AuditLogData.Log("Create Payment Method", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result<byte>.Ok(m.PaymentMethodId) : Result<byte>.Fail("Failed to create payment method.");
        }

        public Result UpdatePaymentMethod(PaymentMethod m)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (m.PaymentMethodId <= 0)
                return Result.Fail("Invalid PaymentMethodId.");

            var old = PaymentMethodData.GetById(m.PaymentMethodId);
            if (old is null)
                return Result.Fail("Payment method not found.");

            var v = IsValidateData(m);
            if (!v.IsValid)
                return Result.Fail(v.Errors);

            bool ok = PaymentMethodData.Update(m);

            AuditWriter.Write<PaymentMethod>(
                action: GetAuditMessage("UPDATE", m),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: m.PaymentMethodId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: m,
                failureReason: ok ? null : "Update returned false"
            );

            if (ok) AuditLogData.Log("Update Payment Method", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Failed to update payment method.");
        }

        // =======================
        // DELETE (Hard)
        // =======================
        public Result DeletePaymentMethod(byte paymentMethodId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            if (paymentMethodId <= 0)
                return Result.Fail("Invalid PaymentMethodId.");

            var old = PaymentMethodData.GetById(paymentMethodId);
            if (old is null)
                return Result.Fail("Payment method not found.");

            bool ok = PaymentMethodData.Delete(paymentMethodId);

            AuditWriter.Write<PaymentMethod>(
                action: GetAuditMessage("DELETE", old),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: paymentMethodId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: default,
                failureReason: ok ? null : "Delete returned false"
            );

            if (ok) AuditLogData.Log("Delete Payment Method", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Delete failed.");
        }

        // =======================
        // AUDIT MESSAGE
        // =======================
        protected override string GetAuditMessage(string operation, PaymentMethod entity)
            => $"{EntityName} [{entity.PaymentMethodId}] {operation} performed.";
    }
}
