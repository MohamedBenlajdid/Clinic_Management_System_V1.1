using Clinic_Management_BLL.AuditWritter;
using Clinic_Management_BLL.CrudInterface;
using Clinic_Management_BLL.LoginProcess;
using Clinic_Management_BLL.ResultWraper;
using Clinic_Management_DAL.Data;
using Clinic_Management_Entities;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace Clinic_Management_BLL.Service
{
    public class UserService : BaseCrudService<User>
    {
        protected override string EntityName => "User";

        protected override int DalCreate(User entity) => UserData.Insert(entity);

        protected override bool DalUpdate(User entity) => UserData.Update(entity);

        protected override bool DalDelete(int id) => UserData.DeleteUser(id);

        protected override User? DalGetById(int id) => UserData.GetById(id);

        protected override IEnumerable<User> DalGetAll() => UserData.GetAll();

        protected override int GetEntityId(User entity) => entity.UserId;

        protected override ValidationResult.ValidationResult IsValidateData(User entity)
        {
            var validation = ValidationResult.ValidationResult.Success();

            if(entity.PersonId <=0)
            {
                validation.Add("Person Not Exist!");
            }

            if (string.IsNullOrWhiteSpace(entity.Username))
                validation.Add("Username cannot be empty.");

            if (UserData.IsUsernameExist(entity.Username, entity.UserId == 0 ? null : entity.UserId))
                validation.Add("Username already exists.");

            if (!string.IsNullOrWhiteSpace(entity.Email) &&
                UserData.IsEmailExist(entity.Email, entity.UserId == 0 ? null : entity.UserId))
                validation.Add("Email already exists.");

            // ====== Person already has User check ======
            if (entity.PersonId != 0 && // make sure PersonId is valid
                UserData.IsPersonExist(entity.PersonId, entity.UserId == 0 ? null : entity.UserId))
            {
                validation.Add("This person already has a user account.");
            }


            return validation;
        }

        protected override string CreatePermissionCode => "USER_CREATE";
        protected override string UpdatePermissionCode => "USER_UPDATE";
        protected override string DeletePermissionCode => "USER_DELETE";
        protected override string ViewPermissionCode => "USER_VIEW";
        protected
        override string GetAuditMessage(string operation, User entity)
            => $"User {operation}: {entity.Username} (ID: {entity.UserId})";

        // Override Delete to handle related cleanup or special rules if needed
        private bool DeleteUser(int userId)
        {
            // Example: Before deleting user, maybe check constraints or soft-delete instead.
            // For now, directly delete:

            return UserData.DeleteUser(userId);
        }


        public  Result<int> AddNew(User entity)
        {
            // 1) sanitize + auto-fill
            ApplyCreateDefaults(entity);


            var validation = IsValidateData(entity);
            if (!validation.IsValid)
                return Result<int>.Fail(validation.Errors);


            if (SecurityContext.Current == null)
            {
                var ID = DalCreate(entity);
                UserRoleData.SetUserRole(ID, 1002,true);
                return Result<int>.Ok(ID);

            }

            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result<int>.Fail("Permission denied.");



            var id = DalCreate(entity);

           
            AuditWriter.Write(
                action: GetAuditMessage("CREATE", entity),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: id.ToString(),
                success: true,
                newEntity: entity
            );

            return Result<int>.Ok(id);
        }

        // =========================================================
        // FINDERS (used by ucUserFinder)
        // =========================================================

        public Result<User> FindByPersonId(int personId)
        {
            if (personId <= 0)
                return Result<User>.Fail("Invalid Person ID.");

            var user = UserData.GetByPersonID(personId);

            return user == null
                ? Result<User>.Fail("User not found for this person.")
                : Result<User>.Ok(user);
        }

        public Result<User> FindByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return Result<User>.Fail("Username is required.");

            var user = UserData.GetByUsername(username.Trim());

            return user == null
                ? Result<User>.Fail("User not found.")
                : Result<User>.Ok(user);
        }

        public Result<User> FindByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Result<User>.Fail("Email is required.");

            var user = UserData.GetByEmail(email.Trim());

            return user == null
                ? Result<User>.Fail("User not found.")
                : Result<User>.Ok(user);
        }


        public void ApplyCreateDefaults(User entity)
        {
            entity.Username = entity.Username?.Trim();

            if(entity.PersonId <= 0)
            {
                throw new InvalidOperationException("Person Not Exist !");
            }
            entity.Email = PersonData.GetById(entity.PersonId).Email;
            // Must have hash+salt
            if (entity.PasswordHash == null || entity.PasswordHash.Length == 0)
                throw new InvalidOperationException("PasswordHash is required.");

            if (entity.PasswordSalt == null || entity.PasswordSalt.Length == 0)
                throw new InvalidOperationException("PasswordSalt is required.");

            // DB insert columns defaults
            entity.IsLocked = false;
            entity.LockoutEndAt = null;
            entity.FailedLoginCount = 0;
            entity.LastLoginAt = null;

            // if UI didn't set them, set defaults
            if (entity.UserId == 0)
            {
                // optional policies
                // entity.MustChangePassword = true; // if you want
                // entity.IsActive = true;          // if you want force active by default
            }
        }


        // Optional: Additional User-specific BLL methods

        public Result<User> Authenticate(string username, byte[] passwordHash)
        {
            var user = UserData.Authenticate(username, passwordHash);
            return user is null
                ? Result<User>.Fail("Invalid username or password.")
                : Result<User>.Ok(user);
        }

        public Result LockUser(int userId, DateTime? lockoutEndAt = null)
        {
            if (!UserData.LockUser(userId, lockoutEndAt))
                return Result.Fail("Failed to lock user.");

            // Add audit log here if you want

            return Result.Ok();
        }

        public Result UnlockUser(int userId)
        {
            if (!UserData.UnlockUser(userId))
                return Result.Fail("Failed to unlock user.");

            return Result.Ok();
        }

        public Result IncrementFailedLoginCount(int userId)
        {
            if (!UserData.IncrementFailedLoginCount(userId))
                return Result.Fail("Failed to increment failed login count.");

            return Result.Ok();
        }

        public Result ResetFailedLoginCount(int userId)
        {
            if (!UserData.ResetFailedLoginCount(userId))
                return Result.Fail("Failed to reset failed login count.");

            return Result.Ok();
        }

        public Result UpdateLastLogin(int userId)
        {
            if (!UserData.UpdateLastLogin(userId))
                return Result.Fail("Failed to update last login time.");

            return Result.Ok();
        }

        public Result ChangePassword(int userId, byte[] newPasswordHash, byte[]? newPasswordSalt = null, bool resetMustChange = true)
        {
            if (!UserData.ChangePassword(userId, newPasswordHash, newPasswordSalt, resetMustChange))
                return Result.Fail("Failed to change password.");

            return Result.Ok();
        }
    }

}
