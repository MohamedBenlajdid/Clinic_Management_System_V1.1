using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class RolePermissionData
    {
        private const string Columns = @"
     RoleId,PermissionId,IsGranted,CreatedAt";

        // === Check if RolePermission exists ===
        public static bool Exists(int roleId, int permissionId)
        {
            string query = @"
SELECT 1
FROM RolePermissions
WHERE RoleId = @RoleId AND PermissionId = @PermissionId";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read();
                },
                SqlParameterFactory.Create("@RoleId", roleId),
                SqlParameterFactory.Create("@PermissionId", permissionId)
            );
        }

        // === Insert RolePermission ===
        public static bool Insert(RolePermission rolePermission)
        {
            string query = @"
INSERT INTO RolePermissions
(
    RoleId, PermissionId, IsGranted, CreatedAt
)
VALUES
(
    @RoleId, @PermissionId, @IsGranted, @CreatedAt
)";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@RoleId", rolePermission.RoleId),
                SqlParameterFactory.Create("@PermissionId", rolePermission.PermissionId),
                SqlParameterFactory.Create("@IsGranted", rolePermission.IsGranted),
                SqlParameterFactory.Create("@CreatedAt", rolePermission.CreatedAt)
            );
        }

        // === Update RolePermission ===
        public static bool Update(RolePermission rolePermission)
        {
            string query = @"
UPDATE RolePermissions SET
    IsGranted = @IsGranted
WHERE RoleId = @RoleId AND PermissionId = @PermissionId";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@IsGranted", rolePermission.IsGranted),
                SqlParameterFactory.Create("@RoleId", rolePermission.RoleId),
                SqlParameterFactory.Create("@PermissionId", rolePermission.PermissionId)
            );
        }

        // === Delete RolePermission ===
        public static bool Delete(int roleId, int permissionId)
        {
            string query = @"
DELETE FROM RolePermissions
WHERE RoleId = @RoleId AND PermissionId = @PermissionId";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@RoleId", roleId),
                SqlParameterFactory.Create("@PermissionId", permissionId)
            );
        }

        // === Get RolePermission by RoleId and PermissionId ===
        public static RolePermission GetByIds(int roleId, int permissionId)
        {
            string query = $@"
SELECT {Columns}
FROM RolePermissions
WHERE RoleId = @RoleId AND PermissionId = @PermissionId";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<RolePermission>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@RoleId", roleId),
                SqlParameterFactory.Create("@PermissionId", permissionId)
            );
        }

        // === Get all Permissions for a Role ===
        public static IEnumerable<RolePermission> GetByRoleId(int roleId)
        {
            string query = $@"
SELECT {Columns}
FROM RolePermissions
WHERE RoleId = @RoleId";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<RolePermission>();
                    while (reader.Read())
                    {
                        list.Add(DbMapper<RolePermission>.Map(reader));
                    }
                    return list;
                },
                SqlParameterFactory.Create("@RoleId", roleId)
            );
        }

        public static IEnumerable<string> GetPermissionsByUserId(int userId)
        {
            const string query = @"
SELECT DISTINCT p.PermissionCode
FROM UserRoles ur
INNER JOIN RolePermissions rp 
    ON ur.RoleId = rp.RoleId
INNER JOIN Permissions p 
    ON rp.PermissionId = p.PermissionId
WHERE ur.UserId = @UserId
AND p.IsActive = 1;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();

                    var list = new List<string>(32); // small pre-allocation

                    while (reader.Read())
                        list.Add(reader.GetString(0));

                    return list;
                },
                SqlParameterFactory.Create("@UserId", userId)
            );
        }

        // === Get all Roles for a Permission ===
        public static IEnumerable<RolePermission> GetByPermissionId(int permissionId)
        {
            string query = $@"
SELECT {Columns}
FROM RolePermissions
WHERE PermissionId = @PermissionId";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<RolePermission>();
                    while (reader.Read())
                    {
                        list.Add(DbMapper<RolePermission>.Map(reader));
                    }
                    return list;
                },
                SqlParameterFactory.Create("@PermissionId", permissionId)
            );

        }

        public static bool Grant(int roleId, int permissionId)
        {
            if (Exists(roleId, permissionId))
            {
                return Update(new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = permissionId,
                    IsGranted = true
                });
            }

            return Insert(new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId,
                IsGranted = true,
                CreatedAt = DateTime.UtcNow
            });
        }



        public static bool Revoke(int roleId, int permissionId)
        {
            string query = @"
UPDATE RolePermissions
SET IsGranted = 0
WHERE RoleId = @RoleId AND PermissionId = @PermissionId";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@RoleId", roleId),
                SqlParameterFactory.Create("@PermissionId", permissionId)
            );
        }

        public static bool SetPermission(int roleId, int permissionId, bool isGranted)
        {
            if (Exists(roleId, permissionId))
            {
                return Update(new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = permissionId,
                    IsGranted = isGranted
                });
            }

            return Insert(new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId,
                IsGranted = isGranted,
                CreatedAt = DateTime.UtcNow
            });
        }


        public static bool HasPermission(int roleId, int permissionId)
        {
            string query = @"
SELECT 1
FROM RolePermissions
WHERE RoleId = @RoleId
  AND PermissionId = @PermissionId
  AND IsGranted = 1";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read();
                },
                SqlParameterFactory.Create("@RoleId", roleId),
                SqlParameterFactory.Create("@PermissionId", permissionId)
            );
        }

        public static bool DeleteAllByRole(int roleId)
        {
            string query = @"
DELETE FROM RolePermissions
WHERE RoleId = @RoleId";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@RoleId", roleId)
            );
        }




    }

}
