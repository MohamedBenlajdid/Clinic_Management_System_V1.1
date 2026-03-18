using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities
{
    public sealed class Gender
    {
        [DbColumn("GenderId")]
        public byte GenderId { get; set; }

        [DbColumn("Name")]
        public string Name { get; set; }
    }

}
