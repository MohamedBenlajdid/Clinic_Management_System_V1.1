using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class DiagnosticResultData
    {
        private const string Columns = @"
        DiagnosticResultId,
        DiagnosticRequestItemId,
        ResultText,
        ResultNumeric,
        Unit,
        RefRange,
        ReportText,
        PerformedAt,
        VerifiedAt";

        // =========================
        // GET BY ID
        // =========================
        public static DiagnosticResult? GetById(int id)
        {
            string query = $@"
SELECT {Columns}
FROM DiagnosticResults
WHERE DiagnosticResultId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<DiagnosticResult>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // =========================
        // GET BY REQUEST ITEM (usually 1:1)
        // =========================
        public static DiagnosticResult? GetByRequestItemId(int diagnosticRequestItemId)
        {
            string query = $@"
SELECT {Columns}
FROM DiagnosticResults
WHERE DiagnosticRequestItemId = @ItemId;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<DiagnosticResult>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@ItemId", diagnosticRequestItemId)
            );
        }

        // =========================
        // GET ALL
        // =========================
        public static IEnumerable<DiagnosticResult> GetAll()
        {
            string query = $@"
SELECT {Columns}
FROM DiagnosticResults
ORDER BY DiagnosticResultId DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<DiagnosticResult>();
                    while (reader.Read())
                        list.Add(DbMapper<DiagnosticResult>.Map(reader));
                    return list;
                }
            );
        }

        // =========================
        // INSERT (returns new DiagnosticResultId)
        // =========================
        public static int Insert(DiagnosticResult r)
        {
            if (r.DiagnosticRequestItemId <= 0)
                throw new ArgumentOutOfRangeException(nameof(r.DiagnosticRequestItemId));

            // must match CK_DiagnosticResults_AtLeastOne
            if (r.ResultText is null && r.ResultNumeric is null && r.ReportText is null)
                throw new ArgumentException("At least one of ResultText, ResultNumeric, or ReportText must be provided.");

            string query = @"
INSERT INTO DiagnosticResults
(
    DiagnosticRequestItemId,
    ResultText,
    ResultNumeric,
    Unit,
    RefRange,
    ReportText,
    PerformedAt,
    VerifiedAt
)
VALUES
(
    @DiagnosticRequestItemId,
    @ResultText,
    @ResultNumeric,
    @Unit,
    @RefRange,
    @ReportText,
    @PerformedAt,
    @VerifiedAt
);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),

                SqlParameterFactory.Create("@DiagnosticRequestItemId", r.DiagnosticRequestItemId),
                SqlParameterFactory.Create("@ResultText", (object?)r.ResultText ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@ResultNumeric", (object?)r.ResultNumeric ?? DBNull.Value, SqlDbType.Decimal),
                SqlParameterFactory.Create("@Unit", (object?)r.Unit ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@RefRange", (object?)r.RefRange ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@ReportText", (object?)r.ReportText ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@PerformedAt", (object?)r.PerformedAt ?? DBNull.Value, SqlDbType.DateTime2),
                SqlParameterFactory.Create("@VerifiedAt", (object?)r.VerifiedAt ?? DBNull.Value, SqlDbType.DateTime2)
            );
        }

        // =========================
        // UPDATE (general)
        // =========================
        public static bool Update(DiagnosticResult r)
        {
            if (r.DiagnosticResultId <= 0)
                throw new ArgumentOutOfRangeException(nameof(r.DiagnosticResultId));

            if (r.DiagnosticRequestItemId <= 0)
                throw new ArgumentOutOfRangeException(nameof(r.DiagnosticRequestItemId));

            // must match CK_DiagnosticResults_AtLeastOne
            if (r.ResultText is null && r.ResultNumeric is null && r.ReportText is null)
                throw new ArgumentException("At least one of ResultText, ResultNumeric, or ReportText must be provided.");

            string query = @"
UPDATE DiagnosticResults SET
    DiagnosticRequestItemId = @DiagnosticRequestItemId,
    ResultText = @ResultText,
    ResultNumeric = @ResultNumeric,
    Unit = @Unit,
    RefRange = @RefRange,
    ReportText = @ReportText,
    PerformedAt = @PerformedAt,
    VerifiedAt = @VerifiedAt
WHERE DiagnosticResultId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,

                SqlParameterFactory.Create("@Id", r.DiagnosticResultId),
                SqlParameterFactory.Create("@DiagnosticRequestItemId", r.DiagnosticRequestItemId),
                SqlParameterFactory.Create("@ResultText", (object?)r.ResultText ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@ResultNumeric", (object?)r.ResultNumeric ?? DBNull.Value, SqlDbType.Decimal),
                SqlParameterFactory.Create("@Unit", (object?)r.Unit ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@RefRange", (object?)r.RefRange ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@ReportText", (object?)r.ReportText ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@PerformedAt", (object?)r.PerformedAt ?? DBNull.Value, SqlDbType.DateTime2),
                SqlParameterFactory.Create("@VerifiedAt", (object?)r.VerifiedAt ?? DBNull.Value, SqlDbType.DateTime2)
            );
        }

        // =========================
        // QUICK: SET PERFORMED AT
        // =========================
        public static bool SetPerformedAt(int diagnosticResultId, DateTime performedAt)
        {
            string query = @"
UPDATE DiagnosticResults SET
    PerformedAt = @PerformedAt
WHERE DiagnosticResultId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", diagnosticResultId),
                SqlParameterFactory.Create("@PerformedAt", performedAt, SqlDbType.DateTime2)
            );
        }

        // =========================
        // QUICK: SET VERIFIED AT
        // =========================
        public static bool SetVerifiedAt(int diagnosticResultId, DateTime verifiedAt)
        {
            string query = @"
UPDATE DiagnosticResults SET
    VerifiedAt = @VerifiedAt
WHERE DiagnosticResultId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", diagnosticResultId),
                SqlParameterFactory.Create("@VerifiedAt", verifiedAt, SqlDbType.DateTime2)
            );
        }

        // =========================
        // DELETE (hard delete - optional)
        // =========================
        public static bool Delete(int id)
        {
            string query = @"
DELETE FROM DiagnosticResults
WHERE DiagnosticResultId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // =========================
        // EXISTS BY REQUEST ITEM (helper)
        // =========================
        public static bool ExistsByRequestItemId(int diagnosticRequestItemId, int? ignoreResultId = null)
        {
            string where = @"
DiagnosticRequestItemId = @ItemId
" + (ignoreResultId.HasValue ? "AND DiagnosticResultId <> @IgnoreId" : "");

            string query = $@"
SELECT 1
FROM DiagnosticResults
WHERE {where};";

            var ps = new List<SqlParameter>
        {
            SqlParameterFactory.Create("@ItemId", diagnosticRequestItemId)
        };

            if (ignoreResultId.HasValue)
                ps.Add(SqlParameterFactory.Create("@IgnoreId", ignoreResultId.Value));

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
