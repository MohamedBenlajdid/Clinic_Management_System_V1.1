using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Clinic_Management_BLL.Data
{
    public static class AppointmentData
    {
        private const string Columns = @"
        AppointmentId,PatientId,DoctorId,StartAt,EndAt,Status,Reason,
        CreatedAt,UpdatedAt,CreatedByUserId,UpdatedByUserId,
        CancelReason,CancelledAt,IsDeleted";

        // =========================
        // GET BY ID (not deleted)
        // =========================
        public static Appointment? GetById(int appointmentId)
        {
            string query = $@"
SELECT {Columns}
FROM Appointments
WHERE AppointmentId = @Id
  AND IsDeleted = 0;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Appointment>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", appointmentId)
            );
        }

        // =========================
        // GET ALL (not deleted)
        // =========================
        public static IEnumerable<Appointment> GetAll()
        {
            string query = $@"
SELECT {Columns}
FROM Appointments
WHERE IsDeleted = 0
ORDER BY StartAt DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Appointment>();
                    while (reader.Read())
                        list.Add(DbMapper<Appointment>.Map(reader));
                    return list;
                }
            );
        }

        // =========================
        // GET BY DOCTOR (optional range)
        // =========================
        public static IEnumerable<Appointment> GetByDoctorId(
            int doctorId,
            DateTime? from = null,
            DateTime? to = null,
            bool includeDeleted = false)
        {
            string deleted = includeDeleted ? "" : "AND IsDeleted = 0";

            var ps = new List<SqlParameter>
        {
            SqlParameterFactory.Create("@DoctorId", doctorId)
        };

            string range = "";
            if (from.HasValue)
            {
                range += " AND StartAt >= @From";
                ps.Add(SqlParameterFactory.Create("@From", from.Value, SqlDbType.DateTime2));
            }
            if (to.HasValue)
            {
                range += " AND StartAt < @To";
                ps.Add(SqlParameterFactory.Create("@To", to.Value, SqlDbType.DateTime2));
            }

            string query = $@"
SELECT {Columns}
FROM Appointments
WHERE DoctorId = @DoctorId
{deleted}
{range}
ORDER BY StartAt;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Appointment>();
                    while (reader.Read())
                        list.Add(DbMapper<Appointment>.Map(reader));
                    return list;
                },
                ps.ToArray()
            );
        }

        // =========================
        // GET BY PATIENT (optional range)
        // =========================
        public static IEnumerable<Appointment> GetByPatientId(
            int patientId,
            DateTime? from = null,
            DateTime? to = null,
            bool includeDeleted = false)
        {
            string deleted = includeDeleted ? "" : "AND IsDeleted = 0";

            var ps = new List<SqlParameter>
        {
            SqlParameterFactory.Create("@PatientId", patientId)
        };

            string range = "";
            if (from.HasValue)
            {
                range += " AND StartAt >= @From";
                ps.Add(SqlParameterFactory.Create("@From", from.Value, SqlDbType.DateTime2));
            }
            if (to.HasValue)
            {
                range += " AND StartAt < @To";
                ps.Add(SqlParameterFactory.Create("@To", to.Value, SqlDbType.DateTime2));
            }

            string query = $@"
SELECT {Columns}
FROM Appointments
WHERE PatientId = @PatientId
{deleted}
{range}
ORDER BY StartAt DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Appointment>();
                    while (reader.Read())
                        list.Add(DbMapper<Appointment>.Map(reader));
                    return list;
                },
                ps.ToArray()
            );
        }

        // =========================
        // INSERT (returns new AppointmentId)
        // =========================
        public static int Insert(Appointment a)
        {
            if (a.EndAt <= a.StartAt)
                throw new ArgumentException("EndAt must be greater than StartAt.");

            string query = @"
INSERT INTO Appointments
(
    PatientId,DoctorId,StartAt,EndAt,Status,Reason,
    CreatedAt,UpdatedAt,CreatedByUserId,UpdatedByUserId,
    CancelReason,CancelledAt,IsDeleted
)
VALUES
(
    @PatientId,@DoctorId,@StartAt,@EndAt,@Status,@Reason,
    SYSUTCDATETIME(),NULL,@CreatedByUserId,NULL,
    NULL,NULL,0
);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),
                SqlParameterFactory.Create("@PatientId", a.PatientId),
                SqlParameterFactory.Create("@DoctorId", a.DoctorId),
                SqlParameterFactory.Create("@StartAt", a.StartAt, SqlDbType.DateTime2),
                SqlParameterFactory.Create("@EndAt", a.EndAt, SqlDbType.DateTime2),
                SqlParameterFactory.Create("@Status", a.Status, SqlDbType.TinyInt),
                SqlParameterFactory.Create("@Reason", (object?)a.Reason ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@CreatedByUserId", (object?)a.CreatedByUserId ?? DBNull.Value, SqlDbType.Int)
            );
        }

        // =========================
        // UPDATE (general update)
        // =========================
        public static bool Update(Appointment a)
        {
            if (a.AppointmentId <= 0) throw new ArgumentOutOfRangeException(nameof(a.AppointmentId));
            if (a.EndAt <= a.StartAt) throw new ArgumentException("EndAt must be greater than StartAt.");

            string query = @"
UPDATE Appointments SET
    PatientId = @PatientId,
    DoctorId  = @DoctorId,
    StartAt   = @StartAt,
    EndAt     = @EndAt,
    Status    = @Status,
    Reason    = @Reason,
    UpdatedAt = SYSUTCDATETIME(),
    UpdatedByUserId = @UpdatedByUserId
WHERE AppointmentId = @AppointmentId
  AND IsDeleted = 0;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@AppointmentId", a.AppointmentId),
                SqlParameterFactory.Create("@PatientId", a.PatientId),
                SqlParameterFactory.Create("@DoctorId", a.DoctorId),
                SqlParameterFactory.Create("@StartAt", a.StartAt, SqlDbType.DateTime2),
                SqlParameterFactory.Create("@EndAt", a.EndAt, SqlDbType.DateTime2),
                SqlParameterFactory.Create("@Status", a.Status, SqlDbType.TinyInt),
                SqlParameterFactory.Create("@Reason", (object?)a.Reason ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@UpdatedByUserId", (object?)a.UpdatedByUserId ?? DBNull.Value, SqlDbType.Int)
            );
        }

        // =========================
        // CANCEL (common operation)
        // =========================
        public static bool Cancel(int appointmentId, string? cancelReason, int? cancelledByUserId)
        {
            string query = @"
UPDATE Appointments SET
    Status = @CancelledStatus,
    CancelReason = @CancelReason,
    CancelledAt = SYSUTCDATETIME(),
    UpdatedAt = SYSUTCDATETIME(),
    UpdatedByUserId = @CancelledByUserId
WHERE AppointmentId = @Id
  AND IsDeleted = 0;";

            // NOTE: choose your status code for Cancelled (example: 3)
            const byte CancelledStatus = 4;

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", appointmentId),
                SqlParameterFactory.Create("@CancelledStatus", CancelledStatus, SqlDbType.TinyInt),
                SqlParameterFactory.Create("@CancelReason", (object?)cancelReason ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@CancelledByUserId", (object?)cancelledByUserId ?? DBNull.Value, SqlDbType.Int)
            );
        }

        // =========================
        // SOFT DELETE
        // =========================
        public static bool SoftDelete(int appointmentId, int? deletedByUserId = null)
        {
            string query = @"
UPDATE Appointments SET
    IsDeleted = 1,
    UpdatedAt = SYSUTCDATETIME(),
    UpdatedByUserId = @DeletedByUserId
WHERE AppointmentId = @Id
  AND IsDeleted = 0;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", appointmentId),
                SqlParameterFactory.Create("@DeletedByUserId", (object?)deletedByUserId ?? DBNull.Value, SqlDbType.Int)
            );
        }

        // =========================
        // EXISTS HELPER (typed)
        // =========================
        private static bool Exists(string whereSql, params SqlParameter[] parameters)
        {
            string query = $@"
SELECT 1
FROM Appointments
WHERE {whereSql};";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read();
                },
                parameters
            );
        }

        // =========================
        // OVERLAP CHECK (Doctor)
        // Prevent double-booking for same doctor
        // =========================
        public static bool IsDoctorSlotTaken(
            int doctorId,
            DateTime startAt,
            DateTime endAt,
            int? ignoreAppointmentId = null)
        {
            if (endAt <= startAt)
                throw new ArgumentException("EndAt must be greater than StartAt.");

            // Overlap: newStart < existingEnd AND newEnd > existingStart
            string where = @"
DoctorId = @DoctorId
AND IsDeleted = 0
AND Status <> @CancelledStatus
AND (@StartAt < EndAt AND @EndAt > StartAt)
" + (ignoreAppointmentId.HasValue ? "AND AppointmentId <> @IgnoreId" : "");

            const byte CancelledStatus = 3;

            var ps = new List<SqlParameter>
        {
            SqlParameterFactory.Create("@DoctorId", doctorId),
            SqlParameterFactory.Create("@StartAt", startAt, SqlDbType.DateTime2),
            SqlParameterFactory.Create("@EndAt", endAt, SqlDbType.DateTime2),
            SqlParameterFactory.Create("@CancelledStatus", CancelledStatus, SqlDbType.TinyInt),
        };

            if (ignoreAppointmentId.HasValue)
                ps.Add(SqlParameterFactory.Create("@IgnoreId", ignoreAppointmentId.Value));

            return Exists(where, ps.ToArray());
        }

        // =========================
        // OVERLAP CHECK (Patient)
        // Prevent patient from having two appointments at same time
        // =========================
        public static bool IsPatientSlotTaken(
            int patientId,
            DateTime startAt,
            DateTime endAt,
            int? ignoreAppointmentId = null)
        {
            if (endAt <= startAt)
                throw new ArgumentException("EndAt must be greater than StartAt.");

            string where = @"
PatientId = @PatientId
AND IsDeleted = 0
AND Status <> @CancelledStatus
AND (@StartAt < EndAt AND @EndAt > StartAt)
" + (ignoreAppointmentId.HasValue ? "AND AppointmentId <> @IgnoreId" : "");

            const byte CancelledStatus = 3;

            var ps = new List<SqlParameter>
        {
            SqlParameterFactory.Create("@PatientId", patientId),
            SqlParameterFactory.Create("@StartAt", startAt, SqlDbType.DateTime2),
            SqlParameterFactory.Create("@EndAt", endAt, SqlDbType.DateTime2),
            SqlParameterFactory.Create("@CancelledStatus", CancelledStatus, SqlDbType.TinyInt),
        };

            if (ignoreAppointmentId.HasValue)
                ps.Add(SqlParameterFactory.Create("@IgnoreId", ignoreAppointmentId.Value));

            return Exists(where, ps.ToArray());
        }

        // =========================
        // Quick status update
        // =========================
        public static bool SetStatus(int appointmentId, byte status, int? updatedByUserId = null)
        {
            string query = @"
UPDATE Appointments SET
    Status = @Status,
    UpdatedAt = SYSUTCDATETIME(),
    UpdatedByUserId = @UpdatedByUserId
WHERE AppointmentId = @Id
  AND IsDeleted = 0;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", appointmentId),
                SqlParameterFactory.Create("@Status", status, SqlDbType.TinyInt),
                SqlParameterFactory.Create("@UpdatedByUserId", (object?)updatedByUserId ?? DBNull.Value, SqlDbType.Int)
            );
        }
    }


}
