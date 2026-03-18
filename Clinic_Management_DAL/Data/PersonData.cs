using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_DAL.Data
{
    public static class PersonData
    {
        private const string Columns = @"
         PersonId,FirstName,LastName,BirthDate,GenderId,
         Phone1,Phone2,Email,CountryId,City,
         AddressLine,NationalId,CreatedAt,UpdatedAt,IsDeleted";

        // === Get by Primary Key ===
        public static Person GetById(int id)
        {
            string query = $@"
         SELECT {Columns}
         FROM People
         WHERE PersonId=@Id
         AND IsDeleted=0";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();

                    return reader.Read()
                        ? DbMapper<Person>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // === Insert New Person ===
        public static int Insert(Person p)
        {
            string query = @"
    INSERT INTO People
    (
        FirstName,LastName,BirthDate,GenderId,
        Phone1,Phone2,Email,CountryId,
        City,AddressLine,NationalId
    )
    VALUES
    (
        @FN,@LN,@BD,@G,
        @P1,@P2,@E,@C,
        @City,@Addr,@NID
    );

    SELECT SCOPE_IDENTITY();";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),

                SqlParameterFactory.Create("@FN", p.FirstName),
                SqlParameterFactory.Create("@LN", p.LastName),
                SqlParameterFactory.Create("@BD", p.BirthDate),
                SqlParameterFactory.Create("@G", p.GenderId),
                SqlParameterFactory.Create("@P1", p.Phone1),
                SqlParameterFactory.Create("@P2", p.Phone2),
                SqlParameterFactory.Create("@E", p.Email),
                SqlParameterFactory.Create("@C", p.CountryId),
                SqlParameterFactory.Create("@City", p.City),
                SqlParameterFactory.Create("@Addr", p.AddressLine),
                SqlParameterFactory.Create("@NID", p.NationalId)
            );
        }

        // === Update Existing Person ===
        public static bool Update(Person p)
        {
            string query = @"
    UPDATE People SET
        FirstName=@FN,
        LastName=@LN,
        BirthDate=@BD,
        GenderId=@G,
        Phone1=@P1,
        Phone2=@P2,
        Email=@E,
        CountryId=@C,
        City=@City,
        AddressLine=@Addr,
        NationalId=@NID,
        UpdatedAt=SYSUTCDATETIME()
    WHERE PersonId=@Id";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,

                SqlParameterFactory.Create("@Id", p.PersonId),
                SqlParameterFactory.Create("@FN", p.FirstName),
                SqlParameterFactory.Create("@LN", p.LastName),
                SqlParameterFactory.Create("@BD", p.BirthDate),
                SqlParameterFactory.Create("@G", p.GenderId),
                SqlParameterFactory.Create("@P1", p.Phone1),
                SqlParameterFactory.Create("@P2", p.Phone2),
                SqlParameterFactory.Create("@E", p.Email),
                SqlParameterFactory.Create("@C", p.CountryId),
                SqlParameterFactory.Create("@City", p.City),
                SqlParameterFactory.Create("@Addr", p.AddressLine),
                SqlParameterFactory.Create("@NID", p.NationalId)
            );
        }

        // === Soft Delete Person ===
        public static bool SoftDelete(int id)
        {
            string query = @"
    UPDATE People
    SET IsDeleted=1,
        UpdatedAt=SYSUTCDATETIME()
    WHERE PersonId=@Id";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@Id", id)
            );
        }

        // === Internal Helper: Fast Existence Check ===
        private static bool Exists(string field, object value, int? ignoreId = null)
        {
            string query = $@"
            SELECT 1
            FROM People
            WHERE {field} = @Value
            AND IsDeleted = 0
            {(ignoreId.HasValue ? "AND PersonId <> @IgnoreId" : "")}";

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

        // === Check if Phone Exists (checks both Phone1 and Phone2) ===
        public static bool IsPhoneExist(string phone, int? ignoreId = null)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            return Exists("Phone1", phone, ignoreId)
                || Exists("Phone2", phone, ignoreId);
        }

        // === Check if Email Exists ===
        public static bool IsEmailExist(string email, int? ignoreId = null)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return Exists("Email", email, ignoreId);
        }

        // === Check if NationalId Exists ===
        public static bool IsNationalIdExist(string nationalId, int? ignoreId = null)
        {
            if (string.IsNullOrWhiteSpace(nationalId))
                return false;

            return Exists("NationalId", nationalId, ignoreId);
        }

        // === Get Person by NationalId ===
        public static Person GetByNationalId(string nationalId)
        {
            string query = $@"
            SELECT {Columns}
            FROM People
            WHERE NationalId = @NID
            AND IsDeleted = 0";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Person>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@NID", nationalId)
            );
        }

        // === Get Person by Email ===
        public static Person GetByEmail(string email)
        {
            string query = $@"
            SELECT {Columns}
            FROM People
            WHERE Email = @Email
            AND IsDeleted = 0";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Person>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Email", email)
            );
        }

        // === Get Person by Phone (checks Phone1 or Phone2) ===
        public static Person GetByPhone(string phone)
        {
            string query = $@"
            SELECT {Columns}
            FROM People
            WHERE (Phone1 = @Phone OR Phone2 = @Phone)
            AND IsDeleted = 0";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Person>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@Phone", phone)
            );
        }

        public static IEnumerable<Person> GetAll()
        {
            string query = $@"
        SELECT {Columns}
        FROM People
        WHERE IsDeleted = 0";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Person>();
                    while (reader.Read())
                    {
                        list.Add(DbMapper<Person>.Map(reader));
                    }
                    return list;
                }
            );
        }



    }



}
