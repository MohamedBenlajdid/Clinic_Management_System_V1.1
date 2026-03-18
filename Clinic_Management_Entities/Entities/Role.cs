using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities
{
    public sealed class Role
    {
        [DbColumn("RoleId")]
        public int RoleId { get; set; }

        [DbColumn("Code")]
        public string Code { get; set; } = null!;

        [DbColumn("Name")]
        public string Name { get; set; } = null!;

        [DbColumn("Description")]
        public string? Description { get; set; }

        [DbColumn("IsActive")]
        public bool IsActive { get; set; }
    }

}
