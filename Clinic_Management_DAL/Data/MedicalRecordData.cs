using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class MedicalRecordData
    {
        private const string Columns = @"
        MedicalRecordId,
        AppointmentId,
        PatientId,
        DoctorId,
        ChiefComplaint,
        HistoryOfPresentIllness,
        Examination,
        Diagnosis,
        Notes,
        CreatedAt,
        UpdatedAt";

        // =========================
        // GET BY ID
        // =========================
        public static MedicalRecord? GetById(int medicalRecordId)
        {
            string query = $@"
SELECT {Columns}
FROM MedicalRecords
WHERE MedicalRecordId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<MedicalRecord>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", medicalRecordId)
            );
        }

        // =========================
        // GET BY APPOINTMENT (1:1 usually)
        // =========================
        public static MedicalRecord? GetByAppointmentId(int appointmentId)
        {
            string query = $@"
SELECT {Columns}
FROM MedicalRecords
WHERE AppointmentId = @AppointmentId;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<MedicalRecord>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@AppointmentId", appointmentId)
            );
        }

        // =========================
        // GET BY PATIENT (list)
        // =========================
        public static IEnumerable<MedicalRecord> GetByPatientId(int patientId, DateTime? from = null, DateTime? to = null)
        {
            var ps = new List<SqlParameter>
        {
            SqlParameterFactory.Create("@PatientId", patientId)
        };

            string range = "";
            if (from.HasValue)
            {
                range += " AND CreatedAt >= @From";
                ps.Add(SqlParameterFactory.Create("@From", from.Value, SqlDbType.DateTime2));
            }
            if (to.HasValue)
            {
                range += " AND CreatedAt < @To";
                ps.Add(SqlParameterFactory.Create("@To", to.Value, SqlDbType.DateTime2));
            }

            string query = $@"
SELECT {Columns}
FROM MedicalRecords
WHERE PatientId = @PatientId
{range}
ORDER BY CreatedAt DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<MedicalRecord>();
                    while (reader.Read())
                        list.Add(DbMapper<MedicalRecord>.Map(reader));
                    return list;
                },
                ps.ToArray()
            );
        }

        // =========================
        // GET BY DOCTOR (list)
        // =========================
        public static IEnumerable<MedicalRecord> GetByDoctorId(int doctorId, DateTime? from = null, DateTime? to = null)
        {
            var ps = new List<SqlParameter>
        {
            SqlParameterFactory.Create("@DoctorId", doctorId)
        };

            string range = "";
            if (from.HasValue)
            {
                range += " AND CreatedAt >= @From";
                ps.Add(SqlParameterFactory.Create("@From", from.Value, SqlDbType.DateTime2));
            }
            if (to.HasValue)
            {
                range += " AND CreatedAt < @To";
                ps.Add(SqlParameterFactory.Create("@To", to.Value, SqlDbType.DateTime2));
            }

            string query = $@"
SELECT {Columns}
FROM MedicalRecords
WHERE DoctorId = @DoctorId
{range}
ORDER BY CreatedAt DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<MedicalRecord>();
                    while (reader.Read())
                        list.Add(DbMapper<MedicalRecord>.Map(reader));
                    return list;
                },
                ps.ToArray()
            );
        }

        // =========================
        // GET ALL
        // =========================
        public static IEnumerable<MedicalRecord> GetAll()
        {
            string query = $@"
SELECT {Columns}
FROM MedicalRecords
ORDER BY CreatedAt DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<MedicalRecord>();
                    while (reader.Read())
                        list.Add(DbMapper<MedicalRecord>.Map(reader));
                    return list;
                }
            );
        }

        // =========================
        // INSERT (returns new MedicalRecordId)
        // =========================
        public static int Insert(MedicalRecord r)
        {
            if (r.AppointmentId <= 0) throw new ArgumentOutOfRangeException(nameof(r.AppointmentId));
            if (r.PatientId <= 0) throw new ArgumentOutOfRangeException(nameof(r.PatientId));
            if (r.DoctorId <= 0) throw new ArgumentOutOfRangeException(nameof(r.DoctorId));

            string query = @"
INSERT INTO MedicalRecords
(
    AppointmentId,
    PatientId,
    DoctorId,
    ChiefComplaint,
    HistoryOfPresentIllness,
    Examination,
    Diagnosis,
    Notes,
    CreatedAt,
    UpdatedAt
)
VALUES
(
    @AppointmentId,
    @PatientId,
    @DoctorId,
    @ChiefComplaint,
    @HistoryOfPresentIllness,
    @Examination,
    @Diagnosis,
    @Notes,
    SYSUTCDATETIME(),
    NULL
);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),
                SqlParameterFactory.Create("@AppointmentId", r.AppointmentId),
                SqlParameterFactory.Create("@PatientId", r.PatientId),
                SqlParameterFactory.Create("@DoctorId", r.DoctorId),

                SqlParameterFactory.Create("@ChiefComplaint", (object?)r.ChiefComplaint ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@HistoryOfPresentIllness", (object?)r.HistoryOfPresentIllness ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Examination", (object?)r.Examination ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Diagnosis", (object?)r.Diagnosis ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Notes", (object?)r.Notes ?? DBNull.Value, SqlDbType.NVarChar)
            );
        }

        // =========================
        // UPDATE (general update)
        // =========================
        public static bool Update(MedicalRecord r)
        {
            if (r.MedicalRecordId <= 0) throw new ArgumentOutOfRangeException(nameof(r.MedicalRecordId));
            if (r.AppointmentId <= 0) throw new ArgumentOutOfRangeException(nameof(r.AppointmentId));
            if (r.PatientId <= 0) throw new ArgumentOutOfRangeException(nameof(r.PatientId));
            if (r.DoctorId <= 0) throw new ArgumentOutOfRangeException(nameof(r.DoctorId));

            string query = @"
UPDATE MedicalRecords SET
    AppointmentId = @AppointmentId,
    PatientId = @PatientId,
    DoctorId = @DoctorId,

    ChiefComplaint = @ChiefComplaint,
    HistoryOfPresentIllness = @HistoryOfPresentIllness,
    Examination = @Examination,
    Diagnosis = @Diagnosis,
    Notes = @Notes,

    UpdatedAt = SYSUTCDATETIME()
WHERE MedicalRecordId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", r.MedicalRecordId),

                SqlParameterFactory.Create("@AppointmentId", r.AppointmentId),
                SqlParameterFactory.Create("@PatientId", r.PatientId),
                SqlParameterFactory.Create("@DoctorId", r.DoctorId),

                SqlParameterFactory.Create("@ChiefComplaint", (object?)r.ChiefComplaint ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@HistoryOfPresentIllness", (object?)r.HistoryOfPresentIllness ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Examination", (object?)r.Examination ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Diagnosis", (object?)r.Diagnosis ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Notes", (object?)r.Notes ?? DBNull.Value, SqlDbType.NVarChar)
            );
        }

        // =========================
        // DELETE (hard delete - optional)
        // =========================
        public static bool Delete(int medicalRecordId)
        {
            string query = @"
DELETE FROM MedicalRecords
WHERE MedicalRecordId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", medicalRecordId)
            );
        }

        // =========================
        // EXISTS (helper)
        // =========================
        public static bool ExistsByAppointmentId(int appointmentId, int? ignoreMedicalRecordId = null)
        {
            string where = @"
AppointmentId = @AppointmentId
" + (ignoreMedicalRecordId.HasValue ? "AND MedicalRecordId <> @IgnoreId" : "");

            string query = $@"
SELECT 1
FROM MedicalRecords
WHERE {where};";

            var ps = new List<SqlParameter>
        {
            SqlParameterFactory.Create("@AppointmentId", appointmentId)
        };

            if (ignoreMedicalRecordId.HasValue)
                ps.Add(SqlParameterFactory.Create("@IgnoreId", ignoreMedicalRecordId.Value));

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read();
                },
                ps.ToArray()
            );
        }
    }


}
