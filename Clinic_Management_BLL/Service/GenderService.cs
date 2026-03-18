using Clinic_Management_DAL.Data;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.Service
{
    public class GenderService
    {
        protected readonly string EntityName = "Gender";

        // Since Gender is read-only here (no Insert/Update/Delete in DAL), only provide GetAll

        public static IEnumerable<Gender> GetAll()
        {
            return GenderData.GetAll();
        }

        // Optionally, if you want GetById or GetByName you can add them here if DAL supports it
    }

}
