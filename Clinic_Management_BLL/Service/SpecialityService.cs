using Clinic_Management_BLL.CrudInterface;
using Clinic_Management_DAL.Data;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.Service
{
    public class SpecialtyService : BaseCrudService<Specialty>
    {
        // Permission codes (customize as needed)
        protected override string CreatePermissionCode => "SPECIALTY_CREATE";
        protected override string UpdatePermissionCode => "SPECIALTY_UPDATE";
        protected override string DeletePermissionCode => "SPECIALTY_DELETE";
        protected override string ViewPermissionCode => "SPECIALTY_VIEW";

        protected override string EntityName => "Specialty";

        // DAL method implementations

        protected override int DalCreate(Specialty entity)
            => SpecialtyData.Insert(entity);

        protected override bool DalUpdate(Specialty entity)
            => SpecialtyData.Update(entity);

        protected override bool DalDelete(int id)
            => SpecialtyData.Exists(id) && SpecialtyData.Delete(id);

        protected override Specialty? DalGetById(int id)
            => SpecialtyData.GetById(id);

        protected override IEnumerable<Specialty> DalGetAll()
            => SpecialtyData.GetAll();

        protected override int GetEntityId(Specialty entity)
            => entity.SpecialtyId;

        protected override ValidationResult.ValidationResult IsValidateData(Specialty entity)
        {
            var validation = ValidationResult.ValidationResult.Success();

            // Name validation
            if (string.IsNullOrWhiteSpace(entity.Name))
                validation.Add("Name cannot be empty.");

            // Unique Name check (ignore current entity if updating)
            bool nameExists = SpecialtyData.GetAll()
                .Any(s => s.Name.Equals(entity.Name, StringComparison.OrdinalIgnoreCase)
                          && s.SpecialtyId != entity.SpecialtyId);

            if (nameExists)
                validation.Add("Name already exists.");

            return validation;
        }

        protected override string GetAuditMessage(string operation, Specialty entity)
            => $"{EntityName} [{entity.SpecialtyId}] {operation} performed.";
    }

}
