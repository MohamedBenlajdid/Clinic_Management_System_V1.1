using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management.Helpers
{
    using System.Windows.Forms;

    public static class clsMessage
    {
        // ===============================
        // BASIC
        // ===============================

        public static void ShowInfo(string message, string title = "Info")
            => Show(message, title, MessageBoxIcon.Information);

        public static void ShowError(string message, string title = "Error")
            => Show(message, title, MessageBoxIcon.Error);

        public static void ShowWarning(string message, string title = "Warning")
            => Show(message, title, MessageBoxIcon.Warning);

        public static void ShowSuccess(string message, string title = "Success")
            => Show(message, title, MessageBoxIcon.Information);

        // ===============================
        // CONFIRMATIONS
        // ===============================

        public static bool Confirm(string message, string title = "Confirm")
            => AskYesNo(message, title, MessageBoxIcon.Question);

        public static bool ConfirmDelete(string entityName = "item")
            => AskYesNo(
                $"Are you sure you want to delete this {entityName}?",
                "Confirm Delete",
                MessageBoxIcon.Warning);

        public static bool ConfirmSave(string entityName = "changes")
            => AskYesNo(
                $"Do you want to save the {entityName}?",
                "Confirm Save",
                MessageBoxIcon.Question);

        public static bool ConfirmExit()
            => AskYesNo(
                "Are you sure you want to exit without saving?",
                "Exit",
                MessageBoxIcon.Warning);

        // ===============================
        // ADVANCED QUESTIONS
        // ===============================

        public static bool AskRetry(string message, string title = "Retry?")
        {
            return MessageBox.Show(
                message,
                title,
                MessageBoxButtons.RetryCancel,
                MessageBoxIcon.Question) == DialogResult.Retry;
        }

        public static bool AskYesNoCancel(
            string message,
            string title,
            out bool isCancel)
        {
            var result = MessageBox.Show(
                message,
                title,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            isCancel = result == DialogResult.Cancel;
            return result == DialogResult.Yes;
        }

        // ===============================
        // EXCEPTION HANDLING
        // ===============================

        public static void ShowException(Exception ex, string title = "Error")
        {
            ShowError(
                ex.GetBaseException().Message,
                title);
        }

        public static void ShowException(
            string userFriendlyMessage,
            Exception ex,
            string title = "Error")
        {
            ShowError(
                $"{userFriendlyMessage}\n\nDetails:\n{ex.GetBaseException().Message}",
                title);
        }

        // ===============================
        // VALIDATION
        // ===============================

        public static void ShowValidationErrors(
            IEnumerable<string> errors,
            string title = "Validation Error")
        {
            if (errors == null || !errors.Any())
                return;

            string message = string.Join(
                Environment.NewLine + "• ",
                errors.Prepend("Please fix the following issues:"));

            ShowWarning(message, title);
        }

        // ===============================
        // INTERNAL CORE
        // ===============================

        private static void Show(
            string message,
            string title,
            MessageBoxIcon icon)
        {
            MessageBox.Show(
                message,
                title,
                MessageBoxButtons.OK,
                icon);
        }

        private static bool AskYesNo(
            string message,
            string title,
            MessageBoxIcon icon)
        {
            return MessageBox.Show(
                message,
                title,
                MessageBoxButtons.YesNo,
                icon) == DialogResult.Yes;
        }
    }


}
