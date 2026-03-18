using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities.Entities
{
    // =========================================================
    // ENTITY: Appointment  (dbo.Appointments)
    // =========================================================
    public sealed class Appointment
    {
        [DbColumn("AppointmentId")]
        public int AppointmentId { get; set; }

        [DbColumn("PatientId")]
        public int PatientId { get; set; }

        [DbColumn("DoctorId")]
        public int DoctorId { get; set; }

        [DbColumn("StartAt")]
        public DateTime StartAt { get; set; }

        [DbColumn("EndAt")]
        public DateTime EndAt { get; set; }

        [DbColumn("Status")]
        public byte Status { get; set; }   // you can map to enum in BLL

        [DbColumn("Reason")]
        public string? Reason { get; set; }

        [DbColumn("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [DbColumn("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [DbColumn("CreatedByUserId")]
        public int? CreatedByUserId { get; set; }

        [DbColumn("UpdatedByUserId")]
        public int? UpdatedByUserId { get; set; }

        [DbColumn("CancelReason")]
        public string? CancelReason { get; set; }

        [DbColumn("CancelledAt")]
        public DateTime? CancelledAt { get; set; }

        [DbColumn("IsDeleted")]
        public bool IsDeleted { get; set; }
    }



}
