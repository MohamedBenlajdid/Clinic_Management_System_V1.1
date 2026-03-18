using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class DoctorDayOverrideSessionData
    {
        private const string Columns = @"
        SessionId,OverrideId,StartTime,EndTime,SlotMinutes,CreatedAt";

        // =========================
        // GET BY ID
        // =========================
        public static DoctorDayOverrideSession? GetById(int sessionId)
        {
            string query = $@"
SELECT {Columns}
FROM DoctorDayOverrideSessions
WHERE SessionId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<DoctorDayOverrideSession>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", sessionId)
            );
        }

        // =========================
        // GET BY OVERRIDE
        // =========================
        public static IEnumerable<DoctorDayOverrideSession> GetByOverrideId(int overrideId)
        {
            string query = $@"
SELECT {Columns}
FROM DoctorDayOverrideSessions
WHERE OverrideId = @OverrideId
ORDER BY StartTime;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<DoctorDayOverrideSession>();
                    while (reader.Read())
                        list.Add(DbMapper<DoctorDayOverrideSession>.Map(reader));
                    return list;
                },
                SqlParameterFactory.Create("@OverrideId", overrideId)
            );
        }

        // =========================
        // INSERT
        // =========================
        public static int Insert(DoctorDayOverrideSession s)
        {
            string query = @"
INSERT INTO DoctorDayOverrideSessions
(
    OverrideId, StartTime, EndTime, SlotMinutes, CreatedAt
)
VALUES
(
    @OverrideId, @StartTime, @EndTime, @SlotMinutes, SYSUTCDATETIME()
);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),
                SqlParameterFactory.Create("@OverrideId", s.OverrideId),
                SqlParameterFactory.Create("@StartTime", s.StartTime, SqlDbType.Time),
                SqlParameterFactory.Create("@EndTime", s.EndTime, SqlDbType.Time),
                SqlParameterFactory.Create("@SlotMinutes", s.SlotMinutes)
            );
        }

        // =========================
        // UPDATE
        // =========================
        public static bool Update(DoctorDayOverrideSession s)
        {
            string query = @"
UPDATE DoctorDayOverrideSessions SET
    OverrideId = @OverrideId,
    StartTime = @StartTime,
    EndTime = @EndTime,
    SlotMinutes = @SlotMinutes
WHERE SessionId = @SessionId;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@SessionId", s.SessionId),
                SqlParameterFactory.Create("@OverrideId", s.OverrideId),
                SqlParameterFactory.Create("@StartTime", s.StartTime, SqlDbType.Time),
                SqlParameterFactory.Create("@EndTime", s.EndTime, SqlDbType.Time),
                SqlParameterFactory.Create("@SlotMinutes", s.SlotMinutes)
            );
        }

        // =========================
        // DELETE
        // =========================
        public static bool Delete(int sessionId)
        {
            string query = @"DELETE FROM DoctorDayOverrideSessions WHERE SessionId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", sessionId)
            );
        }

        // =========================
        // EXISTS HELPER
        // =========================
        private static bool Exists(string whereSql, params SqlParameter[] parameters)
        {
            string query = $@"
SELECT 1
FROM DoctorDayOverrideSessions
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
        // CHECK OVERLAP (IMPORTANT)
        // =========================
        public static bool IsOverlapping(
            int overrideId,
            TimeSpan startTime,
            TimeSpan endTime,
            int? ignoreSessionId = null)
        {
            if (endTime <= startTime)
                throw new ArgumentException("EndTime must be greater than StartTime.");

            string where = @"
OverrideId = @OverrideId
AND (@StartTime < EndTime AND @EndTime > StartTime)
" + (ignoreSessionId.HasValue ? "AND SessionId <> @IgnoreId" : "");

            var ps = new List<SqlParameter>
        {
            SqlParameterFactory.Create("@OverrideId", overrideId),
            SqlParameterFactory.Create("@StartTime", startTime, SqlDbType.Time),
            SqlParameterFactory.Create("@EndTime", endTime, SqlDbType.Time),
        };

            if (ignoreSessionId.HasValue)
                ps.Add(SqlParameterFactory.Create("@IgnoreId", ignoreSessionId.Value));

            return Exists(where, ps.ToArray());
        }

        // =========================
        // CHECK EXIST BY OVERRIDE
        // =========================
        public static bool HasAnySession(int overrideId)
        {
            return Exists(
                "OverrideId = @OverrideId",
                SqlParameterFactory.Create("@OverrideId", overrideId)
            );
        }
    }
}
