using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities
{
    public sealed class Patient
    {
        [DbColumn("PatientId")]
        public int PatientId { get; set; }

        [DbColumn("PersonId")]
        public int PersonId { get; set; }

        [DbColumn("MedicalRecordNumber")]
        public string MedicalRecordNumber { get; set; } = null!;

        [DbColumn("BloodTypeId")]
        public byte? BloodTypeId { get; set; }

        [DbColumn("EmergencyContactName")]
        public string? EmergencyContactName { get; set; }

        [DbColumn("EmergencyContactPhone")]
        public string? EmergencyContactPhone { get; set; }

        [DbColumn("Notes")]
        public string? Notes { get; set; }

        [DbColumn("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [DbColumn("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }

}
