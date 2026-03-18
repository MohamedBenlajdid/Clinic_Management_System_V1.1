using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class PatientData
    {
        private const string Columns = @"
     PatientId,PersonId,MedicalRecordNumber,BloodTypeId,
     EmergencyContactName,EmergencyContactPhone,Notes,
     CreatedAt,UpdatedAt";

        // === Get by Primary Key ===
        public static Patient GetById(int id)
        {
            string query = $@"
     SELECT {Columns}
     FROM Patients
     WHERE PatientId=@Id";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Patient>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // === Insert New Patient ===
        public static int Insert(Patient p)
        {
            string query = @"
INSERT INTO Patients
(
    PersonId,MedicalRecordNumber,BloodTypeId,
    EmergencyContactName,EmergencyContactPhone,Notes
)
VALUES
(
    @PersonId,@MRN,@BloodTypeId,
    @ECName,@ECPhone,@Notes
);

SELECT SCOPE_IDENTITY();";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),

                SqlParameterFactory.Create("@PersonId", p.PersonId),
                SqlParameterFactory.Create("@MRN", p.MedicalRecordNumber),
                SqlParameterFactory.Create("@BloodTypeId", (object)p.BloodTypeId ?? DBNull.Value),
                SqlParameterFactory.Create("@ECName", (object)p.EmergencyContactName ?? DBNull.Value),
                SqlParameterFactory.Create("@ECPhone", (object)p.EmergencyContactPhone ?? DBNull.Value),
                SqlParameterFactory.Create("@Notes", (object)p.Notes ?? DBNull.Value)
            );
        }


        public static bool Delete(int id)
        {
            string query = @"
DELETE FROM Patients
WHERE PatientId = @Id";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", id)
            );
        }


        // === Update Existing Patient ===
        public static bool Update(Patient p)
        {
            string query = @"
UPDATE Patients SET
    PersonId=@PersonId,
    MedicalRecordNumber=@MRN,
    BloodTypeId=@BloodTypeId,
    EmergencyContactName=@ECName,
    EmergencyContactPhone=@ECPhone,
    Notes=@Notes,
    UpdatedAt=SYSUTCDATETIME()
WHERE PatientId=@Id";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,

                SqlParameterFactory.Create("@Id", p.PatientId),
                SqlParameterFactory.Create("@PersonId", p.PersonId),
                SqlParameterFactory.Create("@MRN", p.MedicalRecordNumber),
                SqlParameterFactory.Create("@BloodTypeId", (object)p.BloodTypeId ?? DBNull.Value),
                SqlParameterFactory.Create("@ECName", (object)p.EmergencyContactName ?? DBNull.Value),
                SqlParameterFactory.Create("@ECPhone", (object)p.EmergencyContactPhone ?? DBNull.Value),
                SqlParameterFactory.Create("@Notes", (object)p.Notes ?? DBNull.Value)
            );
        }

        // === Soft Delete Patient (Not defined in your table, but mirroring PersonData style) ===
        // Since no IsDeleted column exists, we skip this.

        // === Internal Helper: Fast Existence Check ===
        private static bool Exists(string field, object value, int? ignoreId = null)
        {
            string query = $@"
        SELECT 1
        FROM Patients
        WHERE {field} = @Value
        {(ignoreId.HasValue ? "AND PatientId <> @IgnoreId" : "")}";

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

        // === Check if MedicalRecordNumber Exists ===
        public static bool IsMedicalRecordNumberExist(string mrn, int? ignoreId = null)
        {
            if (string.IsNullOrWhiteSpace(mrn))
                return false;

            return Exists("MedicalRecordNumber", mrn, ignoreId);
        }

        // === Get Patient by MedicalRecordNumber ===
        public static Patient GetByMedicalRecordNumber(string mrn)
        {
            string query = $@"
        SELECT {Columns}
        FROM Patients
        WHERE MedicalRecordNumber = @MRN";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Patient>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@MRN", mrn)
            );
        }

        // === Get Patient by PersonId ===
        public static Patient GetByPersonId(int personId)
        {
            string query = $@"
        SELECT {Columns}
        FROM Patients
        WHERE PersonId = @PersonId";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Patient>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@PersonId", personId)
            );
        }




        // === Get All Patients ===
        public static IEnumerable<Patient> GetAll()
        {
            string query = $@"
    SELECT {Columns}
    FROM Patients";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Patient>();
                    while (reader.Read())
                    {
                        list.Add(DbMapper<Patient>.Map(reader));
                    }
                    return list;
                }
            );
        }
    }



}
