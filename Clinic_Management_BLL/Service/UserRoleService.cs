using Clinic_Management_BLL.AuditWritter;
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
    public sealed class UserRoleService : BaseCrudService<UserRole>
    {
        // Assume UserRole has properties: UserId, RoleId, AssignedAt, AssignedByUserId

        // ------------------- DAL Implementation -------------------

        protected override int DalCreate(UserRole entity)
        {
            // Call your DAL Insert method here, returns success or new id
            // Since UserRole is a join table (UserId + RoleId) maybe return 1 if success
            bool success = UserRoleData.Insert(entity);
            return success ? 1 : 0;
        }

        protected override bool DalUpdate(UserRole entity)
        {
            // Usually UserRole join tables don't update, but if needed implement
            // Or return false if no update allowed
            return false; // or implement if you want to support update
        }

        protected override bool DalDelete(int UserID)
        {
            // Here, UserRole does not have a single Id, but composite key (UserId, RoleId)
            // So, you can’t use single int id. Instead, you must implement Delete by (UserId, RoleId)
            // For this blueprint, assume id is UserId, and you want to delete all roles for that user.

            return false; // you need to add this DAL method
        }

        protected override UserRole? DalGetById(int id)
        {
            // Same as Delete, no single id.
            // Instead, either throw or return null.
            // Or implement GetByUserId returning first role or null
            var roles = UserRoleData.GetByUserId(id);
            return roles.FirstOrDefault();
        }

        protected override IEnumerable<UserRole> DalGetAll()
        {
            // Retrieve all user-role pairs
            return UserRoleData.GetAll (); // You need to implement this in DAL
        }

        protected override ValidationResult.ValidationResult IsValidateData(UserRole entity)
        {
            var result = ValidationResult.ValidationResult.Success();

            if (entity.UserId <= 0)
                result.Add("Invalid UserId.");

            if (entity.RoleId <= 0)
                result.Add("Invalid RoleId.");

            // Additional business rules, e.g., no duplicate roles, etc.
            // You can call Exists to check duplicates

            if (UserRoleData.Exists(entity.UserId))
                result.Add("A role is already assigned to the user.");

            return result;
        }

        protected override int GetEntityId(UserRole entity)
            => entity.UserId; // Note: Composite keys can be tricky — consider special handling

        // ------------------- Permission Codes -------------------

        protected override string CreatePermissionCode => "USER_ROLE_CREATE";
        protected override string UpdatePermissionCode => "USER_ROLE_UPDATE";
        protected override string DeletePermissionCode => "USER_ROLE_DELETE";
        protected override string ViewPermissionCode => "USER_ROLE_VIEW";

        // ------------------- Audit Message -------------------

        protected override string GetAuditMessage(string operation, UserRole entity)
            => $"{operation} UserRole: UserId={entity.UserId}, RoleId={entity.RoleId}";

        // ------------------- Custom Methods -------------------

        public Result AssignRole(int userId, int roleId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result.Fail("Permission denied.");

            var entity = new UserRole
            {
                UserId = userId,
                RoleId = roleId,
                AssignedAt = DateTime.UtcNow,
                AssignedByUserId = SecurityContext.Current.UserId
            };

            var validation = IsValidateData(entity);
            if (!validation.IsValid)
                return Result.Fail(validation.Errors);

            if(UserRoleData.Exists(userId))
            {
               if(! UserRoleData.Delete(userId))
                {
                    return Result.Fail("The Existence Role Doesn't Delete");
                }
            }

            var success = UserRoleData.Insert(entity);

            AuditWriter.Write(
                action: GetAuditMessage("ASSIGN_ROLE", entity),
                performedBy: SecurityContext.Current.UserId,
                entityType: "UserRole",
                entityId: $"{userId}_{roleId}",
                success: success,
                newEntity: entity,
                failureReason: success ? null : "Failed to assign role."
            );

            return success ? Result.Ok() : Result.Fail("Failed to assign role.");
        }

        public Result RemoveRole(int userId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

          var entity =   UserRoleData.GetByUserId(userId).First();

            var success = UserRoleData.Delete(userId);

            AuditWriter.Write(
                action: GetAuditMessage("REMOVE_ROLE", entity),
                performedBy: SecurityContext.Current.UserId,
                entityType: "UserRole",
                entityId: $"{userId}",
                success: success,
                oldEntity: entity,
                failureReason: success ? null : "Failed to remove role."
            );

            return success ? Result.Ok() : Result.Fail("Failed to remove role.");
        }

        public Result<IEnumerable<UserRole>> GetRolesForUser(int userId, int performedBy)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(performedBy, ViewPermissionCode))
                return Result<IEnumerable<UserRole>>.Fail("Permission denied.");

            var roles = UserRoleData.GetByUserId(userId);
            return Result<IEnumerable<UserRole>>.Ok(roles);
        }

        public Result<IEnumerable<UserRole>> GetUsersForRole(int roleId, int performedBy)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(performedBy, ViewPermissionCode))
                return Result<IEnumerable<UserRole>>.Fail("Permission denied.");

            var users = UserRoleData.GetByRoleId(roleId);
            return Result<IEnumerable<UserRole>>.Ok(users);
        }
    }


}
