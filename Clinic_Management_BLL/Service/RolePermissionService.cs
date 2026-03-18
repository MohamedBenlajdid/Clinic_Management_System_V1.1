using Clinic_Management_BLL.AuditWritter;
using Clinic_Management_BLL.LoginProcess;
using Clinic_Management_BLL.ResultWraper;
using Clinic_Management_DAL.Data;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.Service
{
    public class RolePermissionService
    {
        private const string EntityName = nameof(RolePermission);

        // Permission codes for controlling access to RolePermission management
        public string CreatePermissionCode => "PERMISSION_CREATE";
        public string UpdatePermissionCode => "PERMISSION_UPDATE";
        public string DeletePermissionCode => "PERMISSION_DELETE";
        public string ViewPermissionCode => "PERMISSION_VIEW";

        // Validation for RolePermission entity
        protected ValidationResult.ValidationResult Validate(RolePermission entity)
        {
            var result = ValidationResult.ValidationResult.Success();

            if (entity.RoleId <= 0)
                result.Add("Invalid RoleId.");

            if (entity.PermissionId <= 0)
                result.Add("Invalid PermissionId.");

            // IsGranted is bool, so no further validation needed here

            return result;
        }

        // Create or update RolePermission (Upsert)
        public Result SetPermission(RolePermission entity)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result.Fail("Permission denied.");

            var validation = Validate(entity);
            if (!validation.IsValid)
                return Result.Fail(validation.Errors);

            bool success;
            string operation;

            var exists = RolePermissionData.Exists(entity.RoleId, entity.PermissionId);

            if (exists)
            {
                if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                    return Result.Fail("Permission denied for update.");

                success = RolePermissionData.Update(entity);
                operation = "UPDATE";
            }
            else
            {
                success = RolePermissionData.Insert(entity);
                operation = "CREATE";
            }

            AuditWriter.Write(
                action: GetAuditMessage(operation, entity),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: $"{entity.RoleId}-{entity.PermissionId}",
                success: success,
                newEntity: entity,
                failureReason: success ? null : $"{operation} operation failed."
            );

            return success
                ? Result.Ok($"{EntityName} {operation} successful.")
                : Result.Fail($"{EntityName} {operation} failed.");
        }

        // Grant permission (convenience method)
        public Result Grant(int roleId, int permissionId)
        {
            var entity = new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId,
                IsGranted = true,
                CreatedAt = DateTime.UtcNow
            };

            return SetPermission(entity);
        }

        // Revoke permission (convenience method)
        public Result Revoke(int roleId, int permissionId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied for revoke.");

            var existing = RolePermissionData.GetByIds(roleId, permissionId);
            if (existing == null)
                return Result.Fail($"{EntityName} not found.");

            var success = RolePermissionData.Revoke(roleId, permissionId);

            AuditWriter.Write(
                action: $"{EntityName}.REVOKE",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: $"{roleId}-{permissionId}",
                success: success,
                oldEntity: existing,
                failureReason: success ? null : "Revoke operation failed."
            );

            return success
                ? Result.Ok($"{EntityName} revoked successfully.")
                : Result.Fail($"{EntityName} revoke failed.");
        }

        // Delete RolePermission explicitly
        public Result Delete(int roleId, int permissionId, int performedBy)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(performedBy, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            var existing = RolePermissionData.GetByIds(roleId, permissionId);
            if (existing == null)
                return Result.Fail($"{EntityName} not found.");

            var success = RolePermissionData.Delete(roleId, permissionId);

            AuditWriter.Write(
                action: $"{EntityName}.DELETE",
                performedBy: performedBy,
                entityType: EntityName,
                entityId: $"{roleId}-{permissionId}",
                success: success,
                oldEntity: existing,
                failureReason: success ? null : "Delete operation failed."
            );

            return success
                ? Result.Ok($"{EntityName} deleted successfully.")
                : Result.Fail($"{EntityName} delete failed.");
        }

        // Get RolePermission by RoleId and PermissionId
        public Result<RolePermission> GetByIds(int roleId, int permissionId, int performedBy)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(performedBy, ViewPermissionCode))
                return Result<RolePermission>.Fail("Permission denied.");

            var entity = RolePermissionData.GetByIds(roleId, permissionId);
            return entity == null
                ? Result<RolePermission>.Fail($"{EntityName} not found.")
                : Result<RolePermission>.Ok(entity);
        }

        // Get all RolePermissions for a Role
        public Result<IEnumerable<RolePermission>> GetByRoleId(int roleId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<RolePermission>>.Fail("Permission denied.");

            var list = RolePermissionData.GetByRoleId(roleId);
            return Result<IEnumerable<RolePermission>>.Ok(list);
        }

            // =====================================================
            // Returns ONLY granted permissions coming from roles
            // =====================================================
            public static Result<HashSet<int>> GetPermissionsForUser(int userId)
            {
                var roles = UserRoleData.GetByUserId(userId);

                if (roles == null || !roles.Any())
                    return Result<HashSet<int>>.Ok(new HashSet<int>());

                var permissionIds = new HashSet<int>();

                foreach (var role in roles)
                {
                    var rolePermissions = RolePermissionData.GetByRoleId(role.RoleId);

                    foreach (var perm in rolePermissions)
                    {
                        if (perm.IsGranted)
                            permissionIds.Add(perm.PermissionId);
                    }
                }

                return Result<HashSet<int>>.Ok(permissionIds);
            }


        // Get all RolePermissions for a Permission
        public Result<IEnumerable<RolePermission>> GetByPermissionId(int permissionId, int performedBy)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(performedBy, ViewPermissionCode))
                return Result<IEnumerable<RolePermission>>.Fail("Permission denied.");

            var list = RolePermissionData.GetByPermissionId(permissionId);
            return Result<IEnumerable<RolePermission>>.Ok(list);
        }

        // Check if role has granted permission
        public bool HasPermission(int roleId, int permissionId)
        {
            return RolePermissionData.HasPermission(roleId, permissionId);
        }

        // Helper for audit messages
        public string GetAuditMessage(string operation, RolePermission entity)
        {
            return $"{EntityName}.{operation} - RoleId: {entity.RoleId}, PermissionId: {entity.PermissionId}, IsGranted: {entity.IsGranted}";
        }
    }

}
