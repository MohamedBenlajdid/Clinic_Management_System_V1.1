using Clinic_Management_Entities;
using Clinic_Management_DAL.Infrastractor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class AuditLogData
    {
        private const string Columns = @"
AuditId, [At], UserId, Action,
EntityType, EntityId, Success,
FailureReason, MachineName,
SessionId, CorrelationId, MetadataJson";

        // ===============================
        // Insert (CORE METHOD)
        // ===============================
        public static long Insert(AuditLog log)
        {
            string query = @"
INSERT INTO AuditLog
(
    UserId, Action, EntityType, EntityId,
    Success, FailureReason, MachineName,
    SessionId, CorrelationId, MetadataJson
)
VALUES
(
    @UserId, @Action, @EntityType, @EntityId,
    @Success, @FailureReason, @MachineName,
    @SessionId, @CorrelationId, @MetadataJson
);

SELECT SCOPE_IDENTITY();";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    object result = cmd.ExecuteScalar();
                    return Convert.ToInt64(result);
                },
                SqlParameterFactory.Create("@UserId", log.UserId),
                SqlParameterFactory.Create("@Action", log.Action),
                SqlParameterFactory.Create("@EntityType", log.EntityType),
                SqlParameterFactory.Create("@EntityId", log.EntityId),
                SqlParameterFactory.Create("@Success", log.Success),
                SqlParameterFactory.Create("@FailureReason", log.FailureReason),
                SqlParameterFactory.Create("@MachineName", log.MachineName),
                SqlParameterFactory.Create("@SessionId", log.SessionId),
                SqlParameterFactory.Create("@CorrelationId", log.CorrelationId),
                SqlParameterFactory.Create("@MetadataJson", log.MetadataJson)
            );
        }

        // ===============================
        // Get By Id
        // ===============================
        public static AuditLog GetById(long auditId)
        {
            string query = $@"
SELECT {Columns}
FROM AuditLog
WHERE AuditId = @AuditId";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<AuditLog>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@AuditId", auditId)
            );
        }

        // ===============================
        // Get By User
        // ===============================
        public static IEnumerable<AuditLog> GetByUser(int userId)
        {
            string query = $@"
SELECT {Columns}
FROM AuditLog
WHERE UserId = @UserId
ORDER BY [At] DESC";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<AuditLog>();

                    while (reader.Read())
                        list.Add(DbMapper<AuditLog>.Map(reader));

                    return list;
                },
                SqlParameterFactory.Create("@UserId", userId)
            );
        }

        // ===============================
        // Get Recent Logs
        // ===============================
        public static IEnumerable<AuditLog> GetRecent(int top = 100)
        {
            string query = $@"
SELECT TOP (@Top) {Columns}
FROM AuditLog
ORDER BY [At] DESC";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<AuditLog>();

                    while (reader.Read())
                        list.Add(DbMapper<AuditLog>.Map(reader));

                    return list;
                },
                SqlParameterFactory.Create("@Top", top)
            );
        }

        // ===============================
        // Quick Helper (VERY IMPORTANT)
        // ===============================
        public static long Log(
            string action,
            bool success,
            int? userId = null,
            string entityType = null,
            string entityId = null,
            string failureReason = null,
            string metadataJson = null,
            Guid? correlationId = null)
        {
            return Insert(new AuditLog
            {
                UserId = userId,
                Action = action,
                EntityType = entityType,
                EntityId = entityId,
                Success = success,
                FailureReason = failureReason,
                MachineName = Environment.MachineName,
                SessionId = Environment.ProcessId.ToString(),
                CorrelationId = correlationId,
                MetadataJson = metadataJson
            });
        }
    }

}
