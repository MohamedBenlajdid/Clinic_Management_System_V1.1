using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    using Clinic_Management_DAL.Infrastractor;
    using Clinic_Management_Entities.Entities;
    using Microsoft.Data.SqlClient;
    using System;
    using System.Collections.Generic;
    using System.Data;

    // =========================================================
    // DAL: DoctorScheduleData
    // (Mirror your DAL psychology: DbExecutor + SqlParameterFactory + DbMapper)
    // =========================================================
    public static class DoctorScheduleData
    {
        private const string Columns = @"
        ScheduleId,DoctorId,DayOfWeek,StartTime,EndTime,SlotMinutes,IsActive,CreatedAt,UpdatedAt";

        // =========================
        // GET BY ID
        // =========================
        public static DoctorSchedule? GetById(int scheduleId)
        {
            string query = $@"
SELECT {Columns}
FROM DoctorSchedules
WHERE ScheduleId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<DoctorSchedule>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", scheduleId)
            );
        }

        // =========================
        // GET ALL
        // =========================
        public static IEnumerable<DoctorSchedule> GetAll()
        {
            string query = $@"
SELECT {Columns}
FROM DoctorSchedules
ORDER BY DoctorId, DayOfWeek, StartTime;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<DoctorSchedule>();
                    while (reader.Read())
                        list.Add(DbMapper<DoctorSchedule>.Map(reader));
                    return list;
                }
            );
        }

        // =========================
        // GET BY DOCTOR
        // =========================
        public static IEnumerable<DoctorSchedule> GetByDoctorId(int doctorId, bool onlyActive = true)
        {
            string query = $@"
SELECT {Columns}
FROM DoctorSchedules
WHERE DoctorId = @DoctorId
{(onlyActive ? "AND IsActive = 1" : "")}
ORDER BY DayOfWeek, StartTime;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<DoctorSchedule>();
                    while (reader.Read())
                        list.Add(DbMapper<DoctorSchedule>.Map(reader));
                    return list;
                },
                SqlParameterFactory.Create("@DoctorId", doctorId)
            );
        }

        // =========================
        // GET BY DOCTOR + DAY
        // =========================
        public static IEnumerable<DoctorSchedule> GetByDoctorAndDay(int doctorId, byte dayOfWeek, bool onlyActive = true)
        {
            string query = $@"
SELECT {Columns}
FROM DoctorSchedules
WHERE DoctorId = @DoctorId
  AND DayOfWeek = @DayOfWeek
{(onlyActive ? "AND IsActive = 1" : "")}
ORDER BY StartTime;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<DoctorSchedule>();
                    while (reader.Read())
                        list.Add(DbMapper<DoctorSchedule>.Map(reader));
                    return list;
                },
                SqlParameterFactory.Create("@DoctorId", doctorId),
                SqlParameterFactory.Create("@DayOfWeek", dayOfWeek)
            );
        }

        // =========================
        // INSERT
        // =========================
        public static int Insert(DoctorSchedule s)
        {
            // If your table has defaults for CreatedAt, you can omit it in INSERT.
            // But since you SELECT it in your list, keep it consistent.
            string query = @"
INSERT INTO DoctorSchedules
(
    DoctorId, DayOfWeek, StartTime, EndTime, SlotMinutes, IsActive, CreatedAt, UpdatedAt
)
VALUES
(
    @DoctorId, @DayOfWeek, @StartTime, @EndTime, @SlotMinutes, @IsActive, SYSUTCDATETIME(), NULL
);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),
                SqlParameterFactory.Create("@DoctorId", s.DoctorId),
                SqlParameterFactory.Create("@DayOfWeek", s.DayOfWeek),
                SqlParameterFactory.Create("@StartTime", s.StartTime, SqlDbType.Time),
                SqlParameterFactory.Create("@EndTime", s.EndTime, SqlDbType.Time),
                SqlParameterFactory.Create("@SlotMinutes", s.SlotMinutes),
                SqlParameterFactory.Create("@IsActive", s.IsActive)
            );
        }

        // =========================
        // UPDATE
        // =========================
        public static bool Update(DoctorSchedule s)
        {
            string query = @"
UPDATE DoctorSchedules SET
    DoctorId    = @DoctorId,
    DayOfWeek   = @DayOfWeek,
    StartTime   = @StartTime,
    EndTime     = @EndTime,
    SlotMinutes = @SlotMinutes,
    IsActive    = @IsActive,
    UpdatedAt   = SYSUTCDATETIME()
WHERE ScheduleId = @ScheduleId;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@ScheduleId", s.ScheduleId),
                SqlParameterFactory.Create("@DoctorId", s.DoctorId),
                SqlParameterFactory.Create("@DayOfWeek", s.DayOfWeek),
                SqlParameterFactory.Create("@StartTime", s.StartTime, SqlDbType.Time),
                SqlParameterFactory.Create("@EndTime", s.EndTime, SqlDbType.Time),
                SqlParameterFactory.Create("@SlotMinutes", s.SlotMinutes),
                SqlParameterFactory.Create("@IsActive", s.IsActive)
            );
        }

        // =========================
        // DELETE (Hard delete like your DoctorData)
        // =========================
        public static bool Delete(int scheduleId)
        {
            string query = @"DELETE FROM DoctorSchedules WHERE ScheduleId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", scheduleId)
            );
        }

        // =========================
        // EXISTS HELPERS
        // =========================
        private static bool Exists(string whereSql, params SqlParameter[] parameters)
        {
            string query = $@"
SELECT 1
FROM DoctorSchedules
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


        // === Primary existence by ScheduleId ===
        public static bool IsScheduleIdExist(int scheduleId)
        {
            if (scheduleId <= 0) return false;

            return Exists(
                "ScheduleId = @Id",
                SqlParameterFactory.Create("@Id", scheduleId)
            );
        }

        // === Check if a Doctor has ANY schedule rows ===
        public static bool HasAnySchedule(int doctorId)
        {
            if (doctorId <= 0) return false;

            return Exists(
                "DoctorId = @DoctorId",
                SqlParameterFactory.Create("@DoctorId", doctorId)
            );
        }

        // === Check overlap: same Doctor + Day, time intersects another row
        // (Useful to prevent conflicting weekly template sessions)
        public static bool IsOverlapping(
    int doctorId,
    byte dayOfWeek,
    TimeSpan startTime,
    TimeSpan endTime,
    int? ignoreScheduleId = null)
        {
            if (doctorId <= 0)
                throw new ArgumentOutOfRangeException(nameof(doctorId));

            if (dayOfWeek > 6)
                throw new ArgumentOutOfRangeException(nameof(dayOfWeek));

            if (endTime <= startTime)
                throw new ArgumentException("EndTime must be greater than StartTime.");

            string where = @"
DoctorId = @DoctorId
AND DayOfWeek = @DayOfWeek
AND IsActive = 1
AND (@StartTime < EndTime AND @EndTime > StartTime)
" + (ignoreScheduleId.HasValue ? "AND ScheduleId <> @IgnoreId" : "");

            var parameters = new List<SqlParameter>
    {
        SqlParameterFactory.Create("@DoctorId", doctorId),
        SqlParameterFactory.Create("@DayOfWeek", dayOfWeek),
        SqlParameterFactory.Create("@StartTime", startTime, SqlDbType.Time),
        SqlParameterFactory.Create("@EndTime", endTime, SqlDbType.Time)
    };

            if (ignoreScheduleId.HasValue)
            {
                parameters.Add(
                    SqlParameterFactory.Create("@IgnoreId", ignoreScheduleId.Value));
            }

            return Exists(where, parameters.ToArray());
        }


        // === Get by DoctorId + DayOfWeek + exact StartTime (common uniqueness-like lookup)
        public static DoctorSchedule? GetByDoctorDayStart(int doctorId, byte dayOfWeek, TimeSpan startTime)
        {
            string query = $@"
SELECT {Columns}
FROM DoctorSchedules
WHERE DoctorId = @DoctorId
  AND DayOfWeek = @DayOfWeek
  AND StartTime = @StartTime;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<DoctorSchedule>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@DoctorId", doctorId),
                SqlParameterFactory.Create("@DayOfWeek", dayOfWeek),
                SqlParameterFactory.Create("@StartTime", startTime, SqlDbType.Time)
            );
        }

        // === Quick toggle active
        public static bool SetActive(int scheduleId, bool isActive)
        {
            string query = @"
UPDATE DoctorSchedules
SET IsActive = @IsActive,
    UpdatedAt = SYSUTCDATETIME()
WHERE ScheduleId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", scheduleId),
                SqlParameterFactory.Create("@IsActive", isActive)
            );
        }
    }



}
