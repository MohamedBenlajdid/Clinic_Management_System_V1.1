using Clinic_Management_BLL.CrudInterface;
using Clinic_Management_BLL.ResultWraper;
using Clinic_Management_DAL.Data;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.Service
{
    public class PersonService : BaseCrudService<Person>
    {
        // Permission codes (customize as needed)
        protected override string CreatePermissionCode => "PERSON_CREATE";
        protected override string UpdatePermissionCode => "PERSON_UPDATE";
        protected override string DeletePermissionCode => "PERSON_DELETE";
        protected override string ViewPermissionCode => "PERSON_VIEW";

        protected override string EntityName => "Person";

        // DAL method implementations

        protected override int DalCreate(Person entity)
            => PersonData.Insert(entity);

        protected override bool DalUpdate(Person entity)
            => PersonData.Update(entity);

        protected override bool DalDelete(int id)
            => PersonData.GetById(id) != null && PersonData.SoftDelete(id);

        protected override Person? DalGetById(int id)
            => PersonData.GetById(id);

        protected override IEnumerable<Person> DalGetAll()
            => PersonData.GetAll();

        protected override int GetEntityId(Person entity)
            => entity.PersonId;

        protected override ValidationResult.ValidationResult IsValidateData(Person entity)
        {
            var validation = ValidationResult.ValidationResult.Success();

            // FirstName and LastName validation
            if (string.IsNullOrWhiteSpace(entity.FirstName))
                validation.Add("FirstName cannot be empty.");

            if (string.IsNullOrWhiteSpace(entity.LastName))
                validation.Add("LastName cannot be empty.");

            // BirthDate validation (optional, example)
            if (entity.BirthDate == default)
                validation.Add("BirthDate must be specified.");

            // GenderId validation (optional)
            if (entity.GenderId <= 0)
                validation.Add("GenderId must be valid.");

            // Unique Phone check (Phone1 and Phone2)
            if (!string.IsNullOrWhiteSpace(entity.Phone1))
            {
                bool phone1Exists = PersonData.IsPhoneExist(entity.Phone1, entity.PersonId == 0 ? null : entity.PersonId);
                if (phone1Exists)
                    validation.Add("Phone1 already exists.");
            }

            if (!string.IsNullOrWhiteSpace(entity.Phone2))
            {
                bool phone2Exists = PersonData.IsPhoneExist(entity.Phone2, entity.PersonId == 0 ? null : entity.PersonId);
                if (phone2Exists)
                    validation.Add("Phone2 already exists.");
            }

            // Unique Email check
            if (!string.IsNullOrWhiteSpace(entity.Email))
            {
                bool emailExists = PersonData.IsEmailExist(entity.Email, entity.PersonId == 0 ? null : entity.PersonId);
                if (emailExists)
                    validation.Add("Email already exists.");
            }

            // Unique NationalId check
            if (!string.IsNullOrWhiteSpace(entity.NationalId))
            {
                bool nationalIdExists = PersonData.IsNationalIdExist(entity.NationalId, entity.PersonId == 0 ? null : entity.PersonId);
                if (nationalIdExists)
                    validation.Add("NationalId already exists.");
            }

            // You can add additional validations here if needed (e.g., CountryId, City, etc.)

            return validation;
        }

        // =========================================================
        // FINDERS (Unique / Search Helpers)
        // =========================================================

        public Result<Person> FindByNationalId(string nationalId)
        {
            if (string.IsNullOrWhiteSpace(nationalId))
                return Result<Person>.Fail("National ID is required.");

            var p = PersonData.GetByNationalId(nationalId.Trim());
            return p is null
                ? Result<Person>.Fail("Person not found.")
                : Result<Person>.Ok(p);
        }

        public Result<Person> FindByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Result<Person>.Fail("Email is required.");

            var p = PersonData.GetByEmail(email.Trim());
            return p is null
                ? Result<Person>.Fail("Person not found.")
                : Result<Person>.Ok(p);
        }

        public Result<Person> FindByPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return Result<Person>.Fail("Phone is required.");

            var p = PersonData.GetByPhone(phone.Trim()); // should check Phone1 OR Phone2 in DAL
            return p is null
                ? Result<Person>.Fail("Person not found.")
                : Result<Person>.Ok(p);
        }

        // Optional but very useful: Find by either phone1 or phone2 explicitly
        public Result<Person> FindByPhone1(string phone1)
        {
            if (string.IsNullOrWhiteSpace(phone1))
                return Result<Person>.Fail("Phone1 is required.");

            var p = PersonData.GetByPhone(phone1.Trim());
            return p is null
                ? Result<Person>.Fail("Person not found.")
                : Result<Person>.Ok(p);
        }

        public Result<Person> FindByPhone2(string phone2)
        {
            if (string.IsNullOrWhiteSpace(phone2))
                return Result<Person>.Fail("Phone2 is required.");

            var p = PersonData.GetByPhone(phone2.Trim());
            return p is null
                ? Result<Person>.Fail("Person not found.")
                : Result<Person>.Ok(p);
        }

        // =========================================================
        // EXISTS HELPERS (UI validation / quick checks)
        // =========================================================

        public bool IsEmailExist(string email, int? excludePersonId = null)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return PersonData.IsEmailExist(email.Trim(), excludePersonId);
        }

        public bool IsNationalIdExist(string nationalId, int? excludePersonId = null)
        {
            if (string.IsNullOrWhiteSpace(nationalId))
                return false;

            return PersonData.IsNationalIdExist(nationalId.Trim(), excludePersonId);
        }

        public bool IsPhoneExist(string phone, int? excludePersonId = null)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            return PersonData.IsPhoneExist(phone.Trim(), excludePersonId);
        }



        protected override string GetAuditMessage(string operation, Person entity)
            => $"{EntityName} [{entity.PersonId}] {operation} performed.";
    }

}
