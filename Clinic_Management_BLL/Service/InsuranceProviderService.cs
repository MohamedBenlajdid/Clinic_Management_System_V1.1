using Clinic_Management_BLL.CrudInterface;
using Clinic_Management_DAL.Data;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.Service
{
    public class InsuranceProviderService : BaseCrudService<InsuranceProvider>
    {
        // Permission codes (customize as needed)
        protected override string CreatePermissionCode => "INSURANCEPROVIDER_CREATE";
        protected override string UpdatePermissionCode => "INSURANCEPROVIDER_UPDATE";
        protected override string DeletePermissionCode => "INSURANCEPROVIDER_DELETE";
        protected override string ViewPermissionCode => "INSURANCEPROVIDER_VIEW";

        protected override string EntityName => "InsuranceProvider";

        // DAL method implementations

        protected override int DalCreate(InsuranceProvider entity)
            => InsuranceProviderData.Insert(entity);

        protected override bool DalUpdate(InsuranceProvider entity)
            => InsuranceProviderData.Update(entity);

        protected override bool DalDelete(int id)
            => InsuranceProviderData.GetById(id) != null && InsuranceProviderData.Delete(id);

        protected override InsuranceProvider? DalGetById(int id)
            => InsuranceProviderData.GetById(id);

        protected override IEnumerable<InsuranceProvider> DalGetAll()
            => InsuranceProviderData.GetAll();

        protected override int GetEntityId(InsuranceProvider entity)
            => entity.InsuranceProviderId;

        protected override ValidationResult.ValidationResult IsValidateData(InsuranceProvider entity)
        {
            var validation = ValidationResult.ValidationResult.Success();

            // Name validation
            if (string.IsNullOrWhiteSpace(entity.Name))
                validation.Add("Name cannot be empty.");
            else if (InsuranceProviderData.IsNameExist(entity.Name, entity.InsuranceProviderId == 0 ? null : entity.InsuranceProviderId))
                validation.Add("Name already exists.");

            // Phone validation (optional)
            if (!string.IsNullOrWhiteSpace(entity.Phone) && InsuranceProviderData.IsPhoneExist(entity.Phone, entity.InsuranceProviderId == 0 ? null : entity.InsuranceProviderId))
                validation.Add("Phone already exists.");

            // Email validation (optional)
            if (!string.IsNullOrWhiteSpace(entity.Email) && InsuranceProviderData.IsEmailExist(entity.Email, entity.InsuranceProviderId == 0 ? null : entity.InsuranceProviderId))
                validation.Add("Email already exists.");

            // Website validation (optional)
            if (!string.IsNullOrWhiteSpace(entity.Website) && InsuranceProviderData.IsWebsiteExist(entity.Website, entity.InsuranceProviderId == 0 ? null : entity.InsuranceProviderId))
                validation.Add("Website already exists.");

            // AddressLine validation (optional)
            if (!string.IsNullOrWhiteSpace(entity.AddressLine) && InsuranceProviderData.IsAddressLineExist(entity.AddressLine, entity.InsuranceProviderId == 0 ? null : entity.InsuranceProviderId))
                validation.Add("AddressLine already exists.");

            // Additional validations can be added here

            return validation;
        }

        protected override string GetAuditMessage(string operation, InsuranceProvider entity)
            => $"{EntityName} [{entity.InsuranceProviderId}] {operation} performed.";
    }

}
