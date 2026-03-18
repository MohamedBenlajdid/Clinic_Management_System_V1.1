using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities.Entities
{
    public sealed class Medicament
    {
        [DbColumn("MedicamentId")]
        public int MedicamentId { get; set; }

        [DbColumn("Name")]
        public string Name { get; set; } = null!;

        [DbColumn("GenericName")]
        public string? GenericName { get; set; }

        [DbColumn("Form")]
        public string? Form { get; set; }

        [DbColumn("Strength")]
        public string? Strength { get; set; }

        [DbColumn("Manufacturer")]
        public string? Manufacturer { get; set; }

        [DbColumn("IsActive")]
        public bool IsActive { get; set; }

        [DbColumn("CreatedAt")]
        public DateTime CreatedAt { get; set; }
    }

}
