using Clinic_Management_BLL;
using Clinic_Management_BLL.AuditWritter;
using Clinic_Management_BLL.CrudInterface;
using Clinic_Management_BLL.LoginProcess;
using Clinic_Management_BLL.ResultWraper;
using Clinic_Management_DAL.Data;
using Clinic_Management_DAL.Infrastractor;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Clinic_Management_BLL.Service
{
    public class DoctorService : BaseCrudService<Doctor> 
    {
        // Permission codes (customize as needed)
        protected override string CreatePermissionCode => "DOCTOR_CREATE";
        protected override string UpdatePermissionCode => "DOCTOR_UPDATE";
        protected override string DeletePermissionCode => "DOCTOR_DELETE";
        protected override string ViewPermissionCode => "DOCTOR_VIEW";

        protected override string EntityName => "Doctor";

        // DAL method implementations

        protected override int DalCreate(Doctor entity)
            => DoctorData.Insert(entity) ? entity.StaffId : 0;  // Insert returns bool, so adapt accordingly

        protected override bool DalUpdate(Doctor entity)
            => DoctorData.Update(entity);

        protected override bool DalDelete(int id)
            => DoctorData.GetById(id) != null && DoctorData.Delete(id);


        protected override Doctor? DalGetById(int id)
            => DoctorData.GetById(id);

        protected override IEnumerable<Doctor> DalGetAll()
            => DoctorData.GetAll();

        protected override int GetEntityId(Doctor entity)
            => entity.StaffId;

        protected override ValidationResult.ValidationResult IsValidateData(Doctor entity)
        {
            var validation = ValidationResult.ValidationResult.Success();

            // StaffId validation
            if (entity.StaffId <= 0)
                validation.Add("StaffId must be valid.");

            // LicenseNumber validation
            if (!string.IsNullOrWhiteSpace(entity.LicenseNumber))
            {
                bool exists = DoctorData.IsLicenseNumberExist(entity.LicenseNumber, entity.StaffId == 0 ? null : entity.StaffId);
                if (exists)
                    validation.Add("LicenseNumber already exists.");
            }
            else
            {
                validation.Add("LicenseNumber cannot be empty.");
            }

            if(DoctorData.IsStuffIDExist(entity.StaffId,entity.StaffId))
            {
                validation.Add("Stuff ID Already Linked !");
            }

            // SpecialtyId optional, but you can add validation if required
            // ConsultationFee optional, add checks if needed

            return validation;
        }

        private StaffService staffService = new StaffService();

        public Result<Doctor> GetByStaffId(int staffId)
        {

            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result<Doctor>.Fail("Permission denied.");

            //var validation = IsValidateData(entity);
            //if (!validation.IsValid)
            //    return Result<int>.Fail(validation.Errors);


            if (staffId <= 0)
                return Result<Doctor>.Fail("Invalid StaffId.");

            var doc = DoctorData.GetById(staffId);   // Doctor PK == StaffId

            


            if(doc is null)
            {
                return Result<Doctor>.Fail("Doctor not found.");
            }


            AuditWriter.Write(
                action: GetAuditMessage("VIEW",doc ),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: doc.StaffId.ToString(),
                success: true,
                newEntity: doc
            );

            return  Result<Doctor>.Ok(doc);
        }

        public Result<Doctor> GetByLicenseNumber(string  licenseNumber)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result<Doctor>.Fail("Permission denied.");


            if (string.IsNullOrEmpty(licenseNumber))
                return Result<Doctor>.Fail("Invalid License Number.");

            var doc = DoctorData.GetByLicenseNumber(licenseNumber);   // Doctor PK == StaffId




            if (doc is null)
            {
                return Result<Doctor>.Fail("Doctor not found.");
            }


            AuditWriter.Write(
                action: GetAuditMessage("VIEW", doc),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: doc.StaffId.ToString(),
                success: true,
                newEntity: doc
            );

            return Result<Doctor>.Ok(doc);

        }


        public Result<IEnumerable<Doctor>> GetAllBySpecialtyId(int specialtyId)
        {
            var result = Result<IEnumerable<Doctor>>.Fail("Unknown error.");

            try
            {
                // =========================
                // Permission Check
                // =========================
                if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId,ViewPermissionCode))
                    return Result<IEnumerable<Doctor>>.Fail("You do not have permission to view doctors.");

                // =========================
                // Validation
                // =========================
                if (specialtyId <= 0)
                    return Result<IEnumerable<Doctor>>.Fail("Invalid Specialty ID.");

                // =========================
                // DAL Call
                // =========================
                var doctors = DoctorData.GetAllBySpecialtyId(specialtyId);

                AuditLogData.Log("View Doctors By Speciality",
                    true, SecurityContext.Current.UserId,
                    "Doctor");

                return Result<IEnumerable<Doctor>>.Ok(doctors ?? Enumerable.Empty<Doctor>());
            }
            catch (Exception ex)
            {
                // Optional logging here
                return Result<IEnumerable<Doctor>>.Fail(ex.Message);
            }
        }


        public bool Exists(int staffId)
            => staffId > 0 && DoctorData.GetById(staffId) != null;

        // =========================================================
        // ✅ ATOMIC CREATE: Staff then Doctor (one transaction)
        // =========================================================
        public Result<int> CreateWithStaff(Staff staff, Doctor doctor)
        {

            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, CreatePermissionCode))
                return Result<int>.Fail("Permission denied.");


            // ---- basic guards
            if (staff is null) return Result<int>.Fail("Staff is required.");
            if (doctor is null) return Result<int>.Fail("Doctor is required.");


            var vStaff = staffService.Validate(staff);
            if(!vStaff.IsValid)
            {
                return Result<int>.Fail(vStaff.Errors);
            }
            
            // ---- validate doctor (your existing validation)
            var vDoctor = IsValidateData(doctor);
            if (!vDoctor.IsValid)
                return Result<int>.Fail(vDoctor.Errors);

            // ---- validate staff (light validation here; or call StaffService validation if you have it)
            if (staff.PersonId <= 0)
                return Result<int>.Fail("PersonId is required for Staff.");

            if (string.IsNullOrWhiteSpace(staff.StaffCode))
                return Result<int>.Fail("StaffCode is required.");

            if (staff.DepartmentId <= 0)
                return Result<int>.Fail("Department is required.");

            // ---- timestamps (optional)
            staff.CreatedAt = DateTime.Now;
            staff.UpdatedAt = DateTime.Now;
            

            // ---- DAL does the transaction and returns new StaffId
            // (DAL MUST: insert staff -> set doctor.StaffId -> insert doctor -> COMMIT)
            int newStaffId = StaffData.Insert(staff);
            if(newStaffId <=0)
            {
                return Result<int>.Fail("Stuff Not Inserted Successfuly.");
            }

            bool Success = DoctorData.Insert(doctor);

            if(Success)
            {
                AuditWriter.Write(
                action: GetAuditMessage("CREATE", doctor),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: doctor.StaffId.ToString(),
                success: true,
                newEntity: doctor
            );
            }

            return Success
                ? Result<int>.Ok(newStaffId)
                : Result<int>.Fail("Failed to create doctor (transaction rolled back).");
        }

        // =========================================================
        // ✅ ATOMIC UPDATE: update Staff + Doctor (one transaction)
        // =========================================================
        public Result UpdateWithStaff(Staff staff, Doctor doctor)
        {

            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, UpdatePermissionCode))
                return Result<int>.Fail("Permission denied.");


            var vStaff = staffService.Validate(staff);
            if (!vStaff.IsValid)
            {
                return Result<int>.Fail(vStaff.Errors);
            }

            var validation = IsValidateData(doctor);
            if (!validation.IsValid)
                return Result<int>.Fail(validation.Errors);



            if (staff is null) return Result.Fail("Staff is required.");
            if (doctor is null) return Result.Fail("Doctor is required.");

            // RequirePermission("STAFF_UPDATE");
            // RequirePermission(UpdatePermissionCode);

            if (staff.StaffId <= 0)
                return Result.Fail("Invalid StaffId (Staff).");

            if (doctor.StaffId <= 0)
                return Result.Fail("Invalid StaffId (Doctor).");

            if (staff.StaffId != doctor.StaffId)
                return Result.Fail("StaffId mismatch between Staff and Doctor.");

            

            // staff validation (light)
            if (staff.PersonId <= 0)
                return Result.Fail("PersonId is required.");

            if (string.IsNullOrWhiteSpace(staff.StaffCode))
                return Result.Fail("StaffCode is required.");

            if (staff.DepartmentId <= 0)
                return Result.Fail("Department is required.");

            staff.UpdatedAt = DateTime.Now;

            // DAL MUST update both inside one transaction
            bool ok = DoctorData.Update(doctor) && StaffData.Update(staff);

            if (ok)
            {
                AuditWriter.Write(
                action: GetAuditMessage("UPDATE", doctor),
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: doctor.StaffId.ToString(),
                success: true,
                newEntity: doctor
            );
            }

            return ok
                ? Result.Ok()
                : Result.Fail("Failed to update doctor (transaction rolled back).");
        }

        // =========================================================
        // OPTIONAL: Atomic delete (if you want)
        // =========================================================
        public Result DeleteWithStaff(int staffId)
        {
            if (!PermissionChecker.PermissionChecker.HasPermission(SecurityContext.Current.UserId, DeletePermissionCode))
                return Result.Fail("Permission denied.");

            if (staffId <= 0)
                return Result.Fail("Invalid StaffId.");

            // capture old entity BEFORE delete (for audit)
            var oldDoctor = DoctorData.GetById(staffId);

            bool ok = DoctorData.Delete(staffId); // ✅ transactional DAL

            // audit
            AuditWriter.Write<Doctor>(
                action: "DELETE",
                performedBy: SecurityContext.Current.UserId,
                entityType: EntityName,
                entityId: staffId.ToString(),
                success: ok,
                oldEntity: oldDoctor,
                newEntity: default,
                failureReason: ok ? null : "Failed to delete doctor/staff."
            );

            return ok ? Result.Ok() : Result.Fail("Failed to delete doctor/staff.");
        }


        protected override string GetAuditMessage(string operation, Doctor entity)
            => $"{EntityName} [{entity.StaffId}] {operation} performed.";
    }

}
