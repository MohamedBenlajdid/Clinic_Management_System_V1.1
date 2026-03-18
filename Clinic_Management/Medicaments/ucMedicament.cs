using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Medicaments
{
    using Clinic_Management.Helpers;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities.Entities;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public partial class ucMedicament : UserControl
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
        public int MedicamentID => Medicament?.MedicamentId ?? -1;

        public Medicament Medicament { get; private set; } = new Medicament();

        // =======================
        // EVENTS
        // =======================
        public event Action<int>? OnMedicamentCreated; // medicamentId
        public event Action<bool>? DirtyChanged;

        // =======================
        // SERVICES
        // =======================
        private readonly MedicamentService _medicamentService = new();

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
        public ucMedicament()
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
        public void LoadEntityData(int medicamentId, enMode mode = enMode.View)
        {
            ClearErrors();

            if (medicamentId <= 0)
            {
                LoadNew();
                return;
            }

            CurrentMode = mode;

            var res = _medicamentService.GetById(medicamentId);
            if (!res.IsSuccess || res.Value is null)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Medicament not found.");
                return;
            }

            Medicament = res.Value;

            BindEntityToUI();
            SetDirty(false);
        }

        public void LoadNew()
        {
            ClearErrors();

            Medicament = new Medicament
            {
                MedicamentId = 0,
                Name = "",
                GenericName = "",
                Form = "",
                Strength = "",
                Manufacturer = "",
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
                var res = _medicamentService.Create(Medicament);

                if (!res.IsSuccess || res.Value <= 0)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to create medicament.");
                    return false;
                }

                Medicament.MedicamentId = res.Value;

                BindEntityToUI();
                SetDirty(false);
                CurrentMode = enMode.View;

                OnMedicamentCreated?.Invoke(res.Value);
                return true;
            }
            else
            {
                var res = _medicamentService.Update(Medicament);

                if (!res.IsSuccess)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to update medicament.");
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
            if (MedicamentID <= 0)
                return false;

            if (!clsMessage.ConfirmDelete("medicament"))
                return false;

            var res = _medicamentService.Delete(MedicamentID);
            if (!res.IsSuccess)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Delete failed.");
                return false;
            }

            LoadNew();
            clsMessage.ShowSuccess("Medicament deleted successfully.");
            return true;
        }

        // =======================
        // UI CORE
        // =======================
        private void ResetUI()
        {
            ClearErrors();

            lblMedicamentId.Text = "[N/A]";
            txtName.Text = string.Empty;
            txtGenericName.Text = string.Empty;
            txtForm.Text = string.Empty;
            txtStrength.Text = string.Empty;
            txtManufacturer.Text = string.Empty;
            chkIsActive.Checked = true;

            ApplyMode();
        }

        private void BindEntityToUI()
        {
            lblMedicamentId.Text = Medicament.MedicamentId > 0 ? Medicament.MedicamentId.ToString() : "[N/A]";

            txtName.Text = Medicament.Name ?? "";
            txtGenericName.Text = Medicament.GenericName ?? "";
            txtForm.Text = Medicament.Form ?? "";
            txtStrength.Text = Medicament.Strength ?? "";
            txtManufacturer.Text = Medicament.Manufacturer ?? "";
            chkIsActive.Checked = Medicament.IsActive;

            ApplyMode();
        }

        private void MapUIToEntity()
        {
            Medicament.Name = txtName.Text.Trim();
            Medicament.GenericName = txtGenericName.Text.Trim();
            Medicament.Form = txtForm.Text.Trim();
            Medicament.Strength = txtStrength.Text.Trim();
            Medicament.Manufacturer = txtManufacturer.Text.Trim();
            Medicament.IsActive = chkIsActive.Checked;
        }

        private void ApplyMode()
        {
            bool editable = (CurrentMode != enMode.View);

            txtName.ReadOnly = !editable;
            txtGenericName.ReadOnly = !editable;
            txtForm.ReadOnly = !editable;
            txtStrength.ReadOnly = !editable;
            txtManufacturer.ReadOnly = !editable;
            chkIsActive.Enabled = editable;

            btnSave.Enabled = editable;

            linkEdit.Visible = (CurrentMode == enMode.View && MedicamentID > 0);

            lblMedicamentId.Text = MedicamentID > 0 ? MedicamentID.ToString() : "[N/A]";
        }

        // =======================
        // VALIDATION
        // =======================
        private bool ValidateUI()
        {
            ClearErrors();
            bool ok = true;

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                SetError(txtName, "Name is required.");
                ok = false;
            }

            // optional but recommended
            if (string.IsNullOrWhiteSpace(txtGenericName.Text))
            {
                SetError(txtGenericName, "Generic name is required.");
                ok = false;
            }

            if (string.IsNullOrWhiteSpace(txtForm.Text))
            {
                SetError(txtForm, "Form is required (e.g., Tablet, Syrup).");
                ok = false;
            }

            if (string.IsNullOrWhiteSpace(txtStrength.Text))
            {
                SetError(txtStrength, "Strength is required (e.g., 500 mg).");
                ok = false;
            }

            if (!ok)
            {
                if (!string.IsNullOrEmpty(errorProvider1.GetError(txtName))) txtName.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(txtGenericName))) txtGenericName.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(txtForm))) txtForm.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(txtStrength))) txtStrength.Focus();
            }

            return ok;
        }

        // =======================
        // DIRTY WIRING
        // =======================
        private void WireDirtyEvents()
        {
            txtName.TextChanged += (_, __) => SetDirty(true);
            txtGenericName.TextChanged += (_, __) => SetDirty(true);
            txtForm.TextChanged += (_, __) => SetDirty(true);
            txtStrength.TextChanged += (_, __) => SetDirty(true);
            txtManufacturer.TextChanged += (_, __) => SetDirty(true);
            chkIsActive.CheckedChanged += (_, __) => SetDirty(true);
        }

        // =======================
        // UI EVENTS
        // =======================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!SaveCurrent())
            {
                clsMessage.ShowError("Medicament failed to save.");
                return;
            }

            clsMessage.ShowSuccess("Medicament saved successfully.");
        }

        private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentMode = enMode.Edit;
        }
    }

    /* 
    NOTES / EXPECTED DEPENDENCIES (same as your pattern):
    - Controls: lblMedicamentId, txtName, txtGenericName, txtForm, txtStrength, txtManufacturer, chkIsActive,
                btnSave, linkEdit, errorProvider1
    - Entity: Medicament { int MedicamentId; string? Name; string? GenericName; string? Form; string? Strength; string? Manufacturer; bool IsActive; }
    - Service: MedicamentService : BaseCrudService<Medicament> with Create/GetById/Update/Delete returning Result/Result<T>
    - clsMessage: ShowError/ShowSuccess/ConfirmDelete
    */



}
