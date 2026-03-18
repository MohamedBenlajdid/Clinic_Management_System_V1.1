using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.Service
{
    using Clinic_Management_BLL.AuditWritter;
    using Clinic_Management_BLL.CrudInterface;
    using Clinic_Management_BLL.Data;
    using Clinic_Management_BLL.LoginProcess;
    using Clinic_Management_BLL.ResultWraper;
    using Clinic_Management_DAL.Data;
    using Clinic_Management_Entities.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public enum enInvoiceStatus : byte
    {
        Draft = 1,
        Issued = 2,
        PartiallyPaid = 3,
        Paid = 4,
        Overdue = 5,
        Cancelled = 6
    }

    public sealed class InvoiceService : BaseCrudService<Invoice>
    {
        // =======================
        // PERMISSIONS
        // =======================
        protected override string CreatePermissionCode => "INVOICE_CREATE";
        protected override string UpdatePermissionCode => "INVOICE_UPDATE";
        protected override string DeletePermissionCode => "INVOICE_DELETE";
        protected override string ViewPermissionCode => "INVOICE_VIEW";

        protected override string EntityName => "Invoice";

        // =======================
        // DAL WIRING
        // =======================
        protected override int DalCreate(Invoice entity)
            => InvoiceData.Insert(entity);

        protected override bool DalUpdate(Invoice entity)
            => InvoiceData.Update(entity);

        protected override bool DalDelete(int id)
            => InvoiceData.GetById(id) != null
               && InvoiceData.SoftDelete(id, SecurityContext.Current.UserId);

        protected override Invoice? DalGetById(int id)
            => InvoiceData.GetById(id);

        protected override IEnumerable<Invoice> DalGetAll()
            => InvoiceData.GetAll() ?? Enumerable.Empty<Invoice>();

        protected override int GetEntityId(Invoice entity)
            => entity.InvoiceId;

        // =======================
        // VALIDATION
        // =======================
        protected override ValidationResult.ValidationResult IsValidateData(Invoice i)
        {
            var v = ValidationResult.ValidationResult.Success();

            if (i is null)
            {
                v.Add("Invoice is required.");
                return v;
            }

            if (string.IsNullOrWhiteSpace(i.InvoiceNumber))
                v.Add("InvoiceNumber is required.");

            if (i.PatientId <= 0)
                v.Add("PatientId is required.");

            // Amount rules (match your DAL CK idea)
            if (i.SubTotal < 0) v.Add("SubTotal must be >= 0.");
            if (i.DiscountAmount < 0) v.Add("DiscountAmount must be >= 0.");
            if (i.TaxAmount < 0) v.Add("TaxAmount must be >= 0.");
            if (i.TotalAmount < 0) v.Add("TotalAmount must be >= 0.");
            if (i.PaidAmount < 0) v.Add("PaidAmount must be >= 0.");

            // Total formula (recommended)
            decimal expectedTotal = i.SubTotal - i.DiscountAmount + i.TaxAmount;
            if (expectedTotal < 0) v.Add("TotalAmount cannot be negative (check SubTotal/Discount/Tax).");
            if (i.TotalAmount != expectedTotal)
                v.Add("TotalAmount must equal SubTotal - DiscountAmount + TaxAmount.");

            // Paid cannot exceed total (recommended)
            if (i.PaidAmount > i.TotalAmount)
                v.Add("PaidAmount cannot be greater than TotalAmount.");

            // Optional: DueDate >= IssueDate (IssueDate is set by DB on insert)
            // If you set IssueDate in UI later, you can validate it here.

            // Uniqueness
            if (!string.IsNullOrWhiteSpace(i.InvoiceNumber))
            {
                bool exists = InvoiceData.ExistsByInvoiceNumber(
                    i.InvoiceNumber.Trim(),
                    ignoreInvoiceId: i.InvoiceId > 0 ? i.InvoiceId : null);

                if (exists)
                    v.Add("InvoiceNumber already exists.");
            }


            if (i.AppointmentId > 0)
            {
                var appointment = AppointmentData.GetById((int)i.AppointmentId);

                if (appointment == null)
                {
                    v.Add("Associated appointment does not exist.");
                }
                else if (appointment.Status != (byte)enAppointmentStatus.InProgress)
                {
                    v.Add("Medical record can only be created when appointment status is InProgress.");
                }
            }


            return v;
        }

        // =======================
        // READ OPERATIONS (with Permission + Audit)
        // =======================
        public Result<Invoice> GetByIdSafe(int invoiceId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<Invoice>.Fail("Permission denied.");

            if (invoiceId <= 0)
                return Result<Invoice>.Fail("Invalid InvoiceId.");

            var i = InvoiceData.GetById(invoiceId);
            if (i is null)
                return Result<Invoice>.Fail("Invoice not found.");

            AuditWriter.Write(
                action: GetAuditMessage("VIEW", i),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: i.InvoiceId.ToString(),
                success: true,
                newEntity: i
            );

            return Result<Invoice>.Ok(i);
        }

        public Result<Invoice> GetByInvoiceNumberSafe(string invoiceNumber)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<Invoice>.Fail("Permission denied.");

            if (string.IsNullOrWhiteSpace(invoiceNumber))
                return Result<Invoice>.Fail("InvoiceNumber is required.");

            var i = InvoiceData.GetByInvoiceNumber(invoiceNumber.Trim());
            if (i is null)
                return Result<Invoice>.Fail("Invoice not found.");

            AuditWriter.Write(
                action: $"{EntityName} [{i.InvoiceId}] VIEW_BY_NUMBER performed.",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: i.InvoiceId.ToString(),
                success: true,
                newEntity: i
            );

            return Result<Invoice>.Ok(i);
        }

        public Result<IEnumerable<Invoice>> GetAllSafe()
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<Invoice>>.Fail("Permission denied.");

            var list = InvoiceData.GetAll() ?? Enumerable.Empty<Invoice>();

            AuditLogData.Log("View Invoices", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<Invoice>>.Ok(list);
        }

        public Result<IEnumerable<Invoice>> GetByPatientId(int patientId, DateTime? from = null, DateTime? to = null, bool includeDeleted = false)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<Invoice>>.Fail("Permission denied.");

            if (patientId <= 0)
                return Result<IEnumerable<Invoice>>.Fail("Invalid PatientId.");

            var list = InvoiceData.GetByPatientId(patientId, from, to, includeDeleted) ?? Enumerable.Empty<Invoice>();

            AuditLogData.Log("View Invoices By Patient", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<Invoice>>.Ok(list);
        }

        public Result<IEnumerable<Invoice>> GetByAppointmentId(int appointmentId, bool includeDeleted = false)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<Invoice>>.Fail("Permission denied.");

            if (appointmentId <= 0)
                return Result<IEnumerable<Invoice>>.Fail("Invalid AppointmentId.");

            var list = InvoiceData.GetByAppointmentId(appointmentId, includeDeleted) ?? Enumerable.Empty<Invoice>();

            if(list.Any())
            {
                AuditLogData.Log("View Invoices By Appointment", true, SecurityContext.Current.UserId, EntityName);

                return Result<IEnumerable<Invoice>>.Ok(list);

            }


            return Result<IEnumerable<Invoice>>.Fail("Cannot Find Invoice Record");


        }

        // =======================
        // CREATE / UPDATE (Smart wrappers with Audit)
        // =======================
        public Result<int> CreateInvoice(Invoice i)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result<int>.Fail("Permission denied.");

            // attach audit fields
            i.CreatedByUserId = SecurityContext.Current.UserId;
            i.UpdatedByUserId = null;

            // recommended: compute totals server-side (prevents UI bugs)
            NormalizeAmounts(i);

            var v = IsValidateData(i);
            if (!v.IsValid)
                return Result<int>.Fail(v.Errors);

            int newId = InvoiceData.Insert(i);

            bool ok = newId > 0;
            if (ok)
            {
                i.InvoiceId = newId;

                AuditWriter.Write(
                    action: GetAuditMessage("CREATE", i),
                    performedBy: SecurityContext.Current.UserId,
                    entityType: EntityName,
                    entityId: newId.ToString(),
                    success: true,
                    newEntity: i
                );

                AuditLogData.Log("Create Invoice", true, SecurityContext.Current.UserId, EntityName);

                return Result<int>.Ok(newId);
            }

            AuditWriter.Write(
                action: $"{EntityName} CREATE failed",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: "0",
                success: false,
                newEntity: i,
                failureReason: "Insert returned 0"
            );

            return Result<int>.Fail("Failed to create invoice.");
        }

        public Result UpdateInvoice(Invoice i)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (i.InvoiceId <= 0)
                return Result.Fail("Invalid InvoiceId.");

            var old = InvoiceData.GetById(i.InvoiceId);
            if (old is null)
                return Result.Fail("Invoice not found.");

            i.UpdatedByUserId = SecurityContext.Current.UserId;

            NormalizeAmounts(i);

            var v = IsValidateData(i);
            if (!v.IsValid)
                return Result.Fail(v.Errors);

            bool ok = InvoiceData.Update(i);

            AuditWriter.Write<Invoice>(
                action: GetAuditMessage("UPDATE", i),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: i.InvoiceId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: i,
                failureReason: ok ? null : "Update returned false"
            );

            if (ok) AuditLogData.Log("Update Invoice", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Failed to update invoice.");
        }

        // =======================
        // STATUS / PAYMENT
        // =======================
        public Result SetStatus(int invoiceId, enInvoiceStatus status)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (invoiceId <= 0)
                return Result.Fail("Invalid InvoiceId.");

            var old = InvoiceData.GetById(invoiceId);
            if (old is null)
                return Result.Fail("Invoice not found.");

            // Optional transition rules (keep simple)
            // If you want strict rules, implement IsValidTransition(oldStatus, status)
            bool ok = InvoiceData.SetStatus(invoiceId, (byte)status, SecurityContext.Current.UserId);

            var after = Clone(old);
            after.Status = (byte)status;
            after.UpdatedByUserId = SecurityContext.Current.UserId;
            after.UpdatedAt = DateTime.UtcNow;

            AuditWriter.Write<Invoice>(
                action: $"{EntityName} [{invoiceId}] STATUS performed.",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: invoiceId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: after,
                failureReason: ok ? null : "SetStatus returned false"
            );

            if (ok) AuditLogData.Log("Set Invoice Status", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Status update failed.");
        }

        public Result AddPayment(int invoiceId, decimal amount)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (invoiceId <= 0)
                return Result.Fail("Invalid InvoiceId.");

            if (amount <= 0)
                return Result.Fail("Payment amount must be > 0.");

            var old = InvoiceData.GetById(invoiceId);
            if (old is null)
                return Result.Fail("Invoice not found.");

            // Prevent overpay at BLL level (recommended)
            decimal newPaid = old.PaidAmount + amount;
            if (newPaid > old.TotalAmount)
                return Result.Fail("Payment exceeds invoice total.");

            bool ok = InvoiceData.AddPayment(invoiceId, amount, SecurityContext.Current.UserId);

            var after = Clone(old);
            after.PaidAmount = newPaid;
            after.RemainingAmount = Math.Max(0, after.TotalAmount - after.PaidAmount);
            after.Status = (byte)DeriveStatusFromAmounts(after);
            after.UpdatedByUserId = SecurityContext.Current.UserId;
            after.UpdatedAt = DateTime.UtcNow;

            AuditWriter.Write<Invoice>(
                action: $"{EntityName} [{invoiceId}] ADD_PAYMENT performed.",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: invoiceId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: after,
                failureReason: ok ? null : "AddPayment returned false"
            );

            if (ok) AuditLogData.Log("Add Invoice Payment", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Add payment failed.");
        }

        // =======================
        // DELETE (Soft)
        // =======================
        public Result SoftDelete(int invoiceId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            if (invoiceId <= 0)
                return Result.Fail("Invalid InvoiceId.");

            var old = InvoiceData.GetById(invoiceId);
            if (old is null)
                return Result.Fail("Invoice not found.");

            bool ok = InvoiceData.SoftDelete(invoiceId, SecurityContext.Current.UserId);

            AuditWriter.Write<Invoice>(
                action: GetAuditMessage("DELETE", old),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: invoiceId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: default,
                failureReason: ok ? null : "SoftDelete returned false"
            );

            if (ok) AuditLogData.Log("Delete Invoice", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Delete failed.");
        }

        // =======================
        // HELPERS
        // =======================
        private static void NormalizeAmounts(Invoice i)
        {
            // ensure Remaining/Total are consistent before DAL insert/update
            i.TotalAmount = i.SubTotal - i.DiscountAmount + i.TaxAmount;
            if (i.TotalAmount < 0) i.TotalAmount = 0;

            if (i.PaidAmount < 0) i.PaidAmount = 0;
            if (i.PaidAmount > i.TotalAmount) i.PaidAmount = i.TotalAmount;

            i.RemainingAmount = Math.Max(0, i.TotalAmount - i.PaidAmount);

            // optional: derive status automatically
            i.Status = (byte)DeriveStatusFromAmounts(i);
        }

        private static enInvoiceStatus DeriveStatusFromAmounts(Invoice i)
        {
            if (i.TotalAmount <= 0) return enInvoiceStatus.Draft;

            if (i.PaidAmount <= 0) return enInvoiceStatus.Issued;

            if (i.PaidAmount >= i.TotalAmount) return enInvoiceStatus.Paid;

            return enInvoiceStatus.PartiallyPaid;
        }

        private static Invoice Clone(Invoice src) => new Invoice
        {
            InvoiceId = src.InvoiceId,
            InvoiceNumber = src.InvoiceNumber,
            PatientId = src.PatientId,
            AppointmentId = src.AppointmentId,
            IssueDate = src.IssueDate,
            DueDate = src.DueDate,
            SubTotal = src.SubTotal,
            DiscountAmount = src.DiscountAmount,
            TaxAmount = src.TaxAmount,
            TotalAmount = src.TotalAmount,
            PaidAmount = src.PaidAmount,
            RemainingAmount = src.RemainingAmount,
            Status = src.Status,
            Notes = src.Notes,
            CreatedAt = src.CreatedAt,
            UpdatedAt = src.UpdatedAt,
            CreatedByUserId = src.CreatedByUserId,
            UpdatedByUserId = src.UpdatedByUserId,
            IsDeleted = src.IsDeleted
        };

        // =======================
        // AUDIT MESSAGE
        // =======================
        protected override string GetAuditMessage(string operation, Invoice entity)
            => $"{EntityName} [{entity.InvoiceId}] {operation} performed.";
    }
}
