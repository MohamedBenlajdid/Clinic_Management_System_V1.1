using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class InsuranceProviderData
    {
        private const string Columns = @"
     InsuranceProviderId,Name,Phone,Email,
     Website,AddressLine,City,CountryId,
     IsActive,CreatedAt";

        // === Get by Primary Key ===
        public static InsuranceProvider GetById(int id)
        {
            string query = $@"
     SELECT {Columns}
     FROM InsuranceProviders
     WHERE InsuranceProviderId = @Id";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<InsuranceProvider>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // === Insert New InsuranceProvider ===
        public static int Insert(InsuranceProvider ip)
        {
            string query = @"
INSERT INTO InsuranceProviders
(
    Name,Phone,Email,Website,
    AddressLine,City,CountryId,IsActive
)
VALUES
(
    @Name,@Phone,@Email,@Website,
    @AddressLine,@City,@CountryId,@IsActive
);

SELECT SCOPE_IDENTITY();";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),

                SqlParameterFactory.Create("@Name", ip.Name),
                SqlParameterFactory.Create("@Phone", (object)ip.Phone ?? DBNull.Value),
                SqlParameterFactory.Create("@Email", (object)ip.Email ?? DBNull.Value),
                SqlParameterFactory.Create("@Website", (object)ip.Website ?? DBNull.Value),
                SqlParameterFactory.Create("@AddressLine", (object)ip.AddressLine ?? DBNull.Value),
                SqlParameterFactory.Create("@City", (object)ip.City ?? DBNull.Value),
                SqlParameterFactory.Create("@CountryId", (object)ip.CountryId ?? DBNull.Value),
                SqlParameterFactory.Create("@IsActive", ip.IsActive)
            );
        }

        // === Update Existing InsuranceProvider ===
        public static bool Update(InsuranceProvider ip)
        {
            string query = @"
UPDATE InsuranceProviders SET
    Name = @Name,
    Phone = @Phone,
    Email = @Email,
    Website = @Website,
    AddressLine = @AddressLine,
    City = @City,
    CountryId = @CountryId,
    IsActive = @IsActive
WHERE InsuranceProviderId = @Id";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,

                SqlParameterFactory.Create("@Id", ip.InsuranceProviderId),
                SqlParameterFactory.Create("@Name", ip.Name),
                SqlParameterFactory.Create("@Phone", (object)ip.Phone ?? DBNull.Value),
                SqlParameterFactory.Create("@Email", (object)ip.Email ?? DBNull.Value),
                SqlParameterFactory.Create("@Website", (object)ip.Website ?? DBNull.Value),
                SqlParameterFactory.Create("@AddressLine", (object)ip.AddressLine ?? DBNull.Value),
                SqlParameterFactory.Create("@City", (object)ip.City ?? DBNull.Value),
                SqlParameterFactory.Create("@CountryId", (object)ip.CountryId ?? DBNull.Value),
                SqlParameterFactory.Create("@IsActive", ip.IsActive)
            );
        }

        // === Internal Helper: Fast Existence Check (e.g. Name) ===
        private static bool Exists(string field, object value, int? ignoreId = null)
        {
            string query = $@"
        SELECT 1
        FROM InsuranceProviders
        WHERE {field} = @Value
        {(ignoreId.HasValue ? "AND InsuranceProviderId <> @IgnoreId" : "")}";

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

        public static bool Delete(int id)
        {
            string query = @"
DELETE FROM InsuranceProviders
WHERE InsuranceProviderId = @Id";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", id)
            );
        }


        // === Check if Name Exists ===
        public static bool IsNameExist(string name, int? ignoreId = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            return Exists("Name", name, ignoreId);
        }

        // === Get InsuranceProvider by Name ===
        public static InsuranceProvider GetByName(string name)
        {
            string query = $@"
        SELECT {Columns}
        FROM InsuranceProviders
        WHERE Name = @Name";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<InsuranceProvider>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Name", name)
            );
        }

        // === Get All InsuranceProviders ===
        public static IEnumerable<InsuranceProvider> GetAll()
        {
            string query = $@"
    SELECT {Columns}
    FROM InsuranceProviders";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<InsuranceProvider>();
                    while (reader.Read())
                    {
                        list.Add(DbMapper<InsuranceProvider>.Map(reader));
                    }
                    return list;
                }
            );
        }



        // === Check if Phone Exists ===
        public static bool IsPhoneExist(string phone, int? ignoreId = null)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            return Exists("Phone", phone, ignoreId);
        }

        // === Get InsuranceProvider by Phone ===
        public static InsuranceProvider GetByPhone(string phone)
        {
            string query = $@"
    SELECT {Columns}
    FROM InsuranceProviders
    WHERE Phone = @Phone";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<InsuranceProvider>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Phone", phone)
            );
        }

        // === Check if Email Exists ===
        public static bool IsEmailExist(string email, int? ignoreId = null)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return Exists("Email", email, ignoreId);
        }

        // === Get InsuranceProvider by Email ===
        public static InsuranceProvider GetByEmail(string email)
        {
            string query = $@"
    SELECT {Columns}
    FROM InsuranceProviders
    WHERE Email = @Email";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<InsuranceProvider>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Email", email)
            );
        }

        // === Check if Website Exists ===
        public static bool IsWebsiteExist(string website, int? ignoreId = null)
        {
            if (string.IsNullOrWhiteSpace(website))
                return false;

            return Exists("Website", website, ignoreId);
        }

        // === Get InsuranceProvider by Website ===
        public static InsuranceProvider GetByWebsite(string website)
        {
            string query = $@"
    SELECT {Columns}
    FROM InsuranceProviders
    WHERE Website = @Website";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<InsuranceProvider>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Website", website)
            );
        }

        // === Check if AddressLine Exists ===
        public static bool IsAddressLineExist(string addressLine, int? ignoreId = null)
        {
            if (string.IsNullOrWhiteSpace(addressLine))
                return false;

            return Exists("AddressLine", addressLine, ignoreId);
        }

        // === Get InsuranceProvider by AddressLine ===
        public static InsuranceProvider GetByAddressLine(string addressLine)
        {
            string query = $@"
    SELECT {Columns}
    FROM InsuranceProviders
    WHERE AddressLine = @AddressLine";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<InsuranceProvider>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@AddressLine", addressLine)
            );
        }



    }

}
