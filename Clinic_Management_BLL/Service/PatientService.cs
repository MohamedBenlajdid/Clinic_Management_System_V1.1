using Clinic_Management_BLL.CrudInterface;
using Clinic_Management_BLL.ResultWraper;
using Clinic_Management_DAL.Data;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Clinic_Management_BLL.PermissionChecker;
using Clinic_Management_BLL.LoginProcess;

namespace Clinic_Management_BLL.Service
{
    public class PatientService : BaseCrudService<Patient>
    {


        // Permission codes (customize as needed)
        protected override string CreatePermissionCode => "PATIENT_CREATE";
        protected override string UpdatePermissionCode => "PATIENT_UPDATE";
        protected override string DeletePermissionCode => "PATIENT_DELETE";
        protected override string ViewPermissionCode => "PATIENT_VIEW";

        protected override string EntityName => "Patient";

        // DAL method implementations

        protected override int DalCreate(Patient entity)
            => PatientData.Insert(entity);

        protected override bool DalUpdate(Patient entity)
            => PatientData.Update(entity);

        protected override bool DalDelete(int id)
            => PatientData.GetById(id) != null && PatientData.Delete(id);
        // Note: You don't have Delete method in DAL; implement if needed, or skip delete

        protected override Patient? DalGetById(int id)
            => PatientData.GetById(id);

        protected override IEnumerable<Patient> DalGetAll()
            => PatientData.GetAll();

        protected override int GetEntityId(Patient entity)
            => entity.PatientId;

        protected override ValidationResult.ValidationResult IsValidateData(Patient entity)
        {
            var validation = ValidationResult.ValidationResult.Success();

            // PersonId validation
            if (entity.PersonId <= 0)
                validation.Add("PersonId must be valid.");

            // MedicalRecordNumber validation
            if (string.IsNullOrWhiteSpace(entity.MedicalRecordNumber))
                validation.Add("MedicalRecordNumber cannot be empty.");
            else
            {
                bool mrnExists = PatientData.IsMedicalRecordNumberExist(entity.MedicalRecordNumber, entity.PatientId == 0 ? null : entity.PatientId);
                if (mrnExists)
                    validation.Add("MedicalRecordNumber already exists.");
            }

            if (entity.BloodTypeId <= 0 || entity.BloodTypeId >= BloodTypeData.GetAll().Count())
            {
                validation.Add("Blood Type ID Out Of Range ");
            }

            // Optional validations for BloodTypeId, EmergencyContactName, EmergencyContactPhone can be added here

            return validation;
        }

        protected override string GetAuditMessage(string operation, Patient entity)
            => $"{EntityName} [{entity.PatientId}] {operation} performed.";

        public Result<Patient> FindByPersonId(int personId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId,ViewPermissionCode))
                return Result<Patient>.Fail("Access denied.");

            if (personId <= 0)
                return Result<Patient>.Fail("PersonId must be valid.");

            try
            {
                var p = PatientData.GetByPersonId(personId);
                return (p == null)
                    ? Result<Patient>.Fail("No patient found for this person.")
                    : Result<Patient>.Ok(p);
            }
            catch (Exception ex)
            {
                return Result<Patient>.Fail(ex.Message);
            }
        }

        public Result<Patient> FindByMedicalRecordNumber(string mrn)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<Patient>.Fail("Access denied.");

            mrn = mrn?.Trim() ?? "";
            if (mrn.Length == 0)
                return Result<Patient>.Fail("Medical record number is required.");

            try
            {
                var p = PatientData.GetByMedicalRecordNumber(mrn);
                return (p == null)
                    ? Result<Patient>.Fail("No patient found.")
                    : Result<Patient>.Ok(p);
            }
            catch (Exception ex)
            {
                return Result<Patient>.Fail(ex.Message);
            }
        }


        public Result<bool> HasPatientForPerson(int personId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, ViewPermissionCode))
                return Result<bool>.Fail("Access denied.");

            if (personId <= 0)
                return Result<bool>.Fail("PersonId must be valid.");

            try
            {
                bool exists = PatientData.GetByPersonId(personId) != null;
                return Result<bool>.Ok(exists);
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail(ex.Message);
            }
        }


    }

}
