using Clinic_Management_Entities;
using Clinic_Management_DAL.Infrastractor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class BloodTypeData
    {
        private const string Columns = @"BloodTypeId, Name";

        public static IEnumerable<BloodType> GetAll()
        {
            string query = $@"
            SELECT {Columns}
            FROM BloodTypes
            ORDER BY Name;";

            return DbExecutor.Execute(query, cmd =>
            {
                using var reader = cmd.ExecuteReader();
                var list = new List<BloodType>();

                while (reader.Read())
                    list.Add(DbMapper<BloodType>.Map(reader));

                return list;
            });
        }

        // If you want to get ID by "Name" (or Code), use a WHERE query
        public static int? GetIdByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            string query = @"
            SELECT BloodTypeId
            FROM BloodTypes
            WHERE Name = @Name;";

            return DbExecutor.Execute(query, cmd =>
            {
                cmd.Parameters.Add(
                    SqlParameterFactory.Create("@Name", name.Trim(), System.Data.SqlDbType.NVarChar));

                object? result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                    return 0;

                return Convert.ToInt32(result);
            });
        }
    }


}
