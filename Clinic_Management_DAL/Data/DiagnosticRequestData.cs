using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities;
using Clinic_Management_Entities.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class DiagnosticRequestData
    {
        private const string Columns = @"
        DiagnosticRequestId,
        AppointmentId,
        PatientId,
        DoctorId,
        RequestedAt,
        ClinicalInfo,
        Priority,
        Status";

        // =========================
        // GET BY ID
        // =========================
        public static DiagnosticRequest? GetById(int id)
        {
            string query = $@"
SELECT {Columns}
FROM DiagnosticRequests
WHERE DiagnosticRequestId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<DiagnosticRequest>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // =========================
        // GET BY APPOINTMENT
        // =========================
        public static IEnumerable<DiagnosticRequest> GetByAppointmentId(int appointmentId)
        {
            string query = $@"
SELECT {Columns}
FROM DiagnosticRequests
WHERE AppointmentId = @AppointmentId
ORDER BY RequestedAt DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<DiagnosticRequest>();
                    while (reader.Read())
                        list.Add(DbMapper<DiagnosticRequest>.Map(reader));
                    return list;
                },
                SqlParameterFactory.Create("@AppointmentId", appointmentId)
            );
        }

        // =========================
        // GET BY PATIENT (optional range)
        // =========================
        public static IEnumerable<DiagnosticRequest> GetByPatientId(
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
                range += " AND RequestedAt >= @From";
                ps.Add(SqlParameterFactory.Create("@From", from.Value, SqlDbType.DateTime2));
            }
            if (to.HasValue)
            {
                range += " AND RequestedAt < @To";
                ps.Add(SqlParameterFactory.Create("@To", to.Value, SqlDbType.DateTime2));
            }

            string query = $@"
SELECT {Columns}
FROM DiagnosticRequests
WHERE PatientId = @PatientId
{range}
ORDER BY RequestedAt DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<DiagnosticRequest>();
                    while (reader.Read())
                        list.Add(DbMapper<DiagnosticRequest>.Map(reader));
                    return list;
                },
                ps.ToArray()
            );
        }

        // =========================
        // GET BY DOCTOR (optional range)
        // =========================
        public static IEnumerable<DiagnosticRequest> GetByDoctorId(
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
                range += " AND RequestedAt >= @From";
                ps.Add(SqlParameterFactory.Create("@From", from.Value, SqlDbType.DateTime2));
            }
            if (to.HasValue)
            {
                range += " AND RequestedAt < @To";
                ps.Add(SqlParameterFactory.Create("@To", to.Value, SqlDbType.DateTime2));
            }

            string query = $@"
