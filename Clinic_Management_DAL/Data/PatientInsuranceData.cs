using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class PatientInsuranceData
    {
        private const string Columns = @"
     PatientInsuranceId,PatientId,InsuranceProviderId,InsurancePlanId,
     PolicyNumber,MemberId,HolderFullName,HolderRelation,
     EffectiveFrom,EffectiveTo,IsPrimary,IsActive,CreatedAt";

        // === Get by Primary Key ===
        public static PatientInsurance GetById(int id)
        {
            string query = $@"
     SELECT {Columns}
     FROM PatientInsurances
     WHERE PatientInsuranceId = @Id";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<PatientInsurance>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // === Insert New PatientInsurance ===
        public static int Insert(PatientInsurance pi)
        {
            string query = @"
INSERT INTO PatientInsurances
(
    PatientId,InsuranceProviderId,InsurancePlanId,
    PolicyNumber,MemberId,HolderFullName,HolderRelation,
    EffectiveFrom,EffectiveTo,IsPrimary,IsActive
)
VALUES
(
    @PatientId,@InsuranceProviderId,@InsurancePlanId,
    @PolicyNumber,@MemberId,@HolderFullName,@HolderRelation,
    @EffectiveFrom,@EffectiveTo,@IsPrimary,@IsActive
);

SELECT SCOPE_IDENTITY();";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),

                SqlParameterFactory.Create("@PatientId", pi.PatientId),
                SqlParameterFactory.Create("@InsuranceProviderId", pi.InsuranceProviderId),
                SqlParameterFactory.Create("@InsurancePlanId", (object)pi.InsurancePlanId ?? DBNull.Value),
                SqlParameterFactory.Create("@PolicyNumber", pi.PolicyNumber),
                SqlParameterFactory.Create("@MemberId", (object)pi.MemberId ?? DBNull.Value),
                SqlParameterFactory.Create("@HolderFullName", (object)pi.HolderFullName ?? DBNull.Value),
                SqlParameterFactory.Create("@HolderRelation", (object)pi.HolderRelation ?? DBNull.Value),
                SqlParameterFactory.Create("@EffectiveFrom", (object)pi.EffectiveFrom ?? DBNull.Value),
                SqlParameterFactory.Create("@EffectiveTo", (object)pi.EffectiveTo ?? DBNull.Value),
                SqlParameterFactory.Create("@IsPrimary", pi.IsPrimary),
                SqlParameterFactory.Create("@IsActive", pi.IsActive)
            );
        }

        public static bool Delete(int id)
        {
            string query = @"
DELETE FROM PatientInsurances
WHERE PatientInsuranceId = @Id";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", id)
            );
        }



        // === Update Existing PatientInsurance ===
        public static bool Update(PatientInsurance pi)
        {
            string query = @"
UPDATE PatientInsurances SET
    PatientId = @PatientId,
    InsuranceProviderId = @InsuranceProviderId,
    InsurancePlanId = @InsurancePlanId,
    PolicyNumber = @PolicyNumber,
    MemberId = @MemberId,
    HolderFullName = @HolderFullName,
    HolderRelation = @HolderRelation,
    EffectiveFrom = @EffectiveFrom,
    EffectiveTo = @EffectiveTo,
    IsPrimary = @IsPrimary,
    IsActive = @IsActive
WHERE PatientInsuranceId = @Id";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,

                SqlParameterFactory.Create("@Id", pi.PatientInsuranceId),
                SqlParameterFactory.Create("@PatientId", pi.PatientId),
                SqlParameterFactory.Create("@InsuranceProviderId", pi.InsuranceProviderId),
                SqlParameterFactory.Create("@InsurancePlanId", (object)pi.InsurancePlanId ?? DBNull.Value),
                SqlParameterFactory.Create("@PolicyNumber", pi.PolicyNumber),
                SqlParameterFactory.Create("@MemberId", (object)pi.MemberId ?? DBNull.Value),
                SqlParameterFactory.Create("@HolderFullName", (object)pi.HolderFullName ?? DBNull.Value),
                SqlParameterFactory.Create("@HolderRelation", (object)pi.HolderRelation ?? DBNull.Value),
                SqlParameterFactory.Create("@EffectiveFrom", (object)pi.EffectiveFrom ?? DBNull.Value),
                SqlParameterFactory.Create("@EffectiveTo", (object)pi.EffectiveTo ?? DBNull.Value),
                SqlParameterFactory.Create("@IsPrimary", pi.IsPrimary),
                SqlParameterFactory.Create("@IsActive", pi.IsActive)
            );
        }

        // === Get All PatientInsurances for a Patient ===
        public static IEnumerable<PatientInsurance> GetByPatientId(int patientId)
        {
            string query = $@"
    SELECT {Columns}
    FROM PatientInsurances
    WHERE PatientId = @PatientId";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<PatientInsurance>();
                    while (reader.Read())
                    {
                        list.Add(DbMapper<PatientInsurance>.Map(reader));
                    }
                    return list;
                },
                SqlParameterFactory.Create("@PatientId", patientId)
            );
        }

        // === Get All Active Primary PatientInsurance for a Patient (should be max 1) ===
        public static PatientInsurance? GetPrimaryActiveByPatientId(int patientId)
        {
            string query = $@"
    SELECT {Columns}
    FROM PatientInsurances
    WHERE PatientId = @PatientId
    AND IsPrimary = 1
    AND IsActive = 1";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<PatientInsurance>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@PatientId", patientId)
            );
        }

        // === Get All PatientInsurances ===
        public static IEnumerable<PatientInsurance> GetAll()
        {
            string query = $@"
    SELECT {Columns}
    FROM PatientInsurances";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<PatientInsurance>();
                    while (reader.Read())
                    {
                        list.Add(DbMapper<PatientInsurance>.Map(reader));
                    }
                    return list;
                }
            );
        }



        // === Check if PolicyNumber Exists ===
        public static bool IsPolicyNumberExist(string policyNumber, int? ignoreId = null)
        {
            if (string.IsNullOrWhiteSpace(policyNumber))
                return false;

            string query = @"
        SELECT 1
        FROM PatientInsurances
        WHERE PolicyNumber = @PolicyNumber
        " + (ignoreId.HasValue ? "AND PatientInsuranceId <> @IgnoreId" : "");

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
                SqlParameterFactory.Create("@PolicyNumber", policyNumber),
                SqlParameterFactory.Create("@IgnoreId", ignoreId.Value)
                    }
                    : new[]
                    {
                SqlParameterFactory.Create("@PolicyNumber", policyNumber)
                    }
            );
        }

        // === Get PatientInsurance by PolicyNumber ===
        public static PatientInsurance GetByPolicyNumber(string policyNumber)
        {
            string query = $@"
        SELECT {Columns}
        FROM PatientInsurances
        WHERE PolicyNumber = @PolicyNumber";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<PatientInsurance>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@PolicyNumber", policyNumber)
            );
        }

        // === Check if MemberId Exists ===
        public static bool IsMemberIdExist(string memberId, int? ignoreId = null)
        {
            if (string.IsNullOrWhiteSpace(memberId))
                return false;

            string query = @"
        SELECT 1
        FROM PatientInsurances
        WHERE MemberId = @MemberId
        " + (ignoreId.HasValue ? "AND PatientInsuranceId <> @IgnoreId" : "");

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
                SqlParameterFactory.Create("@MemberId", memberId),
                SqlParameterFactory.Create("@IgnoreId", ignoreId.Value)
                    }
                    : new[]
                    {
                SqlParameterFactory.Create("@MemberId", memberId)
                    }
            );
        }

        // === Get PatientInsurance by MemberId ===
        public static PatientInsurance GetByMemberId(string memberId)
        {
            string query = $@"
        SELECT {Columns}
        FROM PatientInsurances
        WHERE MemberId = @MemberId";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<PatientInsurance>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@MemberId", memberId)
            );
        }



    }

}
