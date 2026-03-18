using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities.Entities
{
    public sealed class ClinicalAttachment
    {
        [DbColumn("AttachmentId")]
        public int AttachmentId { get; set; }

        [DbColumn("AppointmentId")]
        public int? AppointmentId { get; set; }

        [DbColumn("MedicalRecordId")]
        public int? MedicalRecordId { get; set; }

        [DbColumn("PrescriptionId")]
        public int? PrescriptionId { get; set; }

        [DbColumn("DiagnosticRequestId")]
        public int? DiagnosticRequestId { get; set; }

        [DbColumn("FileName")]
        public string FileName { get; set; } = null!;

        [DbColumn("StoredFileName")]
        public string StoredFileName { get; set; } = null!;

        [DbColumn("MimeType")]
        public string? MimeType { get; set; }

        [DbColumn("FileSizeBytes")]
        public long? FileSizeBytes { get; set; }

        [DbColumn("UploadedAt")]
        public DateTime UploadedAt { get; set; }
    }

}
