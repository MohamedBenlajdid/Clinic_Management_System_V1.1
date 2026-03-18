using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Clinic_Management_BLL.LoginProcess
{
    using Microsoft.Win32;
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public static class clsStoreCredential
    {
        private const string RegistryPath = @"Software\ClinicMS";
        private const string UserKey = "UserName";
        private const string PassKey = "Password";

        // ==============================
        // SAVE
        // ==============================
        public static void Save(string userName, string password)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryPath);

            key.SetValue(UserKey, userName);
            key.SetValue(PassKey, Encrypt(password));

            key.Close();
        }

        // ==============================
        // LOAD using OUT
        // ==============================
        public static bool Load(out string userName, out string password)
        {
            userName = null;
            password = null;

            RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryPath);

            if (key == null)
                return false;

            object u = key.GetValue(UserKey);
            object p = key.GetValue(PassKey);

            key.Close();

            if (u == null || p == null)
                return false;

            userName = u.ToString();
            password = Decrypt(p.ToString());

            return true;
        }

        // ==============================
        // CLEAR
        // ==============================
        public static void Clear()
        {
            Registry.CurrentUser.DeleteSubKeyTree(RegistryPath, false);
        }

        // ==============================
        // ENCRYPT / DECRYPT
        // ==============================
        private static string Encrypt(string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);

            byte[] encrypted = ProtectedData.Protect(
                data,
                null,
                DataProtectionScope.CurrentUser);

            return Convert.ToBase64String(encrypted);
        }

        private static string Decrypt(string cipher)
        {
            byte[] data = Convert.FromBase64String(cipher);

            byte[] decrypted = ProtectedData.Unprotect(
                data,
                null,
                DataProtectionScope.CurrentUser);

            return Encoding.UTF8.GetString(decrypted);
        }
    }


}
