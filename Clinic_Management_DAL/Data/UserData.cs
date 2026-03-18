using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class UserData
    {
        private const string Columns = @"
     UserId,PersonId,Username,Email,PasswordHash,PasswordSalt,
     MustChangePassword,IsLocked,LockoutEndAt,FailedLoginCount,
     LastLoginAt,IsActive,CreatedAt,UpdatedAt";

        // === Get by Primary Key ===
        public static User GetById(int id)
        {
            string query = $@"
     SELECT {Columns}
     FROM Users
     WHERE UserId = @Id";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<User>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", id)
            );
        }

        public static bool DeleteUser(int userId)
        {
            string query = @"
DELETE FROM Users
WHERE UserId = @UserId";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@UserId", userId)
            );
        }


        // === Insert New User ===
        public static int Insert(User user)
        {
            string query = @"
INSERT INTO Users
(
    PersonId,Username,Email,PasswordHash,PasswordSalt,
    MustChangePassword,IsLocked,LockoutEndAt,FailedLoginCount,
    LastLoginAt,IsActive
)
VALUES
(
    @PersonId,@Username,@Email,@PasswordHash,@PasswordSalt,
    @MustChangePassword,@IsLocked,@LockoutEndAt,@FailedLoginCount,
    @LastLoginAt,@IsActive
);

SELECT SCOPE_IDENTITY();";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),

                SqlParameterFactory.Create("@PersonId", user.PersonId),
                SqlParameterFactory.Create("@Username", user.Username),
                SqlParameterFactory.Create("@Email", (object)user.Email ?? DBNull.Value),
                SqlParameterFactory.Create("@PasswordHash", user.PasswordHash),
                SqlParameterFactory.Create("@PasswordSalt", (object)user.PasswordSalt ?? DBNull.Value),
                SqlParameterFactory.Create("@MustChangePassword", user.MustChangePassword),
                SqlParameterFactory.Create("@IsLocked", user.IsLocked),
                SqlParameterFactory.Create("@LockoutEndAt", (object)user.LockoutEndAt ?? DBNull.Value),
                SqlParameterFactory.Create("@FailedLoginCount", user.FailedLoginCount),
                SqlParameterFactory.Create("@LastLoginAt", (object)user.LastLoginAt ?? DBNull.Value),
                SqlParameterFactory.Create("@IsActive", user.IsActive)
            );
        }

        // === Update Existing User ===
        public static bool Update(User user)
        {
            string query = @"
UPDATE Users SET
    PersonId = @PersonId,
    Username = @Username,
    Email = @Email,
    PasswordHash = @PasswordHash,
    PasswordSalt = @PasswordSalt,
    MustChangePassword = @MustChangePassword,
    IsLocked = @IsLocked,
    LockoutEndAt = @LockoutEndAt,
    FailedLoginCount = @FailedLoginCount,
    LastLoginAt = @LastLoginAt,
    IsActive = @IsActive,
    UpdatedAt = SYSUTCDATETIME()
WHERE UserId = @Id";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,

                SqlParameterFactory.Create("@Id", user.UserId),
                SqlParameterFactory.Create("@PersonId", user.PersonId),
                SqlParameterFactory.Create("@Username", user.Username),
                SqlParameterFactory.Create("@Email", (object)user.Email ?? DBNull.Value),
                SqlParameterFactory.Create("@PasswordHash", user.PasswordHash),
                SqlParameterFactory.Create("@PasswordSalt", (object)user.PasswordSalt ?? DBNull.Value),
                SqlParameterFactory.Create("@MustChangePassword", user.MustChangePassword),
                SqlParameterFactory.Create("@IsLocked", user.IsLocked),
                SqlParameterFactory.Create("@LockoutEndAt", (object)user.LockoutEndAt ?? DBNull.Value),
                SqlParameterFactory.Create("@FailedLoginCount", user.FailedLoginCount),
                SqlParameterFactory.Create("@LastLoginAt", (object)user.LastLoginAt ?? DBNull.Value),
                SqlParameterFactory.Create("@IsActive", user.IsActive)
            );
        }

        // === Internal Helper: Fast Existence Check ===
        private static bool Exists(string field, object value, int? ignoreId = null)
        {
            string query = $@"
        SELECT 1
        FROM Users
        WHERE {field} = @Value
        {(ignoreId.HasValue ? "AND UserId <> @IgnoreId" : "")}";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read();
                },
                ignoreId.HasValue
                    ? new[]
                    {
                    SqlParameterFactory.Create("@Value", value ?? DBNull.Value),
                    SqlParameterFactory.Create("@IgnoreId", ignoreId.Value)
                    }
                    : new[]
                    {
                    SqlParameterFactory.Create("@Value", value ?? DBNull.Value)
                    }
            );
        }

        // === Check if Username Exists ===
        public static bool IsUsernameExist(string username, int? ignoreId = null)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;

            return Exists("Username", username, ignoreId);
        }

        public static bool IsPersonExist(int PersonID, int? ignoreId = null)
        {
            if (PersonID <=0)
                return false;

            return Exists("PersonId", PersonID, ignoreId);
        }


        // === Get User by Username ===
        public static User GetByUsername(string username)
        {
            string query = $@"
        SELECT {Columns}
        FROM Users
        WHERE Username = @Username";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<User>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Username", username)
            );
        }

        // === Check if Email Exists ===
        public static bool IsEmailExist(string email, int? ignoreId = null)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return Exists("Email", email, ignoreId);
        }

        // === Get User by Email ===
        public static User GetByEmail(string email)
        {
            string query = $@"
        SELECT {Columns}
        FROM Users
        WHERE Email = @Email";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<User>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Email", email)
            );
        }


        public static User GetByPersonID(int PersonID)
        {
            string query = $@"
        SELECT {Columns}
        FROM Users
        WHERE PersonId = @PersonId";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<User>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@PersonId", PersonID)
            );
        }


        // === Get All Users ===
        public static IEnumerable<User> GetAll()
        {
            string query = $@"
    SELECT {Columns}
    FROM Users";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<User>();
                    while (reader.Read())
                    {
                        list.Add(DbMapper<User>.Map(reader));
                    }
                    return list;
                }
            );
        }

        // =========================
        // Authentication & Security
        // =========================

        public static User? Authenticate(string username, byte[] passwordHash)
        {
            string query = $@"
        SELECT {Columns}
        FROM Users
        WHERE Username = @Username
        AND PasswordHash = @PasswordHash
        AND IsActive = 1
        AND (IsLocked = 0 OR LockoutEndAt <= SYSUTCDATETIME() OR LockoutEndAt IS NULL)";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read() ? DbMapper<User>.Map(reader) : null;
                },
                SqlParameterFactory.Create("@Username", username),
                SqlParameterFactory.Create("@PasswordHash", passwordHash)
            );
        }

        public static bool LockUser(int userId, DateTime? lockoutEndAt = null)
        {
            string query = @"
        UPDATE Users
        SET IsLocked = 1,
            LockoutEndAt = @LockoutEndAt,
            UpdatedAt = SYSUTCDATETIME()
        WHERE UserId = @UserId";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@LockoutEndAt", (object)lockoutEndAt ?? DBNull.Value),
                SqlParameterFactory.Create("@UserId", userId)
            );
        }

        public static bool UnlockUser(int userId)
        {
            string query = @"
        UPDATE Users
        SET IsLocked = 0,
            LockoutEndAt = NULL,
            FailedLoginCount = 0,
            UpdatedAt = SYSUTCDATETIME()
        WHERE UserId = @UserId";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@UserId", userId)
            );
        }

        public static bool IncrementFailedLoginCount(int userId)
        {
            string query = @"
        UPDATE Users
        SET FailedLoginCount = FailedLoginCount + 1,
            UpdatedAt = SYSUTCDATETIME()
        WHERE UserId = @UserId";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@UserId", userId)
            );
        }

        public static bool ResetFailedLoginCount(int userId)
        {
            string query = @"
        UPDATE Users
        SET FailedLoginCount = 0,
            UpdatedAt = SYSUTCDATETIME()
        WHERE UserId = @UserId";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@UserId", userId)
            );
        }

        public static bool UpdateLastLogin(int userId)
        {
            string query = @"
        UPDATE Users
        SET LastLoginAt = SYSUTCDATETIME(),
            UpdatedAt = SYSUTCDATETIME()
        WHERE UserId = @UserId";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@UserId", userId)
            );
        }

        public static bool ChangePassword(int userId, byte[] newPasswordHash, byte[]? newPasswordSalt = null, bool resetMustChange = true)
        {
            string query = @"
        UPDATE Users
        SET PasswordHash = @PasswordHash,
            PasswordSalt = @PasswordSalt,
            MustChangePassword = @MustChangePassword,
            UpdatedAt = SYSUTCDATETIME()
        WHERE UserId = @UserId";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@PasswordHash", newPasswordHash),
                SqlParameterFactory.Create("@PasswordSalt", (object)newPasswordSalt ?? DBNull.Value),
                SqlParameterFactory.Create("@MustChangePassword", !resetMustChange ? true : false),
                SqlParameterFactory.Create("@UserId", userId)
            );
        }








    }


}
