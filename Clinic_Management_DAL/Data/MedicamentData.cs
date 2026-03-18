using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class MedicamentData
    {
        private const string Columns = @"
        MedicamentId,
        Name,
        GenericName,
        Form,
        Strength,
        Manufacturer,
        IsActive,
        CreatedAt";

        // =========================
        // GET BY ID
        // =========================
        public static Medicament? GetById(int medicamentId)
        {
            string query = $@"
SELECT {Columns}
FROM Medicaments
WHERE MedicamentId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Medicament>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", medicamentId)
            );
        }

        // =========================
        // GET ALL
        // =========================
        public static IEnumerable<Medicament> GetAll(bool onlyActive = true)
        {
            string activeFilter = onlyActive ? "WHERE IsActive = 1" : "";

            string query = $@"
SELECT {Columns}
FROM Medicaments
{activeFilter}
ORDER BY Name;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Medicament>();
                    while (reader.Read())
                        list.Add(DbMapper<Medicament>.Map(reader));
                    return list;
                }
            );
        }

        // =========================
        // INSERT
        // =========================
        public static int Insert(Medicament m)
        {
            if (string.IsNullOrWhiteSpace(m.Name))
                throw new ArgumentException("Medicament Name is required.");

            string query = @"
INSERT INTO Medicaments
(
    Name,
    GenericName,
    Form,
    Strength,
    Manufacturer,
    IsActive
)
VALUES
(
    @Name,
    @GenericName,
    @Form,
    @Strength,
    @Manufacturer,
    @IsActive
);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),
                SqlParameterFactory.Create("@Name", m.Name, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@GenericName", (object?)m.GenericName ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Form", (object?)m.Form ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Strength", (object?)m.Strength ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Manufacturer", (object?)m.Manufacturer ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@IsActive", m.IsActive, SqlDbType.Bit)
            );
        }

        // =========================
        // UPDATE
        // =========================
        public static bool Update(Medicament m)
        {
            if (m.MedicamentId <= 0)
                throw new ArgumentOutOfRangeException(nameof(m.MedicamentId));

            if (string.IsNullOrWhiteSpace(m.Name))
                throw new ArgumentException("Medicament Name is required.");

            string query = @"
UPDATE Medicaments SET
    Name = @Name,
    GenericName = @GenericName,
    Form = @Form,
    Strength = @Strength,
    Manufacturer = @Manufacturer,
    IsActive = @IsActive
WHERE MedicamentId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", m.MedicamentId),
                SqlParameterFactory.Create("@Name", m.Name, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@GenericName", (object?)m.GenericName ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Form", (object?)m.Form ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Strength", (object?)m.Strength ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Manufacturer", (object?)m.Manufacturer ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@IsActive", m.IsActive, SqlDbType.Bit)
            );
        }

        // =========================
        // SOFT DEACTIVATE
        // =========================
        public static bool Deactivate(int medicamentId)
        {
            string query = @"
UPDATE Medicaments SET
    IsActive = 0
WHERE MedicamentId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", medicamentId)
            );
        }

        // =========================
        // EXISTS BY NAME
        // =========================
        public static bool ExistsByName(string name, int? ignoreId = null)
        {
            string where = @"
Name = @Name
" + (ignoreId.HasValue ? "AND MedicamentId <> @IgnoreId" : "");

            string query = $@"
SELECT 1
FROM Medicaments
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
    }

}
