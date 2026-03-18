using Clinic_Management_BLL.CrudInterface;
using Clinic_Management_DAL.Data;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.Service
{
    public class InsurancePlanService : BaseCrudService<InsurancePlan>
    {
        // Permission codes (customize as needed)
        protected override string CreatePermissionCode => "INSURANCEPLAN_CREATE";
        protected override string UpdatePermissionCode => "INSURANCEPLAN_UPDATE";
        protected override string DeletePermissionCode => "INSURANCEPLAN_DELETE";
        protected override string ViewPermissionCode => "INSURANCEPLAN_VIEW";

        protected override string EntityName => "InsurancePlan";

        // DAL method implementations

        protected override int DalCreate(InsurancePlan entity)
            => InsurancePlanData.Insert(entity);

        protected override bool DalUpdate(InsurancePlan entity)
            => InsurancePlanData.Update(entity);

        protected override bool DalDelete(int id)
            => InsurancePlanData.GetById(id) != null && InsurancePlanData.Delete(id);

        protected override InsurancePlan? DalGetById(int id)
            => InsurancePlanData.GetById(id);

        protected override IEnumerable<InsurancePlan> DalGetAll()
            => InsurancePlanData.GetAll();

        protected override int GetEntityId(InsurancePlan entity)
            => entity.InsurancePlanId;

        protected override ValidationResult.ValidationResult IsValidateData(InsurancePlan entity)
        {
            var validation = ValidationResult.ValidationResult.Success();

            // InsuranceProviderId validation
            if (entity.InsuranceProviderId <= 0)
                validation.Add("InsuranceProviderId must be valid.");

            // PlanName validation
            if (string.IsNullOrWhiteSpace(entity.PlanName))
                validation.Add("PlanName cannot be empty.");
            else if (InsurancePlanData.IsPlanNameExist(entity.InsuranceProviderId, entity.PlanName, entity.InsurancePlanId == 0 ? null : entity.InsurancePlanId))
                validation.Add("PlanName already exists for this provider.");

            // PlanCode validation (optional)
            if (!string.IsNullOrWhiteSpace(entity.PlanCode) &&
                InsurancePlanData.IsPlanCodeExist(entity.InsuranceProviderId, entity.PlanCode, entity.InsurancePlanId == 0 ? null : entity.InsurancePlanId))
                validation.Add("PlanCode already exists for this provider.");

            // Additional validations for CoverageNotes, IsActive could be added here

            return validation;
        }

        protected override string GetAuditMessage(string operation, InsurancePlan entity)
            => $"{EntityName} [{entity.InsurancePlanId}] {operation} performed.";
    }

}
