using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities.Entities
{
    

    public sealed class DoctorDayOverrideSession
    {
        [DbColumn("SessionId")]
        public int SessionId { get; set; }

        [DbColumn("OverrideId")]
        public int OverrideId { get; set; }

        [DbColumn("StartTime")]
        public TimeSpan StartTime { get; set; }

        [DbColumn("EndTime")]
        public TimeSpan EndTime { get; set; }

        [DbColumn("SlotMinutes")]
        public short SlotMinutes { get; set; }

        [DbColumn("CreatedAt")]
        public DateTime CreatedAt { get; set; }
    }

}
