using Clinic_Management_BLL.LoginProcess;
using Clinic_Management_BLL.Service;
using Clinic_Management_DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.PermissionChecker
{
    public static class PermissionChecker
    {
        public static bool HasPermission(int userId, string permissionCode)
        {
            if(!PermissionData.IsCodeExist(permissionCode))
            {
               int NewPerID =  PermissionData.Insert(
                    new Clinic_Management_Entities.Permission
                    {
                        Code = permissionCode,
                        Description = "Allows User To " + permissionCode.ToLower(),
                        Name = permissionCode.ToLower(),
                        IsActive = true,
                        Module = "ADMIN"
                    });

                if(NewPerID<=0)
                {
                    throw new Exception("New Permission Not Created Succesfuly!");
                }

            }


            if (SecurityContext.HasPermission(permissionCode))
            {
                return true;
            }
            return false;


            // Step 1: Map permissionCode to PermissionId
            int? permissionId = PermissionData.GetByCode(permissionCode).PermissionId;
            if (permissionId == null)
                return false; // Permission code unknown

            // Step 2: Check explicit user permission overrides
            var userOverride = UserPermissionOverrideData.GetByIds(userId, permissionId.Value);
            if (userOverride != null)
            {
                if (userOverride.OverrideType == (byte)UserPermissionOverrideData.PermissionOverrideType.Deny)
                    return false; // Explicitly denied

                if (userOverride.OverrideType == (byte)UserPermissionOverrideData.PermissionOverrideType.Grant)
                    return true; // Explicitly granted
            }

            // Step 3: Check user's roles and their permissions
            var roles = UserRoleData.GetByUserId(userId);
            foreach (var role in roles)
            {
                bool roleHasPermission = RolePermissionData.HasPermission(role.RoleId, permissionId.Value);
                if (roleHasPermission)
                    return true; // Granted via role
            }

            // Step 4: Default deny if no explicit grant found
            return false;
        }
    }

}
