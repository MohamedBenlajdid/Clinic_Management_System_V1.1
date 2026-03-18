using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class InvoiceItemData
    {
        private const string Columns = @"
        InvoiceItemId,
        InvoiceId,
        ItemType,
        ReferenceId,
        Description,
        Quantity,
        UnitPrice,
        Discount,
        Total";

        // =========================
        // GET BY ID
        // =========================
        public static InvoiceItem? GetById(int id)
        {
            string query = $@"
SELECT {Columns}
FROM InvoiceItems
WHERE InvoiceItemId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<InvoiceItem>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // =========================
        // GET BY INVOICE
        // =========================
        public static IEnumerable<InvoiceItem> GetByInvoiceId(int invoiceId)
        {
            string query = $@"
SELECT {Columns}
FROM InvoiceItems
WHERE InvoiceId = @InvoiceId
ORDER BY InvoiceItemId;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<InvoiceItem>();
                    while (reader.Read())
                        list.Add(DbMapper<InvoiceItem>.Map(reader));
                    return list;
                },
                SqlParameterFactory.Create("@InvoiceId", invoiceId)
            );
        }

        // =========================
        // GET ALL
        // =========================
        public static IEnumerable<InvoiceItem> GetAll()
        {
            string query = $@"
SELECT {Columns}
FROM InvoiceItems
ORDER BY InvoiceItemId DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<InvoiceItem>();
                    while (reader.Read())
                        list.Add(DbMapper<InvoiceItem>.Map(reader));
                    return list;
                }
            );
        }

        // =========================
        // INSERT (returns new InvoiceItemId)
        // =========================
        public static int Insert(InvoiceItem item)
        {
            if (item.InvoiceId <= 0)
                throw new ArgumentOutOfRangeException(nameof(item.InvoiceId));

            if (string.IsNullOrWhiteSpace(item.Description))
                throw new ArgumentException("Description is required.");

            ValidateValues(item);

            string query = @"
INSERT INTO InvoiceItems
(
    InvoiceId,
    ItemType,
    ReferenceId,
    Description,
    Quantity,
    UnitPrice,
    Discount
)
VALUES
(
    @InvoiceId,
    @ItemType,
    @ReferenceId,
    @Description,
    @Quantity,
    @UnitPrice,
    @Discount
);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),

                SqlParameterFactory.Create("@InvoiceId", item.InvoiceId),
                SqlParameterFactory.Create("@ItemType", item.ItemType, SqlDbType.TinyInt),
                SqlParameterFactory.Create("@ReferenceId", (object?)item.ReferenceId ?? DBNull.Value, SqlDbType.Int),
                SqlParameterFactory.Create("@Description", item.Description, SqlDbType.NVarChar),

                SqlParameterFactory.Create("@Quantity", item.Quantity, SqlDbType.Decimal),
                SqlParameterFactory.Create("@UnitPrice", item.UnitPrice, SqlDbType.Decimal),
                SqlParameterFactory.Create("@Discount", item.Discount, SqlDbType.Decimal)
            );
        }

        // =========================
        // UPDATE
        // =========================
        public static bool Update(InvoiceItem item)
        {
            if (item.InvoiceItemId <= 0)
                throw new ArgumentOutOfRangeException(nameof(item.InvoiceItemId));

            if (item.InvoiceId <= 0)
                throw new ArgumentOutOfRangeException(nameof(item.InvoiceId));

            if (string.IsNullOrWhiteSpace(item.Description))
                throw new ArgumentException("Description is required.");

            ValidateValues(item);

            string query = @"
UPDATE InvoiceItems SET
    InvoiceId = @InvoiceId,
    ItemType = @ItemType,
    ReferenceId = @ReferenceId,
    Description = @Description,
    Quantity = @Quantity,
    UnitPrice = @UnitPrice,
    Discount = @Discount
WHERE InvoiceItemId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,

                SqlParameterFactory.Create("@Id", item.InvoiceItemId),
                SqlParameterFactory.Create("@InvoiceId", item.InvoiceId),
                SqlParameterFactory.Create("@ItemType", item.ItemType, SqlDbType.TinyInt),
                SqlParameterFactory.Create("@ReferenceId", (object?)item.ReferenceId ?? DBNull.Value, SqlDbType.Int),
                SqlParameterFactory.Create("@Description", item.Description, SqlDbType.NVarChar),

                SqlParameterFactory.Create("@Quantity", item.Quantity, SqlDbType.Decimal),
                SqlParameterFactory.Create("@UnitPrice", item.UnitPrice, SqlDbType.Decimal),
                SqlParameterFactory.Create("@Discount", item.Discount, SqlDbType.Decimal)
            );
        }

        // =========================
        // DELETE
        // =========================
        public static bool Delete(int id)
        {
            string query = @"
DELETE FROM InvoiceItems
WHERE InvoiceItemId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // =========================
        // DELETE BY INVOICE (bulk)
        // =========================
        public static bool DeleteByInvoiceId(int invoiceId)
        {
            string query = @"
DELETE FROM InvoiceItems
WHERE InvoiceId = @InvoiceId;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@InvoiceId", invoiceId)
            );
        }

        // =========================
        // VALIDATION (matches CK)
        // =========================
        private static void ValidateValues(InvoiceItem item)
        {
            if (item.Quantity <= 0)
                throw new ArgumentException("Quantity must be > 0.");

            if (item.UnitPrice < 0)
                throw new ArgumentException("UnitPrice must be >= 0.");

            if (item.Discount < 0)
                throw new ArgumentException("Discount must be >= 0.");
        }
    }


}
