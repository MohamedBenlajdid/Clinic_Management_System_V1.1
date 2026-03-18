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

    public enum enAppointmentStatus : byte
    {
        Scheduled = 1,
        Completed = 2,
        NoShow = 3,
        Cancelled = 4,
        InProgress = 5
    }

    public sealed class AppointmentService : BaseCrudService<Appointment>
    {
        // =======================
        // PERMISSIONS
        // =======================
        protected override string CreatePermissionCode => "APPOINTMENT_CREATE";
        protected override string UpdatePermissionCode => "APPOINTMENT_UPDATE";
        protected override string DeletePermissionCode => "APPOINTMENT_DELETE";
        protected override string ViewPermissionCode => "APPOINTMENT_VIEW";

        protected override string EntityName => "Appointment";

        // =======================
        // DAL WIRING
        // =======================
        protected override int DalCreate(Appointment entity)
            => AppointmentData.Insert(entity);

        protected override bool DalUpdate(Appointment entity)
            => AppointmentData.Update(entity);

        protected override bool DalDelete(int id)
            => AppointmentData.GetById(id) != null
               && AppointmentData.SoftDelete(id, SecurityContext.Current.UserId);

        protected override Appointment? DalGetById(int id)
            => AppointmentData.GetById(id);

        protected override IEnumerable<Appointment> DalGetAll()
            => AppointmentData.GetAll();

        protected override int GetEntityId(Appointment entity)
            => entity.AppointmentId;

        // =======================
        // VALIDATION
        // =======================
        protected override ValidationResult.ValidationResult IsValidateData(Appointment a)
        {
            var v = ValidationResult.ValidationResult.Success();

            if (a.PatientId <= 0) v.Add("PatientId is required.");
            if (a.DoctorId <= 0) v.Add("DoctorId is required.");

            if (a.StartAt == default) v.Add("StartAt is required.");
            if (a.EndAt == default) v.Add("EndAt is required.");
            if (a.EndAt <= a.StartAt) v.Add("EndAt must be greater than StartAt.");

            // overlap checks (ignore itself in update)
            if (a.DoctorId > 0 && a.StartAt != default && a.EndAt != default)
            {
                bool doctorTaken = AppointmentData.IsDoctorSlotTaken(
                    a.DoctorId, a.StartAt, a.EndAt,
                    ignoreAppointmentId: a.AppointmentId > 0 ? a.AppointmentId : null);

                if (doctorTaken)
                    v.Add("Doctor slot is already taken.");
            }

            if (a.PatientId > 0 && a.StartAt != default && a.EndAt != default)
            {
                bool patientTaken = AppointmentData.IsPatientSlotTaken(
                    a.PatientId, a.StartAt, a.EndAt,
                    ignoreAppointmentId: a.AppointmentId > 0 ? a.AppointmentId : null);

                if (patientTaken)
                    v.Add("Patient already has an appointment at this time.");
            }

            // cancel reason required if cancelled
            if ((enAppointmentStatus)a.Status == enAppointmentStatus.Cancelled &&
                string.IsNullOrWhiteSpace(a.CancelReason))
            {
                v.Add("CancelReason is required when appointment is cancelled.");
            }


            if(a.AppointmentId <= 0 && (enAppointmentStatus)a.Status != enAppointmentStatus.Scheduled)
            {
                v.Add("New Appointment Should Be Scheduled mode .");

            }


            return v;
        }

        // =======================
        // READ OPERATIONS (with Permission + Audit)
        // =======================
        public Result<Appointment> GetByIdSafe(int appointmentId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<Appointment>.Fail("Permission denied.");

            if (appointmentId <= 0)
                return Result<Appointment>.Fail("Invalid AppointmentId.");

            var a = AppointmentData.GetById(appointmentId);
            if (a is null)
                return Result<Appointment>.Fail("Appointment not found.");

            AuditWriter.Write(
                action: GetAuditMessage("VIEW", a),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: a.AppointmentId.ToString(),
                success: true,
                newEntity: a
            );

            return Result<Appointment>.Ok(a);
        }

        public Result<IEnumerable<Appointment>> GetAllSafe()
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<Appointment>>.Fail("Permission denied.");

            var list = AppointmentData.GetAll() ?? Enumerable.Empty<Appointment>();

            AuditLogData.Log("View Appointments", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<Appointment>>.Ok(list);
        }

        public Result<IEnumerable<Appointment>> GetByDoctorId(int doctorId, DateTime? from = null, DateTime? to = null, bool includeDeleted = false)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<Appointment>>.Fail("Permission denied.");

            if (doctorId <= 0)
                return Result<IEnumerable<Appointment>>.Fail("Invalid DoctorId.");

            var list = AppointmentData.GetByDoctorId(doctorId, from, to, includeDeleted) ?? Enumerable.Empty<Appointment>();

            AuditLogData.Log("View Appointments By Doctor", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<Appointment>>.Ok(list);
        }

        public Result<IEnumerable<Appointment>> GetByPatientId(int patientId, DateTime? from = null, DateTime? to = null, bool includeDeleted = false)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<Appointment>>.Fail("Permission denied.");

            if (patientId <= 0)
                return Result<IEnumerable<Appointment>>.Fail("Invalid PatientId.");

            var list = AppointmentData.GetByPatientId(patientId, from, to, includeDeleted) ?? Enumerable.Empty<Appointment>();

            AuditLogData.Log("View Appointments By Patient", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<Appointment>>.Ok(list);
        }

        // =======================
        // CREATE / UPDATE (Smart wrappers with Audit)
        // =======================
        public Result<int> CreateAppointment(Appointment a)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result<int>.Fail("Permission denied.");

            // attach audit fields
            a.CreatedByUserId = SecurityContext.Current.UserId;
            a.UpdatedByUserId = null;

            // validate
            var v = IsValidateData(a);
            if (!v.IsValid)
                return Result<int>.Fail(v.Errors);

            int newId = AppointmentData.Insert(a);

            bool ok = newId > 0;
            if (ok)
            {
                a.AppointmentId = newId;

                AuditWriter.Write(
                    action: GetAuditMessage("CREATE", a),
                    performedBy: SecurityContext.Current.UserId,
                    entityType: EntityName,
                    entityId: newId.ToString(),
                    success: true,
                    newEntity: a
                );

                AuditLogData.Log("Create Appointment", true, SecurityContext.Current.UserId, EntityName);
                return Result<int>.Ok(newId);
            }

            AuditWriter.Write(
                action: $"{EntityName} CREATE failed",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: "0",
                success: false,
                newEntity: a,
                failureReason: "Insert returned 0"
            );

            return Result<int>.Fail("Failed to create appointment.");
        }

        public Result UpdateAppointment(Appointment a)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (a.AppointmentId <= 0)
                return Result.Fail("Invalid AppointmentId.");

            var old = AppointmentData.GetById(a.AppointmentId);

            a.UpdatedByUserId = SecurityContext.Current.UserId;

            var v = IsValidateData(a);
            if (!v.IsValid)
                return Result.Fail(v.Errors);

            bool ok = AppointmentData.Update(a);

            AuditWriter.Write<Appointment>(
                action: GetAuditMessage("UPDATE", a),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: a.AppointmentId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: a,
                failureReason: ok ? null : "Update returned false"
            );

            if (ok) AuditLogData.Log("Update Appointment", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Failed to update appointment.");
        }

        // =======================
        // CANCEL / STATUS
        // =======================
        public Result Cancel(int appointmentId, string? cancelReason)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (appointmentId <= 0)
                return Result.Fail("Invalid AppointmentId.");

            var old = AppointmentData.GetById(appointmentId);
            if (old is null)
                return Result.Fail("Appointment not found.");
            if (old.Status != (byte)enAppointmentStatus.Scheduled)
                return Result.Fail("Appointment Should Be In Scheduled Mode ,Contact You Admin for Help.");

            bool ok = AppointmentData.Cancel(appointmentId, cancelReason, SecurityContext.Current.UserId);

            // Build "new" snapshot for audit (minimal)
            var after = old;
            after.Status = (byte)enAppointmentStatus.Cancelled;
            after.CancelReason = cancelReason;
            after.CancelledAt = DateTime.UtcNow;
            after.UpdatedByUserId = SecurityContext.Current.UserId;

            AuditWriter.Write<Appointment>(
                action: GetAuditMessage("CANCEL", after),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: appointmentId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: after,
                failureReason: ok ? null : "Cancel returned false"
            );

            if (ok) AuditLogData.Log("Cancel Appointment", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Cancel failed.");
        }

        public Result SetStatus(int appointmentId, enAppointmentStatus status)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (appointmentId <= 0)
                return Result.Fail("Invalid AppointmentId.");

            // Force Cancel scenario through Cancel() because it requires a reason
            if (status == enAppointmentStatus.Cancelled)
                return Result.Fail("Use Cancel(appointmentId, cancelReason) for Cancelled.");

            var old = AppointmentData.GetById(appointmentId);
            if (old is null)
                return Result.Fail("Appointment not found.");

            var current = (enAppointmentStatus)old.Status;

            // Validate transition rules
            if (!IsValidTransition(current, status))
                return Result.Fail($"Invalid status transition: {current} -> {status}.");

            // Apply
            bool ok = AppointmentData.SetStatus(appointmentId, (byte)status, SecurityContext.Current.UserId);

            // IMPORTANT: create a "new" snapshot for audit (don't mutate old reference)
            var after = new Appointment
            {
                AppointmentId = old.AppointmentId,
                PatientId = old.PatientId,
                DoctorId = old.DoctorId,
                StartAt = old.StartAt,
                EndAt = old.EndAt,
                Status = (byte)status,
                Reason = old.Reason,
                CreatedAt = old.CreatedAt,
                UpdatedAt = DateTime.Now, // optional (depends on DB trigger)
                CreatedByUserId = old.CreatedByUserId,
                UpdatedByUserId = SecurityContext.Current.UserId,
                CancelReason = old.CancelReason,
                CancelledAt = old.CancelledAt,
                IsDeleted = old.IsDeleted
            };

            AuditWriter.Write<Appointment>(
                action: GetAuditMessage("STATUS", after),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: appointmentId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: after,
                failureReason: ok ? null : "SetStatus returned false"
            );

            if (ok)
                AuditLogData.Log("Set Appointment Status", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Status update failed.");
        }

        // =======================
        // TRANSITION RULES
        // =======================
        private static bool IsValidTransition(enAppointmentStatus from, enAppointmentStatus to)
        {
            if (from == to)
                return true; // allow idempotent call

            return from switch
            {
                enAppointmentStatus.Scheduled =>
                    to == enAppointmentStatus.InProgress
                    || to == enAppointmentStatus.NoShow,

                enAppointmentStatus.InProgress =>
                    to == enAppointmentStatus.Completed,

                // terminal states: no transitions allowed
                enAppointmentStatus.Completed => false,
                enAppointmentStatus.NoShow => false,
                enAppointmentStatus.Cancelled => false,

                _ => false
            };
        }

        // =======================
        // DELETE (Soft)
        // =======================
        public Result SoftDelete(int appointmentId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            if (appointmentId <= 0)
                return Result.Fail("Invalid AppointmentId.");

            var old = AppointmentData.GetById(appointmentId);

            bool ok = AppointmentData.SoftDelete(appointmentId, SecurityContext.Current.UserId);

            AuditWriter.Write<Appointment>(
                action: GetAuditMessage("DELETE", old ?? new Appointment { AppointmentId = appointmentId }),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: appointmentId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: default,
                failureReason: ok ? null : "SoftDelete returned false"
            );

            if (ok) AuditLogData.Log("Delete Appointment", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Delete failed.");
        }

        // =======================
        // AUDIT MESSAGE
        // =======================
        protected override string GetAuditMessage(string operation, Appointment entity)
            => $"{EntityName} [{entity.AppointmentId}] {operation} performed.";
    }

}
