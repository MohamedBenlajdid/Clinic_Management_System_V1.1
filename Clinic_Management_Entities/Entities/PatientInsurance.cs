using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities
{
    public sealed class PatientInsurance
    {
        [DbColumn("PatientInsuranceId")]
        public int PatientInsuranceId { get; set; }

        [DbColumn("PatientId")]
        public int PatientId { get; set; }

        [DbColumn("InsuranceProviderId")]
        public int InsuranceProviderId { get; set; }

        [DbColumn("InsurancePlanId")]
        public int? InsurancePlanId { get; set; }

        [DbColumn("PolicyNumber")]
        public string PolicyNumber { get; set; } = null!;

        [DbColumn("MemberId")]
        public string? MemberId { get; set; }

        [DbColumn("HolderFullName")]
        public string? HolderFullName { get; set; }

        [DbColumn("HolderRelation")]
        public string? HolderRelation { get; set; }

        [DbColumn("EffectiveFrom")]
        public DateTime? EffectiveFrom { get; set; }

        [DbColumn("EffectiveTo")]
        public DateTime? EffectiveTo { get; set; }

        [DbColumn("IsPrimary")]
        public bool IsPrimary { get; set; }

        [DbColumn("IsActive")]
        public bool IsActive { get; set; }

        [DbColumn("CreatedAt")]
        public DateTime CreatedAt { get; set; }
    }

}
