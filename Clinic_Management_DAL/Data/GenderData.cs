using System;
using System.Collections.Generic;
using System.Text;
using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities;

namespace Clinic_Management_DAL.Data
{
    public static class GenderData
{
    private const string Columns = @"GenderId, Name";

    public static IEnumerable<Gender> GetAll()
    {
        string query = $"SELECT {Columns} FROM Genders ORDER BY Name";

        return DbExecutor.Execute(query, cmd =>
        {
            using var reader = cmd.ExecuteReader();
            var list = new List<Gender>();

            while (reader.Read())
                list.Add(DbMapper<Gender>.Map(reader));

            return list;
        });
    }
}

}
