using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Appointment
{
    using Clinic_Management.Helpers;
    using Clinic_Management.Patients;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities.Entities;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;

    public partial class ucAppointment : UserControl
    {
        private void ClearErrors() => errorProvider1.Clear();
        private void SetError(Control ctrl, string message) => errorProvider1.SetError(ctrl, message);

        // =========================================================
        // MODE
        // =========================================================
        public enum enMode { AddNew, View, Edit }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public enMode CurrentMode
        {
            get => _mode;
            set { _mode = value; ApplyMode(); }
        }
        private enMode _mode = enMode.AddNew;

        // =========================================================
        // SLOT PROVIDER (Injected From Form)
        // =========================================================
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Func<int, DateTime, IEnumerable<(DateTime Start, DateTime End)>>? SlotProvider { get; set; }


        // =========================================================
        // EXPOSITION
        // =========================================================
        public int AppointmentID => Appointment?.AppointmentId ?? -1;
        public int DoctorID => Appointment?.DoctorId ?? -1;
        public int PatientID => Appointment?.PatientId ?? -1;

        public Appointment Appointment { get; private set; } = new Appointment();

        // =========================================================
        // EVENTS
        // =========================================================
        public event Action<int>? OnAppointmentCreated;
        public event Action<bool>? DirtyChanged;
        public event Action<Appointment>? SaveRequested;

        // =========================================================
        // DIRTY
        // =========================================================
        private bool _isDirty;
        public bool IsDirty => _isDirty;

        private void SetDirty(bool dirty)
        {
            if (_isDirty == dirty) return;
            _isDirty = dirty;
            DirtyChanged?.Invoke(dirty);
        }

        private bool _isLoading;

        // =========================================================
        // CTOR
        // =========================================================
        public ucAppointment()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            InitStatusCombo();
            WireUiEvents();
            WireDirtyEvents();
        }

        // =========================================================
        // INIT
        // =========================================================
        private void InitStatusCombo()
        {
            cbStatus.Items.Clear();
            cbStatus.Items.AddRange(new object[]
            {
            "Scheduled",
            "Completed",
            "NoShow",
            "Cancelled",
            "InProgress"
            });
            cbStatus.SelectedIndex = 0;
        }

        // =========================================================
        // PUBLIC API
        // =========================================================
        public void LoadNew()
        {
            _isLoading = true;

            Appointment = new Appointment
            {
                AppointmentId = 0,
                DoctorId = 0,
                PatientId = 0,
                StartAt = DateTime.Now,
                EndAt = DateTime.Now.AddMinutes(15),
                Status = (byte)enAppointmentStatus.Scheduled,
                Reason = ""
            };

            ResetUI();
            CurrentMode = enMode.AddNew;
            SetDirty(false);

            _isLoading = false;
        }

        public void LoadEntityData(Appointment entity, enMode mode = enMode.View)
        {
            if (entity == null)
            {
                LoadNew();
                return;
            }

            if(entity.Status == (byte)enAppointmentStatus.Cancelled ||
                entity.Status == (byte)enAppointmentStatus.Completed
                )
            {
                this.Enabled = false;
            }

            _isLoading = true;

            Appointment = entity;
            CurrentMode = mode;

            BindEntityToUI();
            SetDirty(false);

            _isLoading = false;
        }

        public void SetDoctorID(int doctorId)
        {
            if (doctorId <= 0) return;

            Appointment.DoctorId = doctorId;
            lblDoctorId.Text = doctorId.ToString();

            LoadAvailableSlots();   // ✅ load slots when doctor selected
            SetDirty(true);
        }

        public void SetPatientID(int patientId)
        {
            if (patientId <= 0) return;

            Appointment.PatientId = patientId;
            lblPatientId.Text = patientId.ToString();

            SetDirty(true);
        }

        public void SetAppointmentId(int id)
        {
            Appointment.AppointmentId = id;
            lblAppointmentId.Text = id.ToString();
        }

        public bool SaveCurrent()
        {
            if (!ValidateUI())
                return false;

            MapUIToEntity();

            SaveRequested?.Invoke(Appointment);

            CurrentMode = enMode.View;
            SetDirty(false);

            if (Appointment.AppointmentId > 0)
                OnAppointmentCreated?.Invoke(Appointment.AppointmentId);

            return true;
        }

        // =========================================================
        // SLOT LOADING
        // =========================================================
        private void LoadAvailableSlots()
        {
            cbAppointmentSlots.Items.Clear();
            cbAppointmentSlots.Items.Add("-- Select Slot --");
            cbAppointmentSlots.SelectedIndex = 0;

            if (Appointment.DoctorId <= 0)
                return;

            if (SlotProvider == null)
                return;

            var date = dtpAppointmentDate.Value.Date;

            var slots = SlotProvider.Invoke(Appointment.DoctorId, date);

            foreach (var slot in slots)
                cbAppointmentSlots.Items.Add(slot);
        }

        // =========================================================
        // UI CORE
        // =========================================================
        private void ResetUI()
        {
            lblAppointmentId.Text = "[N/A]";
            lblDoctorId.Text = "[N/A]";
            lblPatientId.Text = "[N/A]";

            dtpStartAt.Value = Appointment.StartAt;
            dtpEndAt.Value = Appointment.EndAt;

            txtReason.Text = "";
            txtCancelReason.Text = "";

            cbStatus.SelectedIndex = 0;
            cbAppointmentSlots.Items.Clear();
        }

        private void BindEntityToUI()
        {
            lblAppointmentId.Text = Appointment.AppointmentId.ToString();
            lblDoctorId.Text = Appointment.DoctorId.ToString();
            lblPatientId.Text = Appointment.PatientId.ToString();

            dtpStartAt.Value = Appointment.StartAt;
            dtpEndAt.Value = Appointment.EndAt;

            // ⭐ set appointment date from StartAt
            dtpAppointmentDate.Value = Appointment.StartAt.Date;

            txtReason.Text = Appointment.Reason ?? "";
            txtCancelReason.Text = Appointment.CancelReason ?? "";

            cbStatus.SelectedIndex = (int)(Appointment.Status - 1);

            // ⭐ Load slots for this doctor/date
            LoadAvailableSlots();

            // ⭐ Select the correct slot
            SelectAppointmentSlot();
        }


        private void SelectAppointmentSlot()
        {
            for (int i = 0; i < cbAppointmentSlots.Items.Count; i++)
            {
                if (cbAppointmentSlots.Items[i] is ValueTuple<DateTime, DateTime> slot)
                {
                    if (slot.Item1 == Appointment.StartAt &&
                        slot.Item2 == Appointment.EndAt)
                    {
                        cbAppointmentSlots.SelectedIndex = i;
                        return;
                    }
                }
            }
        }

        private void MapUIToEntity()
        {
            Appointment.StartAt = dtpStartAt.Value;
            Appointment.EndAt = dtpEndAt.Value;
            Appointment.Reason = txtReason.Text.Trim();
            Appointment.Status = (byte)(cbStatus.SelectedIndex +1);

            if (Appointment.Status == (byte)enAppointmentStatus.Cancelled)
                Appointment.CancelReason = txtCancelReason.Text.Trim();
            else
                Appointment.CancelReason = null;
        }

        private void ApplyMode()
        {
            bool editable = (CurrentMode != enMode.View);

            dtpAppointmentDate.Enabled = editable;
            cbAppointmentSlots.Enabled = editable;  
            gbCancellingInfos.Enabled = CurrentMode == enMode.Edit;

            dtpEndAt.Enabled = false;
            dtpStartAt.Enabled = false;
            cbStatus.Enabled = false;
            txtCancelReason.Enabled = false;
            linkLabel1.Enabled = CurrentMode == enMode.AddNew;

            ucDoctorSelecter2.Enabled = editable;

            txtReason.ReadOnly = !editable;
            txtCancelReason.ReadOnly = !editable;

            btnSave.Enabled = editable;
            linkEdit.Visible = (CurrentMode == enMode.View && AppointmentID > 0);
        }

        // =========================================================
        // VALIDATION
        // =========================================================
        private bool ValidateUI()
        {
            ClearErrors();
            bool ok = true;

            if (Appointment.DoctorId <= 0)
            {
                SetError(lblDoctorId, "Doctor required.");
                ok = false;
            }

            if (Appointment.PatientId <= 0)
            {
                SetError(lblPatientId, "Patient required.");
                ok = false;
            }

            if (dtpEndAt.Value <= dtpStartAt.Value)
            {
                SetError(dtpEndAt, "Invalid time range.");
                ok = false;
            }

            if (string.IsNullOrWhiteSpace(txtReason.Text))
            {
                SetError(txtReason, "Reason required.");
                ok = false;
            }

            return ok;
        }

        // =========================================================
        // EVENTS
        // =========================================================
        private void WireUiEvents()
        {
            ucDoctorSelecter2.OnDoctorSelected += id =>
            {
                if (_isLoading) return;
                SetDoctorID(id);
            };

            dtpAppointmentDate.ValueChanged += (_, __) =>
            {
                if (_isLoading) return;
                LoadAvailableSlots(); // ✅ reload on date change
            };

            cbAppointmentSlots.SelectedIndexChanged += (_, __) =>
            {
                if (_isLoading) return;

                if (cbAppointmentSlots.SelectedItem is ValueTuple<DateTime, DateTime> slot)
                {
                    dtpStartAt.Value = slot.Item1;
                    dtpEndAt.Value = slot.Item2;
                }
            };

            btnSave.Click += (_, __) =>
            {
                if (!SaveCurrent())
                    clsMessage.ShowError("Failed to save appointment.");
            };

            linkEdit.LinkClicked += (_, __) =>
            {
                CurrentMode = enMode.Edit;
            };
        }

        private void WireDirtyEvents()
        {
            txtReason.TextChanged += (_, __) => { if (!_isLoading) SetDirty(true); };
            txtCancelReason.TextChanged += (_, __) => { if (!_isLoading) SetDirty(true); };
        }

        private void chkIsCancelled_CheckedChanged(object sender, EventArgs e)
        {
            txtCancelReason.Enabled = chkIsCancelled.Checked;
            cbStatus.SelectedIndex = (byte)(enAppointmentStatus.Cancelled - 1);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPatientFinder frm = new frmPatientFinder();
            frm.OnPatientSelected += SetPatientID;
            frm.OnPatientSaved += SetPatientID;
            frm.ShowDialog();
        }
    }

    // =========================================================
    // ENUM
    // =========================================================
    public enum enAppointmentStatus : byte
    {
        Scheduled = 1,
        Completed = 2,
        NoShow = 3,
        Cancelled = 4,
        InProgress = 5
    }



}
