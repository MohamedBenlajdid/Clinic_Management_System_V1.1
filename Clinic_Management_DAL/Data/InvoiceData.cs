using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities;
using Clinic_Management_Entities.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class InvoiceData
    {
        private const string Columns = @"
        InvoiceId,
        InvoiceNumber,
        PatientId,
        AppointmentId,
        IssueDate,
        DueDate,
        SubTotal,
        DiscountAmount,
        TaxAmount,
        TotalAmount,
        PaidAmount,
        RemainingAmount,
        Status,
        Notes,
        CreatedAt,
        UpdatedAt,
        CreatedByUserId,
        UpdatedByUserId,
        IsDeleted";

        // =========================
        // GET BY ID (not deleted)
        // =========================
        public static Invoice? GetById(int invoiceId)
        {
            string query = $@"
SELECT {Columns}
FROM Invoices
WHERE InvoiceId = @Id
  AND IsDeleted = 0;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Invoice>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", invoiceId)
            );
        }

        // =========================
        // GET BY NUMBER (not deleted)
        // =========================
        public static Invoice? GetByInvoiceNumber(string invoiceNumber)
        {
            if (string.IsNullOrWhiteSpace(invoiceNumber))
                throw new ArgumentException("InvoiceNumber is required.");

            string query = $@"
SELECT {Columns}
FROM Invoices
WHERE InvoiceNumber = @InvoiceNumber
  AND IsDeleted = 0;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Invoice>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@InvoiceNumber", invoiceNumber, SqlDbType.NVarChar)
            );
        }

        // =========================
        // GET ALL (not deleted)
        // =========================
        public static IEnumerable<Invoice> GetAll()
        {
            string query = $@"
SELECT {Columns}
FROM Invoices
WHERE IsDeleted = 0
ORDER BY IssueDate DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Invoice>();
                    while (reader.Read())
                        list.Add(DbMapper<Invoice>.Map(reader));
                    return list;
                }
            );
        }

        // =========================
        // GET BY PATIENT (optional range)
        // =========================
        public static IEnumerable<Invoice> GetByPatientId(
            int patientId,
            DateTime? from = null,
            DateTime? to = null,
            bool includeDeleted = false)
        {
            string deleted = includeDeleted ? "" : "AND IsDeleted = 0";

            var ps = new List<SqlParameter>
        {
            SqlParameterFactory.Create("@PatientId", patientId)
        };

            string range = "";
            if (from.HasValue)
            {
                range += " AND IssueDate >= @From";
                ps.Add(SqlParameterFactory.Create("@From", from.Value, SqlDbType.DateTime2));
            }
            if (to.HasValue)
            {
                range += " AND IssueDate < @To";
                ps.Add(SqlParameterFactory.Create("@To", to.Value, SqlDbType.DateTime2));
            }

            string query = $@"
SELECT {Columns}
FROM Invoices
WHERE PatientId = @PatientId
{deleted}
{range}
ORDER BY IssueDate DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Invoice>();
                    while (reader.Read())
                        list.Add(DbMapper<Invoice>.Map(reader));
                    return list;
                },
                ps.ToArray()
            );
        }

        // =========================
        // GET BY APPOINTMENT
        // =========================
        public static IEnumerable<Invoice> GetByAppointmentId(int appointmentId, bool includeDeleted = false)
        {
            string deleted = includeDeleted ? "" : "AND IsDeleted = 0";

            string query = $@"
SELECT {Columns}
FROM Invoices
WHERE AppointmentId = @AppointmentId
{deleted}
ORDER BY IssueDate DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Invoice>();
                    while (reader.Read())
                        list.Add(DbMapper<Invoice>.Map(reader));
                    return list;
                },
                SqlParameterFactory.Create("@AppointmentId", appointmentId)
            );
        }

        // =========================
        // INSERT (returns new InvoiceId)
        // =========================
        public static int Insert(Invoice i)
        {
            ValidateAmounts(i);

            if (string.IsNullOrWhiteSpace(i.InvoiceNumber))
                throw new ArgumentException("InvoiceNumber is required.");

            if (i.PatientId <= 0)
                throw new ArgumentOutOfRangeException(nameof(i.PatientId));

            string query = @"
INSERT INTO Invoices
(
    InvoiceNumber,
    PatientId,
    AppointmentId,
    IssueDate,
    DueDate,
    SubTotal,
    DiscountAmount,
    TaxAmount,
    TotalAmount,
    PaidAmount,
    Status,
    Notes,
    CreatedAt,
    UpdatedAt,
    CreatedByUserId,
    UpdatedByUserId,
    IsDeleted
)
VALUES
(
    @InvoiceNumber,
    @PatientId,
    @AppointmentId,
    SYSUTCDATETIME(),
    @DueDate,
    @SubTotal,
    @DiscountAmount,
    @TaxAmount,
    @TotalAmount,
    @PaidAmount,
    @Status,
    @Notes,
    SYSUTCDATETIME(),
    NULL,
    @CreatedByUserId,
    NULL,
    0
);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),

                SqlParameterFactory.Create("@InvoiceNumber", i.InvoiceNumber, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@PatientId", i.PatientId),
                SqlParameterFactory.Create("@AppointmentId", (object?)i.AppointmentId ?? DBNull.Value),

                SqlParameterFactory.Create("@DueDate", (object?)i.DueDate ?? DBNull.Value, SqlDbType.DateTime2),

                SqlParameterFactory.Create("@SubTotal", i.SubTotal, SqlDbType.Decimal),
                SqlParameterFactory.Create("@DiscountAmount", i.DiscountAmount, SqlDbType.Decimal),
                SqlParameterFactory.Create("@TaxAmount", i.TaxAmount, SqlDbType.Decimal),
                SqlParameterFactory.Create("@TotalAmount", i.TotalAmount, SqlDbType.Decimal),
                SqlParameterFactory.Create("@PaidAmount", i.PaidAmount, SqlDbType.Decimal),

                SqlParameterFactory.Create("@Status", i.Status, SqlDbType.TinyInt),
                SqlParameterFactory.Create("@Notes", (object?)i.Notes ?? DBNull.Value, SqlDbType.NVarChar),

                SqlParameterFactory.Create("@CreatedByUserId", (object?)i.CreatedByUserId ?? DBNull.Value, SqlDbType.Int)
            );
        }

        // =========================
        // UPDATE (general)
        // =========================
        public static bool Update(Invoice i)
        {
            if (i.InvoiceId <= 0)
                throw new ArgumentOutOfRangeException(nameof(i.InvoiceId));

            ValidateAmounts(i);

            if (string.IsNullOrWhiteSpace(i.InvoiceNumber))
                throw new ArgumentException("InvoiceNumber is required.");

            if (i.PatientId <= 0)
                throw new ArgumentOutOfRangeException(nameof(i.PatientId));

            string query = @"
UPDATE Invoices SET
    InvoiceNumber = @InvoiceNumber,
    PatientId = @PatientId,
    AppointmentId = @AppointmentId,
    DueDate = @DueDate,
    SubTotal = @SubTotal,
    DiscountAmount = @DiscountAmount,
    TaxAmount = @TaxAmount,
    TotalAmount = @TotalAmount,
    PaidAmount = @PaidAmount,
    Status = @Status,
    Notes = @Notes,
    UpdatedAt = SYSUTCDATETIME(),
    UpdatedByUserId = @UpdatedByUserId
WHERE InvoiceId = @Id
  AND IsDeleted = 0;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,

                SqlParameterFactory.Create("@Id", i.InvoiceId),

                SqlParameterFactory.Create("@InvoiceNumber", i.InvoiceNumber, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@PatientId", i.PatientId),
                SqlParameterFactory.Create("@AppointmentId", (object?)i.AppointmentId ?? DBNull.Value),
                SqlParameterFactory.Create("@DueDate", (object?)i.DueDate ?? DBNull.Value, SqlDbType.DateTime2),

                SqlParameterFactory.Create("@SubTotal", i.SubTotal, SqlDbType.Decimal),
                SqlParameterFactory.Create("@DiscountAmount", i.DiscountAmount, SqlDbType.Decimal),
                SqlParameterFactory.Create("@TaxAmount", i.TaxAmount, SqlDbType.Decimal),
                SqlParameterFactory.Create("@TotalAmount", i.TotalAmount, SqlDbType.Decimal),
                SqlParameterFactory.Create("@PaidAmount", i.PaidAmount, SqlDbType.Decimal),

                SqlParameterFactory.Create("@Status", i.Status, SqlDbType.TinyInt),
                SqlParameterFactory.Create("@Notes", (object?)i.Notes ?? DBNull.Value, SqlDbType.NVarChar),

                SqlParameterFactory.Create("@UpdatedByUserId", (object?)i.UpdatedByUserId ?? DBNull.Value, SqlDbType.Int)
            );
        }

        // =========================
        // QUICK: SET STATUS
        // =========================
        public static bool SetStatus(int invoiceId, byte status, int? updatedByUserId = null)
        {
            string query = @"
UPDATE Invoices SET
    Status = @Status,
    UpdatedAt = SYSUTCDATETIME(),
    UpdatedByUserId = @UpdatedByUserId
WHERE InvoiceId = @Id
  AND IsDeleted = 0;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", invoiceId),
                SqlParameterFactory.Create("@Status", status, SqlDbType.TinyInt),
                SqlParameterFactory.Create("@UpdatedByUserId", (object?)updatedByUserId ?? DBNull.Value, SqlDbType.Int)
            );
        }

        // =========================
        // QUICK: ADD PAYMENT (PaidAmount += amount)
        // =========================
        public static bool AddPayment(int invoiceId, decimal amount, int? updatedByUserId = null)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Payment amount must be > 0.");

            string query = @"
