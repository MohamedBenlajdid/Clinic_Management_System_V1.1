using Clinic_Management_BLL.CrudInterface;
using Clinic_Management_BLL.LoginProcess;
using Clinic_Management_BLL.ResultWraper;
using Clinic_Management_DAL.Data;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.Service
{
    public class RoleService : BaseCrudService<Role>
    {
        // Permission codes (customize as needed)
        protected override string CreatePermissionCode => "ROLE_CREATE";
        protected override string UpdatePermissionCode => "ROLE_UPDATE";
        protected override string DeletePermissionCode => "ROLE_DELETE";
        protected override string ViewPermissionCode => "ROLE_VIEW";

        protected override string EntityName => "Role";

        // DAL method implementations

        protected override int DalCreate(Role entity)
            => RoleData.Insert(entity);

        protected override bool DalUpdate(Role entity)
            => RoleData.Update(entity);

        protected override bool DalDelete(int id)
            => RoleData.GetById(id) != null && RoleData.Delete(id);

        protected override Role? DalGetById(int id)
            => RoleData.GetById(id);

        protected override IEnumerable<Role> DalGetAll()
            => RoleData.GetAll();

        protected override int GetEntityId(Role entity)
            => entity.RoleId;

        protected override ValidationResult.ValidationResult IsValidateData(Role entity)
        {
            var validation = ValidationResult.ValidationResult.Success();

            // Code validation
            if (string.IsNullOrWhiteSpace(entity.Code))
                validation.Add("Code cannot be empty.");

            // Unique Code check (ignore current entity if updating)
            bool codeExists = RoleData.IsCodeExist(entity.Code, entity.RoleId == 0 ? null : entity.RoleId);
            if (codeExists)
                validation.Add("Code already exists.");

            // Name validation
            if (string.IsNullOrWhiteSpace(entity.Name))
                validation.Add("Name cannot be empty.");

            return validation;
        }


        public Result<Role> FindByCode(string code)
        {
            if(!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId,ViewPermissionCode))
            {
                return Result<Role>.Fail("Access Denied!");
            }

            Role role = RoleData.GetByCode(code);


            // Here We should Put Audit 


            return Result<Role>.Ok(role);

        }

        protected override string GetAuditMessage(string operation, Role entity)
            => $"{EntityName} [{entity.RoleId}] {operation} performed.";
    }

}
