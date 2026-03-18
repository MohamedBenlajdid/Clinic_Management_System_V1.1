using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class UserRoleData
    {
        private const string Columns = @"
     UserId,RoleId,AssignedAt,AssignedByUserId";

        // === Check if Role assigned to User exists ===
        public static bool Exists(int userId)
        {
            string query = @"
        SELECT 1
        FROM UserRoles
        WHERE UserId = @UserId ";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read();
                },
                SqlParameterFactory.Create("@UserId", userId)
            );
        }

        // === Assign Role to User ===
        public static bool Insert(UserRole userRole)
        {
            string query = @"
INSERT INTO UserRoles
(
    UserId, RoleId, AssignedAt, AssignedByUserId
)
VALUES
(
    @UserId, @RoleId, @AssignedAt, @AssignedByUserId
)";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,

                SqlParameterFactory.Create("@UserId", userRole.UserId),
                SqlParameterFactory.Create("@RoleId", userRole.RoleId),
                SqlParameterFactory.Create("@AssignedAt", userRole.AssignedAt),
                SqlParameterFactory.Create("@AssignedByUserId", (object)userRole.AssignedByUserId ?? DBNull.Value)
            );
        }

        // === Remove Role from User ===
        public static bool Delete(int userId)
        {
            string query = @"
DELETE FROM UserRoles
WHERE UserId = @UserId ";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@UserId", userId)
            );
        }

  
        // === Get Roles assigned to a User ===
        public static IEnumerable<UserRole> GetByUserId(int userId)
        {
            string query = $@"
        SELECT {Columns}
        FROM UserRoles
        WHERE UserId = @UserId";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<UserRole>();
                    while (reader.Read())
                    {
                        list.Add(DbMapper<UserRole>.Map(reader));
                    }
                    return list;
                },
                SqlParameterFactory.Create("@UserId", userId)
            );
        }


        public static IEnumerable<UserRole> GetAll()
        {
            string query = $@"
SELECT {Columns}
FROM UserRoles";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<UserRole>();
                    while (reader.Read())
                    {
                        list.Add(DbMapper<UserRole>.Map(reader));
                    }
                    return list;
                }
            );
        }


        // === Get Users assigned to a Role ===
        public static IEnumerable<UserRole> GetByRoleId(int roleId)
        {
            string query = $@"
        SELECT {Columns}
        FROM UserRoles
        WHERE RoleId = @RoleId";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<UserRole>();
                    while (reader.Read())
                    {
                        list.Add(DbMapper<UserRole>.Map(reader));
                    }
                    return list;
                },
                SqlParameterFactory.Create("@RoleId", roleId)
            );
        }


        public static bool HasRole(int userId)
        {
            string query = @"
SELECT 1
FROM UserRoles
WHERE UserId = @UserId ";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read();
                },
                SqlParameterFactory.Create("@UserId", userId)
                
            );
        }

        public static bool SetUserRole(int userId, int roleId, bool assign, int? assignedByUserId = null)
        {
            if (assign)
            {
                if (!Exists(userId))
                {
                    return Insert(new UserRole
                    {
                        UserId = userId,
                        RoleId = roleId,
                        AssignedAt = DateTime.UtcNow,
                        AssignedByUserId = assignedByUserId
                    });
                }
                return true; // already assigned
            }
            else
            {
                return Delete(userId);
            }
        }




    }

}
