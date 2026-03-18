using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities
{
    public sealed class RolePermission
    {
        [DbColumn("RoleId")]
        public int RoleId { get; set; }

        [DbColumn("PermissionId")]
        public int PermissionId { get; set; }

        [DbColumn("IsGranted")]
        public bool IsGranted { get; set; }

        [DbColumn("CreatedAt")]
        public DateTime CreatedAt { get; set; }
    }

}
