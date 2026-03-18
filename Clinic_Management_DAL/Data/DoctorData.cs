using Clinic_Management_Entities;
using Clinic_Management_DAL.Infrastractor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class DoctorData
    {
        private const string Columns = @"
     StaffId,SpecialtyId,LicenseNumber,ConsultationFee";

        // === Get by Primary Key ===
        public static Doctor GetById(int id)
        {
            string query = $@"
     SELECT {Columns}
     FROM Doctors
     WHERE StaffId = @Id";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Doctor>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // === Insert New Doctor ===
        public static bool Insert(Doctor d)
        {
            string query = @"
INSERT INTO Doctors
(
    StaffId,SpecialtyId,LicenseNumber,ConsultationFee
)
VALUES
(
    @StaffId,@SpecialtyId,@LicenseNumber,@ConsultationFee
);";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,

                SqlParameterFactory.Create("@StaffId", d.StaffId),
                SqlParameterFactory.Create("@SpecialtyId", (object)d.SpecialtyId ?? DBNull.Value),
                SqlParameterFactory.Create("@LicenseNumber", (object)d.LicenseNumber ?? DBNull.Value),
                SqlParameterFactory.Create("@ConsultationFee", (object)d.ConsultationFee ?? DBNull.Value)
            );
        }


        // Since DoctorData doesn't have Delete method, let's create private helper here
        public static bool Delete(int id)
        {
            string query = @"DELETE FROM Doctors WHERE StaffId = @Id";
            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // === Update Existing Doctor ===
        public static bool Update(Doctor d)
        {
            string query = @"
UPDATE Doctors SET
    SpecialtyId = @SpecialtyId,
    LicenseNumber = @LicenseNumber,
    ConsultationFee = @ConsultationFee
WHERE StaffId = @StaffId";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,

                SqlParameterFactory.Create("@StaffId", d.StaffId),
                SqlParameterFactory.Create("@SpecialtyId", (object)d.SpecialtyId ?? DBNull.Value),
                SqlParameterFactory.Create("@LicenseNumber", (object)d.LicenseNumber ?? DBNull.Value),
                SqlParameterFactory.Create("@ConsultationFee", (object)d.ConsultationFee ?? DBNull.Value)
            );
        }

        // === Internal Helper: Fast Existence Check (e.g. LicenseNumber) ===
        private static bool Exists(string field, object value, int? ignoreId = null)
        {
            string query = $@"
        SELECT 1
        FROM Doctors
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

        // === Check if LicenseNumber Exists ===
        public static bool IsLicenseNumberExist(string licenseNumber, int? ignoreId = null)
        {
            if (string.IsNullOrWhiteSpace(licenseNumber))
                return false;

            return Exists("LicenseNumber", licenseNumber, ignoreId);
        }



        public static bool IsStuffIDExist(int StuffID, int? ignoreId = null)
        {
            if (StuffID <= 0)
                throw new ArgumentOutOfRangeException("Stuff Not Exist");

            return Exists("StuffId", StuffID, ignoreId);
        }

        // === Get Doctor by LicenseNumber ===
        public static Doctor GetByLicenseNumber(string licenseNumber)
        {
            string query = $@"
        SELECT {Columns}
        FROM Doctors
        WHERE LicenseNumber = @LicenseNumber";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Doctor>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@LicenseNumber", licenseNumber)
            );
        }

        // === Get All Doctors ===
        public static IEnumerable<Doctor> GetAll()
        {
            string query = $@"
    SELECT {Columns}
    FROM Doctors";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Doctor>();
                    while (reader.Read())
                    {
                        list.Add(DbMapper<Doctor>.Map(reader));
                    }
                    return list;
                }
            );
        }


        public static IEnumerable<Doctor> GetAllBySpecialtyId(int specialtyId)
        {
            string query = $@"
SELECT {Columns}
FROM Doctors
WHERE SpecialtyId = @SpecialtyId";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Doctor>();
                    while (reader.Read())
                        list.Add(DbMapper<Doctor>.Map(reader));
                    return list;
                },
                SqlParameterFactory.Create("@SpecialtyId", specialtyId)
            );
        }







    }



}
