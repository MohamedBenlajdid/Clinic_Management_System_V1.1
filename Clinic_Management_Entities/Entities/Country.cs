using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities
{
    public sealed class Country
    {
        [DbColumn("CountryId")]
        public int CountryId { get; set; }

        [DbColumn("Name")]
        public string Name { get; set; }

        [DbColumn("Iso2")]
        public string Iso2 { get; set; }

        [DbColumn("PhoneCode")]
        public string PhoneCode { get; set; }
    }

}
