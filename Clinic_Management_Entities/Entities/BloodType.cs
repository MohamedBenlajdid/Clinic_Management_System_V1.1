using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities
{
    public sealed class BloodType
    {
        [DbColumn("BloodTypeId")]
        public byte BloodTypeId { get; set; }

        [DbColumn("Name")]
        public string Name { get; set; }
    }

}
