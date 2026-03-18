using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities
{
    public sealed class Person
    {
        [DbColumn("PersonId")]
        public int PersonId { get; set; }

        [DbColumn("FirstName")]
        public string FirstName { get; set; }

        [DbColumn("LastName")]
        public string LastName { get; set; }

        [DbColumn("BirthDate")]
        public DateTime? BirthDate { get; set; }

        [DbColumn("GenderId")]
        public byte? GenderId { get; set; }

        [DbColumn("Phone1")]
        public string? Phone1 { get; set; }

        [DbColumn("Phone2")]
        public string? Phone2 { get; set; }

        [DbColumn("Email")]
        public string? Email { get; set; }

        [DbColumn("CountryId")]
        public int? CountryId { get; set; }

        [DbColumn("City")]
        public string? City { get; set; }

        [DbColumn("AddressLine")]
        public string? AddressLine { get; set; }

        [DbColumn("NationalId")]
        public string? NationalId { get; set; }

        [DbColumn("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [DbColumn("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [DbColumn("IsDeleted")]
        public bool IsDeleted { get; set; }
    }


}
