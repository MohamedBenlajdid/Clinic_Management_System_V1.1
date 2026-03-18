using Clinic_Management_BLL.CrudInterface;
using Clinic_Management_BLL.LoginProcess;
using Clinic_Management_BLL.ResultWraper;
using Clinic_Management_DAL.Data;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Clinic_Management_BLL.Service
{
    public class StaffService : BaseCrudService<Staff>
    {
        // Permission codes (customize as needed)
        protected override string CreatePermissionCode => "STAFF_CREATE";
        protected override string UpdatePermissionCode => "STAFF_UPDATE";
        protected override string DeletePermissionCode => "STAFF_DELETE";
        protected override string ViewPermissionCode => "STAFF_VIEW";

        protected override string EntityName => "Staff";

        // DAL method implementations

        protected override int DalCreate(Staff entity)
            => StaffData.Insert(entity);

        protected override bool DalUpdate(Staff entity)
            => StaffData.Update(entity);

        protected override bool DalDelete(int id)
            => StaffData.GetById(id) != null && StaffData.Delete(id);

        protected override Staff? DalGetById(int id)
            => StaffData.GetById(id);

        protected override IEnumerable<Staff> DalGetAll()
            => StaffData.GetAll();

        protected override int GetEntityId(Staff entity)
            => entity.StaffId;

        protected override ValidationResult.ValidationResult IsValidateData(Staff entity)
        {
            var validation = ValidationResult.ValidationResult.Success();

            if (entity.PersonId <= 0 || PersonData.GetById(entity.PersonId) == null)
                validation.Add("Person Not Existed In the system");

            // StaffCode validation
            if (string.IsNullOrWhiteSpace(entity.StaffCode))
                validation.Add("StaffCode cannot be empty.");

            // Unique StaffCode check (ignore current entity if updating)
            bool staffCodeExists = StaffData.IsStaffCodeExist(entity.StaffCode, entity.StaffId == 0 ? null : entity.StaffId);
            if (staffCodeExists)
                validation.Add("StaffCode already exists.");

            // PersonId must not already be linked to another Staff (except itself)
            bool PersonExists = StaffData.IsPersonIDExist(entity.PersonId, entity.StaffId == 0 ? null : entity.StaffId);
            if (PersonExists)
                validation.Add("PersonId is already associated with another Staff.");

            // Additional validations can go here (e.g., DepartmentId required)

            return validation;
        }

        public Result<Staff> GetByPersonID(int personId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<Staff>.Fail("Permission denied.");


            if (personId <= 0)
            {
                return Result<Staff>.Fail("Person Not Exist");
            }

            Staff staff = StaffData.GetByPersonId(personId);
            if(staff == null)
            {
                return Result<Staff>.Fail("Staff Not Exist");
            }
            return Result<Staff>.Ok(staff);

        }

        public Result<Staff> GetByStuffCode(string StuffCode)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<Staff>.Fail("Permission denied.");


            if (string.IsNullOrEmpty(StuffCode))
            {
                return Result<Staff>.Fail("Stuff Code Is Empty");
            }

            Staff staff = StaffData.GetByStaffCode(StuffCode);
            if (staff == null)
            {
                return Result<Staff>.Fail("Staff Not Exist");
            }
            return Result<Staff>.Ok(staff);

        }


        protected override string GetAuditMessage(string operation, Staff entity)
            => $"{EntityName} [{entity.StaffId}] {operation} performed.";

    }

}
