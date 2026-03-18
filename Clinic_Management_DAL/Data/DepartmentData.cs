using Clinic_Management_Entities;
using Clinic_Management_DAL.Infrastractor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class DepartmentData
    {
        private const string Columns = @"DepartmentId, Name";

        // ===============================
        // Exists By Id
        // ===============================
        public static bool Exists(int id)
        {
            string query = @"
SELECT 1
FROM Departments
WHERE DepartmentId = @Id";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read();
                },
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // ===============================
        // Exists By Name (VERY IMPORTANT)
        // ===============================
        public static bool Exists(string name)
        {
            string query = @"
SELECT 1
FROM Departments
WHERE Name = @Name";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read();
                },
                SqlParameterFactory.Create("@Name", name)
            );
        }

        // ===============================
        // Insert
        // ===============================
        public static int Insert(Department department)
        {
            string query = @"
INSERT INTO Departments(Name)
VALUES(@Name);

SELECT SCOPE_IDENTITY();";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),
                SqlParameterFactory.Create("@Name", department.Name)
            );
        }

        // ===============================
        // Update
        // ===============================
        public static bool Update(Department department)
        {
            string query = @"
UPDATE Departments SET
Name = @Name
WHERE DepartmentId = @Id";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Name", department.Name),
                SqlParameterFactory.Create("@Id", department.DepartmentId)
            );
        }

        // ===============================
        // Delete
        // ===============================
        public static bool Delete(int id)
        {
            string query = @"
DELETE FROM Departments
WHERE DepartmentId = @Id";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // ===============================
        // Get By Id
        // ===============================
        public static Department GetById(int id)
        {
            string query = $@"
SELECT {Columns}
FROM Departments
WHERE DepartmentId = @Id";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Department>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // ===============================
        // Get All
        // ===============================
        public static IEnumerable<Department> GetAll()
        {
            string query = $"SELECT {Columns} FROM Departments ORDER BY Name";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Department>();

                    while (reader.Read())
                        list.Add(DbMapper<Department>.Map(reader));

                    return list;
                });
        }
    }


}
