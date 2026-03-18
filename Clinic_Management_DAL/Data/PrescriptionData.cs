using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class PrescriptionData
    {
        private const string Columns = @"
        PrescriptionId,
        AppointmentId,
        PatientId,
        DoctorId,
        Notes,
        IssuedAt";

        // =========================
        // GET BY ID
        // =========================
        public static Prescription? GetById(int prescriptionId)
        {
            string query = $@"
SELECT {Columns}
FROM Prescriptions
WHERE PrescriptionId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Prescription>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", prescriptionId)
            );
        }

        // =========================
        // GET BY APPOINTMENT (often 1:1 or 1:N)
        // =========================
        public static IEnumerable<Prescription> GetByAppointmentId(int appointmentId)
        {
            string query = $@"
SELECT {Columns}
FROM Prescriptions
WHERE AppointmentId = @AppointmentId
ORDER BY IssuedAt DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Prescription>();
                    while (reader.Read())
                        list.Add(DbMapper<Prescription>.Map(reader));
                    return list;
                },
                SqlParameterFactory.Create("@AppointmentId", appointmentId)
            );
        }

        // =========================
        // GET BY PATIENT
        // =========================
        public static IEnumerable<Prescription> GetByPatientId(
            int patientId,
            DateTime? from = null,
            DateTime? to = null)
        {
            var ps = new List<SqlParameter>
        {
            SqlParameterFactory.Create("@PatientId", patientId)
        };

            string range = "";
            if (from.HasValue)
            {
                range += " AND IssuedAt >= @From";
                ps.Add(SqlParameterFactory.Create("@From", from.Value, SqlDbType.DateTime2));
            }
            if (to.HasValue)
            {
                range += " AND IssuedAt < @To";
                ps.Add(SqlParameterFactory.Create("@To", to.Value, SqlDbType.DateTime2));
            }

            string query = $@"
SELECT {Columns}
FROM Prescriptions
WHERE PatientId = @PatientId
{range}
ORDER BY IssuedAt DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Prescription>();
                    while (reader.Read())
                        list.Add(DbMapper<Prescription>.Map(reader));
                    return list;
                },
                ps.ToArray()
            );
        }

        // =========================
        // GET BY DOCTOR
        // =========================
        public static IEnumerable<Prescription> GetByDoctorId(
            int doctorId,
            DateTime? from = null,
            DateTime? to = null)
        {
            var ps = new List<SqlParameter>
        {
            SqlParameterFactory.Create("@DoctorId", doctorId)
        };

            string range = "";
            if (from.HasValue)
            {
                range += " AND IssuedAt >= @From";
                ps.Add(SqlParameterFactory.Create("@From", from.Value, SqlDbType.DateTime2));
            }
            if (to.HasValue)
            {
                range += " AND IssuedAt < @To";
                ps.Add(SqlParameterFactory.Create("@To", to.Value, SqlDbType.DateTime2));
            }

            string query = $@"
SELECT {Columns}
FROM Prescriptions
WHERE DoctorId = @DoctorId
{range}
ORDER BY IssuedAt DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Prescription>();
                    while (reader.Read())
                        list.Add(DbMapper<Prescription>.Map(reader));
                    return list;
                },
                ps.ToArray()
            );
        }

        // =========================
        // GET ALL
        // =========================
        public static IEnumerable<Prescription> GetAll()
        {
            string query = $@"
SELECT {Columns}
FROM Prescriptions
ORDER BY IssuedAt DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Prescription>();
                    while (reader.Read())
                        list.Add(DbMapper<Prescription>.Map(reader));
                    return list;
                }
            );
        }

        // =========================
        // INSERT (returns new PrescriptionId)
        // =========================
        public static int Insert(Prescription p)
        {
            if (p.AppointmentId <= 0) throw new ArgumentOutOfRangeException(nameof(p.AppointmentId));
            if (p.PatientId <= 0) throw new ArgumentOutOfRangeException(nameof(p.PatientId));
            if (p.DoctorId <= 0) throw new ArgumentOutOfRangeException(nameof(p.DoctorId));

            string query = @"
INSERT INTO Prescriptions
(
    AppointmentId,
    PatientId,
    DoctorId,
    Notes,
    IssuedAt
)
VALUES
(
    @AppointmentId,
    @PatientId,
    @DoctorId,
    @Notes,
    SYSUTCDATETIME()
);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),
                SqlParameterFactory.Create("@AppointmentId", p.AppointmentId),
                SqlParameterFactory.Create("@PatientId", p.PatientId),
                SqlParameterFactory.Create("@DoctorId", p.DoctorId),
                SqlParameterFactory.Create("@Notes", (object?)p.Notes ?? DBNull.Value, SqlDbType.NVarChar)
            );
        }

        // =========================
        // UPDATE (Notes only) + touch IssuedAt? (NO)
        // =========================
        public static bool Update(Prescription p)
        {
            if (p.PrescriptionId <= 0) throw new ArgumentOutOfRangeException(nameof(p.PrescriptionId));
            if (p.AppointmentId <= 0) throw new ArgumentOutOfRangeException(nameof(p.AppointmentId));
            if (p.PatientId <= 0) throw new ArgumentOutOfRangeException(nameof(p.PatientId));
            if (p.DoctorId <= 0) throw new ArgumentOutOfRangeException(nameof(p.DoctorId));

            string query = @"
UPDATE Prescriptions SET
    AppointmentId = @AppointmentId,
    PatientId = @PatientId,
    DoctorId = @DoctorId,
    Notes = @Notes
WHERE PrescriptionId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", p.PrescriptionId),
                SqlParameterFactory.Create("@AppointmentId", p.AppointmentId),
                SqlParameterFactory.Create("@PatientId", p.PatientId),
                SqlParameterFactory.Create("@DoctorId", p.DoctorId),
                SqlParameterFactory.Create("@Notes", (object?)p.Notes ?? DBNull.Value, SqlDbType.NVarChar)
            );
        }

        // =========================
        // DELETE (hard delete - optional)
        // =========================
        public static bool Delete(int prescriptionId)
        {
            string query = @"
DELETE FROM Prescriptions
WHERE PrescriptionId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", prescriptionId)
            );
        }

        // =========================
        // EXISTS BY APPOINTMENT (helper)
        // =========================
        public static bool ExistsByAppointmentId(int appointmentId, int? ignorePrescriptionId = null)
        {
            string where = @"
AppointmentId = @AppointmentId
" + (ignorePrescriptionId.HasValue ? "AND PrescriptionId <> @IgnoreId" : "");

            string query = $@"
SELECT 1
FROM Prescriptions
WHERE {where};";

            var ps = new List<SqlParameter>
        {
            SqlParameterFactory.Create("@AppointmentId", appointmentId)
        };

            if (ignorePrescriptionId.HasValue)
                ps.Add(SqlParameterFactory.Create("@IgnoreId", ignorePrescriptionId.Value));

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
