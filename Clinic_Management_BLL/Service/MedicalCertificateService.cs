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

    public enum enMedicalCertificateType : byte
    {
        SickLeave = 1,
        Fitness = 2,
        ReturnToWork = 3,
        Other = 4
    }

    public sealed class MedicalCertificateService : BaseCrudService<MedicalCertificate>
    {
        // =======================
        // PERMISSIONS
        // =======================
        protected override string CreatePermissionCode => "MEDICAL_CERTIFICATE_CREATE";
        protected override string UpdatePermissionCode => "MEDICAL_CERTIFICATE_UPDATE";
        protected override string DeletePermissionCode => "MEDICAL_CERTIFICATE_DELETE";
        protected override string ViewPermissionCode => "MEDICAL_CERTIFICATE_VIEW";

        protected override string EntityName => "MedicalCertificate";

        // =======================
        // DAL WIRING
        // =======================
        protected override int DalCreate(MedicalCertificate entity)
            => MedicalCertificateData.Insert(entity);

        protected override bool DalUpdate(MedicalCertificate entity)
            => MedicalCertificateData.Update(entity);

        protected override bool DalDelete(int id)
            => MedicalCertificateData.GetById(id) != null
               && MedicalCertificateData.Delete(id);

        protected override MedicalCertificate? DalGetById(int id)
            => MedicalCertificateData.GetById(id);

        protected override IEnumerable<MedicalCertificate> DalGetAll()
            => MedicalCertificateData.GetAll() ?? Enumerable.Empty<MedicalCertificate>();

        protected override int GetEntityId(MedicalCertificate entity)
            => entity.MedicalCertificateId;

        // =======================
        // VALIDATION
        // =======================
        protected override ValidationResult.ValidationResult IsValidateData(MedicalCertificate c)
        {
            var v = ValidationResult.ValidationResult.Success();

            if (c is null)
            {
                v.Add("MedicalCertificate is required.");
                return v;
            }

            if (c.AppointmentId <= 0) v.Add("AppointmentId is required.");
            if (c.PatientId <= 0) v.Add("PatientId is required.");
            if (c.DoctorId <= 0) v.Add("DoctorId is required.");

            if (c.StartDate == default) v.Add("StartDate is required.");
            if (c.EndDate == default) v.Add("EndDate is required.");

            // CK_MedicalCertificates_Dates
            if (c.StartDate.Date > c.EndDate.Date)
                v.Add("StartDate must be <= EndDate.");


            if (c.AppointmentId > 0)
            {
                var appointment = AppointmentData.GetById(c.AppointmentId);

                if (appointment == null)
                {
                    v.Add("Associated appointment does not exist.");
                }
                else if (appointment.Status != (byte)enAppointmentStatus.InProgress)
                {
                    v.Add("Medical record can only be created when appointment status is InProgress.");
                }
            }

            // Optional: require some text
            // if (string.IsNullOrWhiteSpace(c.DiagnosisSummary) && string.IsNullOrWhiteSpace(c.Notes))
            //     v.Add("DiagnosisSummary or Notes is required.");

            return v;
        }

        // =======================
        // READ OPERATIONS (with Permission + Audit)
        // =======================
        public Result<MedicalCertificate> GetByIdSafe(int medicalCertificateId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<MedicalCertificate>.Fail("Permission denied.");

            if (medicalCertificateId <= 0)
                return Result<MedicalCertificate>.Fail("Invalid MedicalCertificateId.");

            var c = MedicalCertificateData.GetById(medicalCertificateId);
            if (c is null)
                return Result<MedicalCertificate>.Fail("Medical certificate not found.");

            AuditWriter.Write(
                action: GetAuditMessage("VIEW", c),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: c.MedicalCertificateId.ToString(),
                success: true,
                newEntity: c
            );

            return Result<MedicalCertificate>.Ok(c);
        }

        public Result<IEnumerable<MedicalCertificate>> GetAllSafe()
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<MedicalCertificate>>.Fail("Permission denied.");

            var list = MedicalCertificateData.GetAll() ?? Enumerable.Empty<MedicalCertificate>();

            AuditLogData.Log("View Medical Certificates", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<MedicalCertificate>>.Ok(list);
        }

        public Result<IEnumerable<MedicalCertificate>> GetByAppointmentId(int appointmentId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<MedicalCertificate>>.Fail("Permission denied.");

            if (appointmentId <= 0)
                return Result<IEnumerable<MedicalCertificate>>.Fail("Invalid AppointmentId.");

            var list = MedicalCertificateData.GetByAppointmentId(appointmentId) ?? Enumerable.Empty<MedicalCertificate>();

            if(list.Any())
            {
                AuditLogData.Log("View Medical Certificates By Appointment", true, SecurityContext.Current.UserId, EntityName);

                return Result<IEnumerable<MedicalCertificate>>.Ok(list);
            }


            return Result<IEnumerable<MedicalCertificate>>.Fail("Cannot Find Medical Certificate");


        }

        public Result<IEnumerable<MedicalCertificate>> GetByPatientId(int patientId, DateTime? issuedFrom = null, DateTime? issuedTo = null)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<MedicalCertificate>>.Fail("Permission denied.");

            if (patientId <= 0)
                return Result<IEnumerable<MedicalCertificate>>.Fail("Invalid PatientId.");

            var list = MedicalCertificateData.GetByPatientId(patientId, issuedFrom, issuedTo) ?? Enumerable.Empty<MedicalCertificate>();

            AuditLogData.Log("View Medical Certificates By Patient", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<MedicalCertificate>>.Ok(list);
        }

        public Result<IEnumerable<MedicalCertificate>> GetByDoctorId(int doctorId, DateTime? issuedFrom = null, DateTime? issuedTo = null)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<MedicalCertificate>>.Fail("Permission denied.");

            if (doctorId <= 0)
                return Result<IEnumerable<MedicalCertificate>>.Fail("Invalid DoctorId.");

            var list = MedicalCertificateData.GetByDoctorId(doctorId, issuedFrom, issuedTo) ?? Enumerable.Empty<MedicalCertificate>();

            AuditLogData.Log("View Medical Certificates By Doctor", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<MedicalCertificate>>.Ok(list);
        }

        // =======================
        // CREATE / UPDATE (Smart wrappers with Audit)
        // =======================
        public Result<int> CreateMedicalCertificate(MedicalCertificate c)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result<int>.Fail("Permission denied.");

            var v = IsValidateData(c);
            if (!v.IsValid)
                return Result<int>.Fail(v.Errors);

            int newId = MedicalCertificateData.Insert(c);

            bool ok = newId > 0;
            if (ok)
            {
                c.MedicalCertificateId = newId;

                AuditWriter.Write(
                    action: GetAuditMessage("CREATE", c),
                    performedBy: SecurityContext.Current.UserId,
                    entityType: EntityName,
                    entityId: newId.ToString(),
                    success: true,
                    newEntity: c
                );

                AuditLogData.Log("Create Medical Certificate", true, SecurityContext.Current.UserId, EntityName);

                return Result<int>.Ok(newId);
            }

            AuditWriter.Write(
                action: $"{EntityName} CREATE failed",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: "0",
                success: false,
                newEntity: c,
                failureReason: "Insert returned 0"
            );

            return Result<int>.Fail("Failed to create medical certificate.");
        }

        public Result UpdateMedicalCertificate(MedicalCertificate c)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (c.MedicalCertificateId <= 0)
                return Result.Fail("Invalid MedicalCertificateId.");

            var old = MedicalCertificateData.GetById(c.MedicalCertificateId);
            if (old is null)
                return Result.Fail("Medical certificate not found.");

            var v = IsValidateData(c);
            if (!v.IsValid)
                return Result.Fail(v.Errors);

            bool ok = MedicalCertificateData.Update(c);

            AuditWriter.Write<MedicalCertificate>(
                action: GetAuditMessage("UPDATE", c),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: c.MedicalCertificateId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: c,
                failureReason: ok ? null : "Update returned false"
            );

            if (ok) AuditLogData.Log("Update Medical Certificate", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Failed to update medical certificate.");
        }

        // =======================
        // DELETE (Hard)
        // =======================
        public Result DeleteMedicalCertificate(int medicalCertificateId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            if (medicalCertificateId <= 0)
                return Result.Fail("Invalid MedicalCertificateId.");

            var old = MedicalCertificateData.GetById(medicalCertificateId);
            if (old is null)
                return Result.Fail("Medical certificate not found.");

            bool ok = MedicalCertificateData.Delete(medicalCertificateId);

            AuditWriter.Write<MedicalCertificate>(
                action: GetAuditMessage("DELETE", old),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: medicalCertificateId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: default,
                failureReason: ok ? null : "Delete returned false"
            );

            if (ok) AuditLogData.Log("Delete Medical Certificate", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Delete failed.");
        }

        // =======================
        // AUDIT MESSAGE
        // =======================
        protected override string GetAuditMessage(string operation, MedicalCertificate entity)
            => $"{EntityName} [{entity.MedicalCertificateId}] {operation} performed.";
    }
}
