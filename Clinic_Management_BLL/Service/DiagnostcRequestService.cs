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

    public enum enDiagnosticRequestPriority : byte
    {
        Routine = 1,
        Urgent = 2,
        Stat = 3
    }

    public enum enDiagnosticRequestStatus : byte
    {
        Pending = 1,
        InProgress = 2,
        Completed = 3,
        Cancelled = 4
    }

    public sealed class DiagnosticRequestService : BaseCrudService<DiagnosticRequest>
    {
        // =======================
        // PERMISSIONS
        // =======================
        protected override string CreatePermissionCode => "DIAGNOSTIC_REQUEST_CREATE";
        protected override string UpdatePermissionCode => "DIAGNOSTIC_REQUEST_UPDATE";
        protected override string DeletePermissionCode => "DIAGNOSTIC_REQUEST_DELETE";
        protected override string ViewPermissionCode => "DIAGNOSTIC_REQUEST_VIEW";

        protected override string EntityName => "DiagnosticRequest";

        // =======================
        // DAL WIRING
        // =======================
        protected override int DalCreate(DiagnosticRequest entity)
            => DiagnosticRequestData.Insert(entity);

        protected override bool DalUpdate(DiagnosticRequest entity)
            => DiagnosticRequestData.Update(entity);

        protected override bool DalDelete(int id)
            => DiagnosticRequestData.GetById(id) != null
               && DiagnosticRequestData.Delete(id);

        protected override DiagnosticRequest? DalGetById(int id)
            => DiagnosticRequestData.GetById(id);

        protected override IEnumerable<DiagnosticRequest> DalGetAll()
            => DiagnosticRequestData.GetAll() ?? Enumerable.Empty<DiagnosticRequest>();

        protected override int GetEntityId(DiagnosticRequest entity)
            => entity.DiagnosticRequestId;

        // =======================
        // VALIDATION
        // =======================
        protected override ValidationResult.ValidationResult IsValidateData(DiagnosticRequest r)
        {
            var v = ValidationResult.ValidationResult.Success();

            if (r is null)
            {
                v.Add("DiagnosticRequest is required.");
                return v;
            }

            if (r.AppointmentId <= 0) v.Add("AppointmentId is required.");
            if (r.PatientId <= 0) v.Add("PatientId is required.");
            if (r.DoctorId <= 0) v.Add("DoctorId is required.");

            // Optional: validate enum ranges (if you use these enums in UI/BLL)
            if (r.Priority <= 0) v.Add("Priority is required.");
            if (r.Status <= 0) v.Add("Status is required.");

            if (r.AppointmentId > 0)
            {
                var appointment = AppointmentData.GetById(r.AppointmentId);

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
        public Result<DiagnosticRequest> GetByIdSafe(int diagnosticRequestId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<DiagnosticRequest>.Fail("Permission denied.");

            if (diagnosticRequestId <= 0)
                return Result<DiagnosticRequest>.Fail("Invalid DiagnosticRequestId.");

            var r = DiagnosticRequestData.GetById(diagnosticRequestId);
            if (r is null)
                return Result<DiagnosticRequest>.Fail("Diagnostic request not found.");

            AuditWriter.Write(
                action: GetAuditMessage("VIEW", r),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: r.DiagnosticRequestId.ToString(),
                success: true,
                newEntity: r
            );

            return Result<DiagnosticRequest>.Ok(r);
        }

        public Result<IEnumerable<DiagnosticRequest>> GetAllSafe()
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<DiagnosticRequest>>.Fail("Permission denied.");

            var list = DiagnosticRequestData.GetAll() ?? Enumerable.Empty<DiagnosticRequest>();

            AuditLogData.Log("View Diagnostic Requests", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<DiagnosticRequest>>.Ok(list);
        }


        public Result<IEnumerable<DiagnosticRequestItemDetail>> GetDiagnosticRequestItems(int DiagRequestID)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<DiagnosticRequestItemDetail>>.Fail("Permission denied.");

            var list = DiagnosticRequestData.GetDetailsByRequestId(DiagRequestID) ?? 
                Enumerable.Empty<DiagnosticRequestItemDetail>();

            if(list.Any())
            {
                AuditLogData.Log("View Diagnostic Requests Items", true, SecurityContext.Current.UserId, EntityName);

                return Result<IEnumerable<DiagnosticRequestItemDetail>>.Ok(list);
            }


            return Result<IEnumerable<DiagnosticRequestItemDetail>>.Fail("Cannot Find Diagnostic Request Items");
        }


        public  Result<IEnumerable<DiagnosticRequestDetail>> GetRequestsByAppointmentId(int appointmentId)
        {
            // Permission check
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<DiagnosticRequestDetail>>.Fail("Permission denied.");

            // Validation
            if (appointmentId <= 0)
            {
                return Result<IEnumerable<DiagnosticRequestDetail>>
                    .Fail("Invalid appointment id.");
            }

            // Call DAL
            var list = DiagnosticRequestData.GetRequestsByAppointmentId(appointmentId);

            if (list.Any())
            {
                AuditLogData.Log("View Diagnostic Requests By Appointment", true, SecurityContext.Current.UserId, EntityName);

                return Result<IEnumerable<DiagnosticRequestDetail>>.Ok(list);

            }

            return Result<IEnumerable<DiagnosticRequestDetail>>.Fail("No Diagnostic Request Exists.");

        }

        public Result<IEnumerable<DiagnosticRequest>> GetByAppointmentId(int appointmentId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<DiagnosticRequest>>.Fail("Permission denied.");

            if (appointmentId <= 0)
                return Result<IEnumerable<DiagnosticRequest>>.Fail("Invalid AppointmentId.");

            var list = DiagnosticRequestData.GetByAppointmentId(appointmentId) ?? Enumerable.Empty<DiagnosticRequest>();

            if(list.Any())
            {
                AuditLogData.Log("View Diagnostic Requests By Appointment", true, SecurityContext.Current.UserId, EntityName);

                return Result<IEnumerable<DiagnosticRequest>>.Ok(list);

            }

            return Result<IEnumerable<DiagnosticRequest>>.Fail("No Diagnostic Request Exists.");


        }

        public Result<IEnumerable<DiagnosticRequest>> GetByPatientId(int patientId, DateTime? from = null, DateTime? to = null)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<DiagnosticRequest>>.Fail("Permission denied.");

            if (patientId <= 0)
                return Result<IEnumerable<DiagnosticRequest>>.Fail("Invalid PatientId.");

            var list = DiagnosticRequestData.GetByPatientId(patientId, from, to) ?? Enumerable.Empty<DiagnosticRequest>();

            AuditLogData.Log("View Diagnostic Requests By Patient", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<DiagnosticRequest>>.Ok(list);
        }

        public Result<IEnumerable<DiagnosticRequest>> GetByDoctorId(int doctorId, DateTime? from = null, DateTime? to = null)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<DiagnosticRequest>>.Fail("Permission denied.");

            if (doctorId <= 0)
                return Result<IEnumerable<DiagnosticRequest>>.Fail("Invalid DoctorId.");

            var list = DiagnosticRequestData.GetByDoctorId(doctorId, from, to) ?? Enumerable.Empty<DiagnosticRequest>();

            AuditLogData.Log("View Diagnostic Requests By Doctor", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<DiagnosticRequest>>.Ok(list);
        }

        // =======================
        // CREATE / UPDATE (Smart wrappers with Audit)
        // =======================
        public Result<int> CreateDiagnosticRequest(DiagnosticRequest r)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result<int>.Fail("Permission denied.");

            // Defaults (optional, safe)
            if (r.RequestedAt == default)
                r.RequestedAt = DateTime.UtcNow;

            var v = IsValidateData(r);
            if (!v.IsValid)
                return Result<int>.Fail(v.Errors);

            int newId = DiagnosticRequestData.Insert(r);

            bool ok = newId > 0;
            if (ok)
            {
                r.DiagnosticRequestId = newId;

                AuditWriter.Write(
                    action: GetAuditMessage("CREATE", r),
                    performedBy: SecurityContext.Current.UserId,
                    entityType: EntityName,
                    entityId: newId.ToString(),
                    success: true,
                    newEntity: r
                );

                AuditLogData.Log("Create Diagnostic Request", true, SecurityContext.Current.UserId, EntityName);

                return Result<int>.Ok(newId);
            }

            AuditWriter.Write(
                action: $"{EntityName} CREATE failed",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: "0",
                success: false,
                newEntity: r,
                failureReason: "Insert returned 0"
            );

            return Result<int>.Fail("Failed to create diagnostic request.");
        }

        public Result UpdateDiagnosticRequest(DiagnosticRequest r)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (r.DiagnosticRequestId <= 0)
                return Result.Fail("Invalid DiagnosticRequestId.");

            var old = DiagnosticRequestData.GetById(r.DiagnosticRequestId);
            if (old is null)
                return Result.Fail("Diagnostic request not found.");

            var v = IsValidateData(r);
            if (!v.IsValid)
                return Result.Fail(v.Errors);

            bool ok = DiagnosticRequestData.Update(r);

            AuditWriter.Write<DiagnosticRequest>(
                action: GetAuditMessage("UPDATE", r),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: r.DiagnosticRequestId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: r,
                failureReason: ok ? null : "Update returned false"
            );

            if (ok) AuditLogData.Log("Update Diagnostic Request", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Failed to update diagnostic request.");
        }

        // =======================
        // STATUS
        // =======================
        public Result SetStatus(int diagnosticRequestId, enDiagnosticRequestStatus status)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (diagnosticRequestId <= 0)
                return Result.Fail("Invalid DiagnosticRequestId.");

            var old = DiagnosticRequestData.GetById(diagnosticRequestId);
            if (old is null)
                return Result.Fail("Diagnostic request not found.");

            var current = (enDiagnosticRequestStatus)old.Status;

            if (!IsValidTransition(current, status))
                return Result.Fail($"Invalid status transition: {current} -> {status}.");

            bool ok = DiagnosticRequestData.SetStatus(diagnosticRequestId, (byte)status);

            // Snapshot for audit (don’t mutate old)
            var after = new DiagnosticRequest
            {
                DiagnosticRequestId = old.DiagnosticRequestId,
                AppointmentId = old.AppointmentId,
                PatientId = old.PatientId,
                DoctorId = old.DoctorId,
                RequestedAt = old.RequestedAt,
                ClinicalInfo = old.ClinicalInfo,
                Priority = old.Priority,
                Status = (byte)status
            };

            AuditWriter.Write<DiagnosticRequest>(
                action: GetAuditMessage("STATUS", after),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: diagnosticRequestId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: after,
                failureReason: ok ? null : "SetStatus returned false"
            );

            if (ok) AuditLogData.Log("Set Diagnostic Request Status", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Status update failed.");
        }

        private static bool IsValidTransition(enDiagnosticRequestStatus from, enDiagnosticRequestStatus to)
        {
            if (from == to) return true;

            return from switch
            {
                enDiagnosticRequestStatus.Pending =>
                    to == enDiagnosticRequestStatus.InProgress
                    || to == enDiagnosticRequestStatus.Cancelled,

                enDiagnosticRequestStatus.InProgress =>
                    to == enDiagnosticRequestStatus.Completed
                    || to == enDiagnosticRequestStatus.Cancelled,

                // terminal states
                enDiagnosticRequestStatus.Completed => false,
                enDiagnosticRequestStatus.Cancelled => false,

                _ => false
            };
        }

        // =======================
        // DELETE (Hard)
        // =======================
        public Result DeleteDiagnosticRequest(int diagnosticRequestId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            if (diagnosticRequestId <= 0)
                return Result.Fail("Invalid DiagnosticRequestId.");

            var old = DiagnosticRequestData.GetById(diagnosticRequestId);
            if (old is null)
                return Result.Fail("Diagnostic request not found.");

            bool ok = DiagnosticRequestData.Delete(diagnosticRequestId);

            AuditWriter.Write<DiagnosticRequest>(
                action: GetAuditMessage("DELETE", old),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: diagnosticRequestId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: default,
                failureReason: ok ? null : "Delete returned false"
            );

            if (ok) AuditLogData.Log("Delete Diagnostic Request", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Delete failed.");
        }


        public Result<IEnumerable<DiagnosticRequestDetail>> GetAllDetails()
        {
            // Permission check
            if (!PermissionChecker.PermissionChecker
                .HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
            {
                return Result<IEnumerable<DiagnosticRequestDetail>>
                    .Fail("Permission denied.");
            }


            // Call DAL
            var list = DiagnosticRequestData.GetAllDetails();

            if (list.Any())
            {
                AuditLogData.Log(
                    "View Diagnostic Request Details",
                    true,
                    SecurityContext.Current.UserId,
                    EntityName
                );

                return Result<IEnumerable<DiagnosticRequestDetail>>.Ok(list);
            }

            return Result<IEnumerable<DiagnosticRequestDetail>>
                .Fail("No Diagnostic Request Details Found.");
        }



        // =======================
        // AUDIT MESSAGE
        // =======================
        protected override string GetAuditMessage(string operation, DiagnosticRequest entity)
            => $"{EntityName} [{entity.DiagnosticRequestId}] {operation} performed.";
    }
}
