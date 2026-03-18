using Clinic_Management_BLL.ResultWraper;
using Clinic_Management_BLL.Service;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.AuditWritter
{
    public static class AuditWriter
    {
        public static Result<long> Write<T>(
            string action,
            int? performedBy,
            string entityType,
            string entityId,
            bool success,
            T oldEntity = default,
            T newEntity = default,
            string failureReason = null,
            string metadataJson = null,
            Guid? correlationId = null)
        {
            var auditResult = AuditLogService.Log(
                action: action,
                success: success,
                userId: performedBy,
                entityType: entityType,
                entityId: entityId,
                failureReason: failureReason,
                metadataJson: metadataJson,
                correlationId: correlationId
            );

            if (!auditResult.IsSuccess)
                return auditResult;

            long auditId = auditResult.Value;

            //  Write Details Automatically
            var details = BuildDetails(auditId, oldEntity, newEntity);

            if (details.Count > 0)
                AuditLogDetailsService.InsertMany(details);

            return Result<long>.Ok(auditId);
        }

        // ===============================
        // AUTO CHANGE DETECTOR (MAGIC)
        // ===============================
        private static List<AuditLogDetail> BuildDetails<T>(
            long auditId,
            T oldEntity,
            T newEntity)
        {
            var details = new List<AuditLogDetail>();

            if (oldEntity == null && newEntity == null)
                return details;

            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                var oldValue = oldEntity == null
                    ? null
                    : prop.GetValue(oldEntity)?.ToString();

                var newValue = newEntity == null
                    ? null
                    : prop.GetValue(newEntity)?.ToString();

                if (oldValue == newValue)
                    continue;

                details.Add(new AuditLogDetail
                {
                    AuditId = auditId,
                    FieldName = prop.Name,
                    OldValue = oldValue,
                    NewValue = newValue
                });
            }

            return details;
        }
    }



}
