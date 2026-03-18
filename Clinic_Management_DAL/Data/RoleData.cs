using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_DAL.Data
{
   
    public static class RoleData
    {
        private const string Columns = @"
     RoleId,Code,Name,Description,IsActive";

        // === Get by Primary Key ===
        public static Role GetById(int id)
        {
            string query = $@"
     SELECT {Columns}
     FROM Roles
     WHERE RoleId = @Id";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Role>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // === Insert New Role ===
        public static int Insert(Role role)
        {
            string query = @"
INSERT INTO Roles
(
    Code, Name, Description, IsActive
)
VALUES
(
    @Code, @Name, @Description, @IsActive
);

SELECT SCOPE_IDENTITY();";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),

                SqlParameterFactory.Create("@Code", role.Code),
                SqlParameterFactory.Create("@Name", role.Name),
                SqlParameterFactory.Create("@Description", (object)role.Description ?? DBNull.Value),
                SqlParameterFactory.Create("@IsActive", role.IsActive)
            );
        }


        public static bool Delete(int id)
        {
            string query = @"
DELETE FROM Roles
WHERE RoleId = @Id";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", id)
            );
        }


        // === Update Existing Role ===
        public static bool Update(Role role)
        {
            string query = @"
UPDATE Roles SET
    Code = @Code,
    Name = @Name,
    Description = @Description,
    IsActive = @IsActive
WHERE RoleId = @Id";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,

                SqlParameterFactory.Create("@Id", role.RoleId),
                SqlParameterFactory.Create("@Code", role.Code),
                SqlParameterFactory.Create("@Name", role.Name),
                SqlParameterFactory.Create("@Description", (object)role.Description ?? DBNull.Value),
                SqlParameterFactory.Create("@IsActive", role.IsActive)
            );
        }

        // === Internal Helper: Fast Existence Check ===
        private static bool Exists(string field, object value, int? ignoreId = null)
        {
            string query = $@"
        SELECT 1
        FROM Roles
        WHERE {field} = @Value
        {(ignoreId.HasValue ? "AND RoleId <> @IgnoreId" : "")}";

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

        // === Get Role by Code ===
        public static Role GetByCode(string code)
        {
            string query = $@"
        SELECT {Columns}
        FROM Roles
        WHERE Code = @Code";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Role>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Code", code)
            );
        }

        // === Get All Roles ===
        public static IEnumerable<Role> GetAll()
        {
            string query = $@"
        SELECT {Columns}
        FROM Roles";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Role>();
                    while (reader.Read())
                    {
                        list.Add(DbMapper<Role>.Map(reader));
                    }
                    return list;
                }
            );
        }
    }

}
