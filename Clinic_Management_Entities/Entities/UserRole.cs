using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities
{
    public sealed class UserRole
    {
        [DbColumn("UserId")]
        public int UserId { get; set; }

        [DbColumn("RoleId")]
        public int RoleId { get; set; }

        [DbColumn("AssignedAt")]
        public DateTime AssignedAt { get; set; }

        [DbColumn("AssignedByUserId")]
        public int? AssignedByUserId { get; set; }
    }

}
