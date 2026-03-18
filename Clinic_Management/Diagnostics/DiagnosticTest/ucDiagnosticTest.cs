using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Clinic_Management.Diagnostics.DiagnosticTest
{
    using Clinic_Management.Helpers;
    using Clinic_Management_BLL.Service;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public partial class ucDiagnosticTest : UserControl
    {
        private void ClearErrors() => errorProvider1.Clear();
        private void SetError(Control ctrl, string message) => errorProvider1.SetError(ctrl, message);

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
        public int DiagnosticTestID => DiagnosticTest?.DiagnosticTestId ?? -1;

        public Clinic_Management_Entities.Entities.DiagnosticTest DiagnosticTest { get; private set; } 
            = new Clinic_Management_Entities.Entities.DiagnosticTest();

        // =======================
        // EVENTS
        // =======================
        public event Action<int>? OnDiagnosticTestCreated;
        public event Action<bool>? DirtyChanged;

        // =======================
        // SERVICES
        // =======================
        private readonly DiagnosticTestService _diagnosticTestService = new();

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
        private static bool IsDesignTime =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime;

        // =======================
        // CTOR
        // =======================
        public ucDiagnosticTest()
        {
            InitializeComponent();

            if (IsDesignTime)
                return;

            chkIsActive.Checked = true;
            WireDirtyEvents();
            //LoadNew();
        }

        // =======================
        // PUBLIC API
        // =======================
        public void LoadEntityData(int diagnosticTestId, enMode mode = enMode.View)
        {
            ClearErrors();

            if (diagnosticTestId <= 0)
            {
                LoadNew();
                return;
            }

            CurrentMode = mode;

            var res = _diagnosticTestService.GetById(diagnosticTestId);
            if (!res.IsSuccess || res.Value is null)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Diagnostic test not found.");
                return;
            }

            DiagnosticTest = res.Value;

            BindEntityToUI();
            SetDirty(false);
        }

        public void LoadNew()
        {
            ClearErrors();

            DiagnosticTest = new Clinic_Management_Entities.Entities.DiagnosticTest
            {
                DiagnosticTestId = 0,
                Code = "",
                Name = "",
                Category = "",
                Unit = "",
                RefRange = "",
                IsActive = true
            };

            ResetUI();
            CurrentMode = enMode.AddNew;
            SetDirty(false);
        }

        public bool SaveCurrent()
        {
            if (!ValidateUI())
                return false;

            MapUIToEntity();

            bool wasAdd = (CurrentMode == enMode.AddNew);

            if (wasAdd)
            {
                var res = _diagnosticTestService.Create(DiagnosticTest);

                if (!res.IsSuccess || res.Value <= 0)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to create diagnostic test.");
                    return false;
                }

                DiagnosticTest.DiagnosticTestId = res.Value;

                BindEntityToUI();
                SetDirty(false);
                CurrentMode = enMode.View;

                OnDiagnosticTestCreated?.Invoke(res.Value);
                return true;
            }
            else
            {
                var res = _diagnosticTestService.Update(DiagnosticTest);

                if (!res.IsSuccess)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to update diagnostic test.");
                    return false;
                }

                BindEntityToUI();
                SetDirty(false);
                CurrentMode = enMode.View;
                return true;
            }
        }

        public bool DeleteCurrent()
        {
            if (DiagnosticTestID <= 0)
                return false;

            if (!clsMessage.ConfirmDelete("diagnostic test"))
                return false;

            var res = _diagnosticTestService.Delete(DiagnosticTestID);
            if (!res.IsSuccess)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Delete failed.");
                return false;
            }

            LoadNew();
            clsMessage.ShowSuccess("Diagnostic test deleted successfully.");
            return true;
        }

        // =======================
        // UI CORE
        // =======================
        private void ResetUI()
        {
            ClearErrors();

            lblDiagnosticTestId.Text = "[N/A]";
            txtCode.Text = string.Empty;
            txtName.Text = string.Empty;
            txtCategory.Text = string.Empty;
            txtUnit.Text = string.Empty;
            txtRefRange.Text = string.Empty;
            chkIsActive.Checked = true;

            ApplyMode();
        }

        private void BindEntityToUI()
        {
            lblDiagnosticTestId.Text = DiagnosticTestID > 0 ? DiagnosticTestID.ToString() : "[N/A]";

            txtCode.Text = DiagnosticTest.Code ?? "";
            txtName.Text = DiagnosticTest.Name ?? "";
            txtCategory.Text = DiagnosticTest.Category ?? "";
            txtUnit.Text = DiagnosticTest.Unit ?? "";
            txtRefRange.Text = DiagnosticTest.RefRange ?? "";
            chkIsActive.Checked = DiagnosticTest.IsActive;

            ApplyMode();
        }

        private void MapUIToEntity()
        {
            DiagnosticTest.Code = txtCode.Text.Trim();
            DiagnosticTest.Name = txtName.Text.Trim();
            DiagnosticTest.Category = txtCategory.Text.Trim();
            DiagnosticTest.Unit = txtUnit.Text.Trim();
            DiagnosticTest.RefRange = txtRefRange.Text.Trim();
            DiagnosticTest.IsActive = chkIsActive.Checked;
        }

        private void ApplyMode()
        {
            bool editable = (CurrentMode != enMode.View);

            txtCode.ReadOnly = !editable;
            txtName.ReadOnly = !editable;
            txtCategory.ReadOnly = !editable;
            txtUnit.ReadOnly = !editable;
            txtRefRange.ReadOnly = !editable;
            chkIsActive.Enabled = editable;

            btnSave.Enabled = editable;
            linkEdit.Visible = (CurrentMode == enMode.View && DiagnosticTestID > 0);

            lblDiagnosticTestId.Text = DiagnosticTestID > 0 ? DiagnosticTestID.ToString() : "[N/A]";
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
                SetError(txtCode, "Code is required.");
                ok = false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                SetError(txtName, "Name is required.");
                ok = false;
            }

            if (!ok)
            {
                if (!string.IsNullOrEmpty(errorProvider1.GetError(txtCode))) txtCode.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(txtName))) txtName.Focus();
            }

            return ok;
        }

        // =======================
        // DIRTY WIRING
        // =======================
        private void WireDirtyEvents()
        {
            txtCode.TextChanged += (_, __) => SetDirty(true);
            txtName.TextChanged += (_, __) => SetDirty(true);
            txtCategory.TextChanged += (_, __) => SetDirty(true);
            txtUnit.TextChanged += (_, __) => SetDirty(true);
            txtRefRange.TextChanged += (_, __) => SetDirty(true);
            chkIsActive.CheckedChanged += (_, __) => SetDirty(true);
        }

        // =======================
        // UI EVENTS
        // =======================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!SaveCurrent())
            {
                clsMessage.ShowError("Diagnostic test failed to save.");
                return;
            }

            clsMessage.ShowSuccess("Diagnostic test saved successfully.");
        }

        private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentMode = enMode.Edit;
        }
    }
}
