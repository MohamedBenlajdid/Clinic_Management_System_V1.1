using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Specialities
{
    using Clinic_Management.Helpers;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public partial class ucSpeciality : UserControl
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
        public int SpecialityID => Speciality?.SpecialtyId ?? -1;
        public Specialty Speciality { get; private set; } = new Specialty();

        // =======================
        // EVENTS
        // =======================
        public event Action<int>? OnSpecialityCreated;
        public event Action<bool>? DirtyChanged;

        // =======================
        // SERVICES (lazy init)
        // =======================
        private SpecialtyService? _specialityService;

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
        public ucSpeciality()
        {
            InitializeComponent();

            this.Load += ucSpeciality_Load;
        }

        private void ucSpeciality_Load(object? sender, EventArgs e)
        {
            if (IsInDesigner)
                return;

            _specialityService = new SpecialtyService();

            WireDirtyEvents();
            LoadNew();
        }

        // =======================
        // PUBLIC API
        // =======================
        public void LoadNew()
        {
            ClearErrors();

            Speciality = new Specialty
            {
                SpecialtyId = 0,
                Name = ""
            };

            ResetUI();
            CurrentMode = enMode.AddNew;
            SetDirty(false);
        }

        public void LoadEntityData(int specialityId, enMode mode = enMode.View)
        {
            ClearErrors();

            if (specialityId <= 0)
            {
                LoadNew();
                return;
            }

            CurrentMode = mode;

            if (_specialityService == null)
                _specialityService = new SpecialtyService();

            var res = _specialityService.GetById(specialityId);
            if (!res.IsSuccess || res.Value == null)
                throw new InvalidOperationException(res.ErrorMessage ?? "Speciality not found.");

            Speciality = res.Value;

            BindEntityToUI();
            SetDirty(false);
        }

        public bool SaveCurrent()
        {
            if (!ValidateUI())
                return false;

            MapUIToEntity();

            if (_specialityService == null)
                _specialityService = new SpecialtyService();

            bool wasAdd = (CurrentMode == enMode.AddNew);

            if (wasAdd)
            {
                var created = _specialityService.Create(Speciality);
                if (!created.IsSuccess || created.Value <= 0)
                {
                    clsMessage.ShowError(created.ErrorMessage ?? "Failed to create speciality.");
                    return false;
                }

                Speciality.SpecialtyId = created.Value;

                CurrentMode = enMode.View;
                SetDirty(false);

                OnSpecialityCreated?.Invoke(Speciality.SpecialtyId);
                return true;
            }
            else
            {
                var updated = _specialityService.Update(Speciality);
                if (!updated.IsSuccess)
                {
                    clsMessage.ShowError(updated.ErrorMessage ?? "Failed to update speciality.");
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

            lblSpecialityID.Text = "[N/A]";
            txtSpecialityName.Clear();

            ApplyMode();
        }

        private void BindEntityToUI()
        {
            lblSpecialityID.Text = SpecialityID > 0 ? SpecialityID.ToString() : "[N/A]";
            txtSpecialityName.Text = Speciality.Name ?? "";

            ApplyMode();
        }

        private void MapUIToEntity()
        {
            Speciality.Name = txtSpecialityName.Text.Trim();
        }

        private void ApplyMode()
        {
            bool view = (CurrentMode == enMode.View);
            bool editable = !view;

            btnSave.Visible = !view;
            txtSpecialityName.ReadOnly = !editable;

            linkEdit.Visible = (view && SpecialityID > 0);
        }

        // =======================
        // VALIDATION
        // =======================
        private bool ValidateUI()
        {
            ClearErrors();
            bool ok = true;

            if (string.IsNullOrWhiteSpace(txtSpecialityName.Text))
            {
                SetError(txtSpecialityName, "Speciality name is required.");
                ok = false;
            }

            if (!ok)
                txtSpecialityName.Focus();

            return ok;
        }

        // =======================
        // DIRTY WIRING
        // =======================
        private void WireDirtyEvents()
        {
            txtSpecialityName.TextChanged += (_, __) =>
            {
                SetDirty(true);
                errorProvider1.SetError(txtSpecialityName, "");
            };
        }

        // =======================
        // UI EVENTS
        // =======================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!SaveCurrent())
            {
                clsMessage.ShowError("Speciality details failed to save.");
                return;
            }

            clsMessage.ShowSuccess("Speciality details saved successfully.");
        }

        private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentMode = enMode.Edit;
        }
    }

}
