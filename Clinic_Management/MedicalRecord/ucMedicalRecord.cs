using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.MedicalRecord
{
    using Clinic_Management.Helpers;
    using Clinic_Management_BLL.Service;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public partial class ucMedicalRecord : UserControl
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
        public int MedicalRecordID => MedicalRecord?.MedicalRecordId ?? -1;
        public int AppointmentID => MedicalRecord?.AppointmentId ?? -1;
        public int PatientID => MedicalRecord?.PatientId ?? -1;
        public int DoctorID => MedicalRecord?.DoctorId ?? -1;

        public Clinic_Management_Entities.Entities.MedicalRecord MedicalRecord { get; private set; } = new Clinic_Management_Entities.Entities.MedicalRecord();

        // =======================
        // EVENTS
        // =======================
        public event Action<int>? OnMedicalRecordCreated;
        public event Action<bool>? DirtyChanged;

        // =======================
        // SERVICES
        // =======================
        private readonly MedicalRecordService _medicalRecordService = new();

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
        public ucMedicalRecord()
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
        public void LoadEntityData(int medicalRecordId, enMode mode = enMode.View)
        {
            ClearErrors();

            if (medicalRecordId <= 0)
            {
                LoadNew();
                return;
            }

            CurrentMode = mode;

            var res = _medicalRecordService.GetById(medicalRecordId);
            if (!res.IsSuccess || res.Value is null)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Medical record not found.");
                return;
            }

            MedicalRecord = res.Value;

            BindEntityToUI();
            SetDirty(false);
        }

        public void LoadNewForAppointment(int appointmentId, int patientId, int doctorId)
        {
            LoadNew();

            MedicalRecord.AppointmentId = appointmentId;
            MedicalRecord.PatientId = patientId;
            MedicalRecord.DoctorId = doctorId;

            lblAppointmentId.Text = appointmentId > 0 ? appointmentId.ToString() : "[A/N]";
            lblPatientId.Text = patientId > 0 ? patientId.ToString() : "[A/N]";
            lblDoctorId.Text = doctorId > 0 ? doctorId.ToString() : "[A/N]";

            SetDirty(true);
        }

        public void LoadNew()
        {
            ClearErrors();

            MedicalRecord = new Clinic_Management_Entities.Entities.MedicalRecord
            {
                MedicalRecordId = 0,
                AppointmentId = 0,
                PatientId = 0,
                DoctorId = 0,
                ChiefComplaint = "",
                HistoryOfPresentIllness = "",
                Diagnosis = "",
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
                var res = _medicalRecordService.Create(MedicalRecord);

                if (!res.IsSuccess || res.Value <= 0)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to create medical record.");
                    return false;
                }

                MedicalRecord.MedicalRecordId = res.Value;

                BindEntityToUI();
                SetDirty(false);
                CurrentMode = enMode.View;

                OnMedicalRecordCreated?.Invoke(res.Value);
                return true;
            }
            else
            {
                var res = _medicalRecordService.Update(MedicalRecord);

                if (!res.IsSuccess)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to update medical record.");
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
            if (MedicalRecordID <= 0)
                return false;

            if (!clsMessage.ConfirmDelete("medical record"))
                return false;

            var res = _medicalRecordService.Delete(MedicalRecordID);
            if (!res.IsSuccess)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Delete failed.");
                return false;
            }

            LoadNew();
            clsMessage.ShowSuccess("Medical record deleted successfully.");
            return true;
        }

        // =======================
        // UI CORE
        // =======================
        private void ResetUI()
        {
            ClearErrors();

            lblMedicalRecordId.Text = "[A/N]";
            lblAppointmentId.Text = "[A/N]";
            lblPatientId.Text = "[A/N]";
            lblDoctorId.Text = "[A/N]";

            txtChiefComplaint.Text = string.Empty;
            txtHistoryOfPresentIllness.Text = string.Empty;
            txtDiagnosis.Text = string.Empty;
            txtNotes.Text = string.Empty;

            linkEdit.Visible = false;
            btnSave.Enabled = false;

            ApplyMode();
        }

        private void BindEntityToUI()
        {
            lblMedicalRecordId.Text = MedicalRecordID > 0 ? MedicalRecordID.ToString() : "[A/N]";
            lblAppointmentId.Text = AppointmentID > 0 ? AppointmentID.ToString() : "[A/N]";
            lblPatientId.Text = PatientID > 0 ? PatientID.ToString() : "[A/N]";
            lblDoctorId.Text = DoctorID > 0 ? DoctorID.ToString() : "[A/N]";

            txtChiefComplaint.Text = MedicalRecord.ChiefComplaint ?? "";
            txtHistoryOfPresentIllness.Text = MedicalRecord.HistoryOfPresentIllness ?? "";
            txtDiagnosis.Text = MedicalRecord.Diagnosis ?? "";
            txtNotes.Text = MedicalRecord.Notes ?? "";

            ApplyMode();
        }

        private void MapUIToEntity()
        {
            MedicalRecord.ChiefComplaint = txtChiefComplaint.Text.Trim();
            MedicalRecord.HistoryOfPresentIllness = txtHistoryOfPresentIllness.Text.Trim();
            MedicalRecord.Diagnosis = txtDiagnosis.Text.Trim();
            MedicalRecord.Notes = txtNotes.Text.Trim();
        }

        private void ApplyMode()
        {
            bool editable = (CurrentMode != enMode.View);

            txtChiefComplaint.ReadOnly = !editable;
            txtHistoryOfPresentIllness.ReadOnly = !editable;
            txtDiagnosis.ReadOnly = !editable;
            txtNotes.ReadOnly = !editable;

            btnSave.Enabled = editable;
            linkEdit.Visible = (CurrentMode == enMode.View && MedicalRecordID > 0);

            lblMedicalRecordId.Text = MedicalRecordID > 0 ? MedicalRecordID.ToString() : "[A/N]";
        }

        // =======================
        // VALIDATION
        // =======================
        private bool ValidateUI()
        {
            ClearErrors();
            bool ok = true;

            if (MedicalRecord.AppointmentId <= 0)
            {
                SetError(lblAppointmentId, "Appointment is required.");
                ok = false;
            }

            if (string.IsNullOrWhiteSpace(txtChiefComplaint.Text))
            {
                SetError(txtChiefComplaint, "Chief complaint is required.");
                ok = false;
            }

            if (string.IsNullOrWhiteSpace(txtDiagnosis.Text))
            {
                SetError(txtDiagnosis, "Diagnosis is required.");
                ok = false;
            }

            if (!ok)
            {
                if (!string.IsNullOrEmpty(errorProvider1.GetError(lblAppointmentId))) lblAppointmentId.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(txtChiefComplaint))) txtChiefComplaint.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(txtDiagnosis))) txtDiagnosis.Focus();
            }

            return ok;
        }

        // =======================
        // DIRTY WIRING
        // =======================
        private void WireDirtyEvents()
        {
            txtChiefComplaint.TextChanged += (_, __) => SetDirty(true);
            txtHistoryOfPresentIllness.TextChanged += (_, __) => SetDirty(true);
            txtDiagnosis.TextChanged += (_, __) => SetDirty(true);
            txtNotes.TextChanged += (_, __) => SetDirty(true);
        }

        // =======================
        // UI EVENTS
        // =======================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!SaveCurrent())
            {
                clsMessage.ShowError("Medical record failed to save.");
                return;
            }

            clsMessage.ShowSuccess("Medical record saved successfully.");
        }

        private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentMode = enMode.Edit;
        }
    }
}
