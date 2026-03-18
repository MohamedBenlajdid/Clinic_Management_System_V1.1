using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class PaymentData
    {
        private const string Columns = @"
        PaymentId,
        InvoiceId,
        PaymentMethodId,
        Amount,
        PaymentDate,
        TransactionReference,
        Notes,
        IsRefund,
        CreatedByUserId";

        // =========================
        // GET BY ID
        // =========================
        public static Payment? GetById(int id)
        {
            string query = $@"
SELECT {Columns}
FROM Payments
WHERE PaymentId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Payment>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // =========================
        // GET BY INVOICE
        // =========================
        public static IEnumerable<Payment> GetByInvoiceId(int invoiceId)
        {
            string query = $@"
SELECT {Columns}
FROM Payments
WHERE InvoiceId = @InvoiceId
ORDER BY PaymentDate DESC, PaymentId DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Payment>();
                    while (reader.Read())
                        list.Add(DbMapper<Payment>.Map(reader));
                    return list;
                },
                SqlParameterFactory.Create("@InvoiceId", invoiceId)
            );
        }

        // =========================
        // GET BY METHOD (optional range)
        // =========================
        public static IEnumerable<Payment> GetByPaymentMethodId(
            byte paymentMethodId,
            DateTime? from = null,
            DateTime? to = null)
        {
            var ps = new List<SqlParameter>
        {
            SqlParameterFactory.Create("@PaymentMethodId", paymentMethodId, SqlDbType.TinyInt)
        };

            string range = "";
            if (from.HasValue)
            {
                range += " AND PaymentDate >= @From";
                ps.Add(SqlParameterFactory.Create("@From", from.Value, SqlDbType.DateTime2));
            }
            if (to.HasValue)
            {
                range += " AND PaymentDate < @To";
                ps.Add(SqlParameterFactory.Create("@To", to.Value, SqlDbType.DateTime2));
            }

            string query = $@"
SELECT {Columns}
FROM Payments
WHERE PaymentMethodId = @PaymentMethodId
{range}
ORDER BY PaymentDate DESC, PaymentId DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Payment>();
                    while (reader.Read())
                        list.Add(DbMapper<Payment>.Map(reader));
                    return list;
                },
                ps.ToArray()
            );
        }

        // =========================
        // GET ALL
        // =========================
        public static IEnumerable<Payment> GetAll(DateTime? from = null, DateTime? to = null)
        {
            var ps = new List<SqlParameter>();
            string range = "WHERE 1=1";

            if (from.HasValue)
            {
                range += " AND PaymentDate >= @From";
                ps.Add(SqlParameterFactory.Create("@From", from.Value, SqlDbType.DateTime2));
            }
            if (to.HasValue)
            {
                range += " AND PaymentDate < @To";
                ps.Add(SqlParameterFactory.Create("@To", to.Value, SqlDbType.DateTime2));
            }

            string query = $@"
SELECT {Columns}
FROM Payments
{range}
ORDER BY PaymentDate DESC, PaymentId DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Payment>();
                    while (reader.Read())
                        list.Add(DbMapper<Payment>.Map(reader));
                    return list;
                },
                ps.ToArray()
            );
        }

        // =========================
        // INSERT (returns new PaymentId)
        // =========================
        public static int Insert(Payment p)
        {
            if (p.InvoiceId <= 0)
                throw new ArgumentOutOfRangeException(nameof(p.InvoiceId));

            if (p.PaymentMethodId <= 0)
                throw new ArgumentOutOfRangeException(nameof(p.PaymentMethodId));

            // match CK_Payments_Amount
            if (p.Amount <= 0)
                throw new ArgumentException("Amount must be > 0.");

            string query = @"
INSERT INTO Payments
(
    InvoiceId,
    PaymentMethodId,
    Amount,
    PaymentDate,
    TransactionReference,
    Notes,
    IsRefund,
    CreatedByUserId
)
VALUES
(
    @InvoiceId,
    @PaymentMethodId,
    @Amount,
    SYSUTCDATETIME(),
    @TransactionReference,
    @Notes,
    @IsRefund,
    @CreatedByUserId
);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),

                SqlParameterFactory.Create("@InvoiceId", p.InvoiceId),
                SqlParameterFactory.Create("@PaymentMethodId", p.PaymentMethodId, SqlDbType.TinyInt),
                SqlParameterFactory.Create("@Amount", p.Amount, SqlDbType.Decimal),

                SqlParameterFactory.Create("@TransactionReference", (object?)p.TransactionReference ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Notes", (object?)p.Notes ?? DBNull.Value, SqlDbType.NVarChar),

                SqlParameterFactory.Create("@IsRefund", p.IsRefund, SqlDbType.Bit),
                SqlParameterFactory.Create("@CreatedByUserId", (object?)p.CreatedByUserId ?? DBNull.Value, SqlDbType.Int)
            );
        }

        // =========================
        // UPDATE (general)
        // =========================
        public static bool Update(Payment p)
        {
            if (p.PaymentId <= 0)
                throw new ArgumentOutOfRangeException(nameof(p.PaymentId));

            if (p.InvoiceId <= 0)
                throw new ArgumentOutOfRangeException(nameof(p.InvoiceId));

            if (p.PaymentMethodId <= 0)
                throw new ArgumentOutOfRangeException(nameof(p.PaymentMethodId));

            if (p.Amount <= 0)
                throw new ArgumentException("Amount must be > 0.");

            string query = @"
UPDATE Payments SET
    InvoiceId = @InvoiceId,
    PaymentMethodId = @PaymentMethodId,
    Amount = @Amount,
    TransactionReference = @TransactionReference,
    Notes = @Notes,
    IsRefund = @IsRefund,
    CreatedByUserId = @CreatedByUserId
WHERE PaymentId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,

                SqlParameterFactory.Create("@Id", p.PaymentId),
                SqlParameterFactory.Create("@InvoiceId", p.InvoiceId),
                SqlParameterFactory.Create("@PaymentMethodId", p.PaymentMethodId, SqlDbType.TinyInt),
                SqlParameterFactory.Create("@Amount", p.Amount, SqlDbType.Decimal),

                SqlParameterFactory.Create("@TransactionReference", (object?)p.TransactionReference ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Notes", (object?)p.Notes ?? DBNull.Value, SqlDbType.NVarChar),

                SqlParameterFactory.Create("@IsRefund", p.IsRefund, SqlDbType.Bit),
                SqlParameterFactory.Create("@CreatedByUserId", (object?)p.CreatedByUserId ?? DBNull.Value, SqlDbType.Int)
            );
        }

        // =========================
        // DELETE
        // =========================
        public static bool Delete(int id)
        {
            string query = @"
DELETE FROM Payments
WHERE PaymentId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", id)
            );
        }
    }

}
