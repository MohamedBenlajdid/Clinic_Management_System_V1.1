using Clinic_Management_BLL.CrudInterface;
using Clinic_Management_DAL.Data;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.Service
{
    public class PatientInsuranceService : BaseCrudService<PatientInsurance>
    {
        // Permission codes (customize as needed)
        protected override string CreatePermissionCode => "PATIENTINSURANCE_CREATE";
        protected override string UpdatePermissionCode => "PATIENTINSURANCE_UPDATE";
        protected override string DeletePermissionCode => "PATIENTINSURANCE_DELETE";
        protected override string ViewPermissionCode => "PATIENTINSURANCE_VIEW";

        protected override string EntityName => "PatientInsurance";

        // DAL method implementations

        protected override int DalCreate(PatientInsurance entity)
            => PatientInsuranceData.Insert(entity);

        protected override bool DalUpdate(PatientInsurance entity)
            => PatientInsuranceData.Update(entity);

        protected override bool DalDelete(int id)
            => PatientInsuranceData.GetById(id) != null && PatientInsuranceData.Delete(id);

        protected override PatientInsurance? DalGetById(int id)
            => PatientInsuranceData.GetById(id);

        protected override IEnumerable<PatientInsurance> DalGetAll()
            => PatientInsuranceData.GetAll();

        protected override int GetEntityId(PatientInsurance entity)
            => entity.PatientInsuranceId;

        protected override ValidationResult.ValidationResult IsValidateData(PatientInsurance entity)
        {
            var validation = ValidationResult.ValidationResult.Success();

            // PatientId validation
            if (entity.PatientId <= 0)
                validation.Add("PatientId must be valid.");

            // InsuranceProviderId validation
            if (entity.InsuranceProviderId <= 0)
                validation.Add("InsuranceProviderId must be valid.");

            // PolicyNumber validation
            if (string.IsNullOrWhiteSpace(entity.PolicyNumber))
                validation.Add("PolicyNumber cannot be empty.");
            else
            {
                bool policyExists = PatientInsuranceData.IsPolicyNumberExist(entity.PolicyNumber, entity.PatientInsuranceId == 0 ? null : entity.PatientInsuranceId);
                if (policyExists)
                    validation.Add("PolicyNumber already exists.");
            }

            // MemberId validation (optional)
            if (!string.IsNullOrWhiteSpace(entity.MemberId))
            {
                bool memberExists = PatientInsuranceData.IsMemberIdExist(entity.MemberId, entity.PatientInsuranceId == 0 ? null : entity.PatientInsuranceId);
                if (memberExists)
                    validation.Add("MemberId already exists.");
            }

            // EffectiveFrom/EffectiveTo validation (optional)
            if (entity.EffectiveFrom.HasValue && entity.EffectiveTo.HasValue && entity.EffectiveFrom > entity.EffectiveTo)
                validation.Add("EffectiveFrom date cannot be after EffectiveTo date.");

            // Additional validations can be added as needed

            return validation;
        }

        protected override string GetAuditMessage(string operation, PatientInsurance entity)
            => $"{EntityName} [{entity.PatientInsuranceId}] {operation} performed.";
    }

}
