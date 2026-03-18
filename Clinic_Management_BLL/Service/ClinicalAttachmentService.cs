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

    public sealed class ClinicalAttachmentService : BaseCrudService<ClinicalAttachment>
    {
        // =======================
        // PERMISSIONS
        // =======================
        protected override string CreatePermissionCode => "CLINICAL_ATTACHMENT_CREATE";
        protected override string UpdatePermissionCode => "CLINICAL_ATTACHMENT_UPDATE";
        protected override string DeletePermissionCode => "CLINICAL_ATTACHMENT_DELETE";
        protected override string ViewPermissionCode => "CLINICAL_ATTACHMENT_VIEW";

        protected override string EntityName => "ClinicalAttachment";

        // =======================
        // DAL WIRING
        // =======================
        protected override int DalCreate(ClinicalAttachment entity)
            => ClinicalAttachmentData.Insert(entity);

        // NOTE: Your DAL currently has NO Update(). We keep BaseCrudService contract safe:
        // - either add ClinicalAttachmentData.Update(entity)
        // - or throw (but better: add Update in DAL)
        protected override bool DalUpdate(ClinicalAttachment entity)
            => throw new NotSupportedException("ClinicalAttachment update is not supported (DAL has no Update).");

        protected override bool DalDelete(int id)
            => ClinicalAttachmentData.GetById(id) != null
               && ClinicalAttachmentData.Delete(id);

        protected override ClinicalAttachment? DalGetById(int id)
            => ClinicalAttachmentData.GetById(id);

        // NOTE: Your DAL currently has NO GetAll(). We keep BaseCrudService contract safe.
        // If you want it, add ClinicalAttachmentData.GetAll().
        protected override IEnumerable<ClinicalAttachment> DalGetAll()
            => Enumerable.Empty<ClinicalAttachment>();

        protected override int GetEntityId(ClinicalAttachment entity)
            => entity.AttachmentId;

        // =======================
        // VALIDATION
        // =======================
        protected override ValidationResult.ValidationResult IsValidateData(ClinicalAttachment a)
        {
            var v = ValidationResult.ValidationResult.Success();

            if (a is null)
            {
                v.Add("ClinicalAttachment is required.");
                return v;
            }

            if (string.IsNullOrWhiteSpace(a.FileName))
                v.Add("FileName is required.");

            if (string.IsNullOrWhiteSpace(a.StoredFileName))
                v.Add("StoredFileName is required.");

            // Must match CK_ClinicalAttachments_Link
            if (a.AppointmentId == null &&
                a.MedicalRecordId == null &&
                a.PrescriptionId == null &&
                a.DiagnosticRequestId == null)
            {
                v.Add("Attachment must be linked to at least one clinical entity.");
            }

            // Optional: size must be >= 0 if provided
            if (a.FileSizeBytes.HasValue && a.FileSizeBytes.Value < 0)
                v.Add("FileSizeBytes cannot be negative.");

            return v;
        }

        // =======================
        // READ OPERATIONS (with Permission + Audit)
        // =======================
        public Result<ClinicalAttachment> GetByIdSafe(int attachmentId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<ClinicalAttachment>.Fail("Permission denied.");

            if (attachmentId <= 0)
                return Result<ClinicalAttachment>.Fail("Invalid AttachmentId.");

            var a = ClinicalAttachmentData.GetById(attachmentId);
            if (a is null)
                return Result<ClinicalAttachment>.Fail("Attachment not found.");

            AuditWriter.Write(
                action: GetAuditMessage("VIEW", a),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: a.AttachmentId.ToString(),
                success: true,
                newEntity: a
            );

            return Result<ClinicalAttachment>.Ok(a);
        }

        public Result<IEnumerable<ClinicalAttachment>> GetByAppointmentId(int appointmentId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<ClinicalAttachment>>.Fail("Permission denied.");

            if (appointmentId <= 0)
                return Result<IEnumerable<ClinicalAttachment>>.Fail("Invalid AppointmentId.");

            var list = ClinicalAttachmentData.GetByAppointmentId(appointmentId) ?? Enumerable.Empty<ClinicalAttachment>();

            AuditLogData.Log("View Clinical Attachments By Appointment", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<ClinicalAttachment>>.Ok(list);
        }

        public Result<IEnumerable<ClinicalAttachment>> GetByMedicalRecordId(int medicalRecordId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<ClinicalAttachment>>.Fail("Permission denied.");

            if (medicalRecordId <= 0)
                return Result<IEnumerable<ClinicalAttachment>>.Fail("Invalid MedicalRecordId.");

            var list = ClinicalAttachmentData.GetByMedicalRecordId(medicalRecordId) ?? Enumerable.Empty<ClinicalAttachment>();

            AuditLogData.Log("View Clinical Attachments By MedicalRecord", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<ClinicalAttachment>>.Ok(list);
        }

        public Result<IEnumerable<ClinicalAttachment>> GetByPrescriptionId(int prescriptionId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<ClinicalAttachment>>.Fail("Permission denied.");

            if (prescriptionId <= 0)
                return Result<IEnumerable<ClinicalAttachment>>.Fail("Invalid PrescriptionId.");

            var list = ClinicalAttachmentData.GetByPrescriptionId(prescriptionId) ?? Enumerable.Empty<ClinicalAttachment>();

            AuditLogData.Log("View Clinical Attachments By Prescription", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<ClinicalAttachment>>.Ok(list);
        }

        public Result<IEnumerable<ClinicalAttachment>> GetByDiagnosticRequestId(int diagnosticRequestId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<ClinicalAttachment>>.Fail("Permission denied.");

            if (diagnosticRequestId <= 0)
                return Result<IEnumerable<ClinicalAttachment>>.Fail("Invalid DiagnosticRequestId.");

            var list = ClinicalAttachmentData.GetByDiagnosticRequestId(diagnosticRequestId) ?? Enumerable.Empty<ClinicalAttachment>();

            AuditLogData.Log("View Clinical Attachments By DiagnosticRequest", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<ClinicalAttachment>>.Ok(list);
        }

        // =======================
        // CREATE (Smart wrapper with Audit)
        // =======================
        public Result<int> CreateAttachment(ClinicalAttachment a)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result<int>.Fail("Permission denied.");

            var v = IsValidateData(a);
            if (!v.IsValid)
                return Result<int>.Fail(v.Errors);

            int newId = ClinicalAttachmentData.Insert(a);

            bool ok = newId > 0;
            if (ok)
            {
                a.AttachmentId = newId;

                AuditWriter.Write(
                    action: GetAuditMessage("CREATE", a),
                    performedBy: SecurityContext.Current.UserId,
                    entityType: EntityName,
                    entityId: newId.ToString(),
                    success: true,
                    newEntity: a
                );

                AuditLogData.Log("Create Clinical Attachment", true, SecurityContext.Current.UserId, EntityName);

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

            return Result<int>.Fail("Failed to create attachment.");
        }

        // =======================
        // DELETE (Hard)
        // =======================
        public Result DeleteAttachment(int attachmentId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            if (attachmentId <= 0)
                return Result.Fail("Invalid AttachmentId.");

            var old = ClinicalAttachmentData.GetById(attachmentId);
            if (old is null)
                return Result.Fail("Attachment not found.");

            bool ok = ClinicalAttachmentData.Delete(attachmentId);

            AuditWriter.Write<ClinicalAttachment>(
                action: GetAuditMessage("DELETE", old),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: attachmentId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: default,
                failureReason: ok ? null : "Delete returned false"
            );

            if (ok) AuditLogData.Log("Delete Clinical Attachment", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Delete failed.");
        }

        // =======================
        // AUDIT MESSAGE
        // =======================
        protected override string GetAuditMessage(string operation, ClinicalAttachment entity)
            => $"{EntityName} [{entity.AttachmentId}] {operation} performed.";
    }

}
