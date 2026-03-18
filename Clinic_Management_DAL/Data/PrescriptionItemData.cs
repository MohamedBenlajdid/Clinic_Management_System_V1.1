using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class PrescriptionItemData
    {
        private const string Columns = @"
        PrescriptionItemId,
        PrescriptionId,
        MedicamentId,
        Dose,
        Frequency,
        DurationDays,
        Route,
        Instructions,
        Quantity";

        // =========================
        // GET BY ID
        // =========================
        public static PrescriptionItem? GetById(int id)
        {
            string query = $@"
SELECT {Columns}
FROM PrescriptionItems
WHERE PrescriptionItemId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<PrescriptionItem>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // =========================
        // GET BY PRESCRIPTION
        // =========================
        public static IEnumerable<PrescriptionItem> GetByPrescriptionId(int prescriptionId)
        {
            string query = $@"
SELECT {Columns}
FROM PrescriptionItems
WHERE PrescriptionId = @PrescriptionId
ORDER BY PrescriptionItemId;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<PrescriptionItem>();
                    while (reader.Read())
                        list.Add(DbMapper<PrescriptionItem>.Map(reader));
                    return list;
                },
                SqlParameterFactory.Create("@PrescriptionId", prescriptionId)
            );
        }

        // =========================
        // GET ALL
        // =========================
        public static IEnumerable<PrescriptionItem> GetAll()
        {
            string query = $@"
SELECT {Columns}
FROM PrescriptionItems
ORDER BY PrescriptionItemId DESC;";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<PrescriptionItem>();
                    while (reader.Read())
                        list.Add(DbMapper<PrescriptionItem>.Map(reader));
                    return list;
                }
            );
        }

        // =========================
        // INSERT
        // =========================
        public static int Insert(PrescriptionItem item)
        {
            if (item.PrescriptionId <= 0)
                throw new ArgumentOutOfRangeException(nameof(item.PrescriptionId));

            if (item.MedicamentId <= 0)
                throw new ArgumentOutOfRangeException(nameof(item.MedicamentId));

            if (item.DurationDays.HasValue &&
                (item.DurationDays < 1 || item.DurationDays > 365))
                throw new ArgumentException("DurationDays must be between 1 and 365.");

            string query = @"
INSERT INTO PrescriptionItems
(
    PrescriptionId,
    MedicamentId,
    Dose,
    Frequency,
    DurationDays,
    Route,
    Instructions,
    Quantity
)
VALUES
(
    @PrescriptionId,
    @MedicamentId,
    @Dose,
    @Frequency,
    @DurationDays,
    @Route,
    @Instructions,
    @Quantity
);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),

                SqlParameterFactory.Create("@PrescriptionId", item.PrescriptionId),
                SqlParameterFactory.Create("@MedicamentId", item.MedicamentId),
                SqlParameterFactory.Create("@Dose", (object?)item.Dose ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Frequency", (object?)item.Frequency ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@DurationDays", (object?)item.DurationDays ?? DBNull.Value, SqlDbType.SmallInt),
                SqlParameterFactory.Create("@Route", (object?)item.Route ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Instructions", (object?)item.Instructions ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Quantity", (object?)item.Quantity ?? DBNull.Value, SqlDbType.Decimal)
            );
        }

        // =========================
        // UPDATE
        // =========================
        public static bool Update(PrescriptionItem item)
        {
            if (item.PrescriptionItemId <= 0)
                throw new ArgumentOutOfRangeException(nameof(item.PrescriptionItemId));

            if (item.PrescriptionId <= 0)
                throw new ArgumentOutOfRangeException(nameof(item.PrescriptionId));

            if (item.MedicamentId <= 0)
                throw new ArgumentOutOfRangeException(nameof(item.MedicamentId));

            if (item.DurationDays.HasValue &&
                (item.DurationDays < 1 || item.DurationDays > 365))
                throw new ArgumentException("DurationDays must be between 1 and 365.");

            string query = @"
UPDATE PrescriptionItems SET
    PrescriptionId = @PrescriptionId,
    MedicamentId = @MedicamentId,
    Dose = @Dose,
    Frequency = @Frequency,
    DurationDays = @DurationDays,
    Route = @Route,
    Instructions = @Instructions,
    Quantity = @Quantity
WHERE PrescriptionItemId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,

                SqlParameterFactory.Create("@Id", item.PrescriptionItemId),
                SqlParameterFactory.Create("@PrescriptionId", item.PrescriptionId),
                SqlParameterFactory.Create("@MedicamentId", item.MedicamentId),
                SqlParameterFactory.Create("@Dose", (object?)item.Dose ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Frequency", (object?)item.Frequency ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@DurationDays", (object?)item.DurationDays ?? DBNull.Value, SqlDbType.SmallInt),
                SqlParameterFactory.Create("@Route", (object?)item.Route ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Instructions", (object?)item.Instructions ?? DBNull.Value, SqlDbType.NVarChar),
                SqlParameterFactory.Create("@Quantity", (object?)item.Quantity ?? DBNull.Value, SqlDbType.Decimal)
            );
        }

        // =========================
        // DELETE
        // =========================
        public static bool Delete(int id)
        {
            string query = @"
DELETE FROM PrescriptionItems
WHERE PrescriptionItemId = @Id;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // =========================
        // DELETE ALL BY PRESCRIPTION
        // =========================
        public static bool DeleteByPrescriptionId(int prescriptionId)
        {
            string query = @"
DELETE FROM PrescriptionItems
WHERE PrescriptionId = @PrescriptionId;";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@PrescriptionId", prescriptionId)
            );
        }
    }

}
