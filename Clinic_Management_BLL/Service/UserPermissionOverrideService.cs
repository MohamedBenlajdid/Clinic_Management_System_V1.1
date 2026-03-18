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
    public class UserPermissionOverrideService
    {
        private const string EntityName = nameof(UserPermissionOverride);

        // Permission codes to override as needed
        public string CreatePermissionCode => "PERMISSION_CREATE";
        public string UpdatePermissionCode => "PERMISSION_UPDATE";
        public string DeletePermissionCode => "PERMISSION_DELETE";
        public string ViewPermissionCode => "PERMISSION_VIEW";

        // Validate override entity data
        protected ValidationResult.ValidationResult Validate(UserPermissionOverride entity)
        {
            var result = ValidationResult.ValidationResult.Success();

            if (entity.UserId <= 0)
                result.Add("Invalid UserId.");

            if (entity.PermissionId <= 0)
                result.Add("Invalid PermissionId.");

            if (entity.OverrideType != (byte)UserPermissionOverrideData.PermissionOverrideType.Grant &&
                entity.OverrideType != (byte)UserPermissionOverrideData.PermissionOverrideType.Deny)
                result.Add("Invalid OverrideType.");

            return result;
        }

        // Create or update (upsert) permission override
        public Result SetPermission(UserPermissionOverride entity)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(
                SecurityContext.Current.UserId, CreatePermissionCode))
                return Result.Fail("Permission denied.");

            var validation = Validate(entity);
            if (!validation.IsValid)
                return Result.Fail(validation.Errors);

            var exists = UserPermissionOverrideData.Exists(entity.UserId, entity.PermissionId);

            bool success;
            string operation;

            if (exists)
            {
                // Update existing
                if (!PermissionChecker.PermissionChecker.HasPermission(
                    SecurityContext.Current.UserId, UpdatePermissionCode))
                    return Result.Fail("Permission denied for update.");

                success = UserPermissionOverrideData.Update(entity);
                operation = "UPDATE";
            }
            else
            {
                // Insert new
                success = UserPermissionOverrideData.Insert(entity);
                operation = "CREATE";
            }

            AuditWriter.Write(
                action: GetAuditMessage(operation, entity),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: $"{entity.UserId}-{entity.PermissionId}",
                success: success,
                newEntity: entity,
                failureReason: success ? null : $"{operation} operation failed."
            );

            return success
                ? Result.Ok($"{EntityName} {operation} successful.")
                : Result.Fail($"{EntityName} {operation} failed.");
        }

        // Delete override
        public Result Delete(int userId, int permissionId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(
                SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            var existing = UserPermissionOverrideData.GetByIds(userId, permissionId);
            if (existing == null)
                return Result.Fail($"{EntityName} not found.");

            var success = UserPermissionOverrideData.Delete(userId, permissionId);

            AuditWriter.Write(
                action: $"{EntityName}.DELETE",
                performedBy:
                SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: $"{userId}-{permissionId}",
                success: success,
                oldEntity: existing,
                failureReason: success ? null : "Delete operation failed."
            );

            return success
                ? Result.Ok($"{EntityName} deleted successfully.")
                : Result.Fail($"{EntityName} delete failed.");
        }

        // Get a single override
        public Result<UserPermissionOverride> GetByIds(int userId, int permissionId, int performedBy)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(performedBy, ViewPermissionCode))
                return Result<UserPermissionOverride>.Fail("Permission denied.");

            var entity = UserPermissionOverrideData.GetByIds(userId, permissionId);
            return entity == null
                ? Result<UserPermissionOverride>.Fail($"{EntityName} not found.")
                : Result<UserPermissionOverride>.Ok(entity);
        }

        // Get all overrides by user
        public Result<IEnumerable<UserPermissionOverride>> GetByUserId(int userId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<UserPermissionOverride>>.Fail("Permission denied.");

            var list = UserPermissionOverrideData.GetByUserId(userId);
            return Result<IEnumerable<UserPermissionOverride>>.Ok(list);
        }

        public static Result<List<UserPermissionOverride>> GetActiveOverrides(int userId)
        {
            var overrides = UserPermissionOverrideData.GetByUserId(userId);

            if (overrides == null)
                return Result<List<UserPermissionOverride>>.Ok(new List<UserPermissionOverride>());

            var now = DateTime.UtcNow;

            var active = overrides
                .Where(o => !o.ExpiresAt.HasValue || o.ExpiresAt > now)
                .ToList();

            return Result<List<UserPermissionOverride>>.Ok(active);
        }

        // Helper for audit messages
        public string GetAuditMessage(string operation, UserPermissionOverride entity)
        {
            return $"{EntityName}.{operation} - UserId: {entity.UserId}, PermissionId: {entity.PermissionId}, OverrideType: {(UserPermissionOverrideData.PermissionOverrideType)entity.OverrideType}";
        }
    }



}
