using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System;


namespace Clinic_Management_DAL.Infrastractor
{
    using Microsoft.Data.SqlClient;
    using System;
    using System.Data;

    public static class SqlParameterFactory
    {
        public static SqlParameter Create(string name, object? value,
            SqlDbType? type = null)
        {
            var parameter = new SqlParameter
            {
                ParameterName = name,
                Value = value ?? DBNull.Value
            };

            if (type.HasValue)
                parameter.SqlDbType = type.Value;

            return parameter;
        }
    }



}
