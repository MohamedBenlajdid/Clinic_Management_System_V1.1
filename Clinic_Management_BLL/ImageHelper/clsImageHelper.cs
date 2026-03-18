using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.ImageHelper
{
    using System;
    using System.IO;

    public static class clsImageHandler
    {
        // ================================
        // CONFIG
        // ================================
        private static readonly string FolderPath =
            @"E:\Clinic_Management\UserImages";

        // ================================
        // CTOR – ensure folder exists
        // ================================
        static clsImageHandler()
        {
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);
        }

        // ================================
        // SAVE IMAGE
        // returns: Guid.extension (for DB)
        // ================================
        public static string Save(string sourceImagePath)
        {
            if (string.IsNullOrWhiteSpace(sourceImagePath))
                return null;

            if (!File.Exists(sourceImagePath))
                return null;

            string extension = Path.GetExtension(sourceImagePath);

            string fileName =
                $"{Guid.NewGuid()}{extension}";

            string destinationPath =
                BuildFullPath(fileName);

            File.Copy(sourceImagePath, destinationPath, overwrite: true);

            return fileName; // <-- ONLY this goes to DB
        }

        // ================================
        // REPLACE IMAGE
        // ================================
        public static string Replace(
            string newSourceImagePath,
            string oldFileName)
        {
            Delete(oldFileName);
            return Save(newSourceImagePath);
        }

        // ================================
        // DELETE IMAGE
        // accepts: Guid.extension
        // ================================
        public static void Delete(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return;

            string fullPath = BuildFullPath(fileName);

            try
            {
                if (File.Exists(fullPath))
                    File.Delete(fullPath);
            }
            catch
            {
                // TODO: log later (do not crash business flow)
            }
        }

        // ================================
        // GET FULL PATH (for UI / PictureBox)
        // ================================
        public static string GetFullPath(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return null;

            return BuildFullPath(fileName);
        }

        // ================================
        // PRIVATE HELPERS
        // ================================
        private static string BuildFullPath(string fileName)
        {
            return Path.Combine(FolderPath, fileName);
        }
    }

}
