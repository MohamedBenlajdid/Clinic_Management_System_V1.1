using Clinic_Management_BLL.CrudInterface;
using Clinic_Management_BLL.ImageHelper;
using Clinic_Management_DAL.Data;
using Clinic_Management_Entities;
using Clinic_Management_Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.Service
{
    public class ImageService : BaseCrudService<Image>
    {
        // ===============================
        // PERMISSIONS
        // ===============================
        protected override string CreatePermissionCode => "PERSON_CREATE";
        protected override string UpdatePermissionCode => "PERSON_UPDATE";
        protected override string DeletePermissionCode => "PERSON_DELETE";
        protected override string ViewPermissionCode => "PERSON_VIEW";

        protected override string EntityName => "Image";

        // ===============================
        // CREATE
        // ===============================
        protected override int DalCreate(Image entity)
        {
            // entity.ImagePath here = SOURCE path (from UI)

            string storedFileName = clsImageHandler.Save(entity.ImagePath);

            if (storedFileName == null)
                throw new InvalidOperationException("Failed to save image file.");

            entity.ImagePath = storedFileName;

            return ImageData.InsertNew(entity);
        }

        // ===============================
        // UPDATE (smart replace)
        // ===============================
        protected override bool DalUpdate(Image entity)
        {
            // Load current image from DB
            var existing = ImageData.GetById(entity.ImageID);
            if (existing == null)
                throw new InvalidOperationException("Image not found.");

            // Replace physical file
            string newFileName = clsImageHandler.Replace(
                entity.ImagePath,          // source file (UI)
                existing.ImagePath         // old Guid.ext from DB
            );

            if (newFileName == null)
                throw new InvalidOperationException("Failed to replace image file.");

            entity.ImagePath = newFileName;

            return ImageData.Replace(entity);
        }

        // ===============================
        // DELETE
        // ===============================
        protected override bool DalDelete(int id)
        {
            var image = ImageData.GetById(id);
            if (image == null)
                return false;

            // Delete physical file FIRST
            clsImageHandler.Delete(image.ImagePath);

            return ImageData.Delete(id);
        }

        // ===============================
        // READ
        // ===============================
        protected override Image? DalGetById(int id)
            => ImageData.GetById(id);

        protected override IEnumerable<Image> DalGetAll()
            => ImageData.GetAll();

        protected override int GetEntityId(Image entity)
            => entity.ImageID;

        // ===============================
        // VALIDATION
        // ===============================
        protected override ValidationResult.ValidationResult IsValidateData(Image entity)
        {
            var validation = ValidationResult.ValidationResult.Success();

            if (entity.PersonID <= 0)
                validation.Add("Invalid Person ID.");

            if (string.IsNullOrWhiteSpace(entity.ImagePath))
                validation.Add("Image is required.");

            // Security: block fake extensions
            if (!HasAllowedExtension(entity.ImagePath))
                validation.Add("Unsupported image format.");

            return validation;
        }

        public static Image GetImageByPersonID(int personID)
        {
            return ImageData.GetById(personID);
        }


        protected override string GetAuditMessage(string operation, Image entity)
            => $"{EntityName} [{entity.ImageID}] {operation} performed.";

        // ===============================
        // PRIVATE SECURITY
        // ===============================
        private static bool HasAllowedExtension(string path)
        {
            string ext = Path.GetExtension(path)?.ToLowerInvariant();

            return ext is ".jpg" or ".jpeg" or ".png" or ".bmp";
        }
    }


}
