using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Prescriptions
{
    using Clinic_Management.Helpers;
    using Clinic_Management.Medicaments;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities.Entities;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public partial class ucPrescriptionItem : UserControl
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
        public int PrescriptionItemID => PrescriptionItem?.PrescriptionItemId ?? -1;
        public int PrescriptionID => PrescriptionItem?.PrescriptionId ?? -1;
        public int MedicamentID => PrescriptionItem?.MedicamentId ?? -1;

        public PrescriptionItem PrescriptionItem { get; private set; } = new PrescriptionItem();

        // =======================
        // EVENTS
        // =======================
        public event Action<int>? OnPrescriptionItemCreated;
        public event Action<bool>? DirtyChanged;

        // =======================
        // SERVICES
        // =======================
        private readonly PrescriptionItemService _prescriptionItemService = new();

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
        public ucPrescriptionItem()
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
        public void LoadEntityData(int prescriptionItemId, enMode mode = enMode.View)
        {
            ClearErrors();

            if (prescriptionItemId <= 0)
            {
                LoadNew();
                return;
            }

            CurrentMode = mode;

            var res = _prescriptionItemService.GetById(prescriptionItemId);
            if (!res.IsSuccess || res.Value is null)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Prescription item not found.");
                return;
            }

            PrescriptionItem = res.Value;

            BindEntityToUI();
            SetDirty(false);
        }

        public void LoadNewForPrescription(int prescriptionId)
        {
            LoadNew();

            PrescriptionItem.PrescriptionId = prescriptionId;
            lblPrescriptionId.Text = prescriptionId > 0 ? prescriptionId.ToString() : "[N/A]";

            SetDirty(true);
        }

        public void LoadNew()
        {
            ClearErrors();

            PrescriptionItem = new PrescriptionItem
            {
                PrescriptionItemId = 0,
                PrescriptionId = 0,
                MedicamentId = 0,
                Dose = "",
                Frequency = "",
                DurationDays = 1,
                Route = "",
                Instructions = "",
                Quantity = 1
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
                var res = _prescriptionItemService.Create(PrescriptionItem);

                if (!res.IsSuccess || res.Value <= 0)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to create prescription item.");
                    return false;
                }

                PrescriptionItem.PrescriptionItemId = res.Value;

                BindEntityToUI();
                SetDirty(false);
                CurrentMode = enMode.View;

                OnPrescriptionItemCreated?.Invoke(res.Value);
                return true;
            }
            else
            {
                var res = _prescriptionItemService.Update(PrescriptionItem);

                if (!res.IsSuccess)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to update prescription item.");
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
            if (PrescriptionItemID <= 0)
                return false;

            if (!clsMessage.ConfirmDelete("prescription item"))
                return false;

            var res = _prescriptionItemService.Delete(PrescriptionItemID);
            if (!res.IsSuccess)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Delete failed.");
                return false;
            }

            LoadNew();
            clsMessage.ShowSuccess("Prescription item deleted successfully.");
            return true;
        }

        // =======================
        // UI CORE
        // =======================
        private void ResetUI()
        {
            ClearErrors();

            lblPrescriptionItemId.Text = "[N/A]";
            lblPrescriptionId.Text = "[N/A]";
            lblMedicamentId.Text = "[N/A]";
            txtDose.Text = string.Empty;
            txtFrequency.Text = string.Empty;
            nudDurationDays.Value = 1;
            txtRoute.Text = string.Empty;
            txtInstructions.Text = string.Empty;
            nudQuantity.Value = 1;

            ApplyMode();
        }

        private void BindEntityToUI()
        {
            lblPrescriptionItemId.Text = PrescriptionItemID > 0 ? PrescriptionItemID.ToString() : "[N/A]";
            lblPrescriptionId.Text = PrescriptionID > 0 ? PrescriptionID.ToString() : "[N/A]";
            lblMedicamentId.Text = MedicamentID > 0 ? MedicamentID.ToString() : "[N/A]";

            txtDose.Text = PrescriptionItem.Dose ?? "";
            txtFrequency.Text = PrescriptionItem.Frequency ?? "";
            nudDurationDays.Value = PrescriptionItem.DurationDays > 0 ? (decimal)PrescriptionItem.DurationDays : 1;
            txtRoute.Text = PrescriptionItem.Route ?? "";
            txtInstructions.Text = PrescriptionItem.Instructions ?? "";
            nudQuantity.Value = PrescriptionItem.Quantity > 0 ? (decimal)PrescriptionItem.Quantity : 1;

            ApplyMode();
        }

        private void MapUIToEntity()
        {
            PrescriptionItem.Dose = txtDose.Text.Trim();
            PrescriptionItem.Frequency = txtFrequency.Text.Trim();
            PrescriptionItem.DurationDays = (short)nudDurationDays.Value;
            PrescriptionItem.Route = txtRoute.Text.Trim();
            PrescriptionItem.Instructions = txtInstructions.Text.Trim();
            PrescriptionItem.Quantity = (int)nudQuantity.Value;
        }

        private void ApplyMode()
        {
            bool editable = (CurrentMode != enMode.View);

            txtDose.ReadOnly = !editable;
            txtFrequency.ReadOnly = !editable;
            nudDurationDays.Enabled = editable;
            txtRoute.ReadOnly = !editable;
            txtInstructions.ReadOnly = !editable;
            nudQuantity.Enabled = editable;

            btnSave.Enabled = editable;
            linkEdit.Visible = (CurrentMode == enMode.View && PrescriptionItemID > 0);

            lblPrescriptionItemId.Text = PrescriptionItemID > 0 ? PrescriptionItemID.ToString() : "[N/A]";
        }

        // =======================
        // VALIDATION
        // =======================
        private bool ValidateUI()
        {
            ClearErrors();
            bool ok = true;

            if (PrescriptionItem.PrescriptionId <= 0)
            {
                SetError(lblPrescriptionId, "Prescription is required.");
                ok = false;
            }

            if (PrescriptionItem.MedicamentId <= 0)
            {
                SetError(lblMedicamentId, "Medicament is required.");
                ok = false;
            }

            if (string.IsNullOrWhiteSpace(txtDose.Text))
            {
                SetError(txtDose, "Dose is required.");
                ok = false;
            }

            if (!ok)
            {
                if (!string.IsNullOrEmpty(errorProvider1.GetError(lblPrescriptionId))) lblPrescriptionId.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(lblMedicamentId))) lblMedicamentId.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(txtDose))) txtDose.Focus();
            }

            return ok;
        }

        // =======================
        // DIRTY WIRING
        // =======================
        private void WireDirtyEvents()
        {
            txtDose.TextChanged += (_, __) => SetDirty(true);
            txtFrequency.TextChanged += (_, __) => SetDirty(true);
            nudDurationDays.ValueChanged += (_, __) => SetDirty(true);
            txtRoute.TextChanged += (_, __) => SetDirty(true);
            txtInstructions.TextChanged += (_, __) => SetDirty(true);
            nudQuantity.ValueChanged += (_, __) => SetDirty(true);
        }

        // =======================
        // UI EVENTS
        // =======================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!SaveCurrent())
            {
                clsMessage.ShowError("Prescription item failed to save.");
                return;
            }

            clsMessage.ShowSuccess("Prescription item saved successfully.");
        }

        private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentMode = enMode.Edit;
        }

        private void linkAddMedicament_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMedicamentFinder frm = new frmMedicamentFinder();
            frm.OnMedicamentSaved += OnMedicamentRecieved;
            frm.OnMedicamentSelected += OnMedicamentRecieved;
            frm.ShowDialog();
        }

        void OnMedicamentRecieved( int MedID)
        {
            this.PrescriptionItem.MedicamentId = MedID;
            lblMedicamentId.Text = MedID.ToString();    
        }

    }
}
