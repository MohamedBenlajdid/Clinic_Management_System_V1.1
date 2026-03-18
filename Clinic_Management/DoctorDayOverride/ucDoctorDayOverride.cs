using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.DoctorDayOverride
{
    using Clinic_Management.Doctors;
    using Clinic_Management.Helpers;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities.Entities;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public partial class ucDoctorDayOverride : UserControl
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
        public int OverrideID => Override?.OverrideId ?? -1;
        public int DoctorID => Override?.DoctorId ?? -1;

        public DoctorDayOverride Override { get; private set; } = new DoctorDayOverride();

        // =======================
        // EVENTS
        // =======================
        public event Action<int>? OnOverrideCreated;   // overrideId
        public event Action<bool>? DirtyChanged;

        // =======================
        // SERVICES
        // =======================
        private readonly DoctorDayOverrideService _overrideService = new();

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
        public ucDoctorDayOverride()
        {
            InitializeComponent();

            if (IsDesignTime)
                return;

            chkIsOverride.Checked = true;
            WireDirtyEvents();
            //LoadNew();
        }

        // =======================
        // PUBLIC API
        // =======================
        public void LoadEntityData(int overrideId, enMode mode = enMode.View)
        {
            ClearErrors();

            if (overrideId <= 0)
            {
                LoadNew();
                return;
            }

            CurrentMode = mode;

            var res = _overrideService.GetById(overrideId);
            if (!res.IsSuccess || res.Value is null)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Override not found.");
                //LoadNew();
                return;
            }

            Override = res.Value;

            BindEntityToUI();
            SetDirty(false);
        }

        public void LoadNewForDoctor(int doctorId)
        {
            LoadNew();

            if (doctorId > 0)
            {
                Override.DoctorId = doctorId;
                lblDoctorId.Text = doctorId.ToString();
                SetDirty(true);
            }
        }

        public void LoadNew()
        {
            ClearErrors();

            Override = new DoctorDayOverride
            {
                OverrideId = 0,
                DoctorId = 0,
                Date = DateTime.Today,
                IsOverride = true,
                IsDayOff = false,
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


                var res = _overrideService.Create(Override);

                if (!res.IsSuccess || res.Value <= 0)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to create override.");
                    return false;
                }

                Override.OverrideId = res.Value;

                BindEntityToUI();
                SetDirty(false);
                CurrentMode = enMode.View;

                OnOverrideCreated?.Invoke(res.Value);
                return true;
            }
            else
            {
                var res = _overrideService.Update(Override);

                if (!res.IsSuccess)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to update override.");
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
            if (OverrideID <= 0)
                return false;

            if (!clsMessage.ConfirmDelete("day override"))
                return false;

            var res = _overrideService.Delete(OverrideID);
            if (!res.IsSuccess)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Delete failed.");
                return false;
            }

            LoadNew();
            clsMessage.ShowSuccess("Override deleted successfully.");
            return true;
        }

        // =======================
        // UI CORE
        // =======================
        private void ResetUI()
        {
            ClearErrors();

            lblOverrideId.Text = "[N/A]";
            lblDoctorId.Text = Override.DoctorId > 0 ? Override.DoctorId.ToString() : "[N/A]";

            dtpDate.Value = DateTime.Today;
            chkIsOverride.Checked = true;
            chkIsDayOff.Checked = false;
            txtNotes.Clear();

            ApplyMode();
        }

        private void BindEntityToUI()
        {
            lblOverrideId.Text = Override.OverrideId > 0 ? Override.OverrideId.ToString() : "[N/A]";
            lblDoctorId.Text = Override.DoctorId > 0 ? Override.DoctorId.ToString() : "[N/A]";

            dtpDate.Value = Override.Date == default ? DateTime.Today : Override.Date.Date;
            chkIsOverride.Checked = Override.IsOverride;
            chkIsDayOff.Checked = Override.IsDayOff;
            txtNotes.Text = Override.Notes ?? "";

            ApplyMode();
        }

        private void MapUIToEntity()
        {
            Override.Date = dtpDate.Value.Date;
            Override.IsOverride = chkIsOverride.Checked;
            Override.IsDayOff = chkIsDayOff.Checked;
            Override.Notes = txtNotes.Text.Trim();
            
            
        }

        private void ApplyMode()
        {
            bool editable = (CurrentMode != enMode.View);

            dtpDate.Enabled = editable;
            chkIsOverride.Enabled = false;
            chkIsDayOff.Enabled = editable;
            txtNotes.ReadOnly = !editable;
            

            btnSave.Enabled = editable;

            linkEdit.Visible = (CurrentMode == enMode.View && OverrideID > 0);
            linkLabel1.Visible = (CurrentMode == enMode.AddNew);

            lblOverrideId.Text = OverrideID > 0 ? OverrideID.ToString() : "[N/A]";
            lblDoctorId.Text = DoctorID > 0 ? DoctorID.ToString() : "[N/A]";
        }

        // =======================
        // VALIDATION
        // =======================
        private bool ValidateUI()
        {
            ClearErrors();
            bool ok = true;

            if (Override.DoctorId <= 0)
            {
                SetError(lblDoctorId, "Please set doctor first.");
                ok = false;
            }

            // date can be anything; keep basic guard
            if (dtpDate.Value.Date < new DateTime(2000, 1, 1))
            {
                SetError(dtpDate, "Invalid date.");
                ok = false;
            }

            if (chkIsDayOff.Checked && string.IsNullOrWhiteSpace(txtNotes.Text))
            {
                SetError(txtNotes, "Write a note/reason for day off.");
                ok = false;
            }

            // If not override and not day off => meaningless row
            if (!chkIsOverride.Checked && !chkIsDayOff.Checked)
            {
                SetError(chkIsOverride, "Select Override or Day Off.");
                SetError(chkIsDayOff, "Select Override or Day Off.");
                ok = false;
            }

            if (!ok)
            {
                if (!string.IsNullOrEmpty(errorProvider1.GetError(lblDoctorId))) lblDoctorId.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(dtpDate))) dtpDate.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(txtNotes))) txtNotes.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(chkIsOverride))) chkIsOverride.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(chkIsDayOff))) chkIsDayOff.Focus();
            }

            return ok;
        }

        // =======================
        // DIRTY WIRING
        // =======================
        private void WireDirtyEvents()
        {
            dtpDate.ValueChanged += (_, __) => SetDirty(true);
            chkIsOverride.CheckedChanged += (_, __) => SetDirty(true);
            chkIsDayOff.CheckedChanged += (_, __) => SetDirty(true);
            txtNotes.TextChanged += (_, __) => SetDirty(true);
        }

        // =======================
        // UI EVENTS
        // =======================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!SaveCurrent())
            {
                clsMessage.ShowError("Override failed to save.");
                return;
            }

            clsMessage.ShowSuccess("Override saved successfully.");
        }

        private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentMode = enMode.Edit;
        }

        // From outside (doctor finder)
        public void SetDoctor(int doctorId)
        {
            if (doctorId <= 0) return;

            Override.DoctorId = doctorId;
            lblDoctorId.Text = doctorId.ToString();

            if (CurrentMode == enMode.View)
                CurrentMode = enMode.Edit;

            SetDirty(true);
        }

        void OnDoctorSelected(int doctorId)
        {
            SetDoctor(doctorId);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDoctorFinder frm = new frmDoctorFinder();
            frm.OnDoctorSelected += OnDoctorSelected;
            frm.OnDoctorSaved += OnDoctorSelected;
            frm.ShowDialog();
        }

        private void chkIsOverride_CheckedChanged(object sender, EventArgs e)
        {
            //chkIsDayOff.Checked = !chkIsOverride.Checked;
        }

        private void chkIsDayOff_CheckedChanged(object sender, EventArgs e)
        {
           // chkIsOverride.Checked = !chkIsDayOff.Checked;
        }
    }



}
