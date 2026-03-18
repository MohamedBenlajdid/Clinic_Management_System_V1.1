using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class MedicalCertificateData
    {
        private const string Columns = @"
        MedicalCertificateId,
        AppointmentId,
        PatientId,
        DoctorId,
        CertificateType,
        StartDate,
        EndDate,
        DiagnosisSummary,
        Notes,
        IssuedAt";

        // =========================
        // GET BY ID
        // =========================
        public static MedicalCertificate? GetById(int id)
        {
            string query = $@"
SELECT {Columns}
FROM MedicalCertificates
WHERE MedicalCertificateId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<MedicalCertificate>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // =========================
        // GET BY APPOINTMENT
        // =========================
        public static IEnumerable<MedicalCertificate> GetByAppointmentId(int appointmentId)
        {
            string query = $@"
SELECT {Columns}
FROM MedicalCertificates
WHERE AppointmentId = @AppointmentId
ORDER BY IssuedAt DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<MedicalCertificate>();
                    while (reader.Read())
                        list.Add(DbMapper<MedicalCertificate>.Map(reader));
                    return list;
                },
                SqlParameterFactory.Create("@AppointmentId", appointmentId)
            );
        }

        // =========================
        // GET BY PATIENT (optional issued range)
        // =========================
        public static IEnumerable<MedicalCertificate> GetByPatientId(
            int patientId,
            DateTime? issuedFrom = null,
            DateTime? issuedTo = null)
        {
            var ps = new List<SqlParameter>
        {
            SqlParameterFactory.Create("@PatientId", patientId)
        };

            string range = "";
            if (issuedFrom.HasValue)
            {
                range += " AND IssuedAt >= @From";
                ps.Add(SqlParameterFactory.Create("@From", issuedFrom.Value, SqlDbType.DateTime2));
            }
            if (issuedTo.HasValue)
            {
                range += " AND IssuedAt < @To";
                ps.Add(SqlParameterFactory.Create("@To", issuedTo.Value, SqlDbType.DateTime2));
            }

            string query = $@"
SELECT {Columns}
FROM MedicalCertificates
WHERE PatientId = @PatientId
{range}
ORDER BY IssuedAt DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<MedicalCertificate>();
                    while (reader.Read())
                        list.Add(DbMapper<MedicalCertificate>.Map(reader));
                    return list;
                },
                ps.ToArray()
            );
        }

        // =========================
        // GET BY DOCTOR (optional issued range)
        // =========================
        public static IEnumerable<MedicalCertificate> GetByDoctorId(
            int doctorId,
            DateTime? issuedFrom = null,
            DateTime? issuedTo = null)
        {
            var ps = new List<SqlParameter>
        {
            SqlParameterFactory.Create("@DoctorId", doctorId)
        };

            string range = "";
            if (issuedFrom.HasValue)
            {
                range += " AND IssuedAt >= @From";
                ps.Add(SqlParameterFactory.Create("@From", issuedFrom.Value, SqlDbType.DateTime2));
            }
            if (issuedTo.HasValue)
            {
                range += " AND IssuedAt < @To";
                ps.Add(SqlParameterFactory.Create("@To", issuedTo.Value, SqlDbType.DateTime2));
            }

            string query = $@"
SELECT {Columns}
FROM MedicalCertificates
WHERE DoctorId = @DoctorId
{range}
ORDER BY IssuedAt DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<MedicalCertificate>();
                    while (reader.Read())
                        list.Add(DbMapper<MedicalCertificate>.Map(reader));
                    return list;
                },
                ps.ToArray()
            );
        }

        // =========================
        // GET ALL
        // =========================
        public static IEnumerable<MedicalCertificate> GetAll()
        {
            string query = $@"
SELECT {Columns}
FROM MedicalCertificates
ORDER BY IssuedAt DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<MedicalCertificate>();
                    while (reader.Read())
                        list.Add(DbMapper<MedicalCertificate>.Map(reader));
                    return list;
                }
            );
        }

        // =========================
        // INSERT (returns new MedicalCertificateId)
        // =========================
        public static int Insert(MedicalCertificate c)
        {
            if (c.AppointmentId <= 0) throw new ArgumentOutOfRangeException(nameof(c.AppointmentId));
            if (c.PatientId <= 0) throw new ArgumentOutOfRangeException(nameof(c.PatientId));
            if (c.DoctorId <= 0) throw new ArgumentOutOfRangeException(nameof(c.DoctorId));

            // match CK_MedicalCertificates_Dates
            if (c.StartDate.Date > c.EndDate.Date)
                throw new ArgumentException("StartDate must be <= EndDate.");

            string query = @"
INSERT INTO MedicalCertificates
(
    AppointmentId,
    PatientId,
    DoctorId,
    CertificateType,
    StartDate,
    EndDate,
    DiagnosisSummary,
    Notes,
    IssuedAt
)
VALUES
(
    @AppointmentId,
    @PatientId,
    @DoctorId,
    @CertificateType,
    @StartDate,
    @EndDate,
    @DiagnosisSummary,
    @Notes,
    SYSUTCDATETIME()
);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),

                SqlParameterFactory.Create("@AppointmentId", c.AppointmentId),
                SqlParameterFactory.Create("@PatientId", c.PatientId),
                SqlParameterFactory.Create("@DoctorId", c.DoctorId),
                SqlParameterFactory.Create("@CertificateType", c.CertificateType, SqlDbType.TinyInt),

                SqlParameterFactory.Create("@StartDate", c.StartDate.Date, SqlDbType.Date),
                SqlParameterFactory.Create("@EndDate", c.EndDate.Date, SqlDbType.Date),

                SqlParameterFactory.Create("@DiagnosisSummary", (object?)c.DiagnosisSummary ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Notes", (object?)c.Notes ?? DBNull.Value, SqlDbType.NVarChar)
            );
        }

        // =========================
        // UPDATE
        // =========================
        public static bool Update(MedicalCertificate c)
        {
            if (c.MedicalCertificateId <= 0) throw new ArgumentOutOfRangeException(nameof(c.MedicalCertificateId));
            if (c.AppointmentId <= 0) throw new ArgumentOutOfRangeException(nameof(c.AppointmentId));
            if (c.PatientId <= 0) throw new ArgumentOutOfRangeException(nameof(c.PatientId));
            if (c.DoctorId <= 0) throw new ArgumentOutOfRangeException(nameof(c.DoctorId));

            if (c.StartDate.Date > c.EndDate.Date)
                throw new ArgumentException("StartDate must be <= EndDate.");

            string query = @"
UPDATE MedicalCertificates SET
    AppointmentId = @AppointmentId,
    PatientId = @PatientId,
    DoctorId = @DoctorId,
    CertificateType = @CertificateType,
    StartDate = @StartDate,
    EndDate = @EndDate,
    DiagnosisSummary = @DiagnosisSummary,
    Notes = @Notes
WHERE MedicalCertificateId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,

                SqlParameterFactory.Create("@Id", c.MedicalCertificateId),

                SqlParameterFactory.Create("@AppointmentId", c.AppointmentId),
                SqlParameterFactory.Create("@PatientId", c.PatientId),
                SqlParameterFactory.Create("@DoctorId", c.DoctorId),
                SqlParameterFactory.Create("@CertificateType", c.CertificateType, SqlDbType.TinyInt),

                SqlParameterFactory.Create("@StartDate", c.StartDate.Date, SqlDbType.Date),
                SqlParameterFactory.Create("@EndDate", c.EndDate.Date, SqlDbType.Date),

                SqlParameterFactory.Create("@DiagnosisSummary", (object?)c.DiagnosisSummary ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Notes", (object?)c.Notes ?? DBNull.Value, SqlDbType.NVarChar)
            );
        }

        // =========================
        // DELETE (hard delete - optional)
        // =========================
        public static bool Delete(int id)
        {
            string query = @"
DELETE FROM MedicalCertificates
WHERE MedicalCertificateId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", id)
            );
        }
    }

}
