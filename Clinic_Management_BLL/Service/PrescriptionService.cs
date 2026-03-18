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

    public sealed class PrescriptionService : BaseCrudService<Prescription>
    {
        // =======================
        // PERMISSIONS
        // =======================
        protected override string CreatePermissionCode => "PRESCRIPTION_CREATE";
        protected override string UpdatePermissionCode => "PRESCRIPTION_UPDATE";
        protected override string DeletePermissionCode => "PRESCRIPTION_DELETE";
        protected override string ViewPermissionCode => "PRESCRIPTION_VIEW";

        protected override string EntityName => "Prescription";

        // =======================
        // DAL WIRING
        // =======================
        protected override int DalCreate(Prescription entity)
            => PrescriptionData.Insert(entity);

        protected override bool DalUpdate(Prescription entity)
            => PrescriptionData.Update(entity);

        protected override bool DalDelete(int id)
            => PrescriptionData.GetById(id) != null
               && PrescriptionData.Delete(id);

        protected override Prescription? DalGetById(int id)
            => PrescriptionData.GetById(id);

        protected override IEnumerable<Prescription> DalGetAll()
            => PrescriptionData.GetAll() ?? Enumerable.Empty<Prescription>();

        protected override int GetEntityId(Prescription entity)
            => entity.PrescriptionId;

        // =======================
        // VALIDATION
        // =======================
        protected override ValidationResult.ValidationResult IsValidateData(Prescription p)
        {
            var v = ValidationResult.ValidationResult.Success();

            if (p is null)
            {
                v.Add("Prescription is required.");
                return v;
            }

            if (p.AppointmentId <= 0) v.Add("AppointmentId is required.");
            if (p.PatientId <= 0) v.Add("PatientId is required.");
            if (p.DoctorId <= 0) v.Add("DoctorId is required.");

            // 1:1 enforcement is optional (your DAL says often 1:1 or 1:N)
            // If you WANT 1:1, keep this check enabled.
            // If you allow 1:N, remove this check.
            bool enforceOnePerAppointment = false;

            if (enforceOnePerAppointment && p.AppointmentId > 0)
            {
                bool exists = PrescriptionData.ExistsByAppointmentId(
                    p.AppointmentId,
                    ignorePrescriptionId: p.PrescriptionId > 0 ? p.PrescriptionId : null);

                if (exists)
                    v.Add("A prescription already exists for this appointment.");
            }

            if (p.AppointmentId > 0)
            {
                var appointment = AppointmentData.GetById(p.AppointmentId);

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
        public Result<Prescription> GetByIdSafe(int prescriptionId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<Prescription>.Fail("Permission denied.");

            if (prescriptionId <= 0)
                return Result<Prescription>.Fail("Invalid PrescriptionId.");

            var p = PrescriptionData.GetById(prescriptionId);
            if (p is null)
                return Result<Prescription>.Fail("Prescription not found.");

            AuditWriter.Write(
                action: GetAuditMessage("VIEW", p),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: p.PrescriptionId.ToString(),
                success: true,
                newEntity: p
            );

            return Result<Prescription>.Ok(p);
        }

        public Result<IEnumerable<Prescription>> GetAllSafe()
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<Prescription>>.Fail("Permission denied.");

            var list = PrescriptionData.GetAll() ?? Enumerable.Empty<Prescription>();

            AuditLogData.Log("View Prescriptions", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<Prescription>>.Ok(list);
        }

        public Result<IEnumerable<Prescription>> GetByAppointmentId(int appointmentId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<Prescription>>.Fail("Permission denied.");

            if (appointmentId <= 0)
                return Result<IEnumerable<Prescription>>.Fail("Invalid AppointmentId.");

            var list = PrescriptionData.GetByAppointmentId(appointmentId) ?? Enumerable.Empty<Prescription>();

            AuditLogData.Log("View Prescriptions By Appointment", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<Prescription>>.Ok(list);
        }

        public Result<IEnumerable<Prescription>> GetByPatientId(int patientId, DateTime? from = null, DateTime? to = null)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<Prescription>>.Fail("Permission denied.");

            if (patientId <= 0)
                return Result<IEnumerable<Prescription>>.Fail("Invalid PatientId.");

            var list = PrescriptionData.GetByPatientId(patientId, from, to) ?? Enumerable.Empty<Prescription>();

            AuditLogData.Log("View Prescriptions By Patient", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<Prescription>>.Ok(list);
        }

        public Result<IEnumerable<Prescription>> GetByDoctorId(int doctorId, DateTime? from = null, DateTime? to = null)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<Prescription>>.Fail("Permission denied.");

            if (doctorId <= 0)
                return Result<IEnumerable<Prescription>>.Fail("Invalid DoctorId.");

            var list = PrescriptionData.GetByDoctorId(doctorId, from, to) ?? Enumerable.Empty<Prescription>();

            AuditLogData.Log("View Prescriptions By Doctor", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<Prescription>>.Ok(list);
        }

        // =======================
        // CREATE / UPDATE (Smart wrappers with Audit)
        // =======================
        public Result<int> CreatePrescription(Prescription p)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result<int>.Fail("Permission denied.");

            var v = IsValidateData(p);
            if (!v.IsValid)
                return Result<int>.Fail(v.Errors);

            int newId = PrescriptionData.Insert(p);

            bool ok = newId > 0;
            if (ok)
            {
                p.PrescriptionId = newId;

                AuditWriter.Write(
                    action: GetAuditMessage("CREATE", p),
                    performedBy: SecurityContext.Current.UserId,
                    entityType: EntityName,
                    entityId: newId.ToString(),
                    success: true,
                    newEntity: p
                );

                AuditLogData.Log("Create Prescription", true, SecurityContext.Current.UserId, EntityName);

                return Result<int>.Ok(newId);
            }

            AuditWriter.Write(
                action: $"{EntityName} CREATE failed",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: "0",
                success: false,
                newEntity: p,
                failureReason: "Insert returned 0"
            );

            return Result<int>.Fail("Failed to create prescription.");
        }

        public Result UpdatePrescription(Prescription p)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (p.PrescriptionId <= 0)
                return Result.Fail("Invalid PrescriptionId.");

            var old = PrescriptionData.GetById(p.PrescriptionId);
            if (old is null)
                return Result.Fail("Prescription not found.");

            var v = IsValidateData(p);
            if (!v.IsValid)
                return Result.Fail(v.Errors);

            bool ok = PrescriptionData.Update(p);

            AuditWriter.Write<Prescription>(
                action: GetAuditMessage("UPDATE", p),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: p.PrescriptionId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: p,
                failureReason: ok ? null : "Update returned false"
            );

            if (ok) AuditLogData.Log("Update Prescription", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Failed to update prescription.");
        }

        // =======================
        // DELETE (Hard)
        // =======================
        public Result DeletePrescription(int prescriptionId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            if (prescriptionId <= 0)
                return Result.Fail("Invalid PrescriptionId.");

            var old = PrescriptionData.GetById(prescriptionId);
            if (old is null)
                return Result.Fail("Prescription not found.");

            bool ok = PrescriptionData.Delete(prescriptionId);

            AuditWriter.Write<Prescription>(
                action: GetAuditMessage("DELETE", old),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: prescriptionId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: default,
                failureReason: ok ? null : "Delete returned false"
            );

            if (ok) AuditLogData.Log("Delete Prescription", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Delete failed.");
        }

        // =======================
        // AUDIT MESSAGE
        // =======================
        protected override string GetAuditMessage(string operation, Prescription entity)
            => $"{EntityName} [{entity.PrescriptionId}] {operation} performed.";
    }
}
