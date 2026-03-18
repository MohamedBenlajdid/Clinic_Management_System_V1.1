using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.MedicalCertificate
{
    using Clinic_Management.Helpers;
    using Clinic_Management_BLL.Service;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public partial class ucMedicalCertificate : UserControl
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
        public int MedicalCertificateID => MedicalCertificate?.MedicalCertificateId ?? -1;
        public int AppointmentID => MedicalCertificate?.AppointmentId ?? -1;
        public int PatientID => MedicalCertificate?.PatientId ?? -1;
        public int DoctorID => MedicalCertificate?.DoctorId ?? -1;

        public Clinic_Management_Entities.Entities.MedicalCertificate MedicalCertificate { get; private set; } = 
            new Clinic_Management_Entities.Entities.MedicalCertificate();

        // =======================
        // EVENTS
        // =======================
        public event Action<int>? OnMedicalCertificateCreated;
        public event Action<bool>? DirtyChanged;

        // =======================
        // SERVICES
        // =======================
        private readonly MedicalCertificateService _service = new();

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
        public ucMedicalCertificate()
        {
            InitializeComponent();

            if (IsDesignTime)
                return;

            InitCombos();
            WireDirtyEvents();
            //LoadNew();
        }

        // =======================
        // INIT
        // =======================
        private void InitCombos()
        {
            cbCertificateType.Items.Clear();
            cbCertificateType.Items.AddRange(new object[]
            {
            "Medical Leave",
            "Fitness",
            "Return To Work",
            "Other"
            });

            cbCertificateType.SelectedIndex = 0;
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
                clsMessage.ShowError(res.ErrorMessage ?? "Medical certificate not found.");
                return;
            }

            MedicalCertificate = res.Value;

            BindEntityToUI();
            SetDirty(false);
        }

        public void LoadNewForAppointment(int appointmentId, int patientId, int doctorId)
        {
            LoadNew();

            MedicalCertificate.AppointmentId = appointmentId;
            MedicalCertificate.PatientId = patientId;
            MedicalCertificate.DoctorId = doctorId;

            lblAppointmentId.Text = appointmentId > 0 ? appointmentId.ToString() : "[N/A]";
            lblPatientId.Text = patientId > 0 ? patientId.ToString() : "[N/A]";
            lblDoctorId.Text = doctorId > 0 ? doctorId.ToString() : "[N/A]";

            SetDirty(true);
        }

        public void LoadNew()
        {
            ClearErrors();

            MedicalCertificate = new Clinic_Management_Entities.Entities.MedicalCertificate
            {
                MedicalCertificateId = 0,
                AppointmentId = 0,
                PatientId = 0,
                DoctorId = 0,
                CertificateType = 0,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                DiagnosisSummary = "",
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
                var res = _service.Create(MedicalCertificate);

                if (!res.IsSuccess || res.Value <= 0)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to create medical certificate.");
                    return false;
                }

                MedicalCertificate.MedicalCertificateId = res.Value;

                BindEntityToUI();
                SetDirty(false);
                CurrentMode = enMode.View;

                OnMedicalCertificateCreated?.Invoke(res.Value);
                return true;
            }
            else
            {
                var res = _service.Update(MedicalCertificate);

                if (!res.IsSuccess)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to update medical certificate.");
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
            if (MedicalCertificateID <= 0)
                return false;

            if (!clsMessage.ConfirmDelete("medical certificate"))
                return false;

            var res = _service.Delete(MedicalCertificateID);
            if (!res.IsSuccess)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Delete failed.");
                return false;
            }

            LoadNew();
            clsMessage.ShowSuccess("Medical certificate deleted successfully.");
            return true;
        }

        // =======================
        // UI CORE
        // =======================
        private void ResetUI()
        {
            ClearErrors();

            lblMedicalCertificateId.Text = "[N/A]";
            lblAppointmentId.Text = "[N/A]";
            lblPatientId.Text = "[N/A]";
            lblDoctorId.Text = "[N/A]";
            cbCertificateType.SelectedIndex = 0;
            dtpStartDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now;
            txtDiagnosticSummary.Text = string.Empty;
            txtNotes.Text = string.Empty;

            ApplyMode();
        }

        private void BindEntityToUI()
        {
            lblMedicalCertificateId.Text = MedicalCertificateID > 0
                ? MedicalCertificateID.ToString()
                : "[N/A]";

            lblAppointmentId.Text = AppointmentID > 0
                ? AppointmentID.ToString()
                : "[N/A]";

            lblPatientId.Text = PatientID > 0
                ? PatientID.ToString()
                : "[N/A]";

            lblDoctorId.Text = DoctorID > 0
                ? DoctorID.ToString()
                : "[N/A]";

            cbCertificateType.SelectedIndex = (byte)(MedicalCertificate.CertificateType-1);
            dtpStartDate.Value = MedicalCertificate.StartDate;
            dtpEndDate.Value = MedicalCertificate.EndDate;
            txtDiagnosticSummary.Text = MedicalCertificate.DiagnosisSummary ?? "";
            txtNotes.Text = MedicalCertificate.Notes ?? "";

            ApplyMode();
        }

        private void MapUIToEntity()
        {
            MedicalCertificate.CertificateType = (byte)(cbCertificateType.SelectedIndex +1);
            MedicalCertificate.StartDate = dtpStartDate.Value;
            MedicalCertificate.EndDate = dtpEndDate.Value;
            MedicalCertificate.DiagnosisSummary = txtDiagnosticSummary.Text.Trim();
            MedicalCertificate.Notes = txtNotes.Text.Trim();
        }

        private void ApplyMode()
        {
            bool editable = (CurrentMode != enMode.View);

            cbCertificateType.Enabled = editable;
            dtpStartDate.Enabled = editable;
            dtpEndDate.Enabled = editable;
            txtDiagnosticSummary.ReadOnly = !editable;
            txtNotes.ReadOnly = !editable;

            btnSave.Enabled = editable;
            linkEdit.Visible = (CurrentMode == enMode.View && MedicalCertificateID > 0);

            lblMedicalCertificateId.Text = MedicalCertificateID > 0
                ? MedicalCertificateID.ToString()
                : "[N/A]";
        }

        // =======================
        // VALIDATION
        // =======================
        private bool ValidateUI()
        {
            ClearErrors();
            bool ok = true;

            if (MedicalCertificate.AppointmentId <= 0)
            {
                SetError(lblAppointmentId, "Appointment is required.");
                ok = false;
            }

            if (dtpEndDate.Value.Date < dtpStartDate.Value.Date)
            {
                SetError(dtpEndDate, "End date must be after start date.");
                ok = false;
            }

            if (!ok)
            {
                if (!string.IsNullOrEmpty(errorProvider1.GetError(lblAppointmentId)))
                    lblAppointmentId.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(dtpEndDate)))
                    dtpEndDate.Focus();
            }

            return ok;
        }

        // =======================
        // DIRTY WIRING
        // =======================
        private void WireDirtyEvents()
        {
            cbCertificateType.SelectedIndexChanged += (_, __) => SetDirty(true);
            dtpStartDate.ValueChanged += (_, __) => SetDirty(true);
            dtpEndDate.ValueChanged += (_, __) => SetDirty(true);
            txtDiagnosticSummary.TextChanged += (_, __) => SetDirty(true);
            txtNotes.TextChanged += (_, __) => SetDirty(true);
        }

        // =======================
        // UI EVENTS
        // =======================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!SaveCurrent())
            {
                clsMessage.ShowError("Medical certificate failed to save.");
                return;
            }

            clsMessage.ShowSuccess("Medical certificate saved successfully.");
        }

        private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentMode = enMode.Edit;
        }
    }

}
