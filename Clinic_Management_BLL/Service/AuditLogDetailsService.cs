using Clinic_Management_BLL.ResultWraper;
using Clinic_Management_DAL.Data;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.Service
{
    public static class AuditLogDetailsService
    {
        private const int MaxFieldNameLength = 120;
        private const int MaxValueLength = 4000; // Assuming NVARCHAR(MAX) limited to 4k here for safety

        // ===============================
        // Insert single detail
        // ===============================
        public static Result<bool> Insert(AuditLogDetail detail)
        {
            var validation = ValidateDetail(detail);
            if (!validation.IsValid)
                return Result<bool>.Fail(string.Join("; ", validation.Errors));

            try
            {
                bool success = AuditLogDetailsData.Insert(detail);
                if (!success)
                    return Result<bool>.Fail("Failed to insert audit log detail.");

                return Result<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail("Exception inserting audit log detail: " + ex.Message);
            }
        }

        // ===============================
        // Bulk insert many details
        // ===============================
        public static Result<bool> InsertMany(IEnumerable<AuditLogDetail> details)
        {
            if (details == null || !details.Any())
                return Result<bool>.Fail("No audit log details provided.");

            foreach (var d in details)
            {
                var validation = ValidateDetail(d);
                if (!validation.IsValid)
                    return Result<bool>.Fail($"Validation failed for field '{d.FieldName}': {string.Join(", ", validation.Errors)}");
            }

            try
            {
                bool success = AuditLogDetailsData.InsertMany(details);
                if (!success)
                    return Result<bool>.Fail("Failed to insert audit log details in bulk.");

                return Result<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail("Exception inserting audit log details in bulk: " + ex.Message);
            }
        }

        // ===============================
        // Get details by AuditId
        // ===============================
        public static Result<IEnumerable<AuditLogDetail>> GetByAuditId(long auditId)
        {
            if (auditId <= 0)
                return Result<IEnumerable<AuditLogDetail>>.Fail("Invalid AuditId.");

            try
            {
                var details = AuditLogDetailsData.GetByAuditId(auditId);
                return Result<IEnumerable<AuditLogDetail>>.Ok(details);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<AuditLogDetail>>.Fail("Exception fetching audit log details: " + ex.Message);
            }
        }

        // ===============================
        // Validation logic for AuditLogDetail
        // ===============================
        private static ValidationResult.ValidationResult ValidateDetail(AuditLogDetail detail)
        {
            var result = new ValidationResult.ValidationResult();

            if (detail == null)
            {
                result.Add("AuditLogDetail cannot be null.");
                return result;
            }

            if (detail.AuditId <= 0)
                result.Add("AuditId must be a positive number.");

            if (string.IsNullOrWhiteSpace(detail.FieldName))
                result.Add("FieldName is required.");

            if (!string.IsNullOrWhiteSpace(detail.FieldName) && detail.FieldName.Length > MaxFieldNameLength)
                result.Add($"FieldName cannot exceed {MaxFieldNameLength} characters.");

            if (!string.IsNullOrEmpty(detail.OldValue) && detail.OldValue.Length > MaxValueLength)
                result.Add($"OldValue cannot exceed {MaxValueLength} characters.");

            if (!string.IsNullOrEmpty(detail.NewValue) && detail.NewValue.Length > MaxValueLength)
                result.Add($"NewValue cannot exceed {MaxValueLength} characters.");

            return result;
        }
    }

}
