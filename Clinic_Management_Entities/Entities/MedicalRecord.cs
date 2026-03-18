using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities.Entities
{
    public sealed class MedicalRecord
    {
        [DbColumn("MedicalRecordId")]
        public int MedicalRecordId { get; set; }

        [DbColumn("AppointmentId")]
        public int AppointmentId { get; set; }

        [DbColumn("PatientId")]
        public int PatientId { get; set; }

        [DbColumn("DoctorId")]
        public int DoctorId { get; set; }

        [DbColumn("ChiefComplaint")]
        public string? ChiefComplaint { get; set; }

        [DbColumn("HistoryOfPresentIllness")]
        public string? HistoryOfPresentIllness { get; set; }

        [DbColumn("Examination")]
        public string? Examination { get; set; }

        [DbColumn("Diagnosis")]
        public string? Diagnosis { get; set; }

        [DbColumn("Notes")]
        public string? Notes { get; set; }

        [DbColumn("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [DbColumn("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }

}
