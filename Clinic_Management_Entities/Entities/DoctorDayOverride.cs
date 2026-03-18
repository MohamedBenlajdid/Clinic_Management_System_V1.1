using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities.Entities
{
    // =========================================================
    // ENTITY: DoctorDayOverride  (dbo.DoctorDayOverrides)
    // =========================================================
    public sealed class DoctorDayOverride
    {
        [DbColumn("OverrideId")]
        public int OverrideId { get; set; }

        [DbColumn("DoctorId")]
        public int DoctorId { get; set; }

        [DbColumn("Date")]
        public DateTime Date { get; set; }   // store date-only in DB if possible

        [DbColumn("IsOverride")]
        public bool IsOverride { get; set; }

        [DbColumn("IsDayOff")]
        public bool IsDayOff { get; set; }

        [DbColumn("Notes")]
        public string? Notes { get; set; }

        [DbColumn("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [DbColumn("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }

}
