using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class PaymentMethodData
    {
        private const string Columns = @"
        PaymentMethodId,
        Name";

        // =========================
        // GET BY ID
        // =========================
        public static PaymentMethod? GetById(byte id)
        {
            string query = $@"
SELECT {Columns}
FROM PaymentMethods
WHERE PaymentMethodId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<PaymentMethod>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", id, SqlDbType.TinyInt)
            );
        }

        // =========================
        // GET ALL
        // =========================
        public static IEnumerable<PaymentMethod> GetAll()
        {
            string query = $@"
SELECT {Columns}
FROM PaymentMethods
ORDER BY PaymentMethodId;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<PaymentMethod>();
                    while (reader.Read())
                        list.Add(DbMapper<PaymentMethod>.Map(reader));
                    return list;
                }
            );
        }

        // =========================
        // INSERT
        // =========================
        public static bool Insert(PaymentMethod method)
        {
            if (method.PaymentMethodId <= 0)
                throw new ArgumentOutOfRangeException(nameof(method.PaymentMethodId));

            if (string.IsNullOrWhiteSpace(method.Name))
                throw new ArgumentException("Payment method name is required.");

            string query = @"
INSERT INTO PaymentMethods
(
    PaymentMethodId,
    Name
)
VALUES
(
    @Id,
    @Name
);";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", method.PaymentMethodId, SqlDbType.TinyInt),
                SqlParameterFactory.Create("@Name", method.Name, SqlDbType.NVarChar)
            );
        }

        // =========================
        // UPDATE
        // =========================
        public static bool Update(PaymentMethod method)
        {
            if (method.PaymentMethodId <= 0)
                throw new ArgumentOutOfRangeException(nameof(method.PaymentMethodId));

            if (string.IsNullOrWhiteSpace(method.Name))
                throw new ArgumentException("Payment method name is required.");

            string query = @"
UPDATE PaymentMethods SET
    Name = @Name
WHERE PaymentMethodId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", method.PaymentMethodId, SqlDbType.TinyInt),
                SqlParameterFactory.Create("@Name", method.Name, SqlDbType.NVarChar)
            );
        }

        // =========================
        // DELETE
        // =========================
        public static bool Delete(byte id)
        {
            string query = @"
DELETE FROM PaymentMethods
WHERE PaymentMethodId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", id, SqlDbType.TinyInt)
            );
        }

        // =========================
        // EXISTS BY NAME
        // =========================
        public static bool ExistsByName(string name, byte? ignoreId = null)
        {
            string where = @"
Name = @Name
" + (ignoreId.HasValue ? "AND PaymentMethodId <> @IgnoreId" : "");

            string query = $@"
SELECT 1
FROM PaymentMethods
WHERE {where};";

            var ps = new List<SqlParameter>
        {
            SqlParameterFactory.Create("@Name", name)
        };

            if (ignoreId.HasValue)
                ps.Add(SqlParameterFactory.Create("@IgnoreId", ignoreId.Value, SqlDbType.TinyInt));

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read();
                },
                ps.ToArray()
            );
        }
    }

}