SELECT {Columns}
FROM DiagnosticRequests
WHERE DoctorId = @DoctorId
{range}
ORDER BY RequestedAt DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<DiagnosticRequest>();
                    while (reader.Read())
                        list.Add(DbMapper<DiagnosticRequest>.Map(reader));
                    return list;
                },
                ps.ToArray()
            );
        }

        // =========================
        // GET ALL
        // =========================
        public static IEnumerable<DiagnosticRequest> GetAll()
        {
            string query = $@"
SELECT {Columns}
FROM DiagnosticRequests
ORDER BY RequestedAt DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<DiagnosticRequest>();
                    while (reader.Read())
                        list.Add(DbMapper<DiagnosticRequest>.Map(reader));
                    return list;
                }
            );
        }

        // =========================
        // INSERT (returns new DiagnosticRequestId)
        // =========================
        public static int Insert(DiagnosticRequest r)
        {
            if (r.AppointmentId <= 0) throw new ArgumentOutOfRangeException(nameof(r.AppointmentId));
            if (r.PatientId <= 0) throw new ArgumentOutOfRangeException(nameof(r.PatientId));
            if (r.DoctorId <= 0) throw new ArgumentOutOfRangeException(nameof(r.DoctorId));

            string query = @"
INSERT INTO DiagnosticRequests
(
    AppointmentId,
    PatientId,
    DoctorId,
    RequestedAt,
    ClinicalInfo,
    Priority,
    Status
)
VALUES
(
    @AppointmentId,
    @PatientId,
    @DoctorId,
    SYSUTCDATETIME(),
    @ClinicalInfo,
    @Priority,
    @Status
);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),
                SqlParameterFactory.Create("@AppointmentId", r.AppointmentId),
                SqlParameterFactory.Create("@PatientId", r.PatientId),
                SqlParameterFactory.Create("@DoctorId", r.DoctorId),
                SqlParameterFactory.Create("@ClinicalInfo", (object?)r.ClinicalInfo ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Priority", r.Priority, SqlDbType.TinyInt),
                SqlParameterFactory.Create("@Status", r.Status, SqlDbType.TinyInt)
            );
        }

        // =========================
        // UPDATE (general)
        // =========================
        public static bool Update(DiagnosticRequest r)
        {
            if (r.DiagnosticRequestId <= 0) throw new ArgumentOutOfRangeException(nameof(r.DiagnosticRequestId));
            if (r.AppointmentId <= 0) throw new ArgumentOutOfRangeException(nameof(r.AppointmentId));
            if (r.PatientId <= 0) throw new ArgumentOutOfRangeException(nameof(r.PatientId));
            if (r.DoctorId <= 0) throw new ArgumentOutOfRangeException(nameof(r.DoctorId));

            string query = @"
UPDATE DiagnosticRequests SET
    AppointmentId = @AppointmentId,
    PatientId = @PatientId,
    DoctorId = @DoctorId,
    ClinicalInfo = @ClinicalInfo,
    Priority = @Priority,
    Status = @Status
WHERE DiagnosticRequestId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", r.DiagnosticRequestId),
                SqlParameterFactory.Create("@AppointmentId", r.AppointmentId),
                SqlParameterFactory.Create("@PatientId", r.PatientId),
                SqlParameterFactory.Create("@DoctorId", r.DoctorId),
                SqlParameterFactory.Create("@ClinicalInfo", (object?)r.ClinicalInfo ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Priority", r.Priority, SqlDbType.TinyInt),
                SqlParameterFactory.Create("@Status", r.Status, SqlDbType.TinyInt)
            );
        }

        // =========================
        // QUICK STATUS UPDATE
        // =========================
        public static bool SetStatus(int diagnosticRequestId, byte status)
        {
            string query = @"
UPDATE DiagnosticRequests SET
    Status = @Status
WHERE DiagnosticRequestId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", diagnosticRequestId),
                SqlParameterFactory.Create("@Status", status, SqlDbType.TinyInt)
            );
        }

        // =========================
        // DELETE (hard delete - optional)
        // =========================
        public static bool Delete(int id)
        {
            string query = @"
DELETE FROM DiagnosticRequests
WHERE DiagnosticRequestId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // =========================
        // GET ITEMS BY REQUEST (VIEW)
        // =========================
        public static IEnumerable<
            Clinic_Management_Entities.Entities.DiagnosticRequestItemDetail> GetDetailsByRequestId(int diagnosticRequestId)
        {
            const string query = @"
SELECT
    DiagnosticRequestItemId,
    DiagnosticRequestId,
    DiagnosticTestId,
    Name,
    Notes
FROM dbo.vDiagnosticRequestItemsDetailed
WHERE DiagnosticRequestId = @DiagnosticRequestId
ORDER BY Name;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<DiagnosticRequestItemDetail>();

                    while (reader.Read())
                        list.Add(DbMapper<DiagnosticRequestItemDetail>.Map(reader));

                    return list;
                },
                SqlParameterFactory.Create("@DiagnosticRequestId", diagnosticRequestId)
            );
        }


        public static IEnumerable<DiagnosticRequestDetail>
     GetRequestsByAppointmentId(int appointmentId)
        {
            const string query = @"
SELECT
    DiagnosticRequestId,
    AppointmentId,
    RequestedAt,
    Priority,
    Status,
    ClinicalInfo,
    PatientName,
    DoctorName
FROM dbo.fnDiagnosticRequestsByAppointmentID(@AppointmentId)
ORDER BY RequestedAt DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<DiagnosticRequestDetail>();

                    while (reader.Read())
                        list.Add(DbMapper<DiagnosticRequestDetail>.Map(reader));

                    return list;
                },
                SqlParameterFactory.Create("@AppointmentId", appointmentId)
            );
        }



        public static IEnumerable<DiagnosticRequestDetail> GetAllDetails()
        {
            const string query = @"
SELECT
    DiagnosticRequestId,
    AppointmentId,
    RequestedAt,
    Priority,
    Status,
    ClinicalInfo,
    PatientName,
    PatientPhone,
    DoctorName,
    SpecialtyId,
    ConsultationFee
FROM dbo.vwDiagnosticRequestDetails";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<DiagnosticRequestDetail>();

                    while (reader.Read())
                        list.Add(DbMapper<DiagnosticRequestDetail>.Map(reader));

                    return list;
                }
            );
        }


    }


}
