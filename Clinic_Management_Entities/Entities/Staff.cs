using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities
{
    public sealed class Staff
    {
        [DbColumn("StaffId")]
        public int StaffId { get; set; }

        [DbColumn("PersonId")]
        public int PersonId { get; set; }

        [DbColumn("StaffCode")]
        public string StaffCode { get; set; } = null!;

        [DbColumn("DepartmentId")]
        public int? DepartmentId { get; set; }

        [DbColumn("HireDate")]
        public DateTime? HireDate { get; set; }

        [DbColumn("IsActive")]
        public bool IsActive { get; set; }

        [DbColumn("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [DbColumn("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }

}
