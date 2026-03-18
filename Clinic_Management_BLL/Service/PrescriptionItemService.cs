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

    public sealed class PrescriptionItemService : BaseCrudService<PrescriptionItem>
    {
        // =======================
        // PERMISSIONS
        // =======================
        protected override string CreatePermissionCode => "PRESCRIPTION_ITEM_CREATE";
        protected override string UpdatePermissionCode => "PRESCRIPTION_ITEM_UPDATE";
        protected override string DeletePermissionCode => "PRESCRIPTION_ITEM_DELETE";
        protected override string ViewPermissionCode => "PRESCRIPTION_ITEM_VIEW";

        protected override string EntityName => "PrescriptionItem";

        // =======================
        // DAL WIRING
        // =======================
        protected override int DalCreate(PrescriptionItem entity)
            => PrescriptionItemData.Insert(entity);

        protected override bool DalUpdate(PrescriptionItem entity)
            => PrescriptionItemData.Update(entity);

        protected override bool DalDelete(int id)
            => PrescriptionItemData.GetById(id) != null
               && PrescriptionItemData.Delete(id);

        protected override PrescriptionItem? DalGetById(int id)
            => PrescriptionItemData.GetById(id);

        protected override IEnumerable<PrescriptionItem> DalGetAll()
            => PrescriptionItemData.GetAll() ?? Enumerable.Empty<PrescriptionItem>();

        protected override int GetEntityId(PrescriptionItem entity)
            => entity.PrescriptionItemId;

        // =======================
        // VALIDATION
        // =======================
        protected override ValidationResult.ValidationResult IsValidateData(PrescriptionItem item)
        {
            var v = ValidationResult.ValidationResult.Success();

            if (item is null)
            {
                
                v.Add("PrescriptionItem is required.");
                return v;
            }

            if (item.PrescriptionId <= 0) v.Add("PrescriptionId is required.");
            if (item.MedicamentId <= 0) v.Add("MedicamentId is required.");

            if (item.DurationDays.HasValue && (item.DurationDays.Value < 1 || item.DurationDays.Value > 365))
                v.Add("DurationDays must be between 1 and 365.");

            if (item.Quantity.HasValue && item.Quantity.Value < 0)
                v.Add("Quantity must be >= 0.");

            // Optional: ensure referenced medicament exists
            // (Only if MedicamentData is available in this layer)
            if (item.MedicamentId > 0)
            {
                var med = MedicamentData.GetById(item.MedicamentId);
                if (med is null)
                    v.Add("Selected medicament not found.");
                else if (!med.IsActive)
                    v.Add("Selected medicament is inactive.");
            }

            // Optional: ensure prescription exists
            if (item.PrescriptionId > 0)
            {
                var p = PrescriptionData.GetById(item.PrescriptionId);
                if (p is null)
                    v.Add("Prescription not found.");
            }

            return v;
        }

        // =======================
        // READ OPERATIONS (with Permission + Audit)
        // =======================
        public Result<PrescriptionItem> GetByIdSafe(int prescriptionItemId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<PrescriptionItem>.Fail("Permission denied.");

            if (prescriptionItemId <= 0)
                return Result<PrescriptionItem>.Fail("Invalid PrescriptionItemId.");

            var item = PrescriptionItemData.GetById(prescriptionItemId);
            if (item is null)
                return Result<PrescriptionItem>.Fail("Prescription item not found.");

            AuditWriter.Write(
                action: GetAuditMessage("VIEW", item),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: item.PrescriptionItemId.ToString(),
                success: true,
                newEntity: item
            );

            return Result<PrescriptionItem>.Ok(item);
        }

        public Result<IEnumerable<PrescriptionItem>> GetAllSafe()
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<PrescriptionItem>>.Fail("Permission denied.");

            var list = PrescriptionItemData.GetAll() ?? Enumerable.Empty<PrescriptionItem>();

            AuditLogData.Log("View Prescription Items", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<PrescriptionItem>>.Ok(list);
        }

        public Result<IEnumerable<PrescriptionItem>> GetByPrescriptionId(int prescriptionId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<PrescriptionItem>>.Fail("Permission denied.");

            if (prescriptionId <= 0)
                return Result<IEnumerable<PrescriptionItem>>.Fail("Invalid PrescriptionId.");

            var list = PrescriptionItemData.GetByPrescriptionId(prescriptionId) ?? Enumerable.Empty<PrescriptionItem>();

            AuditLogData.Log("View Prescription Items By Prescription", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<PrescriptionItem>>.Ok(list);
        }

        // =======================
        // CREATE / UPDATE (Smart wrappers with Audit)
        // =======================
        public Result<int> CreatePrescriptionItem(PrescriptionItem item)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result<int>.Fail("Permission denied.");

            var v = IsValidateData(item);
            if (!v.IsValid)
                return Result<int>.Fail(v.Errors);

            int newId = PrescriptionItemData.Insert(item);

            bool ok = newId > 0;
            if (ok)
            {
                item.PrescriptionItemId = newId;

                AuditWriter.Write(
                    action: GetAuditMessage("CREATE", item),
                    performedBy: SecurityContext.Current.UserId,
                    entityType: EntityName,
                    entityId: newId.ToString(),
                    success: true,
                    newEntity: item
                );

                AuditLogData.Log("Create Prescription Item", true, SecurityContext.Current.UserId, EntityName);

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

            return Result<int>.Fail("Failed to create prescription item.");
        }

        public Result UpdatePrescriptionItem(PrescriptionItem item)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (item.PrescriptionItemId <= 0)
                return Result.Fail("Invalid PrescriptionItemId.");

            var old = PrescriptionItemData.GetById(item.PrescriptionItemId);
            if (old is null)
                return Result.Fail("Prescription item not found.");

            var v = IsValidateData(item);
            if (!v.IsValid)
                return Result.Fail(v.Errors);

            bool ok = PrescriptionItemData.Update(item);

            AuditWriter.Write<PrescriptionItem>(
                action: GetAuditMessage("UPDATE", item),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: item.PrescriptionItemId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: item,
                failureReason: ok ? null : "Update returned false"
            );

            if (ok) AuditLogData.Log("Update Prescription Item", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Failed to update prescription item.");
        }

        // =======================
        // DELETE (Hard)
        // =======================
        public Result DeletePrescriptionItem(int prescriptionItemId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            if (prescriptionItemId <= 0)
                return Result.Fail("Invalid PrescriptionItemId.");

            var old = PrescriptionItemData.GetById(prescriptionItemId);
            if (old is null)
                return Result.Fail("Prescription item not found.");

            bool ok = PrescriptionItemData.Delete(prescriptionItemId);

            AuditWriter.Write<PrescriptionItem>(
                action: GetAuditMessage("DELETE", old),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: prescriptionItemId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: default,
                failureReason: ok ? null : "Delete returned false"
            );

            if (ok) AuditLogData.Log("Delete Prescription Item", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Delete failed.");
        }

        public Result DeleteByPrescriptionId(int prescriptionId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            if (prescriptionId <= 0)
                return Result.Fail("Invalid PrescriptionId.");

            bool ok = PrescriptionItemData.DeleteByPrescriptionId(prescriptionId);

            AuditWriter.Write(
                action: $"{EntityName} [Prescription:{prescriptionId}] BULK DELETE performed.",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: prescriptionId.ToString(),
                success: ok,
                newEntity: new { PrescriptionId = prescriptionId },
                failureReason: ok ? null : "DeleteByPrescriptionId returned false"
            );

            if (ok) AuditLogData.Log("Delete Prescription Items By Prescription", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Bulk delete failed.");
        }

        // =======================
        // AUDIT MESSAGE
        // =======================
        protected override string GetAuditMessage(string operation, PrescriptionItem entity)
            => $"{EntityName} [{entity.PrescriptionItemId}] {operation} performed.";
    }
}
