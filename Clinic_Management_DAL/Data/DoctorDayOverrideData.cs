using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class DoctorDayOverrideData
    {
        private const string Columns = @"
        OverrideId,DoctorId,[Date],IsOverride,IsDayOff,Notes,CreatedAt,UpdatedAt";

        // =========================
        // GET BY ID
        // =========================
        public static DoctorDayOverride? GetById(int overrideId)
        {
            string query = $@"
SELECT {Columns}
FROM DoctorDayOverrides
WHERE OverrideId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<DoctorDayOverride>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", overrideId)
            );
        }

        // =========================
        // GET BY DOCTOR + DATE (Most used)
        // =========================
        public static DoctorDayOverride? GetByDoctorAndDate(int doctorId, DateTime date)
        {
            string query = $@"
SELECT {Columns}
FROM DoctorDayOverrides
WHERE DoctorId = @DoctorId
  AND [Date] = @Date;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<DoctorDayOverride>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@DoctorId", doctorId),
                SqlParameterFactory.Create("@Date", date.Date, SqlDbType.Date)
            );
        }

        // =========================
        // GET ALL
        // =========================
        public static IEnumerable<DoctorDayOverride> GetAll()
        {
            string query = $@"
SELECT {Columns}
FROM DoctorDayOverrides
ORDER BY [Date] DESC, DoctorId;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<DoctorDayOverride>();
                    while (reader.Read())
                        list.Add(DbMapper<DoctorDayOverride>.Map(reader));
                    return list;
                }
            );
        }

        // =========================
        // GET BY DOCTOR (optionally date range)
        // =========================
        public static IEnumerable<DoctorDayOverride> GetByDoctorId(
            int doctorId,
            DateTime? from = null,
            DateTime? to = null)
        {
            string range = "";
            var ps = new List<SqlParameter>
        {
            SqlParameterFactory.Create("@DoctorId", doctorId)
        };

            if (from.HasValue)
            {
                range += " AND [Date] >= @From";
                ps.Add(SqlParameterFactory.Create("@From", from.Value.Date, SqlDbType.Date));
            }

            if (to.HasValue)
            {
                range += " AND [Date] <= @To";
                ps.Add(SqlParameterFactory.Create("@To", to.Value.Date, SqlDbType.Date));
            }

            string query = $@"
SELECT {Columns}
FROM DoctorDayOverrides
WHERE DoctorId = @DoctorId
{range}
ORDER BY [Date] DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<DoctorDayOverride>();
                    while (reader.Read())
                        list.Add(DbMapper<DoctorDayOverride>.Map(reader));
                    return list;
                },
                ps.ToArray()
            );
        }

        // =========================
        // INSERT (returns new OverrideId)
        // =========================
        public static int Insert(DoctorDayOverride o)
        {
            string query = @"
INSERT INTO DoctorDayOverrides
(
    DoctorId, [Date], IsOverride, IsDayOff, Notes, CreatedAt, UpdatedAt
)
VALUES
(
    @DoctorId, @Date, @IsOverride, @IsDayOff, @Notes, SYSUTCDATETIME(), NULL
);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),
                SqlParameterFactory.Create("@DoctorId", o.DoctorId),
                SqlParameterFactory.Create("@Date", o.Date.Date, SqlDbType.Date),
                SqlParameterFactory.Create("@IsOverride", o.IsOverride),
                SqlParameterFactory.Create("@IsDayOff", o.IsDayOff),
                SqlParameterFactory.Create("@Notes", (object?)o.Notes ?? DBNull.Value, SqlDbType.NVarChar)
            );
        }

        // =========================
        // UPDATE
        // =========================
        public static bool Update(DoctorDayOverride o)
        {
            string query = @"
UPDATE DoctorDayOverrides SET
    DoctorId    = @DoctorId,
    [Date]      = @Date,
    IsOverride  = @IsOverride,
    IsDayOff    = @IsDayOff,
    Notes       = @Notes,
    UpdatedAt   = SYSUTCDATETIME()
WHERE OverrideId = @OverrideId;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@OverrideId", o.OverrideId),
                SqlParameterFactory.Create("@DoctorId", o.DoctorId),
                SqlParameterFactory.Create("@Date", o.Date.Date, SqlDbType.Date),
                SqlParameterFactory.Create("@IsOverride", o.IsOverride),
                SqlParameterFactory.Create("@IsDayOff", o.IsDayOff),
                SqlParameterFactory.Create("@Notes", (object?)o.Notes ?? DBNull.Value, SqlDbType.NVarChar)
            );
        }

        // =========================
        // DELETE (Hard delete)
        // =========================
        public static bool Delete(int overrideId)
        {
            string query = @"DELETE FROM DoctorDayOverrides WHERE OverrideId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", overrideId)
            );
        }

        // =========================
        // EXISTS HELPERS
        // =========================
        private static bool Exists(string whereSql, params SqlParameter[] parameters)
        {
            string query = $@"
SELECT 1
FROM DoctorDayOverrides
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

        // === Unique-like check: only one override per doctor per date
        public static bool IsOverrideExist(int doctorId, DateTime date, int? ignoreOverrideId = null)
        {
            if (doctorId <= 0) throw new ArgumentOutOfRangeException(nameof(doctorId));

            string where = @"
DoctorId = @DoctorId
AND [Date] = @Date
" + (ignoreOverrideId.HasValue ? "AND OverrideId <> @IgnoreId" : "");

            var ps = new List<SqlParameter>
        {
            SqlParameterFactory.Create("@DoctorId", doctorId),
            SqlParameterFactory.Create("@Date", date.Date, SqlDbType.Date),
        };

            if (ignoreOverrideId.HasValue)
                ps.Add(SqlParameterFactory.Create("@IgnoreId", ignoreOverrideId.Value));

            return Exists(where, ps.ToArray());
        }

        // Convenience: does doctor have a day off on date?
        public static bool IsDoctorDayOff(int doctorId, DateTime date)
        {
            return Exists(
                "DoctorId = @DoctorId AND [Date] = @Date AND IsDayOff = 1",
                SqlParameterFactory.Create("@DoctorId", doctorId),
                SqlParameterFactory.Create("@Date", date.Date, SqlDbType.Date)
            );
        }
    }
}