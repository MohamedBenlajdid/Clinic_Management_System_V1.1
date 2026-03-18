using Clinic_Management_DAL.Data;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;

namespace Clinic_Management_BLL.Service
{
    public static class AuditLogService
    {
        // ================================
        // Create Log Entry (core method)
        // ================================
      
        public static ResultWraper.Result<long> Log(
            string action,
            bool success,
            int? userId = null,
            string entityType = null,
            string entityId = null,
            string failureReason = null,
            string metadataJson = null,
            Guid? correlationId = null)
        {
            // Validate action (required, max length)
            var validation = ValidateAction(action);
            if (!validation.IsValid)
                return ResultWraper.Result<long>.Fail(validation.Errors.First());

            // Optional: Validate failureReason length (if provided)
            if (!string.IsNullOrEmpty(failureReason) && failureReason.Length > 300)
                return ResultWraper.Result<long>.Fail("FailureReason exceeds max length of 300 characters.");

            // Optional: Validate entityType length
            if (!string.IsNullOrEmpty(entityType) && entityType.Length > 80)
                return ResultWraper.Result<long>.Fail("EntityType exceeds max length of 80 characters.");

            // Optional: Validate entityId length
            if (!string.IsNullOrEmpty(entityId) && entityId.Length > 64)
                return ResultWraper.Result<long>.Fail("EntityId exceeds max length of 64 characters.");

            try
            {
                var auditLog = new AuditLog
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
                };

                long id = AuditLogData.Insert(auditLog);
                return ResultWraper.Result<long>.Ok(id);
            }
            catch (Exception ex)
            {
                // Log exception internally if needed (not shown here)
                
                return ResultWraper.Result<long>.Fail("Failed to write audit log: " + ex.Message);
            }
        }

        // ================================
        // Get audit log by ID
        // ================================
        public static ResultWraper.Result<AuditLog> GetById(long auditId)
        {
            if (auditId <= 0)
                return ResultWraper.Result<AuditLog>.Fail("Invalid AuditId.");

            var log = AuditLogData.GetById(auditId);
            if (log == null)
                return ResultWraper.Result<AuditLog>.Fail("Audit log not found.");

            return ResultWraper.Result<AuditLog>.Ok(log);
        }

        // ================================
        // Get audit logs by User ID
        // ================================
        public static IEnumerable<AuditLog> GetByUser(int userId)
        {
            
            if (userId <= 0)
                return Enumerable.Empty<AuditLog>();

            return AuditLogData.GetByUser(userId);
        }

        // ================================
        // Get recent audit logs with paging
        // ================================
        public static IEnumerable<AuditLog> GetRecent(int top = 100)
        {
            if (top <= 0) top = 100;
            if (top > 1000) top = 1000; // Max limit for safety

            return AuditLogData.GetRecent(top);
        }

        // ================================
        // Validation helper for Action field
        // ================================
        private static ValidationResult.ValidationResult ValidateAction(string action)
        {
            var result = new ValidationResult.ValidationResult();

            if (string.IsNullOrWhiteSpace(action))
                result.Add("Action is required.");

            if (action?.Length > 150)
                result.Add("Action cannot exceed 150 characters.");

            return result;
        }
    }



}
