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

    public sealed class DiagnosticRequestItemService : BaseCrudService<DiagnosticRequestItem>
    {
        // =======================
        // PERMISSIONS
        // =======================
        protected override string CreatePermissionCode => "DIAGNOSTIC_REQUEST_ITEM_CREATE";
        protected override string UpdatePermissionCode => "DIAGNOSTIC_REQUEST_ITEM_UPDATE";
        protected override string DeletePermissionCode => "DIAGNOSTIC_REQUEST_ITEM_DELETE";
        protected override string ViewPermissionCode => "DIAGNOSTIC_REQUEST_ITEM_VIEW";

        protected override string EntityName => "DiagnosticRequestItem";

        // =======================
        // DAL WIRING
        // =======================
        protected override int DalCreate(DiagnosticRequestItem entity)
            => DiagnosticRequestItemData.Insert(entity);

        protected override bool DalUpdate(DiagnosticRequestItem entity)
            => DiagnosticRequestItemData.Update(entity);

        protected override bool DalDelete(int id)
            => DiagnosticRequestItemData.GetById(id) != null
               && DiagnosticRequestItemData.Delete(id);

        protected override DiagnosticRequestItem? DalGetById(int id)
            => DiagnosticRequestItemData.GetById(id);

        protected override IEnumerable<DiagnosticRequestItem> DalGetAll()
            => DiagnosticRequestItemData.GetAll() ?? Enumerable.Empty<DiagnosticRequestItem>();

        protected override int GetEntityId(DiagnosticRequestItem entity)
            => entity.DiagnosticRequestItemId;

        // =======================
        // VALIDATION
        // =======================
        protected override ValidationResult.ValidationResult IsValidateData(DiagnosticRequestItem item)
        {
            var v = ValidationResult.ValidationResult.Success();

            if (item is null)
            {
                v.Add("DiagnosticRequestItem is required.");
                return v;
            }

            if (item.DiagnosticRequestId <= 0) v.Add("DiagnosticRequestId is required.");
            if (item.DiagnosticTestId <= 0) v.Add("DiagnosticTestId is required.");

            // Prevent duplicate test in same request
            if (item.DiagnosticRequestId > 0 && item.DiagnosticTestId > 0)
            {
                bool exists = DiagnosticRequestItemData.ExistsTestInRequest(
                    item.DiagnosticRequestId,
                    item.DiagnosticTestId,
                    ignoreId: item.DiagnosticRequestItemId > 0 ? item.DiagnosticRequestItemId : null);

                if (exists)
                    v.Add("This diagnostic test already exists in the same request.");
            }



            return v;
        }

        // =======================
        // READ OPERATIONS (with Permission + Audit)
        // =======================
        public Result<DiagnosticRequestItem> GetByIdSafe(int diagnosticRequestItemId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<DiagnosticRequestItem>.Fail("Permission denied.");

            if (diagnosticRequestItemId <= 0)
                return Result<DiagnosticRequestItem>.Fail("Invalid DiagnosticRequestItemId.");

            var item = DiagnosticRequestItemData.GetById(diagnosticRequestItemId);
            if (item is null)
                return Result<DiagnosticRequestItem>.Fail("Diagnostic request item not found.");

            AuditWriter.Write(
                action: GetAuditMessage("VIEW", item),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: item.DiagnosticRequestItemId.ToString(),
                success: true,
                newEntity: item
            );

            return Result<DiagnosticRequestItem>.Ok(item);
        }

        public Result<IEnumerable<DiagnosticRequestItem>> GetByRequestId(int diagnosticRequestId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<DiagnosticRequestItem>>.Fail("Permission denied.");

            if (diagnosticRequestId <= 0)
                return Result<IEnumerable<DiagnosticRequestItem>>.Fail("Invalid DiagnosticRequestId.");

            var list = DiagnosticRequestItemData.GetByRequestId(diagnosticRequestId) ?? Enumerable.Empty<DiagnosticRequestItem>();

            AuditLogData.Log("View Diagnostic Request Items By Request", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<DiagnosticRequestItem>>.Ok(list);
        }

        public Result<IEnumerable<DiagnosticRequestItem>> GetAllSafe()
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<DiagnosticRequestItem>>.Fail("Permission denied.");

            var list = DiagnosticRequestItemData.GetAll() ?? Enumerable.Empty<DiagnosticRequestItem>();

            AuditLogData.Log("View Diagnostic Request Items", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<DiagnosticRequestItem>>.Ok(list);
        }

        // =======================
        // CREATE / UPDATE (Smart wrappers with Audit)
        // =======================
        public Result<int> CreateRequestItem(DiagnosticRequestItem item)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result<int>.Fail("Permission denied.");

            var v = IsValidateData(item);
            if (!v.IsValid)
                return Result<int>.Fail(v.Errors);

            int newId = DiagnosticRequestItemData.Insert(item);

            bool ok = newId > 0;
            if (ok)
            {
                item.DiagnosticRequestItemId = newId;

                AuditWriter.Write(
                    action: GetAuditMessage("CREATE", item),
                    performedBy: SecurityContext.Current.UserId,
                    entityType: EntityName,
                    entityId: newId.ToString(),
                    success: true,
                    newEntity: item
                );

                AuditLogData.Log("Create Diagnostic Request Item", true, SecurityContext.Current.UserId, EntityName);

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

            return Result<int>.Fail("Failed to create diagnostic request item.");
        }

        public Result UpdateRequestItem(DiagnosticRequestItem item)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (item.DiagnosticRequestItemId <= 0)
                return Result.Fail("Invalid DiagnosticRequestItemId.");

            var old = DiagnosticRequestItemData.GetById(item.DiagnosticRequestItemId);
            if (old is null)
                return Result.Fail("Diagnostic request item not found.");

            var v = IsValidateData(item);
            if (!v.IsValid)
                return Result.Fail(v.Errors);

            bool ok = DiagnosticRequestItemData.Update(item);

            AuditWriter.Write<DiagnosticRequestItem>(
                action: GetAuditMessage("UPDATE", item),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: item.DiagnosticRequestItemId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: item,
                failureReason: ok ? null : "Update returned false"
            );

            if (ok) AuditLogData.Log("Update Diagnostic Request Item", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Failed to update diagnostic request item.");
        }

        // =======================
        // DELETE
        // =======================
        public Result DeleteRequestItem(int diagnosticRequestItemId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            if (diagnosticRequestItemId <= 0)
                return Result.Fail("Invalid DiagnosticRequestItemId.");

            var old = DiagnosticRequestItemData.GetById(diagnosticRequestItemId);
            if (old is null)
                return Result.Fail("Diagnostic request item not found.");

            bool ok = DiagnosticRequestItemData.Delete(diagnosticRequestItemId);

            AuditWriter.Write<DiagnosticRequestItem>(
                action: GetAuditMessage("DELETE", old),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: diagnosticRequestItemId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: default,
                failureReason: ok ? null : "Delete returned false"
            );

            if (ok) AuditLogData.Log("Delete Diagnostic Request Item", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Delete failed.");
        }

        // =======================
        // BULK DELETE BY REQUEST
        // =======================
        public Result DeleteByRequestId(int diagnosticRequestId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            if (diagnosticRequestId <= 0)
                return Result.Fail("Invalid DiagnosticRequestId.");

            // snapshot for audit (optional: count items)
            var oldItems = DiagnosticRequestItemData.GetByRequestId(diagnosticRequestId)?.ToList()
                           ?? new List<DiagnosticRequestItem>();

            bool ok = DiagnosticRequestItemData.DeleteByRequestId(diagnosticRequestId);

            AuditWriter.Write(
                action: $"{EntityName} [Request:{diagnosticRequestId}] BULK_DELETE performed.",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: diagnosticRequestId.ToString(),
                success: ok,
                newEntity: oldItems,
                failureReason: ok ? null : "DeleteByRequestId returned false"
            );

            if (ok) AuditLogData.Log("Bulk Delete Diagnostic Request Items", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Bulk delete failed.");
        }

        // =======================
        // AUDIT MESSAGE
        // =======================
        protected override string GetAuditMessage(string operation, DiagnosticRequestItem entity)
            => $"{EntityName} [{entity.DiagnosticRequestItemId}] {operation} performed.";
    }
}
