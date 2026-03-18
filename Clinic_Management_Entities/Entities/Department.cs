using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities
{
    public sealed class Department
    {
        [DbColumn("DepartmentId")]
        public int DepartmentId { get; set; }

        [DbColumn("Name")]
        public string Name { get; set; }
    }

}
