using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.Service
{
    using Clinic_Management_BLL.AuditWritter;
    using Clinic_Management_BLL.CrudInterface;
    using Clinic_Management_BLL.LoginProcess;
    using Clinic_Management_BLL.ResultWraper;
    using Clinic_Management_DAL.Data;
    using Clinic_Management_Entities.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class DiagnosticResultService : BaseCrudService<DiagnosticResult>
    {
        // =======================
        // PERMISSIONS
        // =======================
        protected override string CreatePermissionCode => "DIAGNOSTIC_RESULT_CREATE";
        protected override string UpdatePermissionCode => "DIAGNOSTIC_RESULT_UPDATE";
        protected override string DeletePermissionCode => "DIAGNOSTIC_RESULT_DELETE";
        protected override string ViewPermissionCode => "DIAGNOSTIC_RESULT_VIEW";

        protected override string EntityName => "DiagnosticResult";

        // =======================
        // DAL WIRING
        // =======================
        protected override int DalCreate(DiagnosticResult entity)
            => DiagnosticResultData.Insert(entity);

        protected override bool DalUpdate(DiagnosticResult entity)
            => DiagnosticResultData.Update(entity);

        protected override bool DalDelete(int id)
            => DiagnosticResultData.GetById(id) != null
               && DiagnosticResultData.Delete(id);

        protected override DiagnosticResult? DalGetById(int id)
            => DiagnosticResultData.GetById(id);

        protected override IEnumerable<DiagnosticResult> DalGetAll()
            => DiagnosticResultData.GetAll() ?? Enumerable.Empty<DiagnosticResult>();

        protected override int GetEntityId(DiagnosticResult entity)
            => entity.DiagnosticResultId;

        // =======================
        // VALIDATION
        // =======================
        protected override ValidationResult.ValidationResult IsValidateData(DiagnosticResult r)
        {
            var v = ValidationResult.ValidationResult.Success();

            if (r is null)
            {
                v.Add("DiagnosticResult is required.");
                return v;
            }

            if (r.DiagnosticRequestItemId <= 0)
                v.Add("DiagnosticRequestItemId is required.");

            // CK_DiagnosticResults_AtLeastOne
            bool hasAny =
                !string.IsNullOrWhiteSpace(r.ResultText)
                || r.ResultNumeric.HasValue
                || !string.IsNullOrWhiteSpace(r.ReportText);

            if (!hasAny)
                v.Add("At least one of ResultText, ResultNumeric, or ReportText must be provided.");


            if (DiagnosticResultData.ExistsByRequestItemId(
        r.DiagnosticRequestItemId,
        r.DiagnosticResultId))
            {
                v.Add("Diagnostic Item Already Has Result!");
            }

            // Optional: if numeric provided but unit empty (your business rule)
            // if (r.ResultNumeric.HasValue && string.IsNullOrWhiteSpace(r.Unit))
            //     v.Add("Unit is required when ResultNumeric is provided.");

            return v;
        }

        // =======================
        // READ OPERATIONS (with Permission + Audit)
        // =======================
        public Result<DiagnosticResult> GetByIdSafe(int diagnosticResultId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<DiagnosticResult>.Fail("Permission denied.");

            if (diagnosticResultId <= 0)
                return Result<DiagnosticResult>.Fail("Invalid DiagnosticResultId.");

            var r = DiagnosticResultData.GetById(diagnosticResultId);
            if (r is null)
                return Result<DiagnosticResult>.Fail("Diagnostic result not found.");

            AuditWriter.Write(
                action: GetAuditMessage("VIEW", r),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: r.DiagnosticResultId.ToString(),
                success: true,
                newEntity: r
            );

            return Result<DiagnosticResult>.Ok(r);
        }

        public Result<DiagnosticResult> GetByRequestItemIdSafe(int diagnosticRequestItemId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<DiagnosticResult>.Fail("Permission denied.");

            if (diagnosticRequestItemId <= 0)
                return Result<DiagnosticResult>.Fail("Invalid DiagnosticRequestItemId.");

            var r = DiagnosticResultData.GetByRequestItemId(diagnosticRequestItemId);
            if (r is null)
                return Result<DiagnosticResult>.Fail("Diagnostic result not found.");

            AuditWriter.Write(
                action: $"{EntityName} [Item:{diagnosticRequestItemId}] VIEW performed.",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: r.DiagnosticResultId.ToString(),
                success: true,
                newEntity: r
            );

            return Result<DiagnosticResult>.Ok(r);
        }

        public Result<IEnumerable<DiagnosticResult>> GetAllSafe()
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<DiagnosticResult>>.Fail("Permission denied.");

            var list = DiagnosticResultData.GetAll() ?? Enumerable.Empty<DiagnosticResult>();

            if(list.Any())
            {
                AuditLogData.Log("View Diagnostic Results", true, SecurityContext.Current.UserId, EntityName);

                return Result<IEnumerable<DiagnosticResult>>.Ok(list);
            }

            return Result<IEnumerable<DiagnosticResult>>.Fail("Cannot Find Diagnostic Results !");

        }

        // =======================
        // CREATE / UPDATE (Smart wrappers with Audit)
        // =======================
        public Result<int> CreateDiagnosticResult(DiagnosticResult r)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result<int>.Fail("Permission denied.");

            var v = IsValidateData(r);
            if (!v.IsValid)
                return Result<int>.Fail(v.Errors);

            // enforce 1:1 per request item (if your DB enforces unique, this is extra safety)
            if (DiagnosticResultData.ExistsByRequestItemId(r.DiagnosticRequestItemId))
                return Result<int>.Fail("A result already exists for this DiagnosticRequestItemId.");

            int newId = DiagnosticResultData.Insert(r);

            bool ok = newId > 0;
            if (ok)
            {
                r.DiagnosticResultId = newId;

                AuditWriter.Write(
                    action: GetAuditMessage("CREATE", r),
                    performedBy: SecurityContext.Current.UserId,
                    entityType: EntityName,
                    entityId: newId.ToString(),
                    success: true,
                    newEntity: r
                );

                AuditLogData.Log("Create Diagnostic Result", true, SecurityContext.Current.UserId, EntityName);

                return Result<int>.Ok(newId);
            }

            AuditWriter.Write(
                action: $"{EntityName} CREATE failed",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: "0",
                success: false,
                newEntity: r,
                failureReason: "Insert returned 0"
            );

            return Result<int>.Fail("Failed to create diagnostic result.");
        }

        public Result UpdateDiagnosticResult(DiagnosticResult r)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (r.DiagnosticResultId <= 0)
                return Result.Fail("Invalid DiagnosticResultId.");

            var old = DiagnosticResultData.GetById(r.DiagnosticResultId);
            if (old is null)
                return Result.Fail("Diagnostic result not found.");

            var v = IsValidateData(r);
            if (!v.IsValid)
                return Result.Fail(v.Errors);

            // prevent moving result to another item that already has a result
            if (DiagnosticResultData.ExistsByRequestItemId(r.DiagnosticRequestItemId, ignoreResultId: r.DiagnosticResultId))
                return Result.Fail("Another result already exists for this DiagnosticRequestItemId.");

            bool ok = DiagnosticResultData.Update(r);

            AuditWriter.Write<DiagnosticResult>(
                action: GetAuditMessage("UPDATE", r),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: r.DiagnosticResultId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: r,
                failureReason: ok ? null : "Update returned false"
            );

            if (ok) AuditLogData.Log("Update Diagnostic Result", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Failed to update diagnostic result.");
        }

        // =======================
        // QUICK OPS
        // =======================
        public Result SetPerformedAt(int diagnosticResultId, DateTime performedAt)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (diagnosticResultId <= 0)
                return Result.Fail("Invalid DiagnosticResultId.");

            var old = DiagnosticResultData.GetById(diagnosticResultId);
            if (old is null)
                return Result.Fail("Diagnostic result not found.");

            bool ok = DiagnosticResultData.SetPerformedAt(diagnosticResultId, performedAt);

            var after = new DiagnosticResult
            {
                DiagnosticResultId = old.DiagnosticResultId,
                DiagnosticRequestItemId = old.DiagnosticRequestItemId,
                ResultText = old.ResultText,
                ResultNumeric = old.ResultNumeric,
                Unit = old.Unit,
                RefRange = old.RefRange,
                ReportText = old.ReportText,
                PerformedAt = performedAt,
                VerifiedAt = old.VerifiedAt
            };

            AuditWriter.Write<DiagnosticResult>(
                action: $"{EntityName} [{diagnosticResultId}] SET_PERFORMED_AT performed.",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: diagnosticResultId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: after,
                failureReason: ok ? null : "SetPerformedAt returned false"
            );

            if (ok) AuditLogData.Log("Set Diagnostic Result PerformedAt", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("SetPerformedAt failed.");
        }

        public Result SetVerifiedAt(int diagnosticResultId, DateTime verifiedAt)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (diagnosticResultId <= 0)
                return Result.Fail("Invalid DiagnosticResultId.");

            var old = DiagnosticResultData.GetById(diagnosticResultId);
            if (old is null)
                return Result.Fail("Diagnostic result not found.");

            bool ok = DiagnosticResultData.SetVerifiedAt(diagnosticResultId, verifiedAt);

            var after = new DiagnosticResult
            {
                DiagnosticResultId = old.DiagnosticResultId,
                DiagnosticRequestItemId = old.DiagnosticRequestItemId,
                ResultText = old.ResultText,
                ResultNumeric = old.ResultNumeric,
                Unit = old.Unit,
                RefRange = old.RefRange,
                ReportText = old.ReportText,
                PerformedAt = old.PerformedAt,
                VerifiedAt = verifiedAt
            };

            AuditWriter.Write<DiagnosticResult>(
                action: $"{EntityName} [{diagnosticResultId}] SET_VERIFIED_AT performed.",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: diagnosticResultId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: after,
                failureReason: ok ? null : "SetVerifiedAt returned false"
            );

            if (ok) AuditLogData.Log("Set Diagnostic Result VerifiedAt", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("SetVerifiedAt failed.");
        }

        // =======================
        // DELETE (Hard)
        // =======================
        public Result DeleteDiagnosticResult(int diagnosticResultId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            if (diagnosticResultId <= 0)
                return Result.Fail("Invalid DiagnosticResultId.");

            var old = DiagnosticResultData.GetById(diagnosticResultId);
            if (old is null)
                return Result.Fail("Diagnostic result not found.");

            bool ok = DiagnosticResultData.Delete(diagnosticResultId);

            AuditWriter.Write<DiagnosticResult>(
                action: GetAuditMessage("DELETE", old),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: diagnosticResultId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: default,
                failureReason: ok ? null : "Delete returned false"
            );

            if (ok) AuditLogData.Log("Delete Diagnostic Result", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Delete failed.");
        }

        // =======================
        // AUDIT MESSAGE
        // =======================
        protected override string GetAuditMessage(string operation, DiagnosticResult entity)
            => $"{EntityName} [{entity.DiagnosticResultId}] {operation} performed.";
    }
}
