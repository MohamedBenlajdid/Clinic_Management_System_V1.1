using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class DiagnosticRequestItemData
    {
        private const string Columns = @"
        DiagnosticRequestItemId,
        DiagnosticRequestId,
        DiagnosticTestId,
        Notes";

        // =========================
        // GET BY ID
        // =========================
        public static DiagnosticRequestItem? GetById(int id)
        {
            string query = $@"
SELECT {Columns}
FROM DiagnosticRequestItems
WHERE DiagnosticRequestItemId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<DiagnosticRequestItem>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // =========================
        // GET BY REQUEST
        // =========================
        public static IEnumerable<DiagnosticRequestItem> GetByRequestId(int diagnosticRequestId)
        {
            string query = $@"
SELECT {Columns}
FROM DiagnosticRequestItems
WHERE DiagnosticRequestId = @RequestId
ORDER BY DiagnosticRequestItemId;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<DiagnosticRequestItem>();
                    while (reader.Read())
                        list.Add(DbMapper<DiagnosticRequestItem>.Map(reader));
                    return list;
                },
                SqlParameterFactory.Create("@RequestId", diagnosticRequestId)
            );
        }

        // =========================
        // GET ALL
        // =========================
        public static IEnumerable<DiagnosticRequestItem> GetAll()
        {
            string query = $@"
SELECT {Columns}
FROM DiagnosticRequestItems
ORDER BY DiagnosticRequestItemId DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<DiagnosticRequestItem>();
                    while (reader.Read())
                        list.Add(DbMapper<DiagnosticRequestItem>.Map(reader));
                    return list;
                }
            );
        }

        // =========================
        // INSERT
        // =========================
        public static int Insert(DiagnosticRequestItem item)
        {
            if (item.DiagnosticRequestId <= 0)
                throw new ArgumentOutOfRangeException(nameof(item.DiagnosticRequestId));

            if (item.DiagnosticTestId <= 0)
                throw new ArgumentOutOfRangeException(nameof(item.DiagnosticTestId));

            string query = @"
INSERT INTO DiagnosticRequestItems
(
    DiagnosticRequestId,
    DiagnosticTestId,
    Notes
)
VALUES
(
    @DiagnosticRequestId,
    @DiagnosticTestId,
    @Notes
);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),

                SqlParameterFactory.Create("@DiagnosticRequestId", item.DiagnosticRequestId),
                SqlParameterFactory.Create("@DiagnosticTestId", item.DiagnosticTestId),
                SqlParameterFactory.Create("@Notes", (object?)item.Notes ?? DBNull.Value, SqlDbType.NVarChar)
            );
        }

        // =========================
        // UPDATE
        // =========================
        public static bool Update(DiagnosticRequestItem item)
        {
            if (item.DiagnosticRequestItemId <= 0)
                throw new ArgumentOutOfRangeException(nameof(item.DiagnosticRequestItemId));

            if (item.DiagnosticRequestId <= 0)
                throw new ArgumentOutOfRangeException(nameof(item.DiagnosticRequestId));

            if (item.DiagnosticTestId <= 0)
                throw new ArgumentOutOfRangeException(nameof(item.DiagnosticTestId));

            string query = @"
UPDATE DiagnosticRequestItems SET
    DiagnosticRequestId = @DiagnosticRequestId,
    DiagnosticTestId = @DiagnosticTestId,
    Notes = @Notes
WHERE DiagnosticRequestItemId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,

                SqlParameterFactory.Create("@Id", item.DiagnosticRequestItemId),
                SqlParameterFactory.Create("@DiagnosticRequestId", item.DiagnosticRequestId),
                SqlParameterFactory.Create("@DiagnosticTestId", item.DiagnosticTestId),
                SqlParameterFactory.Create("@Notes", (object?)item.Notes ?? DBNull.Value, SqlDbType.NVarChar)
            );
        }

        // =========================
        // DELETE
        // =========================
        public static bool Delete(int id)
        {
            string query = @"
DELETE FROM DiagnosticRequestItems
WHERE DiagnosticRequestItemId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // =========================
        // DELETE BY REQUEST (bulk delete)
        // =========================
        public static bool DeleteByRequestId(int diagnosticRequestId)
        {
            string query = @"
DELETE FROM DiagnosticRequestItems
WHERE DiagnosticRequestId = @RequestId;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@RequestId", diagnosticRequestId)
            );
        }

        // =========================
        // EXISTS (Prevent duplicate test in same request)
        // =========================
        public static bool ExistsTestInRequest(int diagnosticRequestId, int diagnosticTestId, int? ignoreId = null)
        {
            string where = @"
DiagnosticRequestId = @RequestId
AND DiagnosticTestId = @TestId
" + (ignoreId.HasValue ? "AND DiagnosticRequestItemId <> @IgnoreId" : "");

            string query = $@"
SELECT 1
FROM DiagnosticRequestItems
WHERE {where};";

            var ps = new List<SqlParameter>
        {
            SqlParameterFactory.Create("@RequestId", diagnosticRequestId),
            SqlParameterFactory.Create("@TestId", diagnosticTestId)
        };

            if (ignoreId.HasValue)
                ps.Add(SqlParameterFactory.Create("@IgnoreId", ignoreId.Value));

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
