using Clinic_Management_BLL.AuditWritter;
using Clinic_Management_BLL.LoginProcess;
using Clinic_Management_BLL.ResultWraper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.CrudInterface
{
    public interface ICrud<T>
    {
        Result<int> Create(T entity);
        Result Update(T entity);
        Result Delete(int id);
        Result<T> GetById(int id);
        Result<IEnumerable<T>> GetAll();
    }


    public abstract class BaseCrudService<T> : ICrud<T>
    {
        protected virtual string EntityName => typeof(T).Name;

        // --------- DAL Abstract Methods ---------
        protected abstract int DalCreate(T entity);
        protected abstract bool DalUpdate(T entity);
        protected abstract bool DalDelete(int id);
        protected abstract T? DalGetById(int id);
        protected abstract IEnumerable<T> DalGetAll();

        protected abstract ValidationResult.ValidationResult IsValidateData(T entity);
        protected abstract int GetEntityId(T entity);

        // --------- Permission Codes ---------
        protected abstract string CreatePermissionCode { get; }
        protected abstract string UpdatePermissionCode { get; }
        protected abstract string DeletePermissionCode { get; }
        protected abstract string ViewPermissionCode { get; }

        // --------- Audit Message ---------
        protected abstract string GetAuditMessage(string operation, T entity);

        // --------- CREATE ---------
        public Result<int> Create(T entity)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result<int>.Fail("Permission denied.");

            var validation = IsValidateData(entity);
            if (!validation.IsValid)
                return Result<int>.Fail(validation.Errors);

            var id = DalCreate(entity);

            AuditWriter.Write(
                action: GetAuditMessage("CREATE", entity),
                performedBy: 1,
                entityType: EntityName,
                entityId: id.ToString(),
                success: true,
                newEntity: entity
            );

            return Result<int>.Ok(id);
        }

        // --------- UPDATE ---------
        public Result Update(T entity)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result.Fail("Permission denied.");

            var old = DalGetById(GetEntityId(entity));
            if (old is null)
                return Result.Fail($"{EntityName} not found.");

            var validation = IsValidateData(entity);
            if (!validation.IsValid)
                return Result.Fail(validation.Errors);

            var success = DalUpdate(entity);

            if (success)
            {
                AuditWriter.Write(
                    action: GetAuditMessage("UPDATE", entity),
                    performedBy: SecurityContext.Current.UserId,
                    entityType: EntityName,
                    entityId: GetEntityId(entity).ToString(),
                    success: true,
                    oldEntity: old,
                    newEntity: entity
                );
                return Result.Ok();
            }
            else
            {
                return Result.Fail("Update failed.");
            }
        }

        // --------- DELETE ---------
        public Result Delete(int id)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            var old = DalGetById(id);
            if (old is null)
                return Result.Fail($"{EntityName} not found.");

            var success = DalDelete(id);

            if (success)
            {
                AuditWriter.Write(
                    action: $"{EntityName}.DELETE",
                    performedBy: SecurityContext.Current.UserId,
                    entityType: EntityName,
                    entityId: id.ToString(),
                    success: true,
                    oldEntity: old
                );
                return Result.Ok();
            }
            else
            {
                return Result.Fail("Delete failed.");
            }
        }

        // --------- READ ---------
        public Result<T> GetById(int id )
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<T>.Fail("Permission denied.");

            var entity = DalGetById(id);

            return entity is null
                ? Result<T>.Fail($"{EntityName} not found.")
                : Result<T>.Ok(entity);
        }

        public Result<IEnumerable<T>> GetAll()
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<IEnumerable<T>>.Fail("Permission denied.");

            return Result<IEnumerable<T>>.Ok(DalGetAll());
        }

        public ValidationResult.ValidationResult Validate(T entity)
        {
            return IsValidateData(entity);
        }




    }





}
