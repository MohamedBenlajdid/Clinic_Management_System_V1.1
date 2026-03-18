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

    public sealed class DoctorOverrideSessionService : BaseCrudService<DoctorDayOverrideSession>
    {
        // =======================
        // PERMISSIONS
        // =======================
        protected override string CreatePermissionCode => "DOCTOR_OVERRIDE_SESSION_CREATE";
        protected override string UpdatePermissionCode => "DOCTOR_OVERRIDE_SESSION_UPDATE";
        protected override string DeletePermissionCode => "DOCTOR_OVERRIDE_SESSION_DELETE";
        protected override string ViewPermissionCode => "DOCTOR_OVERRIDE_SESSION_VIEW";

        protected override string EntityName => "DoctorDayOverrideSession";

        // =======================
        // DAL WIRING
        // =======================
        protected override int DalCreate(DoctorDayOverrideSession entity)
            => DoctorDayOverrideSessionData.Insert(entity);

        protected override bool DalUpdate(DoctorDayOverrideSession entity)
            => DoctorDayOverrideSessionData.Update(entity);

        protected override bool DalDelete(int id)
            => DoctorDayOverrideSessionData.GetById(id) != null
               && DoctorDayOverrideSessionData.Delete(id);

        protected override DoctorDayOverrideSession? DalGetById(int id)
            => DoctorDayOverrideSessionData.GetById(id);

        protected override IEnumerable<DoctorDayOverrideSession> DalGetAll()
        {
            // DAL does not implement GetAll() for sessions (usually huge table).
            // Keep BaseCrudService happy but discourage its use.
            return Array.Empty<DoctorDayOverrideSession>();
        }

        protected override int GetEntityId(DoctorDayOverrideSession entity)
            => entity.SessionId;

        // =======================
        // VALIDATION
        // =======================
        protected override ValidationResult.ValidationResult IsValidateData(DoctorDayOverrideSession entity)
        {
            var v = ValidationResult.ValidationResult.Success();

            if (entity is null)
            {
                v.Add("Override session is required.");
                return v;
            }

            if (entity.OverrideId <= 0)
                v.Add("OverrideId must be valid.");

            if (DoctorDayOverrideData.GetById(entity.OverrideId).IsDayOff)
                v.Add("Cannot Add Sessions to Day Off");

            if (entity.StartTime == default)
                v.Add("StartTime is required.");

            if (entity.EndTime == default)
                v.Add("EndTime is required.");

            if (entity.EndTime <= entity.StartTime)
                v.Add("EndTime must be greater than StartTime.");

            if (entity.SlotMinutes <= 0)
                v.Add("SlotMinutes must be greater than 0.");



            // Optional: enforce "reasonable" slot minutes (10/15/20/30/45/60...)
            if (v.IsValid && entity.SlotMinutes % 5 != 0) v.Add("SlotMinutes must be multiple of 5.");



            // Overlap check (same overrideId)
            if (v.IsValid)
            {
                bool overlap = DoctorDayOverrideSessionData.IsOverlapping(
                    overrideId: entity.OverrideId,
                    startTime: entity.StartTime,
                    endTime: entity.EndTime,
                    ignoreSessionId: entity.SessionId <= 0 ? (int?)null : entity.SessionId
                );

                if (overlap)
                    v.Add("This session overlaps with an existing session for the same override.");
            }

            return v;
        }

        protected override string GetAuditMessage(string operation, DoctorDayOverrideSession entity)
            => $"{EntityName} [{entity.SessionId}] {operation} performed.";

        // =========================================================
        // EXTRA METHODS (same style as DoctorDayOverrideService)
        // =========================================================

        public Result<DoctorDayOverrideSession> GetByIdSecure(int sessionId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<DoctorDayOverrideSession>.Fail("Permission denied.");

            if (sessionId <= 0)
                return Result<DoctorDayOverrideSession>.Fail("Invalid SessionId.");

            var s = DoctorDayOverrideSessionData.GetById(sessionId);

            if (s is null)
                return Result<DoctorDayOverrideSession>.Fail("Session not found.");

            AuditWriter.Write(
                action: GetAuditMessage("VIEW", s),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: s.SessionId.ToString(),
                success: true,
                newEntity: s
            );

            return Result<DoctorDayOverrideSession>.Ok(s);
        }

        public Result<IEnumerable<DoctorDayOverrideSession>> GetByOverrideId(int overrideId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<DoctorDayOverrideSession>>.Fail("Permission denied.");

            if (overrideId <= 0)
                return Result<IEnumerable<DoctorDayOverrideSession>>.Fail("Invalid OverrideId.");

            var list = DoctorDayOverrideSessionData.GetByOverrideId(overrideId) ?? Array.Empty<DoctorDayOverrideSession>();

            AuditWriter.Write(
                action: $"{EntityName} VIEW BY OVERRIDE [{overrideId}]",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: overrideId.ToString(),
                success: true,
                newEntity: list
            );

            return Result<IEnumerable<DoctorDayOverrideSession>>.Ok(list);
        }

        public Result<bool> HasAnySession(int overrideId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<bool>.Fail("Permission denied.");

            if (overrideId <= 0)
                return Result<bool>.Fail("Invalid OverrideId.");

            bool any = DoctorDayOverrideSessionData.HasAnySession(overrideId);
            return Result<bool>.Ok(any);
        }

        public Result<bool> IsOverlapping(int overrideId, TimeSpan startTime, TimeSpan endTime, int? ignoreSessionId = null)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<bool>.Fail("Permission denied.");

            if (overrideId <= 0)
                return Result<bool>.Fail("Invalid OverrideId.");

            if (endTime <= startTime)
                return Result<bool>.Fail("EndTime must be greater than StartTime.");

            bool overlap = DoctorDayOverrideSessionData.IsOverlapping(overrideId, startTime, endTime, ignoreSessionId);
            return Result<bool>.Ok(overlap);
        }

        public bool Exists(int sessionId)
            => sessionId > 0 && DoctorDayOverrideSessionData.GetById(sessionId) != null;

        // =========================================================
        // Convenience CRUD wrappers (optional)
        // =========================================================

        public Result<int> CreateSession(DoctorDayOverrideSession entity)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result<int>.Fail("Permission denied.");

            var v = IsValidateData(entity);
            if (!v.IsValid)
                return Result<int>.Fail(v.Errors);

            int newId = DoctorDayOverrideSessionData.Insert(entity);
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

            return ok ? Result<int>.Ok(newId) : Result<int>.Fail("Failed to create session.");
        }

        public Result UpdateSession(DoctorDayOverrideSession entity)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (entity is null)
                return Result.Fail("Session is required.");

            if (entity.SessionId <= 0)
                return Result.Fail("Invalid SessionId.");

            var old = DoctorDayOverrideSessionData.GetById(entity.SessionId);
            if (old is null)
                return Result.Fail("Session not found.");

            var v = IsValidateData(entity);
            if (!v.IsValid)
                return Result.Fail(v.Errors);

            bool ok = DoctorDayOverrideSessionData.Update(entity);

            AuditWriter.Write(
                action: GetAuditMessage("UPDATE", entity),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: entity.SessionId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: entity,
                failureReason: ok ? null : "Update failed."
            );

            return ok ? Result.Ok() : Result.Fail("Failed to update session.");
        }

        public Result DeleteSession(int sessionId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            if (sessionId <= 0)
                return Result.Fail("Invalid SessionId.");

            var old = DoctorDayOverrideSessionData.GetById(sessionId);
            if (old is null)
                return Result.Fail("Session not found.");

            bool ok = DoctorDayOverrideSessionData.Delete(sessionId);

            AuditWriter.Write(
                action: $"{EntityName} DELETE [{sessionId}]",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: sessionId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: default(DoctorDayOverrideSession),
                failureReason: ok ? null : "Delete failed."
            );

            return ok ? Result.Ok() : Result.Fail("Failed to delete session.");
        }

        // =========================================================
        // OPTIONAL: Bulk replace sessions for one OverrideId
        // (Very useful in UI: edit sessions grid then Save)
        // =========================================================
        public Result ReplaceSessions(int overrideId, IEnumerable<DoctorDayOverrideSession> sessions)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (overrideId <= 0)
                return Result.Fail("Invalid OverrideId.");

            if (sessions is null)
                return Result.Fail("Sessions list is required.");

            // normalize & validate each session (including overlap with DB)
            var list = sessions.ToList();
            foreach (var s in list)
                s.OverrideId = overrideId;

            // in-memory overlap (fast pre-check) to give nicer error message
            if (HasInMemoryOverlap(list, out var overlapMsg))
                return Result.Fail(overlapMsg);

            // validate one-by-one (includes DB overlap check)
            foreach (var s in list)
            {
                var v = IsValidateData(s);
                if (!v.IsValid)
                    return Result.Fail(v.Errors);
            }

            // NOTE: true "replace" should be transactional at DAL level:
            // - delete old sessions
            // - insert new sessions
            // Here we keep it service-level only; implement transaction in DAL if you want.
            // For now: caller can do manual delete+insert.

            AuditWriter.Write(
                action: $"{EntityName} REPLACE SESSIONS FOR OVERRIDE [{overrideId}]",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: overrideId.ToString(),
                success: true,
                newEntity: list
            );

            return Result.Ok();
        }

        private static bool HasInMemoryOverlap(List<DoctorDayOverrideSession> list, out string message)
        {
            message = string.Empty;
            if (list.Count <= 1) return false;

            var ordered = list.OrderBy(x => x.StartTime).ToList();
            for (int i = 0; i < ordered.Count - 1; i++)
            {
                var a = ordered[i];
                var b = ordered[i + 1];

                if (a.EndTime > b.StartTime) // overlap
                {
                    message = $"Sessions overlap: [{a.StartTime:hh\\:mm}-{a.EndTime:hh\\:mm}] overlaps [{b.StartTime:hh\\:mm}-{b.EndTime:hh\\:mm}].";
                    return true;
                }
            }

            return false;
        }
    }

}
