using Clinic_Management_Entities;
using Clinic_Management_DAL.Infrastractor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class CountryData
    {
        private const string Columns = @"
CountryId, Name, Iso2, PhoneCode";

        // ===============================
        // Get All (MOST USED)
        // ===============================
        public static IEnumerable<Country> GetAll()
        {
            string query = $@"SELECT {Columns} FROM Countries ORDER BY Name";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Country>();

                    while (reader.Read())
                        list.Add(DbMapper<Country>.Map(reader));

                    return list;
                });
        }

        // ===============================
        // Get By Id
        // ===============================
        public static Country GetById(int id)
        {
            string query = $@"
SELECT {Columns}
FROM Countries
WHERE CountryId = @Id";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Country>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", id));
        }
    }

}
