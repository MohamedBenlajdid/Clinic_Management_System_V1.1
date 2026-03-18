using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities.Entities
{
    // =========================================================
    // POCO: DoctorSchedule
    // (Matches dbo.DoctorSchedules)
    // =========================================================
    public sealed class DoctorSchedule
    {
        [DbColumn("ScheduleId")]
        public int ScheduleId { get; set; }

        [DbColumn("DoctorId")]
        public int DoctorId { get; set; }

        [DbColumn("DayOfWeek")]
        public byte DayOfWeek { get; set; }   // 0=Sunday ... 6=Saturday

        [DbColumn("StartTime")]
        public TimeSpan StartTime { get; set; }

        [DbColumn("EndTime")]
        public TimeSpan EndTime { get; set; }

        [DbColumn("SlotMinutes")]
        public short SlotMinutes { get; set; }

        [DbColumn("IsActive")]
        public bool IsActive { get; set; }

        [DbColumn("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [DbColumn("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }



}
