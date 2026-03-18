using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Diagnostics.DiagnosticRequest
{
    using Clinic_Management.Diagnostics.DiagnosticTest;
    using Clinic_Management.Helpers;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities.Entities;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public partial class ucDiagnosticRequestItem : UserControl
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
        public int DiagnosticRequestItemID => DiagnosticRequestItem?.DiagnosticRequestItemId ?? -1;
        public int DiagnosticRequestID => DiagnosticRequestItem?.DiagnosticRequestId ?? -1;
        public int DiagnosticTestID => DiagnosticRequestItem?.DiagnosticTestId ?? -1;

        public DiagnosticRequestItem DiagnosticRequestItem { get; private set; } = new DiagnosticRequestItem();

        // =======================
        // EVENTS
        // =======================
        public event Action<int>? OnDiagnosticRequestItemCreated;
        public event Action<bool>? DirtyChanged;

        // =======================
        // SERVICES
        // =======================
        private readonly DiagnosticRequestItemService _service = new();

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
        public ucDiagnosticRequestItem()
        {
            InitializeComponent();

            if (IsDesignTime)
                return;

            WireDirtyEvents();
            //LoadNew();
        }

        // =======================
        // PUBLIC API
        // =======================
        public void LoadEntityData(int id, enMode mode = enMode.View)
        {
            ClearErrors();

            if (id <= 0)
            {
                LoadNew();
                return;
            }

            CurrentMode = mode;

            var res = _service.GetById(id);
            if (!res.IsSuccess || res.Value is null)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Diagnostic request item not found.");
                return;
            }

            DiagnosticRequestItem = res.Value;

            BindEntityToUI();
            SetDirty(false);
        }

        public void LoadNewForRequest(int diagnosticRequestId)
        {
            LoadNew();

            DiagnosticRequestItem.DiagnosticRequestId = diagnosticRequestId;
            lblDiagnosticRequestId.Text = diagnosticRequestId > 0
                ? diagnosticRequestId.ToString()
                : "[N/A]";

            SetDirty(true);
        }

        public void LoadNew()
        {
            ClearErrors();

            DiagnosticRequestItem = new DiagnosticRequestItem
            {
                DiagnosticRequestItemId = 0,
                DiagnosticRequestId = 0,
                DiagnosticTestId = 0,
                Notes = ""
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
                var res = _service.Create(DiagnosticRequestItem);

                if (!res.IsSuccess || res.Value <= 0)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to create diagnostic request item.");
                    return false;
                }

                DiagnosticRequestItem.DiagnosticRequestItemId = res.Value;

                BindEntityToUI();
                SetDirty(false);
                CurrentMode = enMode.View;

                OnDiagnosticRequestItemCreated?.Invoke(res.Value);
                return true;
            }
            else
            {
                var res = _service.Update(DiagnosticRequestItem);

                if (!res.IsSuccess)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to update diagnostic request item.");
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
            if (DiagnosticRequestItemID <= 0)
                return false;

            if (!clsMessage.ConfirmDelete("diagnostic request item"))
                return false;

            var res = _service.Delete(DiagnosticRequestItemID);
            if (!res.IsSuccess)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Delete failed.");
                return false;
            }

            LoadNew();
            clsMessage.ShowSuccess("Diagnostic request item deleted successfully.");
            return true;
        }

        // =======================
        // UI CORE
        // =======================
        private void ResetUI()
        {
            ClearErrors();

            lblDiagnosticRequestItemId.Text = "[N/A]";
            lblDiagnosticRequestId.Text = "[N/A]";
            lblDiagnosticTestId.Text = "[N/A]";
            txtNotes.Text = string.Empty;

            ApplyMode();
        }

        private void BindEntityToUI()
        {
            lblDiagnosticRequestItemId.Text = DiagnosticRequestItemID > 0
                ? DiagnosticRequestItemID.ToString()
                : "[N/A]";

            lblDiagnosticRequestId.Text = DiagnosticRequestID > 0
                ? DiagnosticRequestID.ToString()
                : "[N/A]";

            lblDiagnosticTestId.Text = DiagnosticTestID > 0
                ? DiagnosticTestID.ToString()
                : "[N/A]";

            txtNotes.Text = DiagnosticRequestItem.Notes ?? "";

            ApplyMode();
        }

        private void MapUIToEntity()
        {
            DiagnosticRequestItem.Notes = txtNotes.Text.Trim();
        }

        private void ApplyMode()
        {
            bool editable = (CurrentMode != enMode.View);

            txtNotes.ReadOnly = !editable;

            btnSave.Enabled = editable;
            linkEdit.Visible = (CurrentMode == enMode.View && DiagnosticRequestItemID > 0);

            lblDiagnosticRequestItemId.Text = DiagnosticRequestItemID > 0
                ? DiagnosticRequestItemID.ToString()
                : "[N/A]";
        }

        // =======================
        // VALIDATION
        // =======================
        private bool ValidateUI()
        {
            ClearErrors();
            bool ok = true;

            if (DiagnosticRequestItem.DiagnosticRequestId <= 0)
            {
                SetError(lblDiagnosticRequestId, "Diagnostic request is required.");
                ok = false;
            }

            if (DiagnosticRequestItem.DiagnosticTestId <= 0)
            {
                SetError(lblDiagnosticTestId, "Diagnostic test is required.");
                ok = false;
            }

            if (!ok)
            {
                if (!string.IsNullOrEmpty(errorProvider1.GetError(lblDiagnosticRequestId)))
                    lblDiagnosticRequestId.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(lblDiagnosticTestId)))
                    lblDiagnosticTestId.Focus();
            }

            return ok;
        }

        // =======================
        // DIRTY WIRING
        // =======================
        private void WireDirtyEvents()
        {
            txtNotes.TextChanged += (_, __) => SetDirty(true);
        }

        // =======================
        // UI EVENTS
        // =======================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!SaveCurrent())
            {
                clsMessage.ShowError("Diagnostic request item failed to save.");
                return;
            }

            clsMessage.ShowSuccess("Diagnostic request item saved successfully.");
        }

        private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentMode = enMode.Edit;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void linkAddOrFindTest_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDiagnosticTestFinder frm = new frmDiagnosticTestFinder();
            frm.OnDiagnosticTestSaved += OnTestIdSelected;
            frm.OnDiagnosticTestSelected += OnTestIdSelected;
            frm.ShowDialog();
        }

        void OnTestIdSelected(int  testId)
        {
            this.DiagnosticRequestItem.DiagnosticTestId = testId;
            this.lblDiagnosticTestId.Text = testId.ToString();  
        }

    }


}
