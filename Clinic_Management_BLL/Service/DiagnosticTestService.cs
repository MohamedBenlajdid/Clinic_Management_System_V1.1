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

    public sealed class DiagnosticTestService : BaseCrudService<DiagnosticTest>
    {
        // =======================
        // PERMISSIONS
        // =======================
        protected override string CreatePermissionCode => "DIAGNOSTIC_TEST_CREATE";
        protected override string UpdatePermissionCode => "DIAGNOSTIC_TEST_UPDATE";
        protected override string DeletePermissionCode => "DIAGNOSTIC_TEST_DELETE"; // (we will map to Deactivate)
        protected override string ViewPermissionCode => "DIAGNOSTIC_TEST_VIEW";

        protected override string EntityName => "DiagnosticTest";

        // =======================
        // DAL WIRING
        // =======================
        protected override int DalCreate(DiagnosticTest entity)
            => DiagnosticTestData.Insert(entity);

        protected override bool DalUpdate(DiagnosticTest entity)
            => DiagnosticTestData.Update(entity);

        // BaseCrud "Delete" mapped to soft deactivate (IsActive = 0)
        protected override bool DalDelete(int id)
            => DiagnosticTestData.GetById(id) != null
               && DiagnosticTestData.Deactivate(id);

        protected override DiagnosticTest? DalGetById(int id)
            => DiagnosticTestData.GetById(id);

        // BaseCrudService doesn't support params, so return active only by default
        protected override IEnumerable<DiagnosticTest> DalGetAll()
            => DiagnosticTestData.GetAll(onlyActive: true) ?? Enumerable.Empty<DiagnosticTest>();

        protected override int GetEntityId(DiagnosticTest entity)
            => entity.DiagnosticTestId;

        // =======================
        // VALIDATION
        // =======================
        protected override ValidationResult.ValidationResult IsValidateData(DiagnosticTest t)
        {
            var v = ValidationResult.ValidationResult.Success();

            if (t is null)
            {
                v.Add("DiagnosticTest is required.");
                return v;
            }

            if (string.IsNullOrWhiteSpace(t.Name))
                v.Add("Name is required.");

            // optional: Code uniqueness if provided
            if (!string.IsNullOrWhiteSpace(t.Code))
            {
                bool codeExists = DiagnosticTestData.ExistsByCode(
                    t.Code.Trim(),
                    ignoreId: t.DiagnosticTestId > 0 ? t.DiagnosticTestId : null);

                if (codeExists)
                    v.Add("Code already exists.");
            }

            // Name must be unique
            if (!string.IsNullOrWhiteSpace(t.Name))
            {
                bool nameExists = DiagnosticTestData.ExistsByName(
                    t.Name.Trim(),
                    ignoreId: t.DiagnosticTestId > 0 ? t.DiagnosticTestId : null);

                if (nameExists)
                    v.Add("Name already exists.");
            }

            return v;
        }

        // =======================
        // READ OPERATIONS (with Permission + Audit)
        // =======================
        public Result<DiagnosticTest> GetByIdSafe(int diagnosticTestId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<DiagnosticTest>.Fail("Permission denied.");

            if (diagnosticTestId <= 0)
                return Result<DiagnosticTest>.Fail("Invalid DiagnosticTestId.");

            var t = DiagnosticTestData.GetById(diagnosticTestId);
            if (t is null)
                return Result<DiagnosticTest>.Fail("Diagnostic test not found.");

            AuditWriter.Write(
                action: GetAuditMessage("VIEW", t),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: t.DiagnosticTestId.ToString(),
                success: true,
                newEntity: t
            );

            return Result<DiagnosticTest>.Ok(t);
        }

        public Result<IEnumerable<DiagnosticTest>> GetAllSafe(bool onlyActive = true)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<DiagnosticTest>>.Fail("Permission denied.");

            var list = DiagnosticTestData.GetAll(onlyActive) ?? Enumerable.Empty<DiagnosticTest>();

            AuditLogData.Log("View Diagnostic Tests", true, SecurityContext.Current.UserId, EntityName);

            return Result<IEnumerable<DiagnosticTest>>.Ok(list);
        }

        // =======================
        // CREATE / UPDATE (Smart wrappers with Audit)
        // =======================
        public Result<int> CreateDiagnosticTest(DiagnosticTest t)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result<int>.Fail("Permission denied.");

            // default
            if (t.IsActive == false && t.DiagnosticTestId <= 0)
            {
                // allow false if you want, but usually default to true for new test
                // t.IsActive = true;
            }

            var v = IsValidateData(t);
            if (!v.IsValid)
                return Result<int>.Fail(v.Errors);

            int newId = DiagnosticTestData.Insert(t);

            bool ok = newId > 0;
            if (ok)
            {
                t.DiagnosticTestId = newId;

                AuditWriter.Write(
                    action: GetAuditMessage("CREATE", t),
                    performedBy: SecurityContext.Current.UserId,
                    entityType: EntityName,
                    entityId: newId.ToString(),
                    success: true,
                    newEntity: t
                );

                AuditLogData.Log("Create Diagnostic Test", true, SecurityContext.Current.UserId, EntityName);

                return Result<int>.Ok(newId);
            }

            AuditWriter.Write(
                action: $"{EntityName} CREATE failed",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: "0",
                success: false,
                newEntity: t,
                failureReason: "Insert returned 0"
            );

            return Result<int>.Fail("Failed to create diagnostic test.");
        }

        public Result UpdateDiagnosticTest(DiagnosticTest t)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            if (t.DiagnosticTestId <= 0)
                return Result.Fail("Invalid DiagnosticTestId.");

            var old = DiagnosticTestData.GetById(t.DiagnosticTestId);
            if (old is null)
                return Result.Fail("Diagnostic test not found.");

            var v = IsValidateData(t);
            if (!v.IsValid)
                return Result.Fail(v.Errors);

            bool ok = DiagnosticTestData.Update(t);

            AuditWriter.Write<DiagnosticTest>(
                action: GetAuditMessage("UPDATE", t),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: t.DiagnosticTestId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: t,
                failureReason: ok ? null : "Update returned false"
            );

            if (ok) AuditLogData.Log("Update Diagnostic Test", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Failed to update diagnostic test.");
        }

        // =======================
        // DEACTIVATE (Soft delete)
        // =======================
        public Result Deactivate(int diagnosticTestId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            if (diagnosticTestId <= 0)
                return Result.Fail("Invalid DiagnosticTestId.");

            var old = DiagnosticTestData.GetById(diagnosticTestId);
            if (old is null)
                return Result.Fail("Diagnostic test not found.");

            bool ok = DiagnosticTestData.Deactivate(diagnosticTestId);

            var after = new DiagnosticTest
            {
                DiagnosticTestId = old.DiagnosticTestId,
                Code = old.Code,
                Name = old.Name,
                Category = old.Category,
                Unit = old.Unit,
                RefRange = old.RefRange,
                IsActive = false
            };

            AuditWriter.Write<DiagnosticTest>(
                action: $"{EntityName} [{diagnosticTestId}] DEACTIVATE performed.",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: diagnosticTestId.ToString(),
                success: ok,
                oldEntity: old,
                newEntity: after,
                failureReason: ok ? null : "Deactivate returned false"
            );

            if (ok) AuditLogData.Log("Deactivate Diagnostic Test", true, SecurityContext.Current.UserId, EntityName);

            return ok ? Result.Ok() : Result.Fail("Deactivate failed.");
        }

        // =======================
        // AUDIT MESSAGE
        // =======================
        protected override string GetAuditMessage(string operation, DiagnosticTest entity)
            => $"{EntityName} [{entity.DiagnosticTestId}] {operation} performed.";
    }
}
