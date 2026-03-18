
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities
{
    public sealed class InsuranceProvider
    {
        [DbColumn("InsuranceProviderId")]
        public int InsuranceProviderId { get; set; }

        [DbColumn("Name")]
        public string Name { get; set; } = null!;

        [DbColumn("Phone")]
        public string? Phone { get; set; }

        [DbColumn("Email")]
        public string? Email { get; set; }

        [DbColumn("Website")]
        public string? Website { get; set; }

        [DbColumn("AddressLine")]
        public string? AddressLine { get; set; }

        [DbColumn("City")]
        public string? City { get; set; }

        [DbColumn("CountryId")]
        public int? CountryId { get; set; }

        [DbColumn("IsActive")]
        public bool IsActive { get; set; }

        [DbColumn("CreatedAt")]
        public DateTime CreatedAt { get; set; }
    }

}
