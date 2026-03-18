using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Diagnostics.DiagnosticResult
{
    using Clinic_Management.Helpers;
    using Clinic_Management_BLL.Service;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public partial class ucDiagnosticResult : UserControl
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
        public int DiagnosticResultID => DiagnosticResult?.DiagnosticResultId ?? -1;
        public int DiagnosticRequestItemID => DiagnosticResult?.DiagnosticRequestItemId ?? -1;

        public Clinic_Management_Entities.Entities.DiagnosticResult DiagnosticResult { get; private set; } =
            new Clinic_Management_Entities.Entities.DiagnosticResult();

        // =======================
        // EVENTS
        // =======================
        public event Action<int>? OnDiagnosticResultCreated;
        public event Action<bool>? DirtyChanged;

        // =======================
        // SERVICES
        // =======================
        private readonly DiagnosticResultService _service = new();

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
        public ucDiagnosticResult()
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
                clsMessage.ShowError(res.ErrorMessage ?? "Diagnostic result not found.");
                return;
            }

            DiagnosticResult = res.Value;

            BindEntityToUI();
            SetDirty(false);
        }

        public void LoadNewForRequestItem(int diagnosticRequestItemId)
        {
            LoadNew();

            DiagnosticResult.DiagnosticRequestItemId = diagnosticRequestItemId;
            lblDiagnosticRequestItemId.Text = diagnosticRequestItemId > 0
                ? diagnosticRequestItemId.ToString()
                : "[N/A]";

            SetDirty(true);
        }

        public void LoadNew()
        {
            ClearErrors();

            DiagnosticResult = new Clinic_Management_Entities.Entities.DiagnosticResult
            {
                DiagnosticResultId = 0,
                DiagnosticRequestItemId = 0,
                ResultText = "",
                ResultNumeric = null,
                Unit = "",
                RefRange = "",
                ReportText = ""
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
                var res = _service.Create(DiagnosticResult);

                if (!res.IsSuccess || res.Value <= 0)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to create diagnostic result.");
                    return false;
                }

                DiagnosticResult.DiagnosticResultId = res.Value;

                BindEntityToUI();
                SetDirty(false);
                CurrentMode = enMode.View;

                OnDiagnosticResultCreated?.Invoke(res.Value);
                return true;
            }
            else
            {
                var res = _service.Update(DiagnosticResult);

                if (!res.IsSuccess)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to update diagnostic result.");
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
            if (DiagnosticResultID <= 0)
                return false;

            if (!clsMessage.ConfirmDelete("diagnostic result"))
                return false;

            var res = _service.Delete(DiagnosticResultID);
            if (!res.IsSuccess)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Delete failed.");
                return false;
            }

            LoadNew();
            clsMessage.ShowSuccess("Diagnostic result deleted successfully.");
            return true;
        }

        // =======================
        // UI CORE
        // =======================
        private void ResetUI()
        {
            ClearErrors();

            lblDiagnosticResultId.Text = "[N/A]";
            lblDiagnosticRequestItemId.Text = "[N/A]";
            txtResultText.Text = string.Empty;
            txtResultNumeric.Text = string.Empty;
            txtUnit.Text = string.Empty;
            txtRefRange.Text = string.Empty;
            txtReportText.Text = string.Empty;

            ApplyMode();
        }

        private void BindEntityToUI()
        {
            lblDiagnosticResultId.Text = DiagnosticResultID > 0
                ? DiagnosticResultID.ToString()
                : "[N/A]";

            lblDiagnosticRequestItemId.Text = DiagnosticRequestItemID > 0
                ? DiagnosticRequestItemID.ToString()
                : "[N/A]";

            txtResultText.Text = DiagnosticResult.ResultText ?? "";
            txtResultNumeric.Text = DiagnosticResult.ResultNumeric?.ToString() ?? "";
            txtUnit.Text = DiagnosticResult.Unit ?? "";
            txtRefRange.Text = DiagnosticResult.RefRange ?? "";
            txtReportText.Text = DiagnosticResult.ReportText ?? "";

            ApplyMode();
        }

        private void MapUIToEntity()
        {
            DiagnosticResult.ResultText = txtResultText.Text.Trim();

            if (decimal.TryParse(txtResultNumeric.Text.Trim(), out decimal num))
                DiagnosticResult.ResultNumeric = num;
            else
                DiagnosticResult.ResultNumeric = null;

            DiagnosticResult.Unit = txtUnit.Text.Trim();
            DiagnosticResult.RefRange = txtRefRange.Text.Trim();
            DiagnosticResult.ReportText = txtReportText.Text.Trim();
        }

        private void ApplyMode()
        {
            bool editable = (CurrentMode != enMode.View);

            txtResultText.ReadOnly = !editable;
            txtResultNumeric.ReadOnly = !editable;
            txtUnit.ReadOnly = !editable;
            txtRefRange.ReadOnly = !editable;
            txtReportText.ReadOnly = !editable;

            btnSave.Enabled = editable;
            linkEdit.Visible = (CurrentMode == enMode.View && DiagnosticResultID > 0);

            lblDiagnosticResultId.Text = DiagnosticResultID > 0
                ? DiagnosticResultID.ToString()
                : "[N/A]";
        }

        // =======================
        // VALIDATION
        // =======================
        private bool ValidateUI()
        {
            ClearErrors();
            bool ok = true;

            if (DiagnosticResult.DiagnosticRequestItemId <= 0)
            {
                SetError(lblDiagnosticRequestItemId, "Diagnostic request item is required.");
                ok = false;
            }

            if (string.IsNullOrWhiteSpace(txtResultText.Text) &&
                string.IsNullOrWhiteSpace(txtResultNumeric.Text))
            {
                SetError(txtResultText, "Enter text or numeric result.");
                SetError(txtResultNumeric, "Enter text or numeric result.");
                ok = false;
            }

            if (!ok)
            {
                if (!string.IsNullOrEmpty(errorProvider1.GetError(lblDiagnosticRequestItemId)))
                    lblDiagnosticRequestItemId.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(txtResultText)))
                    txtResultText.Focus();
            }

            return ok;
        }

        // =======================
        // DIRTY WIRING
        // =======================
        private void WireDirtyEvents()
        {
            txtResultText.TextChanged += (_, __) => SetDirty(true);
            txtResultNumeric.TextChanged += (_, __) => SetDirty(true);
            txtUnit.TextChanged += (_, __) => SetDirty(true);
            txtRefRange.TextChanged += (_, __) => SetDirty(true);
            txtReportText.TextChanged += (_, __) => SetDirty(true);
        }

        // =======================
        // UI EVENTS
        // =======================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!SaveCurrent())
            {
                clsMessage.ShowError("Diagnostic result failed to save.");
                return;
            }

            clsMessage.ShowSuccess("Diagnostic result saved successfully.");
        }

        private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentMode = enMode.Edit;
        }
    }
}
