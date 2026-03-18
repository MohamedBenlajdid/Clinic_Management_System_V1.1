using Clinic_Management_BLL.AuditWritter;
using Clinic_Management_BLL.CrudInterface;
using Clinic_Management_BLL.LoginProcess;
using Clinic_Management_BLL.ResultWraper;
using Clinic_Management_DAL.Data;
using Clinic_Management_Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.Service
{
    // =========================================================
    // BLL: DoctorScheduleService (BaseCrudService Strategy)
    // =========================================================
    public sealed class DoctorScheduleService : BaseCrudService<DoctorSchedule>
    {
        // =======================
        // PERMISSIONS
        // =======================
        protected override string CreatePermissionCode => "DOCTOR_SCHEDULE_CREATE";
        protected override string UpdatePermissionCode => "DOCTOR_SCHEDULE_UPDATE";
        protected override string DeletePermissionCode => "DOCTOR_SCHEDULE_DELETE";
        protected override string ViewPermissionCode => "DOCTOR_SCHEDULE_VIEW";

        protected override string EntityName => "DoctorSchedule";

        // =======================
        // DAL WIRING
        // =======================
        protected override int DalCreate(DoctorSchedule entity)
            => DoctorScheduleData.Insert(entity);

        protected override bool DalUpdate(DoctorSchedule entity)
            => DoctorScheduleData.Update(entity);

        protected override bool DalDelete(int id)
            => DoctorScheduleData.GetById(id) != null && DoctorScheduleData.Delete(id);

        protected override DoctorSchedule? DalGetById(int id)
            => DoctorScheduleData.GetById(id);

        protected override IEnumerable<DoctorSchedule> DalGetAll()
            => DoctorScheduleData.GetAll();

        protected override int GetEntityId(DoctorSchedule entity)
            => entity.ScheduleId;

        // =======================
        // VALIDATION (BUSINESS DECISION LAYER)
        // =======================
        protected override ValidationResult.ValidationResult IsValidateData(DoctorSchedule entity)
        {
            var v = ValidationResult.ValidationResult.Success();

            if (entity is null)
            {
                v.Add("Schedule is required.");
                return v;
            }

            if (entity.DoctorId <= 0)
                v.Add("DoctorId must be valid.");

            if (entity.DayOfWeek > 6)
                v.Add("DayOfWeek must be between 0 and 6.");

            if (entity.DayOfWeek < 0)
                v.Add("DayOfWeek must be between 0 and 6.");

            if (entity.SlotMinutes <= 0)
                v.Add("SlotMinutes must be greater than 0.");

            // Common real-world rule: only allow common slot sizes
            if (entity.SlotMinutes is not (10 or 15 or 20 or 30 or 45 or 60))
                v.Add("SlotMinutes must be one of: 10, 15, 20, 30, 45, 60.");

            if (entity.EndTime <= entity.StartTime)
                v.Add("EndTime must be greater than StartTime.");

            // Overlap check (data logic query) => used as business validation result
            if (v.IsValid)
            {
                bool overlap = DoctorScheduleData.IsOverlapping(
                    doctorId: entity.DoctorId,
                    dayOfWeek: entity.DayOfWeek,
                    startTime: entity.StartTime,
                    endTime: entity.EndTime,
                    ignoreScheduleId: entity.ScheduleId <= 0 ? null : entity.ScheduleId
                );

                if (overlap)
                    v.Add("Schedule overlaps with another active session for this doctor/day.");
            }

            return v;
        }

        protected override string GetAuditMessage(string operation, DoctorSchedule entity)
            => $"{EntityName} [{entity.ScheduleId}] {operation} performed.";

        // =========================================================
        // EXTRA METHODS (like your DoctorService style)
        // =========================================================

        public Result<IEnumerable<DoctorSchedule>> GetByDoctorId(int doctorId, bool onlyActive = true)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<DoctorSchedule>>.Fail("Permission denied.");

            if (doctorId <= 0)
                return Result<IEnumerable<DoctorSchedule>>.Fail("Invalid DoctorId.");

            var list = DoctorScheduleData.GetByDoctorId(doctorId, onlyActive);

            AuditWriter.Write(
                action: $"{EntityName} VIEW BY DOCTOR [{doctorId}]",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: doctorId.ToString(),
                success: true,
                newEntity: list
            );

            return Result<IEnumerable<DoctorSchedule>>.Ok(list);
        }

        public Result<IEnumerable<DoctorSchedule>> GetByDoctorAndDay(int doctorId, byte dayOfWeek, bool onlyActive = true)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<DoctorSchedule>>.Fail("Permission denied.");

            if (doctorId <= 0)
                return Result<IEnumerable<DoctorSchedule>>.Fail("Invalid DoctorId.");

            if (dayOfWeek > 6)
                return Result<IEnumerable<DoctorSchedule>>.Fail("Invalid DayOfWeek.");

            var list = DoctorScheduleData.GetByDoctorAndDay(doctorId, dayOfWeek, onlyActive);

            AuditWriter.Write(
                action: $"{EntityName} VIEW BY DOCTOR/DAY [{doctorId}/{dayOfWeek}]",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: doctorId.ToString(),
                success: true,
                newEntity: list
            );

            return Result<IEnumerable<DoctorSchedule>>.Ok(list);
        }

        public Result<DoctorSchedule> GetByDoctorDayStart(int doctorId, byte dayOfWeek, TimeSpan startTime)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<DoctorSchedule>.Fail("Permission denied.");

            if (doctorId <= 0)
                return Result<DoctorSchedule>.Fail("Invalid DoctorId.");

            if (dayOfWeek > 6)
                return Result<DoctorSchedule>.Fail("Invalid DayOfWeek.");

            var s = DoctorScheduleData.GetByDoctorDayStart(doctorId, dayOfWeek, startTime);

            if (s is null)
                return Result<DoctorSchedule>.Fail("Schedule not found.");

            AuditWriter.Write(
                action: GetAuditMessage("VIEW", s),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: s.ScheduleId.ToString(),
                success: true,
                newEntity: s
            );

            return Result<DoctorSchedule>.Ok(s);
        }

        public Result SetActive(int scheduleId, bool isActive)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (scheduleId <= 0)
                return Result.Fail("Invalid ScheduleId.");

            var old = DoctorScheduleData.GetById(scheduleId);
            if (old is null)
                return Result.Fail("Schedule not found.");

            bool ok = DoctorScheduleData.SetActive(scheduleId, isActive);

            AuditWriter.Write(
                action: $"{EntityName} SET_ACTIVE [{scheduleId}] -> {(isActive ? "ACTIVE" : "INACTIVE")}",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: scheduleId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: DoctorScheduleData.GetById(scheduleId),
                failureReason: ok ? null : "Failed to change schedule IsActive."
            );

            return ok ? Result.Ok() : Result.Fail("Failed to update IsActive.");
        }

        public bool Exists(int scheduleId)
            => scheduleId > 0 && DoctorScheduleData.GetById(scheduleId) != null;

        // =========================================================
        // OPTIONAL: Convenience create with validation result
        // (if BaseCrudService already has Create() you may not need this)
        // =========================================================
        public Result<int> CreateSchedule(DoctorSchedule entity)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result<int>.Fail("Permission denied.");

            var v = IsValidateData(entity);
            if (!v.IsValid)
                return Result<int>.Fail(v.Errors);

            int newId = DoctorScheduleData.Insert(entity);

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

            return ok ? Result<int>.Ok(newId) : Result<int>.Fail("Failed to create schedule.");
        }

        public Result UpdateSchedule(DoctorSchedule entity)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (entity.ScheduleId <= 0)
                return Result.Fail("Invalid ScheduleId.");

            var old = DoctorScheduleData.GetById(entity.ScheduleId);
            if (old is null)
                return Result.Fail("Schedule not found.");

            var v = IsValidateData(entity);
            if (!v.IsValid)
                return Result.Fail(v.Errors);

            bool ok = DoctorScheduleData.Update(entity);

            AuditWriter.Write(
                action: GetAuditMessage("UPDATE", entity),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: entity.ScheduleId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: entity,
                failureReason: ok ? null : "Update failed."
            );

            return ok ? Result.Ok() : Result.Fail("Failed to update schedule.");
        }

        public Result DeleteSchedule(int scheduleId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            if (scheduleId <= 0)
                return Result.Fail("Invalid ScheduleId.");

            var old = DoctorScheduleData.GetById(scheduleId);
            if (old is null)
                return Result.Fail("Schedule not found.");

            bool ok = DoctorScheduleData.Delete(scheduleId);

            AuditWriter.Write(
                action: $"{EntityName} DELETE [{scheduleId}]",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: scheduleId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: default(DoctorSchedule),
                failureReason: ok ? null : "Delete failed."
            );

            return ok ? Result.Ok() : Result.Fail("Failed to delete schedule.");
        }
    }


}
