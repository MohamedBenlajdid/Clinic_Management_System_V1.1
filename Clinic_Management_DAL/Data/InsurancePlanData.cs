using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities;
using Clinic_Management_DAL.Infrastractor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class InsurancePlanData
    {
        private const string Columns = @"
     InsurancePlanId,InsuranceProviderId,PlanName,PlanCode,
     CoverageNotes,IsActive,CreatedAt";

        // === Get by Primary Key ===
        public static InsurancePlan GetById(int id)
        {
            string query = $@"
     SELECT {Columns}
     FROM InsurancePlans
     WHERE InsurancePlanId = @Id";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<InsurancePlan>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // === Insert New InsurancePlan ===
        public static int Insert(InsurancePlan plan)
        {
            string query = @"
INSERT INTO InsurancePlans
(
    InsuranceProviderId,PlanName,PlanCode,
    CoverageNotes,IsActive
)
VALUES
(
    @InsuranceProviderId,@PlanName,@PlanCode,
    @CoverageNotes,@IsActive
);

SELECT SCOPE_IDENTITY();";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),

                SqlParameterFactory.Create("@InsuranceProviderId", plan.InsuranceProviderId),
                SqlParameterFactory.Create("@PlanName", plan.PlanName),
                SqlParameterFactory.Create("@PlanCode", (object)plan.PlanCode ?? DBNull.Value),
                SqlParameterFactory.Create("@CoverageNotes", (object)plan.CoverageNotes ?? DBNull.Value),
                SqlParameterFactory.Create("@IsActive", plan.IsActive)
            );
        }


        // === Delete InsuranceProvider by Id ===
        public static bool Delete(int id)
        {
            string query = @"
DELETE FROM InsuranceProviders
WHERE InsuranceProviderId = @Id";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", id)
            );
        }



        // === Update Existing InsurancePlan ===
        public static bool Update(InsurancePlan plan)
        {
            string query = @"
UPDATE InsurancePlans SET
    InsuranceProviderId = @InsuranceProviderId,
    PlanName = @PlanName,
    PlanCode = @PlanCode,
    CoverageNotes = @CoverageNotes,
    IsActive = @IsActive
WHERE InsurancePlanId = @Id";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,

                SqlParameterFactory.Create("@Id", plan.InsurancePlanId),
                SqlParameterFactory.Create("@InsuranceProviderId", plan.InsuranceProviderId),
                SqlParameterFactory.Create("@PlanName", plan.PlanName),
                SqlParameterFactory.Create("@PlanCode", (object)plan.PlanCode ?? DBNull.Value),
                SqlParameterFactory.Create("@CoverageNotes", (object)plan.CoverageNotes ?? DBNull.Value),
                SqlParameterFactory.Create("@IsActive", plan.IsActive)
            );
        }

        // === Internal Helper: Fast Existence Check (e.g. PlanName for Provider) ===
        private static bool Exists(string field, object value, int insuranceProviderId, int? ignoreId = null)
        {
            string query = $@"
        SELECT 1
        FROM InsurancePlans
        WHERE {field} = @Value
        AND InsuranceProviderId = @InsuranceProviderId
        {(ignoreId.HasValue ? "AND InsurancePlanId <> @IgnoreId" : "")}";

            var parameters = ignoreId.HasValue
                ? new[]
                {
                SqlParameterFactory.Create("@Value", value ?? DBNull.Value),
                SqlParameterFactory.Create("@InsuranceProviderId", insuranceProviderId),
                SqlParameterFactory.Create("@IgnoreId", ignoreId.Value)
                }
                : new[]
                {
                SqlParameterFactory.Create("@Value", value ?? DBNull.Value),
                SqlParameterFactory.Create("@InsuranceProviderId", insuranceProviderId)
                };

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read();
                },
                parameters
            );
        }

        // === Check if PlanName Exists for a Provider ===
        public static bool IsPlanNameExist(int insuranceProviderId, string planName, int? ignoreId = null)
        {
            if (string.IsNullOrWhiteSpace(planName))
                return false;

            return Exists("PlanName", planName, insuranceProviderId, ignoreId);
        }

        // === Get InsurancePlan by PlanName and ProviderId ===
        public static InsurancePlan GetByPlanName(int insuranceProviderId, string planName)
        {
            string query = $@"
        SELECT {Columns}
        FROM InsurancePlans
        WHERE InsuranceProviderId = @InsuranceProviderId
        AND PlanName = @PlanName";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<InsurancePlan>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@InsuranceProviderId", insuranceProviderId),
                SqlParameterFactory.Create("@PlanName", planName)
            );
        }

        // === Get All Plans for a Provider ===
        public static IEnumerable<InsurancePlan> GetByProviderId(int insuranceProviderId)
        {
            string query = $@"
    SELECT {Columns}
    FROM InsurancePlans
    WHERE InsuranceProviderId = @InsuranceProviderId";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<InsurancePlan>();
                    while (reader.Read())
                    {
                        list.Add(DbMapper<InsurancePlan>.Map(reader));
                    }
                    return list;
                },
                SqlParameterFactory.Create("@InsuranceProviderId", insuranceProviderId)
            );
        }

        // === Get All InsurancePlans ===
        public static IEnumerable<InsurancePlan> GetAll()
        {
            string query = $@"
    SELECT {Columns}
    FROM InsurancePlans";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<InsurancePlan>();
                    while (reader.Read())
                    {
                        list.Add(DbMapper<InsurancePlan>.Map(reader));
                    }
                    return list;
                }
            );
        }



        // === Check if PlanCode Exists for a Provider ===
        public static bool IsPlanCodeExist(int insuranceProviderId, string planCode, int? ignoreId = null)
        {
            if (string.IsNullOrWhiteSpace(planCode))
                return false;

            return Exists("PlanCode", planCode, insuranceProviderId, ignoreId);
        }

        // === Get InsurancePlan by PlanCode and ProviderId ===
        public static InsurancePlan GetByPlanCode(int insuranceProviderId, string planCode)
        {
            string query = $@"
    SELECT {Columns}
    FROM InsurancePlans
    WHERE InsuranceProviderId = @InsuranceProviderId
    AND PlanCode = @PlanCode";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<InsurancePlan>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@InsuranceProviderId", insuranceProviderId),
                SqlParameterFactory.Create("@PlanCode", planCode)
            );
        }




    }

}
