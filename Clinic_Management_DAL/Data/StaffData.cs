using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class StaffData
    {
        private const string Columns = @"
     StaffId,PersonId,StaffCode,DepartmentId,
     HireDate,IsActive,CreatedAt,UpdatedAt";

        // === Get by Primary Key ===
        public static Staff GetById(int id)
        {
            string query = $@"
     SELECT {Columns}
     FROM Staff
     WHERE StaffId = @Id";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Staff>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // === Insert New Staff ===
        public static int Insert(Staff s)
        {
            string query = @"
INSERT INTO Staff
(
    PersonId,StaffCode,DepartmentId,
    HireDate,IsActive
)
VALUES
(
    @PersonId,@StaffCode,@DepartmentId,
    @HireDate,@IsActive
);

SELECT SCOPE_IDENTITY();";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),

                SqlParameterFactory.Create("@PersonId", s.PersonId),
                SqlParameterFactory.Create("@StaffCode", s.StaffCode),
                SqlParameterFactory.Create("@DepartmentId", (object)s.DepartmentId ?? DBNull.Value),
                SqlParameterFactory.Create("@HireDate", (object)s.HireDate ?? DBNull.Value),
                SqlParameterFactory.Create("@IsActive", s.IsActive)
            );
        }

        // === Update Existing Staff ===
        public static bool Update(Staff s)
        {
            string query = @"
UPDATE Staff SET
    PersonId = @PersonId,
    StaffCode = @StaffCode,
    DepartmentId = @DepartmentId,
    HireDate = @HireDate,
    IsActive = @IsActive,
    UpdatedAt = SYSUTCDATETIME()
WHERE StaffId = @Id";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,

                SqlParameterFactory.Create("@Id", s.StaffId),
                SqlParameterFactory.Create("@PersonId", s.PersonId),
                SqlParameterFactory.Create("@StaffCode", s.StaffCode),
                SqlParameterFactory.Create("@DepartmentId", (object)s.DepartmentId ?? DBNull.Value),
                SqlParameterFactory.Create("@HireDate", (object)s.HireDate ?? DBNull.Value),
                SqlParameterFactory.Create("@IsActive", s.IsActive)
            );
        }

        // === Internal Helper: Fast Existence Check ===
        private static bool Exists(string field, object value, int? ignoreId = null)
        {
            string query = $@"
        SELECT 1
        FROM Staff
        WHERE {field} = @Value
        {(ignoreId.HasValue ? "AND StaffId <> @IgnoreId" : "")}";

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
        public static bool Delete(int staffId)
        {
            string query = @"
DELETE FROM Staff
WHERE StaffId = @StaffId";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@StaffId", staffId)
            );
        }


        // === Check if StaffCode Exists ===
        public static bool IsStaffCodeExist(string staffCode, int? ignoreId = null)
        {
            if (string.IsNullOrWhiteSpace(staffCode))
                return false;

            return Exists("StaffCode", staffCode, ignoreId);
        }

        public static bool IsPersonIDExist(int PersonID, int? ignoreId = null)
        {
            if (PersonID <= 0)
                throw new ArgumentOutOfRangeException("Person Not Exist");

            return Exists("PersonId", PersonID, ignoreId);
        }



        // === Get Staff by StaffCode ===
        public static Staff GetByStaffCode(string staffCode)
        {
            string query = $@"
        SELECT {Columns}
        FROM Staff
        WHERE StaffCode = @StaffCode";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Staff>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@StaffCode", staffCode)
            );
        }

        // === Get Staff by PersonId ===
        public static Staff GetByPersonId(int personId)
        {
            string query = $@"
        SELECT {Columns}
        FROM Staff
        WHERE PersonId = @PersonId";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Staff>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@PersonId", personId)
            );
        }

        // === Get All Staff ===
        public static IEnumerable<Staff> GetAll()
        {
            string query = $@"
    SELECT {Columns}
    FROM Staff";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Staff>();
                    while (reader.Read())
                    {
                        list.Add(DbMapper<Staff>.Map(reader));
                    }
                    return list;
                }
            );
        }
    }



}
