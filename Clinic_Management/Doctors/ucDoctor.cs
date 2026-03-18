using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Doctors
{
    using Clinic_Management.Helpers;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities;
    using Clinic_Management_Entities.Entities;         // Staff, Doctor, Department, Speciality, Person
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;

    public partial class ucDoctor : UserControl
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
        public int StaffID => Staff?.StaffId ?? -1;
        public int PersonID => Staff?.PersonId ?? -1;

        public Staff Staff { get; private set; } = new Staff();
        public Doctor Doctor { get; private set; } = new Doctor();

        // =======================
        // EVENTS
        // =======================
        public event Action<int>? OnDoctorCreated;  // staffId
        public event Action<bool>? DirtyChanged;

        // =======================
        // SERVICES
        // =======================
        private readonly DoctorService _doctorService = new();
        private readonly StaffService _staffService = new(); // still needed for VIEW loading if you don’t have DoctorWithStaff DTO

        private readonly DepartmentService _departmentService = new();
        private readonly SpecialtyService _specialityService = new();

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
        public ucDoctor()
        {
            InitializeComponent();

            if (IsDesignTime)
                return;

            WireDirtyEvents();
            LoadCombos();
            //LoadNew();
        }

        // =======================
        // PUBLIC API
        // =======================

        public void LoadEntityData(int staffId, enMode mode = enMode.View)
        {
            ClearErrors();

            if (staffId <= 0)
            {
                LoadNew();
                return;
            }

            CurrentMode = mode;

           

            // Doctor (uses your smart method + auditing inside service)
            var docRes = _doctorService.GetByStaffId(staffId);
            if (!docRes.IsSuccess || docRes.Value is null)
            {
                clsMessage.ShowError(docRes.ErrorMessage ?? "Staff not found.");
                LoadNew();
                return;
            }
            else
            {
                Staff = _staffService.GetById(staffId).Value;
                Doctor = docRes.Value;
            }

            BindEntityToUI();
            SetDirty(false);
        }

        public void LoadNewForPerson(int personId)
        {
            LoadNew();

            if (personId > 0)
            {
                Staff.PersonId = personId;
                lblPersonId.Text = personId.ToString();
                SetDirty(true);
            }
        }

        public void LoadNew()
        {
            ClearErrors();

            Staff = new Staff
            {
                StaffId = 0,
                PersonId = 0,
                HireDate = DateTime.Today,
                IsActive = true
            };

            Doctor = new Doctor
            {
                StaffId = 0,
                ConsultationFee = 0m
            };

            ResetUI();
            CurrentMode = enMode.AddNew;
            SetDirty(false);
        }

        public bool SaveCurrent()
        {
            if (!ValidateUI())
                return false;

            MapUIToEntities();

            bool wasAdd = (CurrentMode == enMode.AddNew);

            if (wasAdd)
            {
                // ✅ Use ONE smart BLL method: creates Staff then Doctor
                var res = _doctorService.CreateWithStaff(Staff, Doctor);

                if (!res.IsSuccess || res.Value <= 0)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to create doctor.");
                    return false;
                }

                // reflect saved ids locally
                Staff.StaffId = res.Value;
                Doctor.StaffId = res.Value;

                BindEntityToUI();
                SetDirty(false);
                CurrentMode = enMode.View;

                OnDoctorCreated?.Invoke(res.Value);
                return true;
            }
            else
            {
                // ✅ Use ONE smart BLL method for update
                var res = _doctorService.UpdateWithStaff(Staff, Doctor);

                if (!res.IsSuccess)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to update doctor.");
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
            if (StaffID <= 0)
                return false;

            if (!clsMessage.ConfirmDelete("doctor"))
                return false;

            var res = _doctorService.DeleteWithStaff(StaffID);
            if (!res.IsSuccess)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Delete failed.");
                return false;
            }

            LoadNew();
            clsMessage.ShowSuccess("Doctor deleted successfully.");
            return true;
        }

        // =======================
        // UI CORE
        // =======================
        private void ResetUI()
        {
            ClearErrors();

            lblStaffID.Text = "[N/A]";
            lblPersonId.Text = Staff.PersonId > 0 ? Staff.PersonId.ToString() : "[N/A]";

            txtStaffCode.Clear();
            cbDepartmentID.SelectedValue = 0;
            dtpHireDate.Value = DateTime.Today;
            chkIsActive.Checked = true;

            cbSpecialityID.SelectedValue = 0;
            txtLicenseNumber.Clear();
            nudConsultationFee.Value = 0;

            ApplyMode();
        }

        private void BindEntityToUI()
        {
            lblStaffID.Text = Staff.StaffId > 0 ? Staff.StaffId.ToString() : "[N/A]";
            lblPersonId.Text = Staff.PersonId > 0 ? Staff.PersonId.ToString() : "[N/A]";

            txtStaffCode.Text = Staff.StaffCode ?? "";
            cbDepartmentID.SelectedValue = Staff.DepartmentId > 0 ? Staff.DepartmentId : 0;

            dtpHireDate.Value = Staff.HireDate.HasValue ? Staff.HireDate.Value : DateTime.Today;
            chkIsActive.Checked = Staff.IsActive;

            cbSpecialityID.SelectedValue = Doctor.SpecialtyId > 0 ? Doctor.SpecialtyId : 0;
            txtLicenseNumber.Text = Doctor.LicenseNumber ?? "";

            nudConsultationFee.Value = Doctor.ConsultationFee >= 0 ? (decimal)Doctor.ConsultationFee : 0;

            ApplyMode();
        }

        private void MapUIToEntities()
        {
            // Staff
            Staff.StaffCode = txtStaffCode.Text.Trim();
            Staff.DepartmentId = cbDepartmentID.SelectedValue is int dep ? dep : 0;
            Staff.HireDate = dtpHireDate.Value.Date;
            Staff.IsActive = chkIsActive.Checked;

            // Doctor
            Doctor.SpecialtyId = cbSpecialityID.SelectedValue is int sp ? sp : 0;
            Doctor.LicenseNumber = txtLicenseNumber.Text.Trim();
            Doctor.ConsultationFee = nudConsultationFee.Value;
        }

        private void ApplyMode()
        {
            bool editable = (CurrentMode != enMode.View);

            txtStaffCode.ReadOnly = !editable;
            cbDepartmentID.Enabled = editable;
            dtpHireDate.Enabled = editable;
            chkIsActive.Enabled = editable;

            cbSpecialityID.Enabled = editable;
            txtLicenseNumber.ReadOnly = !editable;
            nudConsultationFee.Enabled = editable;

            btnSave.Enabled = editable;

            // Optional buttons/links (if you have them)
            linkEdit.Visible = (CurrentMode == enMode.View && StaffID > 0);
            // linkDelete.Visible = (CurrentMode == enMode.View && StaffID > 0);

            lblStaffID.Text = StaffID > 0 ? StaffID.ToString() : "[N/A]";
            lblPersonId.Text = PersonID > 0 ? PersonID.ToString() : "[N/A]";
        }

        private void LoadCombos()
        {
            
            cbDepartmentID.DataSource = _departmentService
                .GetAll().Value
                .Prepend(new Department { DepartmentId = 0, Name = "-- Select Department --" })
                .ToList();

            cbDepartmentID.ValueMember = nameof(Department.DepartmentId);  // or DepartmentId (whatever your class has)
            cbDepartmentID.DisplayMember = nameof(Department.Name);

            cbDepartmentID.SelectedValue = 0;

            cbSpecialityID.DataSource = _specialityService
                .GetAll().Value
                .Prepend(new Specialty { SpecialtyId = 0, Name = "-- Select Speciality --" })
                .ToList();

            cbSpecialityID.ValueMember = nameof(Specialty.SpecialtyId); // or SpecialityId
            cbSpecialityID.DisplayMember = nameof(Specialty.Name);

            cbSpecialityID.SelectedValue = 0;
        }

        // =======================
        // VALIDATION
        // =======================
        private bool ValidateUI()
        {
            ClearErrors();
            bool ok = true;

            if (Staff.PersonId <= 0)
            {
                SetError(lblPersonId, "Please select a person first.");
                ok = false;
            }

            if (string.IsNullOrWhiteSpace(txtStaffCode.Text))
            {
                SetError(txtStaffCode, "Staff code is required.");
                ok = false;
            }

            if (!(cbDepartmentID.SelectedValue is int dep) || dep <= 0)
            {
                SetError(cbDepartmentID, "Please select department.");
                ok = false;
            }

            if (dtpHireDate.Value.Date > DateTime.Today)
            {
                SetError(dtpHireDate, "Hire date cannot be in the future.");
                ok = false;
            }

            if (!(cbSpecialityID.SelectedValue is int sp) || sp <= 0)
            {
                SetError(cbSpecialityID, "Please select speciality.");
                ok = false;
            }

            if (string.IsNullOrWhiteSpace(txtLicenseNumber.Text))
            {
                SetError(txtLicenseNumber, "License number is required.");
                ok = false;
            }

            if (nudConsultationFee.Value < 0)
            {
                SetError(nudConsultationFee, "Fee cannot be negative.");
                ok = false;
            }

            if (!ok)
            {
                if (!string.IsNullOrEmpty(errorProvider1.GetError(txtStaffCode))) txtStaffCode.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(cbDepartmentID))) cbDepartmentID.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(dtpHireDate))) dtpHireDate.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(cbSpecialityID))) cbSpecialityID.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(txtLicenseNumber))) txtLicenseNumber.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(nudConsultationFee))) nudConsultationFee.Focus();
            }

            return ok;
        }

        // =======================
        // DIRTY WIRING
        // =======================
        private void WireDirtyEvents()
        {
            txtStaffCode.TextChanged += (_, __) => SetDirty(true);
            cbDepartmentID.SelectedIndexChanged += (_, __) => SetDirty(true);
            dtpHireDate.ValueChanged += (_, __) => SetDirty(true);
            chkIsActive.CheckedChanged += (_, __) => SetDirty(true);

            cbSpecialityID.SelectedIndexChanged += (_, __) => SetDirty(true);
            txtLicenseNumber.TextChanged += (_, __) => SetDirty(true);
            nudConsultationFee.ValueChanged += (_, __) => SetDirty(true);
        }

        // =======================
        // UI EVENTS
        // =======================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!SaveCurrent())
            {
                clsMessage.ShowError("Doctor details failed to save.");
                return;
            }

            clsMessage.ShowSuccess("Doctor details saved successfully.");
        }

        private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentMode = enMode.Edit;
        }

        public void SetSelectedPerson(int personId)
        {
            if (personId <= 0) return;

            Staff.PersonId = personId;
            lblPersonId.Text = personId.ToString();

            if (CurrentMode == enMode.View)
                CurrentMode = enMode.Edit;

            SetDirty(true);
        }




    }
}
