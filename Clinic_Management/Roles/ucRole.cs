using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Clinic_Management.Roles
{
    using Clinic_Management.Helpers;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public partial class ucRole : UserControl
    {
        // =======================
        // ErrorProvider helpers
        // =======================
        private void ClearErrors()
        {
            if (errorProvider1 != null)
                errorProvider1.Clear();
        }

        private void SetError(Control ctrl, string message)
        {
            if (errorProvider1 != null)
                errorProvider1.SetError(ctrl, message);
        }

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
        public int RoleID => Role?.RoleId ?? -1;
        public Role Role { get; private set; } = new Role();

        // =======================
        // EVENTS
        // =======================
        public event Action<int>? OnRoleCreated;
        public event Action<bool>? DirtyChanged;

        // =======================
        // SERVICES (lazy)
        // =======================
        private RoleService? _roleService;

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
        // DESIGN TIME GUARD
        // =======================
        private bool IsInDesigner =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
            (Site?.DesignMode ?? false);

        // =======================
        // CTOR
        // =======================
        public ucRole()
        {
            InitializeComponent();
            this.Load += ucRole_Load;
            
        }

        private void ucRole_Load(object? sender, EventArgs e)
        {
            if (IsInDesigner)
                return;

            _roleService = new RoleService();

            WireDirtyEvents();
            //LoadNew();
        }

        // =======================
        // PUBLIC API
        // =======================
        public void LoadNew()
        {
            ClearErrors();

            Role = new Role
            {
                RoleId = 0,
                Code = "",
                Name = "",
                Description = "",
                IsActive = true
            };

            ResetUI();
            CurrentMode = enMode.AddNew;
            SetDirty(false);
        }

        public void LoadEntityData(int roleId, enMode mode = enMode.View)
        {
            ClearErrors();

            if (roleId <= 0)
            {
                LoadNew();
                return;
            }

            CurrentMode = mode;

            if (_roleService == null)
                _roleService = new RoleService();

            var res = _roleService.GetById(roleId);
            if (!res.IsSuccess || res.Value == null)
                throw new InvalidOperationException(res.ErrorMessage ?? "Role not found.");

            Role = res.Value;

            BindEntityToUI();
            SetDirty(false);
        }

        public bool SaveCurrent()
        {
            if (!ValidateUI())
                return false;

            MapUIToEntity();

            if (_roleService == null)
                _roleService = new RoleService();

            bool wasAdd = (CurrentMode == enMode.AddNew);

            if (wasAdd)
            {
                var created = _roleService.Create(Role);
                if (!created.IsSuccess || created.Value <= 0)
                {
                    clsMessage.ShowError(created.ErrorMessage ?? "Failed to create role.");
                    return false;
                }

                Role.RoleId = created.Value;

                CurrentMode = enMode.View;
                SetDirty(false);

                OnRoleCreated?.Invoke(Role.RoleId);
                return true;
            }
            else
            {
                var updated = _roleService.Update(Role);
                if (!updated.IsSuccess)
                {
                    clsMessage.ShowError(updated.ErrorMessage ?? "Failed to update role.");
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

            lblRoleId.Text = "[A/N]";
            txtCode.Clear();
            txtName.Clear();
            txtDescription.Clear();
            chkIsActive.Checked = true;

            ApplyMode();
        }

        private void BindEntityToUI()
        {
            lblRoleId.Text = RoleID > 0 ? RoleID.ToString() : "[A/N]";

            txtCode.Text = Role.Code ?? "";
            txtName.Text = Role.Name ?? "";
            txtDescription.Text = Role.Description ?? "";
            chkIsActive.Checked = Role.IsActive;

            ApplyMode();
        }

        private void MapUIToEntity()
        {
            Role.Code = txtCode.Text.Trim();
            Role.Name = txtName.Text.Trim();
            Role.Description = txtDescription.Text.Trim();
            Role.IsActive = chkIsActive.Checked;
        }

        private void ApplyMode()
        {
            bool view = (CurrentMode == enMode.View);
            bool editable = !view;

            btnSave.Visible = !view;

            txtCode.ReadOnly = !editable;
            txtName.ReadOnly = !editable;
            txtDescription.ReadOnly = !editable;
            chkIsActive.Enabled = editable;

            linkEdit.Visible = (view && RoleID > 0);
        }

        // =======================
        // VALIDATION
        // =======================
        private bool ValidateUI()
        {
            ClearErrors();
            bool ok = true;

            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                SetError(txtCode, "Role code is required.");
                ok = false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                SetError(txtName, "Role name is required.");
                ok = false;
            }

            if (!ok)
            {
                if (!string.IsNullOrEmpty(errorProvider1.GetError(txtCode)))
                    txtCode.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(txtName)))
                    txtName.Focus();
            }

            return ok;
        }

        // =======================
        // DIRTY WIRING
        // =======================
        private void WireDirtyEvents()
        {
            txtCode.TextChanged += (_, __) =>
            {
                SetDirty(true);
                errorProvider1.SetError(txtCode, "");
            };

            txtName.TextChanged += (_, __) =>
            {
                SetDirty(true);
                errorProvider1.SetError(txtName, "");
            };

            txtDescription.TextChanged += (_, __) => SetDirty(true);
            chkIsActive.CheckedChanged += (_, __) => SetDirty(true);
        }

        // =======================
        // UI EVENTS
        // =======================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!SaveCurrent())
            {
                clsMessage.ShowError("Role details failed to save.");
                return;
            }

            clsMessage.ShowSuccess("Role details saved successfully.");
        }

        private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentMode = enMode.Edit;
        }
    }



}
