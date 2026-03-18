using System;
using System.Collections.Generic;
using System.Text;
using Clinic_Management_Entities;

namespace Clinic_Management_DAL.Data
{
    using Clinic_Management_DAL.Infrastractor;
    using Clinic_Management_Entities.Entities;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class ImageData
    {
        private const string Columns = @"ImageID, PersonID, ImagePath";

        // Base folder where images are stored on disk
        private static readonly string ImageBaseFolder = @"E:\Clinic_Management\UserImages";

        // ===============================
        // Exists for PersonID
        // ===============================
        public static bool ExistsForPerson(int personId)
        {
            string query = @"
SELECT 1
FROM Images
WHERE PersonID = @PersonID";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read();
                },
                SqlParameterFactory.Create("@PersonID", personId)
            );
        }

        // ===============================
        // Get Image By PersonID (assume one image per person)
        // ===============================
        public static Image GetByPersonId(int personId)
        {
            string query = $@"
SELECT {Columns}
FROM Images
WHERE PersonID = @PersonID";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Image>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@PersonID", personId)
            );
        }


        public static Image GetById(int Id)
        {
            string query = $@"
SELECT {Columns}
FROM Images
WHERE ImageID = @ImageID";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    return reader.Read()
                        ? DbMapper<Image>.Map(reader)
                        : null;
                },
                SqlParameterFactory.Create("@ImageID", Id)
            );
        }

        // ===============================
        // Insert New Image
        // ===============================
        public static int InsertNew(Image image)
        {
            string query = @"
INSERT INTO Images (PersonID, ImagePath)
VALUES (@PersonID, @ImagePath);

SELECT SCOPE_IDENTITY();";

            return DbExecutor.Execute(
                query,
                cmd => Convert.ToInt32(cmd.ExecuteScalar()),
                SqlParameterFactory.Create("@PersonID", image.PersonID),
                SqlParameterFactory.Create("@ImagePath", image.ImagePath)
            );
        }

        // ===============================
        // Replace Image (Delete old image file, update path)
        // ===============================
        public static bool Replace(Image image)
        {
            // 1. Load existing image for person
            var oldImage = GetByPersonId(image.PersonID);

            // 2. Delete old image file if exists and path is different
            if (oldImage != null && !string.Equals(oldImage.ImagePath, image.ImagePath, StringComparison.OrdinalIgnoreCase))
            {
                DeleteFileSafe(Path.Combine(ImageBaseFolder, oldImage.ImagePath));
            }

            // 3. If no existing image, Insert new
            if (oldImage == null)
            {
                InsertNew(image);
                return true;
            }

            // 4. Update existing image path
            string query = @"
UPDATE Images SET ImagePath = @ImagePath
WHERE ImageID = @ImageID";

            return DbExecutor.Execute(
                query,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@ImagePath", image.ImagePath),
                SqlParameterFactory.Create("@ImageID", oldImage.ImageID)
            );
        }

        // ===============================
        // Delete image by ImageID (also delete file)
        // ===============================
        public static bool Delete(int imageId)
        {
            // Load image to get path
            string querySelect = @"
SELECT ImagePath FROM Images WHERE ImageID = @ImageID";

            string? imagePath = DbExecutor.Execute(
                querySelect,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    if (reader.Read())
                        return reader.GetString(0);
                    return null;
                },
                SqlParameterFactory.Create("@ImageID", imageId)
            );

            if (imagePath != null)
            {
                DeleteFileSafe(Path.Combine(ImageBaseFolder, imagePath));
            }

            // Delete DB record
            string queryDelete = @"
DELETE FROM Images WHERE ImageID = @ImageID";

            return DbExecutor.Execute(
                queryDelete,
                cmd => cmd.ExecuteNonQuery() > 0,
                SqlParameterFactory.Create("@ImageID", imageId)
            );
        }

        // ===============================
        // Private helper: Delete file safely
        // ===============================
        private static void DeleteFileSafe(string fullPath)
        {
            try
            {
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
            catch
            {
                // Log or ignore — do not throw from DAL
            }
        }

        // ===============================
        // Get All Images (optional)
        // ===============================
        public static IEnumerable<Image> GetAll()
        {
            string query = $"SELECT {Columns} FROM Images ORDER BY ImageID";

            return DbExecutor.Execute(
                query,
                cmd =>
                {
                    using var reader = cmd.ExecuteReader();
                    var list = new List<Image>();

                    while (reader.Read())
                        list.Add(DbMapper<Image>.Map(reader));

                    return list;
                });
        }
    }

}
