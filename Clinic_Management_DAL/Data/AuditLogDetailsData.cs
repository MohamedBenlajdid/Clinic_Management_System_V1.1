using Clinic_Management_Entities;
using Clinic_Management_DAL.Infrastractor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class AuditLogDetailsData
    {
        private const string Columns = @"
AuditId, FieldName, OldValue, NewValue";

        // ===============================
        // Insert Single
        // ===============================
        public static bool Insert(AuditLogDetail detail)
        {
            string query = @"
INSERT INTO AuditLogDetails
(
    AuditId, FieldName, OldValue, NewValue
)
VALUES
(
    @AuditId, @FieldName, @OldValue, @NewValue
)";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@AuditId", detail.AuditId),
                SqlParameterFactory.Create("@FieldName", detail.FieldName),
                SqlParameterFactory.Create("@OldValue", detail.OldValue),
                SqlParameterFactory.Create("@NewValue", detail.NewValue)
            );
        }

        // ===============================
        // Bulk Insert (CRITICAL)
        // ===============================
        public static bool InsertMany(IEnumerable<AuditLogDetail> details)
        {
            string query = @"
INSERT INTO AuditLogDetails
(
    AuditId, FieldName, OldValue, NewValue
)
VALUES
(
    @AuditId, @FieldName, @OldValue, @NewValue
)";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    int affected = 0;

                    foreach (var d in details)
                    {
                        cmd.Parameters.Clear();

                        cmd.Parameters.Add(SqlParameterFactory.Create("@AuditId", d.AuditId));
                        cmd.Parameters.Add(SqlParameterFactory.Create("@FieldName", d.FieldName));
                        cmd.Parameters.Add(SqlParameterFactory.Create("@OldValue", d.OldValue));
                        cmd.Parameters.Add(SqlParameterFactory.Create("@NewValue", d.NewValue));

                        affected += cmd.ExecuteNonQuery();
                    }

                    return affected > 0;
                }
            );
        }

        // ===============================
        // Get By AuditId
        // ===============================
        public static IEnumerable<AuditLogDetail> GetByAuditId(long auditId)
        {
            string query = $@"
SELECT {Columns}
FROM AuditLogDetails
WHERE AuditId = @AuditId";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<AuditLogDetail>();

                    while (reader.Read())
                        list.Add(DbMapper<AuditLogDetail>.Map(reader));

                    return list;
                },
                SqlParameterFactory.Create("@AuditId", auditId)
            );
        }
    }

}
