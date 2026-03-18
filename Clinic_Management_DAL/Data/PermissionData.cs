using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class PermissionData
    {
        private const string Columns = @"
     PermissionId,Code,Name,Module,Description,IsActive";

        // === Get by Primary Key ===
        public static Permission GetById(int id)
        {
            string query = $@"
     SELECT {Columns}
     FROM Permissions
     WHERE PermissionId = @Id";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Permission>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // === Insert New Permission ===
        public static int Insert(Permission permission)
        {
            string query = @"
INSERT INTO Permissions
(
    Code, Name, Module, Description, IsActive
)
VALUES
(
    @Code, @Name, @Module, @Description, @IsActive
);

SELECT SCOPE_IDENTITY();";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),

                SqlParameterFactory.Create("@Code", permission.Code),
                SqlParameterFactory.Create("@Name", permission.Name),
                SqlParameterFactory.Create("@Module", (object)permission.Module ?? DBNull.Value),
                SqlParameterFactory.Create("@Description", (object)permission.Description ?? DBNull.Value),
                SqlParameterFactory.Create("@IsActive", permission.IsActive)
            );
        }

        public static bool Delete(int id)
        {
            string query = @"
DELETE FROM Permissions
WHERE PermissionId = @Id";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", id)
            );
        }



        // === Update Existing Permission ===
        public static bool Update(Permission permission)
        {
            string query = @"
UPDATE Permissions SET
    Code = @Code,
    Name = @Name,
    Module = @Module,
    Description = @Description,
    IsActive = @IsActive
WHERE PermissionId = @Id";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,

                SqlParameterFactory.Create("@Id", permission.PermissionId),
                SqlParameterFactory.Create("@Code", permission.Code),
                SqlParameterFactory.Create("@Name", permission.Name),
                SqlParameterFactory.Create("@Module", (object)permission.Module ?? DBNull.Value),
                SqlParameterFactory.Create("@Description", (object)permission.Description ?? DBNull.Value),
                SqlParameterFactory.Create("@IsActive", permission.IsActive)
            );
        }

        // === Internal Helper: Fast Existence Check ===
        private static bool Exists(string field, object value, int? ignoreId = null)
        {
            string query = $@"
        SELECT 1
        FROM Permissions
        WHERE {field} = @Value
        {(ignoreId.HasValue ? "AND PermissionId <> @IgnoreId" : "")}";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read();
                },
                ignoreId.HasValue
                    ? new[]
                    {
                    SqlParameterFactory.Create("@Value", value ?? DBNull.Value),
                    SqlParameterFactory.Create("@IgnoreId", ignoreId.Value)
                    }
                    : new[]
                    {
                    SqlParameterFactory.Create("@Value", value ?? DBNull.Value)
                    }
            );
        }

        // === Check if Code Exists ===
        public static bool IsCodeExist(string code, int? ignoreId = null)
        {
            if (string.IsNullOrWhiteSpace(code))
                return false;

            return Exists("Code", code, ignoreId);
        }

        // === Get Permission by Code ===
        public static Permission GetByCode(string code)
        {
            string query = $@"
        SELECT {Columns}
        FROM Permissions
        WHERE Code = @Code";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Permission>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Code", code)
            );
        }

        // === Get All Permissions ===
        public static IEnumerable<Permission> GetAll()
        {
            string query = $@"
        SELECT {Columns}
        FROM Permissions";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Permission>();
                    while (reader.Read())
                    {
                        list.Add(DbMapper<Permission>.Map(reader));
                    }
                    return list;
                }
            );
        }



    }

    }
