using Clinic_Management_DAL.Data;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.Service
{
    public class CountryService
    {
        protected readonly string EntityName = "Country";

        // Get all countries
        public static IEnumerable<Country> GetAll()
        {
            return CountryData.GetAll();
        }

        // Get a country by ID
        public Country? GetById(int id)
        {
            if (id <= 0)
                return null;

            return CountryData.GetById(id);
        }
    }

}
