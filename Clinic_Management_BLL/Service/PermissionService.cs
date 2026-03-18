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
    public class PermissionService : BaseCrudService<Permission>
    {
        // Permission codes (customize as needed)
        protected override string CreatePermissionCode => "PERMISSION_CREATE";
        protected override string UpdatePermissionCode => "PERMISSION_UPDATE";
        protected override string DeletePermissionCode => "PERMISSION_DELETE";
        protected override string ViewPermissionCode => "PERMISSION_VIEW";

        protected override string EntityName => "Permission";

        // DAL method implementations

        protected override int DalCreate(Permission entity)
            => PermissionData.Insert(entity);

        protected override bool DalUpdate(Permission entity)
            => PermissionData.Update(entity);

        protected override bool DalDelete(int id)
            => PermissionData.GetById(id) != null && PermissionData.Delete(id);

        protected override Permission? DalGetById(int id)
            => PermissionData.GetById(id);

        protected override IEnumerable<Permission> DalGetAll()
            => PermissionData.GetAll();

        protected override int GetEntityId(Permission entity)
            => entity.PermissionId;

        protected override ValidationResult.ValidationResult IsValidateData(Permission entity)
        {
            var validation = ValidationResult.ValidationResult.Success();

            // Code validation
            if (string.IsNullOrWhiteSpace(entity.Code))
                validation.Add("Code cannot be empty.");

            // Unique Code check (ignore current entity if updating)
            bool codeExists = PermissionData.IsCodeExist(entity.Code, entity.PermissionId == 0 ? null : entity.PermissionId);
            if (codeExists)
                validation.Add("Code already exists.");

            // Name validation
            if (string.IsNullOrWhiteSpace(entity.Name))
                validation.Add("Name cannot be empty.");

            return validation;
        }



        public Result<Permission> FindByCode(string code)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
            {
                return Result<Permission>.Fail("Access Denied!");
            }

            Permission role = PermissionData.GetByCode(code);


            // Here We should Put Audit 


            return Result<Permission>.Ok(role);

        }

        protected override string GetAuditMessage(string operation, Permission entity)
            => $"{EntityName} [{entity.PermissionId}] {operation} performed.";
    }

}
