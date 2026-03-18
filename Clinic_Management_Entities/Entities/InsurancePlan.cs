using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities
{
    public sealed class InsurancePlan
    {
        [DbColumn("InsurancePlanId")]
        public int InsurancePlanId { get; set; }

        [DbColumn("InsuranceProviderId")]
        public int InsuranceProviderId { get; set; }

        [DbColumn("PlanName")]
        public string PlanName { get; set; } = null!;

        [DbColumn("PlanCode")]
        public string? PlanCode { get; set; }

        [DbColumn("CoverageNotes")]
        public string? CoverageNotes { get; set; }

        [DbColumn("IsActive")]
        public bool IsActive { get; set; }

        [DbColumn("CreatedAt")]
        public DateTime CreatedAt { get; set; }
    }

}
