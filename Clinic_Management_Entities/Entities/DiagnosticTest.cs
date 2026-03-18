using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities.Entities
{
    public sealed class DiagnosticTest
    {
        [DbColumn("DiagnosticTestId")]
        public int DiagnosticTestId { get; set; }

        [DbColumn("Code")]
        public string? Code { get; set; }

        [DbColumn("Name")]
        public string Name { get; set; } = null!;

        [DbColumn("Category")]
        public string? Category { get; set; }

        [DbColumn("Unit")]
        public string? Unit { get; set; }

        [DbColumn("RefRange")]
        public string? RefRange { get; set; }

        [DbColumn("IsActive")]
        public bool IsActive { get; set; }
    }

}
