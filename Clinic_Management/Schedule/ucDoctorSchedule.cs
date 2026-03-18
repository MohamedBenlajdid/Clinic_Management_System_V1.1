using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Schedule
{
    using Clinic_Management.Doctors;
    using Clinic_Management.Helpers;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;

    public partial class ucDoctorSchedule : UserControl
    {
        // =======================
        // ErrorProvider helpers
        // =======================
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
        public int ScheduleID => Schedule?.ScheduleId ?? -1;
        public int DoctorID => Schedule?.DoctorId ?? -1;

        public DoctorSchedule Schedule { get; private set; } = new DoctorSchedule();

        // =======================
        // EVENTS
        // =======================
        public event Action<int>? OnScheduleCreated;   // scheduleId
        public event Action<bool>? DirtyChanged;

        // =======================
        // SERVICES
        // =======================
        private readonly DoctorScheduleService _scheduleService = new();
        private readonly DoctorSchedule _doctorService = new(); // optional: if you want to validate doctor exists

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
        public ucDoctorSchedule()
        {
            InitializeComponent();

            if (IsDesignTime)
                return;

            WireDirtyEvents();
            LoadSlotMinutesCombo();
            // LoadNew();
        }

        // =======================
        // PUBLIC API
        // =======================

        public void LoadEntityData(int scheduleId, enMode mode = enMode.View)
        {
            ClearErrors();

            if (scheduleId <= 0)
            {
                LoadNew();
                return;
            }

            CurrentMode = mode;

            var res = _scheduleService.GetById(scheduleId);
            if (!res.IsSuccess || res.Value is null)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Schedule not found.");
                //LoadNew();
                return;
            }

            Schedule = res.Value;

            BindEntityToUI();
            SetDirty(false);
        }

        public void LoadNewForDoctor(int doctorId)
        {
            LoadNew();

            if (doctorId > 0)
            {
                Schedule.DoctorId = doctorId;
                lblDoctorId.Text = doctorId.ToString();
                SetDirty(true);
            }
        }

        public void LoadNew()
        {
            ClearErrors();

            Schedule = new DoctorSchedule
            {
                ScheduleId = 0,
                DoctorId = 0,
                DayOfWeek = 0,
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(17, 0, 0),
                SlotMinutes = 15,
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
                var res = _scheduleService.Create(Schedule);

                if (!res.IsSuccess || res.Value <= 0)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to create schedule.");
                    return false;
                }

                Schedule.ScheduleId = res.Value;

                BindEntityToUI();
                SetDirty(false);
                CurrentMode = enMode.View;

                OnScheduleCreated?.Invoke(res.Value);
                return true;
            }
            else
            {
                var res = _scheduleService.Update(Schedule);

                if (!res.IsSuccess)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to update schedule.");
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
            if (ScheduleID <= 0)
                return false;

            if (!clsMessage.ConfirmDelete("doctor schedule"))
                return false;

            var res = _scheduleService.Delete(ScheduleID);
            if (!res.IsSuccess)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Delete failed.");
                return false;
            }

            LoadNew();
            clsMessage.ShowSuccess("Schedule deleted successfully.");
            return true;
        }

        // =======================
        // UI CORE
        // =======================

        private void ResetUI()
        {
            ClearErrors();

            lblScheduleId.Text = "[N/A]";
            lblDoctorId.Text = Schedule.DoctorId > 0 ? Schedule.DoctorId.ToString() : "[N/A]";

            nudDayOfWeek.Value = 0;

            // dtpStartTime / dtpEndTime are DateTimePickers (format Time)
            dtpStartTime.Value = DateTime.Today.Add(Schedule.StartTime);
            dtpEndTime.Value = DateTime.Today.Add(Schedule.EndTime);

            cbSlotMinutes.SelectedIndex = 0;
            chkIsActive.Checked = true;

            ApplyMode();
        }

        private void BindEntityToUI()
        {
            lblScheduleId.Text = Schedule.ScheduleId > 0 ? Schedule.ScheduleId.ToString() : "[N/A]";
            lblDoctorId.Text = Schedule.DoctorId > 0 ? Schedule.DoctorId.ToString() : "[N/A]";

            
            nudDayOfWeek.Value = Schedule.DayOfWeek;

            dtpStartTime.Value = DateTime.Today.Add(Schedule.StartTime);
            dtpEndTime.Value = DateTime.Today.Add(Schedule.EndTime);

            cbSlotMinutes.SelectedItem = (int)Schedule.SlotMinutes;
            if (cbSlotMinutes.SelectedIndex < 0)
                cbSlotMinutes.SelectedIndex = 1; // default 15

            chkIsActive.Checked = Schedule.IsActive;

            ApplyMode();
        }

        private void MapUIToEntity()
        {
            Schedule.DayOfWeek = (byte)nudDayOfWeek.Value;
            Schedule.StartTime = dtpStartTime.Value.TimeOfDay;
            Schedule.EndTime = dtpEndTime.Value.TimeOfDay;

            Schedule.SlotMinutes = cbSlotMinutes.SelectedValue is int m ? (short)m : (short)15;
            Schedule.IsActive = chkIsActive.Checked;
        }

        private void ApplyMode()
        {
            bool editable = (CurrentMode != enMode.View);

            nudDayOfWeek.Enabled = editable;
            dtpStartTime.Enabled = editable;
            dtpEndTime.Enabled = editable;
            cbSlotMinutes.Enabled = editable;
            chkIsActive.Enabled = editable;

            btnSave.Enabled = editable;

            linkEdit.Visible = (CurrentMode == enMode.View && ScheduleID > 0);
            linkLabel1.Visible = (CurrentMode == enMode.AddNew);

            lblScheduleId.Text = ScheduleID > 0 ? ScheduleID.ToString() : "[N/A]";
            lblDoctorId.Text = DoctorID > 0 ? DoctorID.ToString() : "[N/A]";
        }

        private void LoadSlotMinutesCombo()
        {
            // simple combo: 10/15/20/30/45/60
            var items = new List<int> { 10, 15, 20, 30, 45, 60 };

            cbSlotMinutes.DataSource = items;
            cbSlotMinutes.SelectedIndex = 1; // 15 default
        }

        // =======================
        // VALIDATION
        // =======================
        private bool ValidateUI()
        {
            ClearErrors();
            bool ok = true;

            if (Schedule.DoctorId <= 0)
            {
                SetError(lblDoctorId, "Please set doctor first.");
                ok = false;
            }

            int day = (int)nudDayOfWeek.Value;
            if (day < 0 || day > 6)
            {
                SetError(nudDayOfWeek, "DayOfWeek must be between 0 and 6.");
                ok = false;
            }

            var start = dtpStartTime.Value.TimeOfDay;
            var end = dtpEndTime.Value.TimeOfDay;

            if (end <= start)
            {
                SetError(dtpEndTime, "End time must be greater than start time.");
                ok = false;
            }

            if (!(cbSlotMinutes.SelectedValue is int slot) || slot <= 0)
            {
                SetError(cbSlotMinutes, "Please select slot minutes.");
                ok = false;
            }
            else
            {
                // Optional: enforce divisibility inside window
                var minutes = (end - start).TotalMinutes;
                if (minutes < slot)
                {
                    SetError(cbSlotMinutes, "Slot minutes is larger than the available time window.");
                    ok = false;
                }
            }

            if (!ok)
            {
                if (!string.IsNullOrEmpty(errorProvider1.GetError(lblDoctorId))) lblDoctorId.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(nudDayOfWeek))) nudDayOfWeek.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(dtpEndTime))) dtpEndTime.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(cbSlotMinutes))) cbSlotMinutes.Focus();
            }

            return ok;
        }

        // =======================
        // DIRTY WIRING
        // =======================
        private void WireDirtyEvents()
        {
            nudDayOfWeek.ValueChanged += (_, __) => SetDirty(true);
            dtpStartTime.ValueChanged += (_, __) => SetDirty(true);
            dtpEndTime.ValueChanged += (_, __) => SetDirty(true);
            cbSlotMinutes.SelectedIndexChanged += (_, __) => SetDirty(true);
            chkIsActive.CheckedChanged += (_, __) => SetDirty(true);
        }

        // =======================
        // UI EVENTS
        // =======================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!SaveCurrent())
            {
                clsMessage.ShowError("Schedule failed to save.");
                return;
            }

            clsMessage.ShowSuccess("Schedule saved successfully.");
        }

        private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentMode = enMode.Edit;
        }

        // helper from outside (like doctor finder)
        public void SetDoctor(int doctorId)
        {
            if (doctorId <= 0) return;

            Schedule.DoctorId = doctorId;
            lblDoctorId.Text = doctorId.ToString();

            if (CurrentMode == enMode.View)
                CurrentMode = enMode.Edit;

            SetDirty(true);
        }

        void OnDoctorSelected(int  doctorId)
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
    }



}
