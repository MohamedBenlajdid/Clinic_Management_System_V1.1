using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class DiagnosticTestData
    {
        private const string Columns = @"
        DiagnosticTestId,
        Code,
        Name,
        Category,
        Unit,
        RefRange,
        IsActive";

        // =========================
        // GET BY ID
        // =========================
        public static DiagnosticTest? GetById(int id)
        {
            string query = $@"
SELECT {Columns}
FROM DiagnosticTests
WHERE DiagnosticTestId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<DiagnosticTest>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // =========================
        // GET ALL
        // =========================
        public static IEnumerable<DiagnosticTest> GetAll(bool onlyActive = true)
        {
            string filter = onlyActive ? "WHERE IsActive = 1" : "";

            string query = $@"
SELECT {Columns}
FROM DiagnosticTests
{filter}
ORDER BY Name;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<DiagnosticTest>();
                    while (reader.Read())
                        list.Add(DbMapper<DiagnosticTest>.Map(reader));
                    return list;
                }
            );
        }

        // =========================
        // INSERT
        // =========================
        public static int Insert(DiagnosticTest test)
        {
            if (string.IsNullOrWhiteSpace(test.Name))
                throw new ArgumentException("Diagnostic Test Name is required.");

            string query = @"
INSERT INTO DiagnosticTests
(
    Code,
    Name,
    Category,
    Unit,
    RefRange,
    IsActive
)
VALUES
(
    @Code,
    @Name,
    @Category,
    @Unit,
    @RefRange,
    @IsActive
);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),

                SqlParameterFactory.Create("@Code", (object?)test.Code ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Name", test.Name, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Category", (object?)test.Category ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Unit", (object?)test.Unit ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@RefRange", (object?)test.RefRange ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@IsActive", test.IsActive, SqlDbType.Bit)
            );
        }

        // =========================
        // UPDATE
        // =========================
        public static bool Update(DiagnosticTest test)
        {
            if (test.DiagnosticTestId <= 0)
                throw new ArgumentOutOfRangeException(nameof(test.DiagnosticTestId));

            if (string.IsNullOrWhiteSpace(test.Name))
                throw new ArgumentException("Diagnostic Test Name is required.");

            string query = @"
UPDATE DiagnosticTests SET
    Code = @Code,
    Name = @Name,
    Category = @Category,
    Unit = @Unit,
    RefRange = @RefRange,
    IsActive = @IsActive
WHERE DiagnosticTestId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,

                SqlParameterFactory.Create("@Id", test.DiagnosticTestId),
                SqlParameterFactory.Create("@Code", (object?)test.Code ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Name", test.Name, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Category", (object?)test.Category ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Unit", (object?)test.Unit ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@RefRange", (object?)test.RefRange ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@IsActive", test.IsActive, SqlDbType.Bit)
            );
        }

        // =========================
        // SOFT DEACTIVATE
        // =========================
        public static bool Deactivate(int id)
        {
            string query = @"
UPDATE DiagnosticTests SET
    IsActive = 0
WHERE DiagnosticTestId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // =========================
        // EXISTS BY NAME
        // =========================
        public static bool ExistsByName(string name, int? ignoreId = null)
        {
            string where = @"
Name = @Name
" + (ignoreId.HasValue ? "AND DiagnosticTestId <> @IgnoreId" : "");

            string query = $@"
SELECT 1
FROM DiagnosticTests
WHERE {where};";

            var ps = new List<SqlParameter>
        {
            SqlParameterFactory.Create("@Name", name)
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

        // =========================
        // EXISTS BY CODE
        // =========================
        public static bool ExistsByCode(string code, int? ignoreId = null)
        {
            string where = @"
Code = @Code
" + (ignoreId.HasValue ? "AND DiagnosticTestId <> @IgnoreId" : "");

            string query = $@"
SELECT 1
FROM DiagnosticTests
WHERE {where};";

            var ps = new List<SqlParameter>
        {
            SqlParameterFactory.Create("@Code", code)
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
