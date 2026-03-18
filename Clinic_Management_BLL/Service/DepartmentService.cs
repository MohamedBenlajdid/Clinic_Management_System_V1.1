using Clinic_Management_BLL.CrudInterface;
using Clinic_Management_DAL.Data;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.Service
{
    public class DepartmentService : BaseCrudService<Department>
    {
        // Permission codes (customize as needed)
        protected override string CreatePermissionCode => "DEPARTMENT_CREATE";
        protected override string UpdatePermissionCode => "DEPARTMENT_UPDATE";
        protected override string DeletePermissionCode => "DEPARTMENT_DELETE";
        protected override string ViewPermissionCode => "DEPARTMENT_VIEW";

        protected override string EntityName => "Department";

        // DAL method implementations

        protected override int DalCreate(Department entity)
            => DepartmentData.Insert(entity);

        protected override bool DalUpdate(Department entity)
            => DepartmentData.Update(entity);

        protected override bool DalDelete(int id)
            => DepartmentData.Exists(id) && DepartmentData.Delete(id);

        protected override Department? DalGetById(int id)
            => DepartmentData.GetById(id);

        protected override IEnumerable<Department> DalGetAll()
            => DepartmentData.GetAll();

        protected override int GetEntityId(Department entity)
            => entity.DepartmentId;

        protected override ValidationResult.ValidationResult IsValidateData(Department entity)
        {
            var validation = ValidationResult.ValidationResult.Success();

            // Name validation
            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                validation.Add("Department name cannot be empty.");
            }
            else if (DepartmentData.Exists(entity.Name))
            {
                validation.Add("Department name already exists.");
            }

            return validation;
        }

        protected override string GetAuditMessage(string operation, Department entity)
            => $"{EntityName} [{entity.DepartmentId}] {operation} performed.";
    }

}
