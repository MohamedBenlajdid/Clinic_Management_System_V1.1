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

    public sealed class MedicalRecordService : BaseCrudService<MedicalRecord>
    {
        // =======================
        // PERMISSIONS
        // =======================
        protected override string CreatePermissionCode => "MEDICAL_RECORD_CREATE";
        protected override string UpdatePermissionCode => "MEDICAL_RECORD_UPDATE";
        protected override string DeletePermissionCode => "MEDICAL_RECORD_DELETE";
        protected override string ViewPermissionCode => "MEDICAL_RECORD_VIEW";

        protected override string EntityName => "MedicalRecord";

        // =======================
        // DAL WIRING
        // =======================
        protected override int DalCreate(MedicalRecord entity)
            => MedicalRecordData.Insert(entity);

        protected override bool DalUpdate(MedicalRecord entity)
            => MedicalRecordData.Update(entity);

        protected override bool DalDelete(int id)
            => MedicalRecordData.GetById(id) != null
               && MedicalRecordData.Delete(id);

        protected override MedicalRecord? DalGetById(int id)
            => MedicalRecordData.GetById(id);

        protected override IEnumerable<MedicalRecord> DalGetAll()
            => MedicalRecordData.GetAll() ?? Enumerable.Empty<MedicalRecord>();

        protected override int GetEntityId(MedicalRecord entity)
            => entity.MedicalRecordId;

        // =======================
        // VALIDATION
        // =======================
        protected override ValidationResult.ValidationResult IsValidateData(MedicalRecord r)
        {
            var v = ValidationResult.ValidationResult.Success();

            if (r is null)
            {
                v.Add("MedicalRecord is required.");
                return v;
            }

            if (r.AppointmentId <= 0) v.Add("AppointmentId is required.");
            if (r.PatientId <= 0) v.Add("PatientId is required.");
            if (r.DoctorId <= 0) v.Add("DoctorId is required.");

            // Optional: require some clinical content
            bool hasAny =
                !string.IsNullOrWhiteSpace(r.ChiefComplaint) ||
                !string.IsNullOrWhiteSpace(r.HistoryOfPresentIllness) ||
                !string.IsNullOrWhiteSpace(r.Examination) ||
                !string.IsNullOrWhiteSpace(r.Diagnosis) ||
                !string.IsNullOrWhiteSpace(r.Notes);

            if (!hasAny)
                v.Add("At least one clinical field must be filled (ChiefComplaint/HPI/Examination/Diagnosis/Notes).");

            // ===============================
            // NEW VALIDATION: Appointment must be InProgress
            // ===============================
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

            // Prevent 1:1 duplication on appointment
            if (r.AppointmentId > 0)
            {
                bool exists = MedicalRecordData.ExistsByAppointmentId(
                    r.AppointmentId,
                    ignoreMedicalRecordId: r.MedicalRecordId > 0 ? r.MedicalRecordId : null);

                if (exists)
                    v.Add("A medical record already exists for this appointment.");
            }

            return v;
        }

        // =======================
        // READ OPERATIONS (with Permission + Audit)
        // =======================
        public Result<MedicalRecord> GetByIdSafe(int medicalRecordId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<MedicalRecord>.Fail("Permission denied.");

            if (medicalRecordId <= 0)
                return Result<MedicalRecord>.Fail("Invalid MedicalRecordId.");

            var r = MedicalRecordData.GetById(medicalRecordId);
            if (r is null)
                return Result<MedicalRecord>.Fail("Medical record not found.");

            AuditWriter.Write(
                action: GetAuditMessage("VIEW", r),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: r.MedicalRecordId.ToString(),
                success: true,
                newEntity: r
            );

            return Result<MedicalRecord>.Ok(r);
        }

        public Result<MedicalRecord> GetByAppointmentIdSafe(int appointmentId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<MedicalRecord>.Fail("Permission denied.");

            if (appointmentId <= 0)
                return Result<MedicalRecord>.Fail("Invalid AppointmentId.");

            var r = MedicalRecordData.GetByAppointmentId(appointmentId);
            if (r is null)
                return Result<MedicalRecord>.Fail("Medical record not found for this appointment.");

            AuditWriter.Write(
                action: $"{EntityName} [Appointment:{appointmentId}] VIEW performed.",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: r.MedicalRecordId.ToString(),
                success: true,
                newEntity: r
            );

            return Result<MedicalRecord>.Ok(r);
        }

        public Result<IEnumerable<MedicalRecord>> GetAllSafe()
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<MedicalRecord>>.Fail("Permission denied.");

            var list = MedicalRecordData.GetAll() ?? Enumerable.Empty<MedicalRecord>();

            AuditLogData.Log("View Medical Records", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<MedicalRecord>>.Ok(list);
        }

        public Result<IEnumerable<MedicalRecord>> GetByPatientId(int patientId, DateTime? from = null, DateTime? to = null)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<MedicalRecord>>.Fail("Permission denied.");

            if (patientId <= 0)
                return Result<IEnumerable<MedicalRecord>>.Fail("Invalid PatientId.");

            var list = MedicalRecordData.GetByPatientId(patientId, from, to) ?? Enumerable.Empty<MedicalRecord>();

            AuditLogData.Log("View Medical Records By Patient", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<MedicalRecord>>.Ok(list);
        }

        public Result<IEnumerable<MedicalRecord>> GetByDoctorId(int doctorId, DateTime? from = null, DateTime? to = null)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<MedicalRecord>>.Fail("Permission denied.");

            if (doctorId <= 0)
                return Result<IEnumerable<MedicalRecord>>.Fail("Invalid DoctorId.");

            var list = MedicalRecordData.GetByDoctorId(doctorId, from, to) ?? Enumerable.Empty<MedicalRecord>();

            AuditLogData.Log("View Medical Records By Doctor", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<MedicalRecord>>.Ok(list);
        }

        // =======================
        // CREATE / UPDATE (Smart wrappers with Audit)
        // =======================
        public Result<int> CreateMedicalRecord(MedicalRecord r)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result<int>.Fail("Permission denied.");

            var v = IsValidateData(r);
            if (!v.IsValid)
                return Result<int>.Fail(v.Errors);

            int newId = MedicalRecordData.Insert(r);

            bool ok = newId > 0;
            if (ok)
            {
                r.MedicalRecordId = newId;

                AuditWriter.Write(
                    action: GetAuditMessage("CREATE", r),
                    performedBy: SecurityContext.Current.UserId,
                    entityType: EntityName,
                    entityId: newId.ToString(),
                    success: true,
                    newEntity: r
                );

                AuditLogData.Log("Create Medical Record", true, SecurityContext.Current.UserId, EntityName);

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

            return Result<int>.Fail("Failed to create medical record.");
        }

        public Result UpdateMedicalRecord(MedicalRecord r)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (r.MedicalRecordId <= 0)
                return Result.Fail("Invalid MedicalRecordId.");

            var old = MedicalRecordData.GetById(r.MedicalRecordId);
            if (old is null)
                return Result.Fail("Medical record not found.");

            var v = IsValidateData(r);
            if (!v.IsValid)
                return Result.Fail(v.Errors);

            bool ok = MedicalRecordData.Update(r);

            AuditWriter.Write<MedicalRecord>(
                action: GetAuditMessage("UPDATE", r),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: r.MedicalRecordId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: r,
                failureReason: ok ? null : "Update returned false"
            );

            if (ok) AuditLogData.Log("Update Medical Record", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Failed to update medical record.");
        }

        // =======================
        // DELETE (Hard)
        // =======================
        public Result DeleteMedicalRecord(int medicalRecordId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            if (medicalRecordId <= 0)
                return Result.Fail("Invalid MedicalRecordId.");

            var old = MedicalRecordData.GetById(medicalRecordId);
            if (old is null)
                return Result.Fail("Medical record not found.");

            bool ok = MedicalRecordData.Delete(medicalRecordId);

            AuditWriter.Write<MedicalRecord>(
                action: GetAuditMessage("DELETE", old),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: medicalRecordId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: default,
                failureReason: ok ? null : "Delete returned false"
            );

            if (ok) AuditLogData.Log("Delete Medical Record", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Delete failed.");
        }

        // =======================
        // AUDIT MESSAGE
        // =======================
        protected override string GetAuditMessage(string operation, MedicalRecord entity)
            => $"{EntityName} [{entity.MedicalRecordId}] {operation} performed.";
    }
}
