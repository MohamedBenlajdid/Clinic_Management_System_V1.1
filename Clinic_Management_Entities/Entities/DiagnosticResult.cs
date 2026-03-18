using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities.Entities
{
    public sealed class DiagnosticResult
    {
        [DbColumn("DiagnosticResultId")]
        public int DiagnosticResultId { get; set; }

        [DbColumn("DiagnosticRequestItemId")]
        public int DiagnosticRequestItemId { get; set; }

        [DbColumn("ResultText")]
        public string? ResultText { get; set; }

        [DbColumn("ResultNumeric")]
        public decimal? ResultNumeric { get; set; }

        [DbColumn("Unit")]
        public string? Unit { get; set; }

        [DbColumn("RefRange")]
        public string? RefRange { get; set; }

        [DbColumn("ReportText")]
        public string? ReportText { get; set; }

        [DbColumn("PerformedAt")]
        public DateTime? PerformedAt { get; set; }

        [DbColumn("VerifiedAt")]
        public DateTime? VerifiedAt { get; set; }
    }

}