UPDATE Invoices SET
    PaidAmount = PaidAmount + @Amount,
    UpdatedAt = SYSUTCDATETIME(),
    UpdatedByUserId = @UpdatedByUserId
WHERE InvoiceId = @Id
  AND IsDeleted = 0;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", invoiceId),
                SqlParameterFactory.Create("@Amount", amount, SqlDbType.Decimal),
                SqlParameterFactory.Create("@UpdatedByUserId", (object?)updatedByUserId ?? DBNull.Value, SqlDbType.Int)
            );
        }

        // =========================
        // SOFT DELETE
        // =========================
        public static bool SoftDelete(int invoiceId, int? deletedByUserId = null)
        {
            string query = @"
UPDATE Invoices SET
    IsDeleted = 1,
    UpdatedAt = SYSUTCDATETIME(),
    UpdatedByUserId = @DeletedByUserId
WHERE InvoiceId = @Id
  AND IsDeleted = 0;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", invoiceId),
                SqlParameterFactory.Create("@DeletedByUserId", (object?)deletedByUserId ?? DBNull.Value, SqlDbType.Int)
            );
        }

        // =========================
        // EXISTS (helper)
        // =========================
        private static bool Exists(string whereSql, params SqlParameter[] parameters)
        {
            string query = $@"
SELECT 1
FROM Invoices
WHERE {whereSql};";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read();
                },
                parameters
            );
        }

        // =========================
        // EXISTS BY NUMBER
        // =========================
        public static bool ExistsByInvoiceNumber(string invoiceNumber, int? ignoreInvoiceId = null)
        {
            if (string.IsNullOrWhiteSpace(invoiceNumber))
                throw new ArgumentException("InvoiceNumber is required.");

            string where = @"
InvoiceNumber = @InvoiceNumber
AND IsDeleted = 0
" + (ignoreInvoiceId.HasValue ? "AND InvoiceId <> @IgnoreId" : "");

            var ps = new List<SqlParameter>
        {
            SqlParameterFactory.Create("@InvoiceNumber", invoiceNumber, SqlDbType.NVarChar)
        };

            if (ignoreInvoiceId.HasValue)
                ps.Add(SqlParameterFactory.Create("@IgnoreId", ignoreInvoiceId.Value));

            return Exists(where, ps.ToArray());
        }

        // =========================
        // AMOUNTS VALIDATION (matches CK)
        // =========================
        private static void ValidateAmounts(Invoice i)
        {
            if (i.SubTotal < 0) throw new ArgumentException("SubTotal must be >= 0.");
            if (i.DiscountAmount < 0) throw new ArgumentException("DiscountAmount must be >= 0.");
            if (i.TaxAmount < 0) throw new ArgumentException("TaxAmount must be >= 0.");
            if (i.TotalAmount < 0) throw new ArgumentException("TotalAmount must be >= 0.");
            if (i.PaidAmount < 0) throw new ArgumentException("PaidAmount must be >= 0.");
        }
    }


}
