using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities.Entities
{
    public sealed class DiagnosticRequestItem
    {
        [DbColumn("DiagnosticRequestItemId")]
        public int DiagnosticRequestItemId { get; set; }

        [DbColumn("DiagnosticRequestId")]
        public int DiagnosticRequestId { get; set; }

        [DbColumn("DiagnosticTestId")]
        public int DiagnosticTestId { get; set; }

        [DbColumn("Notes")]
        public string? Notes { get; set; }
    }



}
