using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities.Entities
{
    public sealed class Prescription
    {
        [DbColumn("PrescriptionId")]
        public int PrescriptionId { get; set; }

        [DbColumn("AppointmentId")]
        public int AppointmentId { get; set; }

        [DbColumn("PatientId")]
        public int PatientId { get; set; }

        [DbColumn("DoctorId")]
        public int DoctorId { get; set; }

        [DbColumn("Notes")]
        public string? Notes { get; set; }

        [DbColumn("IssuedAt")]
        public DateTime IssuedAt { get; set; }
    }

}
