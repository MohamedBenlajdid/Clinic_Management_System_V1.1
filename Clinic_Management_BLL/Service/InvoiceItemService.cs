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

    public enum enInvoiceItemType : byte
    {
        Consultation = 1,
        Procedure = 2,
        Medication = 3,
        Diagnostic = 4,
        Other = 5
    }

    public sealed class InvoiceItemService : BaseCrudService<InvoiceItem>
    {
        // =======================
        // PERMISSIONS
        // =======================
        protected override string CreatePermissionCode => "INVOICE_ITEM_CREATE";
        protected override string UpdatePermissionCode => "INVOICE_ITEM_UPDATE";
        protected override string DeletePermissionCode => "INVOICE_ITEM_DELETE";
        protected override string ViewPermissionCode => "INVOICE_ITEM_VIEW";

        protected override string EntityName => "InvoiceItem";

        // =======================
        // DAL WIRING
        // =======================
        protected override int DalCreate(InvoiceItem entity)
            => InvoiceItemData.Insert(entity);

        protected override bool DalUpdate(InvoiceItem entity)
            => InvoiceItemData.Update(entity);

        protected override bool DalDelete(int id)
            => InvoiceItemData.GetById(id) != null
               && InvoiceItemData.Delete(id);

        protected override InvoiceItem? DalGetById(int id)
            => InvoiceItemData.GetById(id);

        protected override IEnumerable<InvoiceItem> DalGetAll()
            => InvoiceItemData.GetAll() ?? Enumerable.Empty<InvoiceItem>();

        protected override int GetEntityId(InvoiceItem entity)
            => entity.InvoiceItemId;

        // =======================
        // VALIDATION
        // =======================
        protected override ValidationResult.ValidationResult IsValidateData(InvoiceItem item)
        {
            var v = ValidationResult.ValidationResult.Success();

            if (item is null)
            {
                v.Add("InvoiceItem is required.");
                return v;
            }

            if (item.InvoiceId <= 0) v.Add("InvoiceId is required.");

            if (string.IsNullOrWhiteSpace(item.Description))
                v.Add("Description is required.");

            if (item.Quantity <= 0) v.Add("Quantity must be > 0.");
            if (item.UnitPrice < 0) v.Add("UnitPrice must be >= 0.");
            if (item.Discount < 0) v.Add("Discount must be >= 0.");

            // Recommended: compute/validate Total
            decimal expectedTotal = (item.Quantity * item.UnitPrice) - item.Discount;
            if (expectedTotal < 0) v.Add("Total cannot be negative (check Discount).");
            if (item.Total != expectedTotal)
                v.Add("Total must equal (Quantity * UnitPrice) - Discount.");

            return v;
        }

        // =======================
        // READ OPERATIONS (with Permission + Audit)
        // =======================
        public Result<InvoiceItem> GetByIdSafe(int invoiceItemId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<InvoiceItem>.Fail("Permission denied.");

            if (invoiceItemId <= 0)
                return Result<InvoiceItem>.Fail("Invalid InvoiceItemId.");

            var item = InvoiceItemData.GetById(invoiceItemId);
            if (item is null)
                return Result<InvoiceItem>.Fail("Invoice item not found.");

            AuditWriter.Write(
                action: GetAuditMessage("VIEW", item),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: item.InvoiceItemId.ToString(),
                success: true,
                newEntity: item
            );

            return Result<InvoiceItem>.Ok(item);
        }

        public Result<IEnumerable<InvoiceItem>> GetByInvoiceId(int invoiceId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<InvoiceItem>>.Fail("Permission denied.");

            if (invoiceId <= 0)
                return Result<IEnumerable<InvoiceItem>>.Fail("Invalid InvoiceId.");

            var list = InvoiceItemData.GetByInvoiceId(invoiceId) ?? Enumerable.Empty<InvoiceItem>();

            AuditLogData.Log("View Invoice Items By Invoice", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<InvoiceItem>>.Ok(list);
        }

        public Result<IEnumerable<InvoiceItem>> GetAllSafe()
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<InvoiceItem>>.Fail("Permission denied.");

            var list = InvoiceItemData.GetAll() ?? Enumerable.Empty<InvoiceItem>();

            AuditLogData.Log("View Invoice Items", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<InvoiceItem>>.Ok(list);
        }

        // =======================
        // CREATE / UPDATE (Smart wrappers with Audit)
        // =======================
        public Result<int> CreateInvoiceItem(InvoiceItem item)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result<int>.Fail("Permission denied.");

            Normalize(item);

            var v = IsValidateData(item);
            if (!v.IsValid)
                return Result<int>.Fail(v.Errors);

            int newId = InvoiceItemData.Insert(item);

            bool ok = newId > 0;
            if (ok)
            {
                item.InvoiceItemId = newId;

                AuditWriter.Write(
                    action: GetAuditMessage("CREATE", item),
                    performedBy: SecurityContext.Current.UserId,
                    entityType: EntityName,
                    entityId: newId.ToString(),
                    success: true,
                    newEntity: item
                );

                AuditLogData.Log("Create Invoice Item", true, SecurityContext.Current.UserId, EntityName);

                return Result<int>.Ok(newId);
            }

            AuditWriter.Write(
                action: $"{EntityName} CREATE failed",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: "0",
                success: false,
                newEntity: item,
                failureReason: "Insert returned 0"
            );

            return Result<int>.Fail("Failed to create invoice item.");
        }

        public Result UpdateInvoiceItem(InvoiceItem item)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (item.InvoiceItemId <= 0)
                return Result.Fail("Invalid InvoiceItemId.");

            var old = InvoiceItemData.GetById(item.InvoiceItemId);
            if (old is null)
                return Result.Fail("Invoice item not found.");

            Normalize(item);

            var v = IsValidateData(item);
            if (!v.IsValid)
                return Result.Fail(v.Errors);

            bool ok = InvoiceItemData.Update(item);

            AuditWriter.Write<InvoiceItem>(
                action: GetAuditMessage("UPDATE", item),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: item.InvoiceItemId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: item,
                failureReason: ok ? null : "Update returned false"
            );

            if (ok) AuditLogData.Log("Update Invoice Item", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Failed to update invoice item.");
        }

        // =======================
        // DELETE
        // =======================
        public Result DeleteInvoiceItem(int invoiceItemId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            if (invoiceItemId <= 0)
                return Result.Fail("Invalid InvoiceItemId.");

            var old = InvoiceItemData.GetById(invoiceItemId);
            if (old is null)
                return Result.Fail("Invoice item not found.");

            bool ok = InvoiceItemData.Delete(invoiceItemId);

            AuditWriter.Write<InvoiceItem>(
                action: GetAuditMessage("DELETE", old),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: invoiceItemId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: default,
                failureReason: ok ? null : "Delete returned false"
            );

            if (ok) AuditLogData.Log("Delete Invoice Item", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Delete failed.");
        }

        // =======================
        // BULK DELETE BY INVOICE
        // =======================
        public Result DeleteByInvoiceId(int invoiceId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            if (invoiceId <= 0)
                return Result.Fail("Invalid InvoiceId.");

            var oldItems = InvoiceItemData.GetByInvoiceId(invoiceId)?.ToList() ?? new List<InvoiceItem>();

            bool ok = InvoiceItemData.DeleteByInvoiceId(invoiceId);

            AuditWriter.Write(
                action: $"{EntityName} [Invoice:{invoiceId}] BULK_DELETE performed.",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: invoiceId.ToString(),
                success: ok,
                newEntity: oldItems,
                failureReason: ok ? null : "DeleteByInvoiceId returned false"
            );

            if (ok) AuditLogData.Log("Bulk Delete Invoice Items", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Bulk delete failed.");
        }

        // =======================
        // HELPERS
        // =======================
        private static void Normalize(InvoiceItem item)
        {
            // keep total consistent
            item.Total = (item.Quantity * item.UnitPrice) - item.Discount;
            if (item.Total < 0) item.Total = 0;
        }

        // =======================
        // AUDIT MESSAGE
        // =======================
        protected override string GetAuditMessage(string operation, InvoiceItem entity)
            => $"{EntityName} [{entity.InvoiceItemId}] {operation} performed.";
    }
}
