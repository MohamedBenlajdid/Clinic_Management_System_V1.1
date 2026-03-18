using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_DAL.Data
{

  



    public static class UserPermissionOverrideData
    {

        private const string Columns = @"
UserId, PermissionId, OverrideType,
Reason, ExpiresAt, CreatedAt, CreatedByUserId";

        public enum PermissionOverrideType : byte
        {
            Grant = 1,
            Deny = 2
        }

        public sealed class UserPermissionOverrideInfo
        {
            public string PermissionCode { get; init; }
            public PermissionOverrideType OverrideType { get; init; }
        }


        // ===============================
        // Exists
        // ===============================
        public static bool Exists(int userId, int permissionId)
        {
            string query = @"
SELECT 1
FROM UserPermissionOverrides
WHERE UserId = @UserId
AND PermissionId = @PermissionId";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read();
                },
                SqlParameterFactory.Create("@UserId", userId),
                SqlParameterFactory.Create("@PermissionId", permissionId)
            );
        }

        // ===============================
        // Insert
        // ===============================
        public static bool Insert(UserPermissionOverride entity)
        {
            string query = @"
INSERT INTO UserPermissionOverrides
(
    UserId, PermissionId, OverrideType,
    Reason, ExpiresAt, CreatedAt, CreatedByUserId
)
VALUES
(
    @UserId, @PermissionId, @OverrideType,
    @Reason, @ExpiresAt, @CreatedAt, @CreatedByUserId
)";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@UserId", entity.UserId),
                SqlParameterFactory.Create("@PermissionId", entity.PermissionId),
                SqlParameterFactory.Create("@OverrideType", entity.OverrideType),
                SqlParameterFactory.Create("@Reason", entity.Reason),
                SqlParameterFactory.Create("@ExpiresAt", entity.ExpiresAt),
                SqlParameterFactory.Create("@CreatedAt", entity.CreatedAt),
                SqlParameterFactory.Create("@CreatedByUserId", entity.CreatedByUserId)
            );
        }

        // ===============================
        // Update
        // ===============================
        public static bool Update(UserPermissionOverride entity)
        {
            string query = @"
UPDATE UserPermissionOverrides SET
    OverrideType = @OverrideType,
    Reason = @Reason,
    ExpiresAt = @ExpiresAt
WHERE UserId = @UserId
AND PermissionId = @PermissionId";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@OverrideType", entity.OverrideType),
                SqlParameterFactory.Create("@Reason", entity.Reason),
                SqlParameterFactory.Create("@ExpiresAt", entity.ExpiresAt),
                SqlParameterFactory.Create("@UserId", entity.UserId),
                SqlParameterFactory.Create("@PermissionId", entity.PermissionId)
            );
        }

        // ===============================
        // Delete
        // ===============================
        public static bool Delete(int userId, int permissionId)
        {
            string query = @"
DELETE FROM UserPermissionOverrides
WHERE UserId = @UserId
AND PermissionId = @PermissionId";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@UserId", userId),
                SqlParameterFactory.Create("@PermissionId", permissionId)
            );
        }

        // ===============================
        // Get By IDs
        // ===============================
        public static UserPermissionOverride GetByIds(int userId, int permissionId)
        {
            string query = $@"
SELECT {Columns}
FROM UserPermissionOverrides
WHERE UserId = @UserId
AND PermissionId = @PermissionId";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<UserPermissionOverride>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@UserId", userId),
                SqlParameterFactory.Create("@PermissionId", permissionId)
            );
        }

        // ===============================
        // Get By User
        // ===============================
        public static IEnumerable<UserPermissionOverride> GetByUserId(int userId)
        {
            string query = $@"
SELECT {Columns}
FROM UserPermissionOverrides
WHERE UserId = @UserId";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<UserPermissionOverride>();

                    while (reader.Read())
                        list.Add(DbMapper<UserPermissionOverride>.Map(reader));

                    return list;
                },
                SqlParameterFactory.Create("@UserId", userId)
            );
        }

        // ===============================
        // Grant
        // ===============================
        public static bool Grant(int userId, int permissionId, int? createdBy = null, string reason = null)
        {
            return SetPermission(
                userId,
                permissionId,
                PermissionOverrideType.Grant,
                createdBy,
                reason
            );
        }

        // ===============================
        // Deny
        // ===============================
        public static bool Deny(int userId, int permissionId, int? createdBy = null, string reason = null)
        {
            return SetPermission(
                userId,
                permissionId,
                PermissionOverrideType.Deny,
                createdBy,
                reason
            );
        }

        // ===============================
        // Set Permission (Core Method)
        // ===============================
        public static bool SetPermission(
            int userId,
            int permissionId,
            PermissionOverrideType type,
            int? createdBy = null,
            string reason = null,
            DateTime? expiresAt = null)
        {
            if (Exists(userId, permissionId))
            {
                return Update(new UserPermissionOverride
                {
                    UserId = userId,
                    PermissionId = permissionId,
                    OverrideType = (byte)type,
                    Reason = reason,
                    ExpiresAt = expiresAt
                });
            }

            return Insert(new UserPermissionOverride
            {
                UserId = userId,
                PermissionId = permissionId,
                OverrideType = (byte)type,
                Reason = reason,
                ExpiresAt = expiresAt,
                CreatedAt = DateTime.UtcNow,
                CreatedByUserId = createdBy
            });
        }

        // ===============================
        // Has Explicit Grant
        // ===============================
        public static bool HasGrant(int userId, int permissionId)
        {
            string query = @"
SELECT 1
FROM UserPermissionOverrides
WHERE UserId = @UserId
AND PermissionId = @PermissionId
AND OverrideType = 1
AND (ExpiresAt IS NULL OR ExpiresAt > SYSUTCDATETIME())";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read();
                },
                SqlParameterFactory.Create("@UserId", userId),
                SqlParameterFactory.Create("@PermissionId", permissionId)
            );
        }


            public static IEnumerable<UserPermissionOverrideInfo> GetActiveByUserId(int userId)
            {
                const string query = @"
SELECT 
    p.PermissionCode,
    uo.OverrideType
FROM UserPermissionOverrides uo
INNER JOIN Permissions p
    ON uo.PermissionId = p.PermissionId
WHERE uo.UserId = @UserId
AND p.IsActive = 1
AND (uo.ExpiresAt IS NULL OR uo.ExpiresAt > SYSUTCDATETIME());";

                return DbExecutor.Execute(
                    query,
                    cmd =>
                    {
                        using var reader = cmd.ExecuteReader();

                        var list = new List<UserPermissionOverrideInfo>(8);

                        while (reader.Read())
                        {
                            list.Add(new UserPermissionOverrideInfo
                            {
                                PermissionCode = reader.GetString(0),
                                OverrideType = (PermissionOverrideType)reader.GetByte(1)
                            });
                        }

                        return list;
                    },
                    SqlParameterFactory.Create("@UserId", userId)
                );
            }
       

        // ===============================
        // Has Explicit Deny
        // ===============================
        public static bool HasDeny(int userId, int permissionId)
        {
            string query = @"
SELECT 1
FROM UserPermissionOverrides
WHERE UserId = @UserId
AND PermissionId = @PermissionId
AND OverrideType = 2
AND (ExpiresAt IS NULL OR ExpiresAt > SYSUTCDATETIME())";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read();
                },
                SqlParameterFactory.Create("@UserId", userId),
                SqlParameterFactory.Create("@PermissionId", permissionId)
            );
        }

        // ===============================
        // Delete All By User
        // ===============================
        public static bool DeleteAllByUser(int userId)
        {
            string query = @"
DELETE FROM UserPermissionOverrides
WHERE UserId = @UserId";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@UserId", userId)
            );
        }
    }

}
