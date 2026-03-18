using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Users
{
    using Clinic_Management.Helpers;
    using Clinic_Management_BLL.LoginProcess; // PasswordHasher namespace (adjust)
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities; // User entity namespace (adjust)
   
        public partial class ucUser : UserControl
        {

        // =======================
        // ErrorProvider helpers
        // =======================
        private void ClearErrors() => errorProvider1.Clear();

        private void SetError(Control ctrl, string message)
            => errorProvider1.SetError(ctrl, message);



        // =======================
        // MODE
        // =======================
        public enum enMode { AddNew, View, Edit }

            [Browsable(false)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public enMode CurrentMode
            {
                get => _mode;
                set { _mode = value; ApplyMode(); }
            }
            private enMode _mode = enMode.AddNew;

            // =======================
            // EXPOSITION
            // =======================
            public int UserID => User?.UserId ?? -1;
            public int PersonID => User?.PersonId ?? -1;
            public User User { get; private set; } = new User();

            // =======================
            // EVENTS
            // =======================
            public event Action<int>? OnUserCreated;
            public event Action<bool>? DirtyChanged;

            // =======================
            // SERVICES
            // =======================
            private readonly UserService _userService = new();

            // =======================
            // DIRTY
            // =======================
            private bool _isDirty;
            public bool IsDirty => _isDirty;

            private void SetDirty(bool dirty)
            {
                if (_isDirty == dirty) return;
                _isDirty = dirty;
                DirtyChanged?.Invoke(dirty);
            }

            // =======================
            // PASSWORD UI STATE
            // =======================
            private bool _passwordVisible = false;

            // =======================
            // DESIGN TIME GUARD
            // =======================
            private static bool IsDesignTime =>
                LicenseManager.UsageMode == LicenseUsageMode.Designtime;

            // =======================
            // CTOR
            // =======================
            public ucUser()
            {
                InitializeComponent();

                if (IsDesignTime)
                    return;

                WireDirtyEvents();
                WirePasswordToggle();

                LoadNew(); // default
            }

            // =======================
            // PUBLIC API
            // =======================
            public void LoadNew(int personId = 0)
            {

            ClearErrors();

            User = new User
                {
                    UserId = 0,
                    PersonId = personId,
                    IsActive = true,
                    MustChangePassword = false,
                    IsLocked = false,
                    LockoutEndAt = null,
                    FailedLoginCount = 0,
                    LastLoginAt = null
                };

                ResetUI();
                CurrentMode = enMode.AddNew;
                SetDirty(false);
            }

            public void LoadEntityData(int userId, enMode mode = enMode.View)
            {
            ClearErrors();

            if (userId <= 0)
                {
                    LoadNew();
                    return;
                }

                CurrentMode = mode;

                var res = _userService.GetById(userId); // Result<User>
                if (!res.IsSuccess || res.Value == null)
                    throw new InvalidOperationException(res.ErrorMessage ?? "User not found.");

                User = res.Value;

                BindEntityToUI();
                SetDirty(false);
            }

            public bool SaveCurrent()
            {
                // Passport rule
                if (PersonID <= 0)
                {
                clsMessage.ShowWarning("PersonID is required to create or edit a user.");
                ClearErrors();
                SetError(lblPersonId, "PersonID is required (Person is the passport).");
                    return false;
                }

                if (!ValidateUI())
                    return false;

                MapUIToEntity();

                bool wasAdd = (CurrentMode == enMode.AddNew);

                if (wasAdd)
                {
                    // Must set password hash+salt during Add
                    string password = txtPassword.Text;
                    var (hash, salt) = PasswordHasher.HashPassword(password);

                    User.PasswordHash = hash;
                    User.PasswordSalt = salt;

                    var created = _userService.AddNew(User); // Result<int>
                    if (!created.IsSuccess || created.Value <= 0)
                    {
                        clsMessage.ShowError(created.ErrorMessage ?? "Failed to create user.");
                        return false;
                    }

                    User.UserId = created.Value;

                    CurrentMode = enMode.View;
                    SetDirty(false);

                    OnUserCreated?.Invoke(User.UserId);
                    return true;
                }
                else
                {
                    // Edit: Password optional.
                    // If user typed a new password => rehash and update.
                    if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                    {
                        var (hash, salt) = PasswordHasher.HashPassword(txtPassword.Text);
                        User.PasswordHash = hash;
                        User.PasswordSalt = salt;

                        // Often: force change password off/on depending policy
                        // User.MustChangePassword = false; // optional
                    }

                    var updated = _userService.Update(User); // Result
                    if (!updated.IsSuccess)
                    {
                        clsMessage.ShowError(updated.ErrorMessage ?? "Failed to update user.");
                        return false;
                    }

                    CurrentMode = enMode.View;
                    SetDirty(false);
                    return true;
                }
            }

            // =======================
            // UI CORE
            // =======================
            private void ResetUI()
            {

            ClearErrors();

            lblUserId.Text = "[N/A]";
                lblPersonId.Text = (PersonID > 0) ? PersonID.ToString() : "[N/A]";

                txtUsername.Clear();

                chkIsActive.Checked = true;

                // Password fields always cleared on load/reset
                txtPassword.Clear();
                txtConfirmPassword.Clear();

                // default hide passwords
                SetPasswordVisibility(false);

                ApplyPassportRule();
            }

            private void BindEntityToUI()
            {
                lblUserId.Text = UserID > 0 ? UserID.ToString() : "[N/A]";
                lblPersonId.Text = PersonID > 0 ? PersonID.ToString() : "[N/A]";

                txtUsername.Text = User.Username ?? "";

                chkIsActive.Checked = User.IsActive;

                txtPassword.Clear();
                txtConfirmPassword.Clear();
                SetPasswordVisibility(false);

                ApplyPassportRule();
            }

            private void MapUIToEntity()
            {
                User.Username = txtUsername.Text.Trim();

                User.IsActive = chkIsActive.Checked;

                // Auto fields for creation are handled in service layer ideally,
                // but we keep them safe here too:
                if (CurrentMode == enMode.AddNew)
                {
                    User.IsLocked = false;
                    User.LockoutEndAt = null;
                    User.FailedLoginCount = 0;
                    User.LastLoginAt = null;
                }
            }

            private void ApplyMode()
            {
                bool view = (CurrentMode == enMode.View);
                bool editable = !view;

                // View mode: hide Save
                btnSave.Visible = !view;

                // Inputs enable/disable
                txtUsername.ReadOnly = !editable;

                chkIsActive.Enabled = editable;

                // Password fields:
                // - AddNew: enabled and required
                // - Edit: enabled but optional
                // - View: disabled
                bool passwordEditable = editable;
                txtPassword.ReadOnly = !passwordEditable;
                txtConfirmPassword.ReadOnly = !passwordEditable;
                btnShowHide.Enabled = passwordEditable;

                // edit link visible in view if user exists
                linkEdit.Visible = (view && UserID > 0);

                ApplyPassportRule();
            }

            private void ApplyPassportRule()
            {
                bool hasPerson = PersonID > 0;

                // PersonID is passport: if missing, disable everything and hide save
                this.Enabled = hasPerson; // put all inputs inside pnlUserDetails
                btnSave.Visible = hasPerson && (CurrentMode != enMode.View);

                if (!hasPerson)
                {
                    // keep label updated and clear fields
                    lblPersonId.Text = "[N/A]";
                }
            }

        // =======================
        // VALIDATION
        // =======================
        private bool ValidateUI()
        {
            ClearErrors();
            bool ok = true;

            // Username
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                SetError(txtUsername, "Username is required.");
                ok = false;
            }

            // AddNew requires password + confirm
            if (CurrentMode == enMode.AddNew)
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    SetError(txtPassword, "Password is required.");
                    ok = false;
                }

                if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
                {
                    SetError(txtConfirmPassword, "Confirm password is required.");
                    ok = false;
                }

                if (!string.IsNullOrWhiteSpace(txtPassword.Text) &&
                    !string.IsNullOrWhiteSpace(txtConfirmPassword.Text) &&
                    txtPassword.Text != txtConfirmPassword.Text)
                {
                    SetError(txtConfirmPassword, "Passwords do not match.");
                    ok = false;
                }
            }

            // Edit: password optional, but if provided => must match confirm
            if (CurrentMode == enMode.Edit)
            {
                bool anyPassword =
                    !string.IsNullOrWhiteSpace(txtPassword.Text) ||
                    !string.IsNullOrWhiteSpace(txtConfirmPassword.Text);

                if (anyPassword)
                {
                    if (string.IsNullOrWhiteSpace(txtPassword.Text))
                    {
                        SetError(txtPassword, "Enter the new password.");
                        ok = false;
                    }

                    if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
                    {
                        SetError(txtConfirmPassword, "Confirm the new password.");
                        ok = false;
                    }

                    if (!string.IsNullOrWhiteSpace(txtPassword.Text) &&
                        !string.IsNullOrWhiteSpace(txtConfirmPassword.Text) &&
                        txtPassword.Text != txtConfirmPassword.Text)
                    {
                        SetError(txtConfirmPassword, "Passwords do not match.");
                        ok = false;
                    }
                }
            }

            // Focus first invalid
            if (!ok)
            {
                if (!string.IsNullOrEmpty(errorProvider1.GetError(txtUsername))) txtUsername.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(txtPassword))) txtPassword.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(txtConfirmPassword))) txtConfirmPassword.Focus();
            }

            return ok;
        }


        // =======================
        // DIRTY WIRING
        // =======================
        private void WireDirtyEvents()
        {
            txtUsername.TextChanged += (_, __) =>
           {
                SetDirty(true);
                errorProvider1.SetError(txtUsername, "");
            };

            chkIsActive.CheckedChanged += (_, __) => SetDirty(true);

            txtPassword.TextChanged += (_, __) =>
            {
                SetDirty(true);
                errorProvider1.SetError(txtPassword, "");
                errorProvider1.SetError(txtConfirmPassword, ""); // because mismatch can be fixed
            };

            txtConfirmPassword.TextChanged += (_, __) =>
            {
                SetDirty(true);
                errorProvider1.SetError(txtConfirmPassword, "");
            };
        }


        // =======================
        // PASSWORD TOGGLE
        // =======================
        private void WirePasswordToggle()
            {
                btnShowHide.Click += (_, __) =>
                {
                    SetPasswordVisibility(!_passwordVisible);
                };
            }

            private void SetPasswordVisibility(bool visible)
            {
                _passwordVisible = visible;

                txtPassword.UseSystemPasswordChar = !visible;
                txtConfirmPassword.UseSystemPasswordChar = !visible;

                // Your resources: Resources.ShowPass / Resources.HidePass
                btnShowHide.Image = visible
                    ? Properties.Resources.HidePass
                    : Properties.Resources.ShowPass;
            }

            // =======================
            // UI EVENTS
            // =======================
            private void btnSave_Click(object sender, EventArgs e)
            {
                if (!SaveCurrent())
                {
                    clsMessage.ShowError("User details failed to save.");
                    return;
                }

                clsMessage.ShowSuccess("User details saved successfully.");
            }

            private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {
                CurrentMode = enMode.Edit;
            }
        }


    }


