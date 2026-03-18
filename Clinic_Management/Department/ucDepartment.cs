using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Department
{
    using Clinic_Management.Helpers;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public partial class ucDepartment : UserControl
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
        public int DepartmentID => Department?.DepartmentId ?? -1;
        public Department Department { get; private set; } = new Department();

        // =======================
        // EVENTS
        // =======================
        public event Action<int>? OnDepartmentCreated;
        public event Action<bool>? DirtyChanged;

        // =======================
        // SERVICES (LAZY)
        // =======================
        private DepartmentService? _departmentService;

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
        // DESIGN TIME GUARD (robust)
        // =======================
        private bool IsInDesigner =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
            (Site?.DesignMode ?? false);

        // =======================
        // CTOR
        // =======================
        public ucDepartment()
        {
            InitializeComponent();

            // IMPORTANT: do NOTHING heavy here.
            // Designer must be able to instantiate the control safely.
            this.Load += ucDepartment_Load;
        }

        private void ucDepartment_Load(object? sender, EventArgs e)
        {
            if (IsInDesigner)
                return;

            // init services at runtime only
            _departmentService = new DepartmentService();

            WireDirtyEvents();

            // default state
            LoadNew();
        }

        // =======================
        // PUBLIC API
        // =======================
        public void LoadNew()
        {
            ClearErrors();

            Department = new Department
            {
                DepartmentId = 0,
                Name = ""
            };

            ResetUI();
            CurrentMode = enMode.AddNew;
            SetDirty(false);
        }

        public void LoadEntityData(int departmentId, enMode mode = enMode.View)
        {
            ClearErrors();

            if (departmentId <= 0)
            {
                LoadNew();
                return;
            }

            CurrentMode = mode;

            if (_departmentService == null)
                _departmentService = new DepartmentService();

            var res = _departmentService.GetById(departmentId); // Result<Department>
            if (!res.IsSuccess || res.Value == null)
                throw new InvalidOperationException(res.ErrorMessage ?? "Department not found.");

            Department = res.Value;

            BindEntityToUI();
            SetDirty(false);
        }

        public bool SaveCurrent()
        {
            if (!ValidateUI())
                return false;

            MapUIToEntity();

            if (_departmentService == null)
                _departmentService = new DepartmentService();

            bool wasAdd = (CurrentMode == enMode.AddNew);

            if (wasAdd)
            {
                var created = _departmentService.Create(Department); // Result<int>
                if (!created.IsSuccess || created.Value <= 0)
                {
                    clsMessage.ShowError(created.ErrorMessage ?? "Failed to create department.");
                    return false;
                }

                Department.DepartmentId = created.Value;

                CurrentMode = enMode.View;
                SetDirty(false);

                OnDepartmentCreated?.Invoke(Department.DepartmentId);
                return true;
            }
            else
            {
                var updated = _departmentService.Update(Department); // Result
                if (!updated.IsSuccess)
                {
                    clsMessage.ShowError(updated.ErrorMessage ?? "Failed to update department.");
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

            lblDepartmentID.Text = "[N/A]";
            txtUsername.Clear();

            ApplyMode();
        }

        private void BindEntityToUI()
        {
            lblDepartmentID.Text = DepartmentID > 0 ? DepartmentID.ToString() : "[N/A]";
            txtUsername.Text = Department.Name ?? "";

            ApplyMode();
        }

        private void MapUIToEntity()
        {
            Department.Name = txtUsername.Text.Trim();
        }

        private void ApplyMode()
        {
            bool view = (CurrentMode == enMode.View);
            bool editable = !view;

            btnSave.Visible = !view;
            txtUsername.ReadOnly = !editable;

            linkEdit.Visible = (view && DepartmentID > 0);
        }

        // =======================
        // VALIDATION
        // =======================
        private bool ValidateUI()
        {
            ClearErrors();
            bool ok = true;

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                SetError(txtUsername, "Department name is required.");
                ok = false;
            }

            if (!ok)
            {
                if (!string.IsNullOrEmpty(errorProvider1.GetError(txtUsername)))
                    txtUsername.Focus();
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
        }

        // =======================
        // UI EVENTS
        // =======================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!SaveCurrent())
            {
                clsMessage.ShowError("Department details failed to save.");
                return;
            }

            clsMessage.ShowSuccess("Department details saved successfully.");
        }

        private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentMode = enMode.Edit;
        }
    }



}
