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

    public sealed class MedicamentService : BaseCrudService<Medicament>
    {
        // =======================
        // PERMISSIONS
        // =======================
        protected override string CreatePermissionCode => "MEDICAMENT_CREATE";
        protected override string UpdatePermissionCode => "MEDICAMENT_UPDATE";
        protected override string DeletePermissionCode => "MEDICAMENT_DELETE"; // mapped to Deactivate
        protected override string ViewPermissionCode => "MEDICAMENT_VIEW";

        protected override string EntityName => "Medicament";

        // =======================
        // DAL WIRING
        // =======================
        protected override int DalCreate(Medicament entity)
            => MedicamentData.Insert(entity);

        protected override bool DalUpdate(Medicament entity)
            => MedicamentData.Update(entity);

        // BaseCrud "Delete" mapped to soft deactivate (IsActive = 0)
        protected override bool DalDelete(int id)
            => MedicamentData.GetById(id) != null
               && MedicamentData.Deactivate(id);

        protected override Medicament? DalGetById(int id)
            => MedicamentData.GetById(id);

        // BaseCrudService doesn't support params, so active only by default
        protected override IEnumerable<Medicament> DalGetAll()
            => MedicamentData.GetAll(onlyActive: true) ?? Enumerable.Empty<Medicament>();

        protected override int GetEntityId(Medicament entity)
            => entity.MedicamentId;

        // =======================
        // VALIDATION
        // =======================
        protected override ValidationResult.ValidationResult IsValidateData(Medicament m)
        {
            var v = ValidationResult.ValidationResult.Success();

            if (m is null)
            {
                v.Add("Medicament is required.");
                return v;
            }

            if (string.IsNullOrWhiteSpace(m.Name))
                v.Add("Name is required.");

            // Unique by name (recommended)
            if (!string.IsNullOrWhiteSpace(m.Name))
            {
                bool exists = MedicamentData.ExistsByName(
                    m.Name.Trim(),
                    ignoreId: m.MedicamentId > 0 ? m.MedicamentId : null);

                if (exists)
                    v.Add("Medicament name already exists.");
            }

            return v;
        }

        // =======================
        // READ OPERATIONS (with Permission + Audit)
        // =======================
        public Result<Medicament> GetByIdSafe(int medicamentId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<Medicament>.Fail("Permission denied.");

            if (medicamentId <= 0)
                return Result<Medicament>.Fail("Invalid MedicamentId.");

            var m = MedicamentData.GetById(medicamentId);
            if (m is null)
                return Result<Medicament>.Fail("Medicament not found.");

            AuditWriter.Write(
                action: GetAuditMessage("VIEW", m),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: m.MedicamentId.ToString(),
                success: true,
                newEntity: m
            );

            return Result<Medicament>.Ok(m);
        }

        public Result<IEnumerable<Medicament>> GetAllSafe(bool onlyActive = true)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<Medicament>>.Fail("Permission denied.");

            var list = MedicamentData.GetAll(onlyActive) ?? Enumerable.Empty<Medicament>();

            AuditLogData.Log("View Medicaments", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<Medicament>>.Ok(list);
        }

        // =======================
        // CREATE / UPDATE (Smart wrappers with Audit)
        // =======================
        public Result<int> CreateMedicament(Medicament m)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result<int>.Fail("Permission denied.");

            // default new medicaments to active (recommended)
            // m.IsActive = true;

            var v = IsValidateData(m);
            if (!v.IsValid)
                return Result<int>.Fail(v.Errors);

            int newId = MedicamentData.Insert(m);

            bool ok = newId > 0;
            if (ok)
            {
                m.MedicamentId = newId;

                AuditWriter.Write(
                    action: GetAuditMessage("CREATE", m),
                    performedBy: SecurityContext.Current.UserId,
                    entityType: EntityName,
                    entityId: newId.ToString(),
                    success: true,
                    newEntity: m
                );

                AuditLogData.Log("Create Medicament", true, SecurityContext.Current.UserId, EntityName);

                return Result<int>.Ok(newId);
            }

            AuditWriter.Write(
                action: $"{EntityName} CREATE failed",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: "0",
                success: false,
                newEntity: m,
                failureReason: "Insert returned 0"
            );

            return Result<int>.Fail("Failed to create medicament.");
        }

        public Result UpdateMedicament(Medicament m)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (m.MedicamentId <= 0)
                return Result.Fail("Invalid MedicamentId.");

            var old = MedicamentData.GetById(m.MedicamentId);
            if (old is null)
                return Result.Fail("Medicament not found.");

            var v = IsValidateData(m);
            if (!v.IsValid)
                return Result.Fail(v.Errors);

            bool ok = MedicamentData.Update(m);

            AuditWriter.Write<Medicament>(
                action: GetAuditMessage("UPDATE", m),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: m.MedicamentId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: m,
                failureReason: ok ? null : "Update returned false"
            );

            if (ok) AuditLogData.Log("Update Medicament", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Failed to update medicament.");
        }

        // =======================
        // DEACTIVATE (Soft delete)
        // =======================
        public Result Deactivate(int medicamentId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            if (medicamentId <= 0)
                return Result.Fail("Invalid MedicamentId.");

            var old = MedicamentData.GetById(medicamentId);
            if (old is null)
                return Result.Fail("Medicament not found.");

            bool ok = MedicamentData.Deactivate(medicamentId);

            var after = new Medicament
            {
                MedicamentId = old.MedicamentId,
                Name = old.Name,
                GenericName = old.GenericName,
                Form = old.Form,
                Strength = old.Strength,
                Manufacturer = old.Manufacturer,
                IsActive = false,
                CreatedAt = old.CreatedAt
            };

            AuditWriter.Write<Medicament>(
                action: $"{EntityName} [{medicamentId}] DEACTIVATE performed.",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: medicamentId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: after,
                failureReason: ok ? null : "Deactivate returned false"
            );

            if (ok) AuditLogData.Log("Deactivate Medicament", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Deactivate failed.");
        }

        // =======================
        // AUDIT MESSAGE
        // =======================
        protected override string GetAuditMessage(string operation, Medicament entity)
            => $"{EntityName} [{entity.MedicamentId}] {operation} performed.";
    }
}
