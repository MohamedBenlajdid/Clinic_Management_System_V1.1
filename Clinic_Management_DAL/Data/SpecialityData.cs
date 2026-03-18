using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class SpecialtyData
    {
        private const string Columns = @"SpecialtyId, Name";

        // ===============================
        // Exists By Id
        // ===============================
        public static bool Exists(int id)
        {
            string query = @"
SELECT 1
FROM Specialties
WHERE SpecialtyId = @Id";

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
        // Exists By Name
        // ===============================
        public static bool Exists(string name)
        {
            string query = @"
SELECT 1
FROM Specialties
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
        public static int Insert(Specialty specialty)
        {
            string query = @"
INSERT INTO Specialties(Name)
VALUES(@Name);

SELECT SCOPE_IDENTITY();";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),
                SqlParameterFactory.Create("@Name", specialty.Name)
            );
        }

        // ===============================
        // Update
        // ===============================
        public static bool Update(Specialty specialty)
        {
            string query = @"
UPDATE Specialties SET
Name = @Name
WHERE SpecialtyId = @Id";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Name", specialty.Name),
                SqlParameterFactory.Create("@Id", specialty.SpecialtyId)
            );
        }

        // ===============================
        // Delete
        // ===============================
        public static bool Delete(int id)
        {
            string query = @"
DELETE FROM Specialties
WHERE SpecialtyId = @Id";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // ===============================
        // Get By Id
        // ===============================
        public static Specialty GetById(int id)
        {
            string query = $@"
SELECT {Columns}
FROM Specialties
WHERE SpecialtyId = @Id";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Specialty>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // ===============================
        // Get All
        // ===============================
        public static IEnumerable<Specialty> GetAll()
        {
            string query = $"SELECT {Columns} FROM Specialties ORDER BY Name";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Specialty>();

                    while (reader.Read())
                        list.Add(DbMapper<Specialty>.Map(reader));

                    return list;
                });
        }
    }


}
