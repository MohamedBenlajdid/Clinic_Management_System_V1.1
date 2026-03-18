using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.Service
{
    using Clinic_Management_BLL.AuditWritter;
    using Clinic_Management_BLL.CrudInterface;
    using Clinic_Management_BLL.LoginProcess;
    using Clinic_Management_BLL.ResultWraper;
    using Clinic_Management_Entities.Entities;
    using Clinic_Management_DAL;
    using System;
    using System.Collections.Generic;
    using Clinic_Management_DAL.Data;

    // =========================================================
    // BLL: DoctorDayOverrideService (BaseCrudService Strategy)
    // =========================================================
    public sealed class DoctorDayOverrideService : BaseCrudService<DoctorDayOverride>
    {
        // =======================
        // PERMISSIONS
        // =======================
        protected override string CreatePermissionCode => "DOCTOR_OVERRIDE_CREATE";
        protected override string UpdatePermissionCode => "DOCTOR_OVERRIDE_UPDATE";
        protected override string DeletePermissionCode => "DOCTOR_OVERRIDE_DELETE";
        protected override string ViewPermissionCode => "DOCTOR_OVERRIDE_VIEW";

        protected override string EntityName => "DoctorDayOverride";

        // =======================
        // DAL WIRING
        // =======================
        protected override int DalCreate(DoctorDayOverride entity)
            => DoctorDayOverrideData.Insert(entity);

        protected override bool DalUpdate(DoctorDayOverride entity)
            => DoctorDayOverrideData.Update(entity);

        protected override bool DalDelete(int id)
            => DoctorDayOverrideData.GetById(id) != null && DoctorDayOverrideData.Delete(id);

        protected override DoctorDayOverride? DalGetById(int id)
            => DoctorDayOverrideData.GetById(id);

        protected override IEnumerable<DoctorDayOverride> DalGetAll()
            => DoctorDayOverrideData.GetAll();

        protected override int GetEntityId(DoctorDayOverride entity)
            => entity.OverrideId;

        // =======================
        // VALIDATION
        // =======================
        protected override ValidationResult.ValidationResult IsValidateData(DoctorDayOverride entity)
        {
            var v = ValidationResult.ValidationResult.Success();

            if (entity is null)
            {
                v.Add("Override is required.");
                return v;
            }

            if (entity.DoctorId <= 0)
                v.Add("DoctorId must be valid.");

            // business rule: date must be date-only (ignore time)
            if (entity.Date == default)
                v.Add("Date is required.");

            // Optional rule: allow either IsOverride or IsDayOff, but not both off? (you decide)
            // Common meaning:
            // - IsDayOff = true => doctor not working at all
            // - IsOverride = true => override sessions exist OR schedule differs from weekly template
            // We allow both, but you can restrict if you want:

             if (!entity.IsOverride) v.Add("Cannot be Not Override.");

            // Unique check (one row per doctor per date)
            if (v.IsValid)
            {
                bool exists = DoctorDayOverrideData.IsOverrideExist(
                    doctorId: entity.DoctorId,
                    date: entity.Date,
                    ignoreOverrideId: entity.OverrideId <= 0 ? null : entity.OverrideId
                );

                if (exists)
                    v.Add("An override already exists for this doctor on this date.");
            }

            // Notes length safety (optional)
            if (!string.IsNullOrWhiteSpace(entity.Notes) && entity.Notes.Length > 500)
                v.Add("Notes is too long (max 500 chars).");

            return v;
        }

        protected override string GetAuditMessage(string operation, DoctorDayOverride entity)
            => $"{EntityName} [{entity.OverrideId}] {operation} performed.";

        // =========================================================
        // EXTRA METHODS (same style as DoctorScheduleService)
        // =========================================================

        public Result<DoctorDayOverride> GetByDoctorAndDate(int doctorId, DateTime date)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<DoctorDayOverride>.Fail("Permission denied.");

            if (doctorId <= 0)
                return Result<DoctorDayOverride>.Fail("Invalid DoctorId.");

            if (date == default)
                return Result<DoctorDayOverride>.Fail("Invalid Date.");

            var o = DoctorDayOverrideData.GetByDoctorAndDate(doctorId, date.Date);

            if (o is null)
                return Result<DoctorDayOverride>.Fail("Override not found.");

            AuditWriter.Write(
                action: GetAuditMessage("VIEW", o),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: o.OverrideId.ToString(),
                success: true,
                newEntity: o
            );

            return Result<DoctorDayOverride>.Ok(o);
        }

        public Result<IEnumerable<DoctorDayOverride>> GetByDoctorId(int doctorId, DateTime? from = null, DateTime? to = null)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<DoctorDayOverride>>.Fail("Permission denied.");

            if (doctorId <= 0)
                return Result<IEnumerable<DoctorDayOverride>>.Fail("Invalid DoctorId.");

            if (from.HasValue && to.HasValue && to.Value.Date < from.Value.Date)
                return Result<IEnumerable<DoctorDayOverride>>.Fail("Invalid date range (to < from).");

            var list = DoctorDayOverrideData.GetByDoctorId(doctorId, from?.Date, to?.Date);

            AuditWriter.Write(
                action: $"{EntityName} VIEW BY DOCTOR [{doctorId}]",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: doctorId.ToString(),
                success: true,
                newEntity: list
            );

            return Result<IEnumerable<DoctorDayOverride>>.Ok(list);
        }

        public Result<bool> IsDoctorDayOff(int doctorId, DateTime date)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<bool>.Fail("Permission denied.");

            if (doctorId <= 0)
                return Result<bool>.Fail("Invalid DoctorId.");

            if (date == default)
                return Result<bool>.Fail("Invalid Date.");

            bool off = DoctorDayOverrideData.IsDoctorDayOff(doctorId, date.Date);

            return Result<bool>.Ok(off);
        }

        public bool Exists(int overrideId)
            => overrideId > 0 && DoctorDayOverrideData.GetById(overrideId) != null;

        // =========================================================
        // Convenience CRUD wrappers (optional)
        // (If BaseCrudService already gives Create/Update/Delete, keep only if you want)
        // =========================================================

        public Result<int> CreateOverride(DoctorDayOverride entity)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result<int>.Fail("Permission denied.");

            var v = IsValidateData(entity);
            if (!v.IsValid)
                return Result<int>.Fail(v.Errors);

            int newId = DoctorDayOverrideData.Insert(entity);
            bool ok = newId > 0;

            AuditWriter.Write(
                action: $"{EntityName} CREATE [{newId}]",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: newId.ToString(),
                success: ok,
                newEntity: entity,
                failureReason: ok ? null : "Insert returned 0."
            );

            return ok ? Result<int>.Ok(newId) : Result<int>.Fail("Failed to create override.");
        }

        public Result UpdateOverride(DoctorDayOverride entity)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (entity.OverrideId <= 0)
                return Result.Fail("Invalid OverrideId.");

            var old = DoctorDayOverrideData.GetById(entity.OverrideId);
            if (old is null)
                return Result.Fail("Override not found.");

            var v = IsValidateData(entity);
            if (!v.IsValid)
                return Result.Fail(v.Errors);

            bool ok = DoctorDayOverrideData.Update(entity);

            AuditWriter.Write(
                action: GetAuditMessage("UPDATE", entity),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: entity.OverrideId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: entity,
                failureReason: ok ? null : "Update failed."
            );

            return ok ? Result.Ok() : Result.Fail("Failed to update override.");
        }

        public Result DeleteOverride(int overrideId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            if (overrideId <= 0)
                return Result.Fail("Invalid OverrideId.");

            var old = DoctorDayOverrideData.GetById(overrideId);
            if (old is null)
                return Result.Fail("Override not found.");

            bool ok = DoctorDayOverrideData.Delete(overrideId);

            AuditWriter.Write(
                action: $"{EntityName} DELETE [{overrideId}]",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: overrideId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: default(DoctorDayOverride),
                failureReason: ok ? null : "Delete failed."
            );

            return ok ? Result.Ok() : Result.Fail("Failed to delete override.");
        }
    }



}
