using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities.Entities
{
    public sealed class MedicalCertificate
    {
        [DbColumn("MedicalCertificateId")]
        public int MedicalCertificateId { get; set; }

        [DbColumn("AppointmentId")]
        public int AppointmentId { get; set; }

        [DbColumn("PatientId")]
        public int PatientId { get; set; }

        [DbColumn("DoctorId")]
        public int DoctorId { get; set; }

        [DbColumn("CertificateType")]
        public byte CertificateType { get; set; }  // map to enum in BLL if you want

        [DbColumn("StartDate")]
        public DateTime StartDate { get; set; }    // DATE in SQL -> DateTime in C#

        [DbColumn("EndDate")]
        public DateTime EndDate { get; set; }      // DATE in SQL -> DateTime in C#

        [DbColumn("DiagnosisSummary")]
        public string? DiagnosisSummary { get; set; }

        [DbColumn("Notes")]
        public string? Notes { get; set; }

        [DbColumn("IssuedAt")]
        public DateTime IssuedAt { get; set; }
    }

}
