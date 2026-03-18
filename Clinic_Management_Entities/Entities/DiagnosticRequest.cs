using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities.Entities
{
    public sealed class DiagnosticRequest
    {
        [DbColumn("DiagnosticRequestId")]
        public int DiagnosticRequestId { get; set; }

        [DbColumn("AppointmentId")]
        public int AppointmentId { get; set; }

        [DbColumn("PatientId")]
        public int PatientId { get; set; }

        [DbColumn("DoctorId")]
        public int DoctorId { get; set; }

        [DbColumn("RequestedAt")]
        public DateTime RequestedAt { get; set; }

        [DbColumn("ClinicalInfo")]
        public string? ClinicalInfo { get; set; }

        [DbColumn("Priority")]
        public byte Priority { get; set; }   // map to enum in BLL if you want

        [DbColumn("Status")]
        public byte Status { get; set; }     // map to enum in BLL if you want
    }


    public sealed class DiagnosticRequestDetail
    {
        [DbColumn("DiagnosticRequestId")]
        public int DiagnosticRequestId { get; set; }

        [DbColumn("AppointmentId")]
        public int AppointmentId { get; set; }

        [DbColumn("RequestedAt")]
        public DateTime RequestedAt { get; set; }

        [DbColumn("Priority")]
        public int Priority { get; set; }

        [DbColumn("Status")]
        public int Status { get; set; }

        [DbColumn("ClinicalInfo")]
        public string ClinicalInfo { get; set; }

        [DbColumn("PatientName")]
        public string PatientName { get; set; }

        [DbColumn("DoctorName")]
        public string DoctorName { get; set; }
    }


}
