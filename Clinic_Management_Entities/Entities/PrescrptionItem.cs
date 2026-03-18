using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities.Entities
{
    public sealed class PrescriptionItem
    {
        [DbColumn("PrescriptionItemId")]
        public int PrescriptionItemId { get; set; }

        [DbColumn("PrescriptionId")]
        public int PrescriptionId { get; set; }

        [DbColumn("MedicamentId")]
        public int MedicamentId { get; set; }

        [DbColumn("Dose")]
        public string? Dose { get; set; }

        [DbColumn("Frequency")]
        public string? Frequency { get; set; }

        [DbColumn("DurationDays")]
        public short? DurationDays { get; set; }

        [DbColumn("Route")]
        public string? Route { get; set; }

        [DbColumn("Instructions")]
        public string? Instructions { get; set; }

        [DbColumn("Quantity")]
        public decimal? Quantity { get; set; }
    }

}
