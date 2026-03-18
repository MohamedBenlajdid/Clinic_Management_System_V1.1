using Clinic_Management.Helpers;
using Clinic_Management.Users;
using Clinic_Management_BLL.LoginProcess;
using Clinic_Management_BLL.ResultWraper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Login
{
    public partial class frmLogin : Form
    {
        // =====================================
        // FIELDS
        // =====================================

        private bool _isPasswordVisible;

        // =====================================
        // CONSTRUCTOR
        // =====================================

        public frmLogin()
        {
            InitializeComponent();

            SetupForm();
            TryLoadRememberedCredentials();
        }

        // =====================================
        // SETUP
        // =====================================

        private void SetupForm()
        {
            txtPassword.UseSystemPasswordChar = true;
            AcceptButton = btnLogin;
        }

        // =====================================
        // REMEMBER ME
        // =====================================

        private void TryLoadRememberedCredentials()
        {
            if (clsStoreCredential.Load(out string user, out string pass))
            {
                txtUserName.Text = user;
                txtPassword.Text = pass;
                chkRememberMe.Checked = true;
            }
        }

        private void HandleRememberMe(string username, string password)
        {
            if (chkRememberMe.Checked)
                clsStoreCredential.Save(username, password);
            else
                clsStoreCredential.Clear();
        }

        // =====================================
        // EVENTS
        // =====================================

        private void btnLogin_Click(object sender, EventArgs e)
            => AttemptLogin();

        private void btnShowHide_Click(object sender, EventArgs e)
            => TogglePassword();

        private void linkAddNewAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            => OpenRegisterForm();

        // =====================================
        // LOGIN FLOW
        // =====================================

        private void AttemptLogin()
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text;

            if (!ValidateInputs(username, password))
                return;

            try
            {
                Cursor = Cursors.WaitCursor;

                var result = AuthenticationService.Login(username, password);

                HandleLoginResult(result, username, password);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void HandleLoginResult(Result<int> result,
                                       string username,
                                       string password)
        {
            if (result.IsFailure)
            {
                clsMessage.ShowWarning(
                    result.ErrorMessage ?? "Login failed.");

                return;
            }

            HandleRememberMe(username, password);

            OpenMainForm(result.Value);
            clsMessage.ShowSuccess("Welcom Again Mrs : " + username + ";");

            //Hide();
        }

        // =====================================
        // UI ACTIONS
        // =====================================

        private void TogglePassword()
        {
            _isPasswordVisible = !_isPasswordVisible;
            txtPassword.UseSystemPasswordChar = !_isPasswordVisible;
        }

        private void OpenRegisterForm()
        {
            using (var frm = new frmCreateUser())
            {
                frm.ShowDialog();
            }
        }

        private void OpenMainForm(int userId)
        {
            var frm = new MainForm.MainForm();

            // Proper app shutdown
            frm.FormClosed += (s, e) => Close();

            frm.Show();
        }

        // =====================================
        // VALIDATION
        // =====================================

        private bool ValidateInputs(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                clsMessage.ShowWarning("Enter username.");
                txtUserName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                clsMessage.ShowWarning("Enter password.");
                txtPassword.Focus();
                return false;
            }

            return true;
        }
    }



}
