using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities;
using Clinic_Management_Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class ClinicalAttachmentData
    {
        private const string Columns = @"
        AttachmentId,
        AppointmentId,
        MedicalRecordId,
        PrescriptionId,
        DiagnosticRequestId,
        FileName,
        StoredFileName,
        MimeType,
        FileSizeBytes,
        UploadedAt";

        // =========================
        // GET BY ID
        // =========================
        public static ClinicalAttachment? GetById(int id)
        {
            string query = $@"
SELECT {Columns}
FROM ClinicalAttachments
WHERE AttachmentId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<ClinicalAttachment>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // =========================
        // GET BY APPOINTMENT
        // =========================
        public static IEnumerable<ClinicalAttachment> GetByAppointmentId(int appointmentId)
        {
            string query = $@"
SELECT {Columns}
FROM ClinicalAttachments
WHERE AppointmentId = @AppointmentId
ORDER BY UploadedAt DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<ClinicalAttachment>();
                    while (reader.Read())
                        list.Add(DbMapper<ClinicalAttachment>.Map(reader));
                    return list;
                },
                SqlParameterFactory.Create("@AppointmentId", appointmentId)
            );
        }

        // =========================
        // GET BY MEDICAL RECORD
        // =========================
        public static IEnumerable<ClinicalAttachment> GetByMedicalRecordId(int medicalRecordId)
        {
            string query = $@"
SELECT {Columns}
FROM ClinicalAttachments
WHERE MedicalRecordId = @MedicalRecordId
ORDER BY UploadedAt DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<ClinicalAttachment>();
                    while (reader.Read())
                        list.Add(DbMapper<ClinicalAttachment>.Map(reader));
                    return list;
                },
                SqlParameterFactory.Create("@MedicalRecordId", medicalRecordId)
            );
        }

        // =========================
        // GET BY PRESCRIPTION
        // =========================
        public static IEnumerable<ClinicalAttachment> GetByPrescriptionId(int prescriptionId)
        {
            string query = $@"
SELECT {Columns}
FROM ClinicalAttachments
WHERE PrescriptionId = @PrescriptionId
ORDER BY UploadedAt DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<ClinicalAttachment>();
                    while (reader.Read())
                        list.Add(DbMapper<ClinicalAttachment>.Map(reader));
                    return list;
                },
                SqlParameterFactory.Create("@PrescriptionId", prescriptionId)
            );
        }

        // =========================
        // GET BY DIAGNOSTIC REQUEST
        // =========================
        public static IEnumerable<ClinicalAttachment> GetByDiagnosticRequestId(int diagnosticRequestId)
        {
            string query = $@"
SELECT {Columns}
FROM ClinicalAttachments
WHERE DiagnosticRequestId = @DiagnosticRequestId
ORDER BY UploadedAt DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<ClinicalAttachment>();
                    while (reader.Read())
                        list.Add(DbMapper<ClinicalAttachment>.Map(reader));
                    return list;
                },
                SqlParameterFactory.Create("@DiagnosticRequestId", diagnosticRequestId)
            );
        }

        // =========================
        // INSERT
        // =========================
        public static int Insert(ClinicalAttachment a)
        {
            if (string.IsNullOrWhiteSpace(a.FileName))
                throw new ArgumentException("FileName is required.");

            if (string.IsNullOrWhiteSpace(a.StoredFileName))
                throw new ArgumentException("StoredFileName is required.");

            // Match CK_ClinicalAttachments_Link
            if (a.AppointmentId == null &&
                a.MedicalRecordId == null &&
                a.PrescriptionId == null &&
                a.DiagnosticRequestId == null)
            {
                throw new ArgumentException("Attachment must be linked to at least one clinical entity.");
            }

            string query = @"
INSERT INTO ClinicalAttachments
(
    AppointmentId,
    MedicalRecordId,
    PrescriptionId,
    DiagnosticRequestId,
    FileName,
    StoredFileName,
    MimeType,
    FileSizeBytes,
    UploadedAt
)
VALUES
(
    @AppointmentId,
    @MedicalRecordId,
    @PrescriptionId,
    @DiagnosticRequestId,
    @FileName,
    @StoredFileName,
    @MimeType,
    @FileSizeBytes,
    SYSUTCDATETIME()
);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),

                SqlParameterFactory.Create("@AppointmentId", (object?)a.AppointmentId ?? DBNull.Value),
                SqlParameterFactory.Create("@MedicalRecordId", (object?)a.MedicalRecordId ?? DBNull.Value),
                SqlParameterFactory.Create("@PrescriptionId", (object?)a.PrescriptionId ?? DBNull.Value),
                SqlParameterFactory.Create("@DiagnosticRequestId", (object?)a.DiagnosticRequestId ?? DBNull.Value),

                SqlParameterFactory.Create("@FileName", a.FileName, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@StoredFileName", a.StoredFileName, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@MimeType", (object?)a.MimeType ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@FileSizeBytes", (object?)a.FileSizeBytes ?? DBNull.Value, SqlDbType.BigInt)
            );
        }

        // =========================
        // DELETE
        // =========================
        public static bool Delete(int id)
        {
            string query = @"
DELETE FROM ClinicalAttachments
WHERE AttachmentId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", id)
            );
        }
    }


}
