using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities
{
    public sealed class Doctor
    {
        [DbColumn("StaffId")]
        public int StaffId { get; set; }

        [DbColumn("SpecialtyId")]
        public int? SpecialtyId { get; set; }

        [DbColumn("LicenseNumber")]
        public string? LicenseNumber { get; set; }

        [DbColumn("ConsultationFee")]
        public decimal? ConsultationFee { get; set; }


        public string? DoctorName { get; set; } 

    }

}
