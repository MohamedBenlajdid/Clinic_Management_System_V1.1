using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Clinic_Management.Permission
{
    using Clinic_Management.Helpers;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public partial class ucPermission : UserControl
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
        public int PermissionID => Permission?.PermissionId ?? -1;
        public Permission Permission { get; private set; } = new Permission();

        // =======================
        // EVENTS
        // =======================
        public event Action<int>? OnPermissionCreated;
        public event Action<bool>? DirtyChanged;

        // =======================
        // SERVICES (lazy)
        // =======================
        private PermissionService? _permissionService;

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
        public ucPermission()
        {
            InitializeComponent();
            this.Load += ucPermission_Load;
            
        }

        private void ucPermission_Load(object? sender, EventArgs e)
        {
            if (IsInDesigner)
                return;

            _permissionService = new PermissionService();

            WireDirtyEvents();
           // LoadNew();
        }

        // =======================
        // PUBLIC API
        // =======================
        public void LoadNew()
        {
            ClearErrors();

            Permission = new Permission
            {
                PermissionId = 0,
                Code = "",
                Name = "",
                Module = "",
                Description = "",
                IsActive = true
            };

            ResetUI();
            CurrentMode = enMode.AddNew;
            SetDirty(false);
        }

        public void LoadEntityData(int permissionId, enMode mode = enMode.View)
        {
            ClearErrors();

            if (permissionId <= 0)
            {
                LoadNew();
                return;
            }

            CurrentMode = mode;

            if (_permissionService == null)
                _permissionService = new PermissionService();

            var res = _permissionService.GetById(permissionId);
            if (!res.IsSuccess || res.Value == null)
                throw new InvalidOperationException(res.ErrorMessage ?? "Permission not found.");

            Permission = res.Value;

            BindEntityToUI();
            SetDirty(false);
        }

        public bool SaveCurrent()
        {
            if (!ValidateUI())
                return false;

            MapUIToEntity();

            if (_permissionService == null)
                _permissionService = new PermissionService();

            bool wasAdd = (CurrentMode == enMode.AddNew);

            if (wasAdd)
            {
                var created = _permissionService.Create(Permission);
                if (!created.IsSuccess || created.Value <= 0)
                {
                    clsMessage.ShowError(created.ErrorMessage ?? "Failed to create permission.");
                    return false;
                }

                Permission.PermissionId = created.Value;

                CurrentMode = enMode.View;
                SetDirty(false);

                OnPermissionCreated?.Invoke(Permission.PermissionId);
                return true;
            }
            else
            {
                var updated = _permissionService.Update(Permission);
                if (!updated.IsSuccess)
                {
                    clsMessage.ShowError(updated.ErrorMessage ?? "Failed to update permission.");
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

            lblPermission.Text = "[A/N]";
            txtCode.Clear();
            txtName.Clear();
            txtModule.Clear();
            txtDescription.Clear();
            chkIsActive.Checked = true;

            ApplyMode();
        }

        private void BindEntityToUI()
        {
            lblPermission.Text = PermissionID > 0 ? PermissionID.ToString() : "[A/N]";

            txtCode.Text = Permission.Code ?? "";
            txtName.Text = Permission.Name ?? "";
            txtModule.Text = Permission.Module ?? "";
            txtDescription.Text = Permission.Description ?? "";
            chkIsActive.Checked = Permission.IsActive;

            ApplyMode();
        }

        private void MapUIToEntity()
        {
            Permission.Code = txtCode.Text.Trim();
            Permission.Name = txtName.Text.Trim();
            Permission.Module = txtModule.Text.Trim();
            Permission.Description = txtDescription.Text.Trim();
            Permission.IsActive = chkIsActive.Checked;
        }

        private void ApplyMode()
        {
            bool view = (CurrentMode == enMode.View);
            bool editable = !view;

            btnSave.Visible = !view;

            txtCode.ReadOnly = !editable;
            txtName.ReadOnly = !editable;
            txtModule.ReadOnly = !editable;
            txtDescription.ReadOnly = !editable;
            chkIsActive.Enabled = editable;

            linkEdit.Visible = (view && PermissionID > 0);
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
                SetError(txtCode, "Permission code is required.");
                ok = false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                SetError(txtName, "Permission name is required.");
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

            txtModule.TextChanged += (_, __) => SetDirty(true);
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
                clsMessage.ShowError("Permission details failed to save.");
                return;
            }

            clsMessage.ShowSuccess("Permission details saved successfully.");
        }

        private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentMode = enMode.Edit;
        }
    }




}
