using Clinic_Management_BLL.ResultWraper;
using Clinic_Management_BLL.Service;
using Clinic_Management_DAL.Data;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace Clinic_Management_BLL.LoginProcess
{
    public sealed class SecurityContext
    {
        private static readonly AsyncLocal<SecurityContext?> _current = new();

        public int UserId { get; }
        public int PersonId { get; }
        public string Username { get; }
        public bool MustChangePassword { get; }
        public DateTime LoginTimeUtc { get; }

        // ✅ Instance-based permissions
        public IReadOnlySet<string> Permissions { get; }

        private SecurityContext(
            int userId,
            int personId,
            string username,
            bool mustChangePassword,
            IReadOnlySet<string> permissions)
        {
            UserId = userId;
            PersonId = personId;
            Username = username;
            MustChangePassword = mustChangePassword;
            Permissions = permissions;

            LoginTimeUtc = DateTime.UtcNow;
        }

        public static SecurityContext Current =>
            _current.Value ?? throw new InvalidOperationException("User is not authenticated.");

        internal static void Create(
            int userId,
            int personId,
            string username,
            bool mustChangePassword,
            IReadOnlySet<string> permissions)
        {
            _current.Value = new SecurityContext(
                userId,
                personId,
                username,
                mustChangePassword,
                permissions);
        }

        public static void Clear()
            => _current.Value = null;

        public static bool IsAuthenticated
            => _current.Value != null;

        // =========================
        // HELPER METHODS
        // =========================
        public static bool HasPermission(string permissionCode)
        {
            return IsAuthenticated &&
                   Current.Permissions.Contains(permissionCode);
        }

        public static void Demand(string permissionCode)
        {
            if (!HasPermission(permissionCode))
                throw new UnauthorizedAccessException(
                    $"Missing permission: {permissionCode}");
        }
    }





    public static class PasswordHasher
    {
        private const int SaltSize = 16; // 128-bit
        private const int HashSize = 32; // 256-bit
        private const int Iterations = 120_000;

        public static (byte[] Hash, byte[] Salt) HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty.");

            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            using var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256);

            byte[] hash = pbkdf2.GetBytes(HashSize);

            return (hash, salt);
        }

        public static bool Verify(
            string password,
            byte[] storedHash,
            byte[] storedSalt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                storedSalt,
                Iterations,
                HashAlgorithmName.SHA256);

            byte[] computedHash = pbkdf2.GetBytes(HashSize);

            return CryptographicOperations.FixedTimeEquals(
                computedHash,
                storedHash);
        }
    }


    public static class PermissionRegistry
    {
        private static Dictionary<int, string>? _idToCode;
        private static Dictionary<string, int>? _codeToId;
        private static readonly object _lock = new();

        private static void EnsureLoaded()
        {
            if (_idToCode == null || _codeToId == null)
            {
                lock (_lock)
                {
                    if (_idToCode == null || _codeToId == null)
                    {
                        var allPermissions = PermissionData.GetAll(); // Your DAL method to get all permissions
                        if (allPermissions == null)
                            throw new InvalidOperationException("Failed to load permissions from data source.");

                        _idToCode = allPermissions.ToDictionary(p => p.PermissionId, p => p.Code);
                        _codeToId = allPermissions.ToDictionary(p => p.Code, p => p.PermissionId);
                    }
                }
            }
        }

        /// <summary>
        /// Convert permission IDs to their string codes.
        /// Throws if any ID not found.
        /// </summary>
        public static IReadOnlySet<string> ConvertToCodes(IEnumerable<int> permissionIds)
        {
            EnsureLoaded();
            return permissionIds.Select(id =>
            {
                if (!_idToCode!.TryGetValue(id, out var code))
                    throw new KeyNotFoundException($"Permission ID '{id}' not found in registry.");
                return code;
            }).ToHashSet();
        }

        /// <summary>
        /// Convert permission codes to their IDs.
        /// Throws if any code not found.
        /// </summary>
        public static IReadOnlySet<int> ConvertToIds(IEnumerable<string> permissionCodes)
        {
            EnsureLoaded();
            return permissionCodes.Select(code =>
            {
                if (!_codeToId!.TryGetValue(code, out var id))
                    throw new KeyNotFoundException($"Permission code '{code}' not found in registry.");
                return id;
            }).ToHashSet();
        }

        /// <summary>
        /// Check if a permission code exists.
        /// </summary>
        public static bool ContainsCode(string code)
        {
            EnsureLoaded();
            return _codeToId!.ContainsKey(code);
        }

        /// <summary>
        /// Check if a permission ID exists.
        /// </summary>
        public static bool ContainsId(int id)
        {
            EnsureLoaded();
            return _idToCode!.ContainsKey(id);
        }
    }





    public static class PermissionResolver
    {
        public static IReadOnlySet<string> GetPermissionsAsCodes(int userId)
        {
            // HashSet = O(1)
            var permissions = new HashSet<int>();

            //----------------------------------------
            // 1️⃣ Load Role Permissions
            //----------------------------------------
            var rolePermissionsResult =
                RolePermissionService.GetPermissionsForUser(userId);

            if (rolePermissionsResult.IsFailure)
                throw new Exception("Failed to resolve role permissions.");

            permissions.UnionWith(rolePermissionsResult.Value);


            //----------------------------------------
            // 2️⃣ Apply Overrides
            //----------------------------------------
            var overridesResult =
                UserPermissionOverrideService.GetActiveOverrides(userId);

            if (overridesResult.IsFailure)
                throw new Exception("Failed to resolve permission overrides.");

            foreach (var ov in overridesResult.Value)
            {
                if (ov.OverrideType ==
                    (byte)UserPermissionOverrideData.PermissionOverrideType.Deny)
                {
                    permissions.Remove(ov.PermissionId);
                }
                else // Grant
                {
                    permissions.Add(ov.PermissionId);
                }
            }

            //----------------------------------------
            // 3️⃣ Convert to Codes
            //----------------------------------------
            return PermissionRegistry.ConvertToCodes(permissions);
        }
    }




    public static class AuthenticationService
    {
        private const int MaxFailedAttempts = 5;
        private static readonly TimeSpan DefaultLockTime = TimeSpan.FromMinutes(15);
        public static IReadOnlySet<string> permissions ;
        // =========================================================
        // LOGIN
        // =========================================================
        public static Result<int> Login(string username, string password)
        {
            // Optional but recommended guard
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                return Result<int>.Fail("Username and password are required.");
            }

            // 1️⃣ Get user
            var user = UserData.GetByUsername(username);

            // 🔐 Prevent username enumeration attack
            if (user is null)
                return Result<int>.Fail("Invalid username or password.");

            if (!user.IsActive)
                return Result<int>.Fail("User is inactive.");

            // 2️⃣ Check lock
            if (user.IsLocked &&
                user.LockoutEndAt.HasValue &&
                user.LockoutEndAt > DateTime.UtcNow)
            {
                return Result<int>.Fail("User is locked. Try again later.");
            }

            // 3️⃣ Verify password
            bool verified = PasswordHasher.Verify(
                password,
                user.PasswordHash,
                user.PasswordSalt);

            if (!verified)
            {
                HandleFailedLogin(user);
                return Result<int>.Fail("Invalid username or password.");
            }

            // ✅ SUCCESS PATH

            UserData.ResetFailedLoginCount(user.UserId);
            UserData.UnlockUser(user.UserId);
            UserData.UpdateLastLogin(user.UserId);

            // 4️⃣ Load permissions snapshot
            IReadOnlySet<string> permissions = PermissionResolver
                .GetPermissionsAsCodes(user.UserId);


            // 5️⃣ Create security context
            SecurityContext.Create(
                user.UserId,
                user.PersonId,
                user.Username,
                user.MustChangePassword,
                permissions);

            return Result<int>.Ok(user.UserId, "Login successful.");
        }


        // =========================================================
        // LOGOUT
        // =========================================================
        public static void Logout()
        {
            SecurityContext.Clear();
        }

        // =========================================================
        // FAILED LOGIN HANDLER
        // =========================================================
        private static void HandleFailedLogin(User user)
        {
            UserData.IncrementFailedLoginCount(user.UserId);

            int newCount = user.FailedLoginCount + 1;

            if (newCount >= MaxFailedAttempts)
            {
                UserData.LockUser(
                    user.UserId,
                    DateTime.UtcNow.Add(DefaultLockTime));
            }
        }
    }


}
