using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_DAL.Infrastractor
{
    public static class DbExecutor
    {
        private static string CS =>
            clsDataAccessSettings.ConnectionString;

        public static T Execute<T>(
            string query,
            Func<SqlCommand, T> operation,
            params SqlParameter[] parameters)
        {
            try
            {
                using var conn = new SqlConnection(CS);
                using var cmd = new SqlCommand(query, conn);

                if (parameters?.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                conn.Open();

                return operation(cmd);
            }
            catch (Exception ex)
            {
                EventLogger.Log(ex, query);
                throw;
            }
        }
    }


}
