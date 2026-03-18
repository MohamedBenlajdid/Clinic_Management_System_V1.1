using Clinic_Management_DAL.Data;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.Service
{
    public static class BloodTypeService
    {
        public static readonly string EntityName = "BloodType";

        // Get all blood types
        public static IEnumerable<BloodType> GetAll()
        {
            return BloodTypeData.GetAll();
        }

        public static int? GetBloodTypeID(string Name)
        {
            return BloodTypeData.GetIdByName(Name);
        }

    }

}
