using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.DoctorOverrideSession
{
    using Clinic_Management.DoctorDayOverride;
    using Clinic_Management.Helpers;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities.Entities;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public partial class ucDoctorOverrideSession : UserControl
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
        public int SessionID => Session?.SessionId ?? -1;
        public int OverrideID => Session?.OverrideId ?? -1;

        public DoctorDayOverrideSession Session { get; private set; } = new DoctorDayOverrideSession();

        // =======================
        // EVENTS
        // =======================
        public event Action<int>? OnSessionCreated;
        public event Action<bool>? DirtyChanged;

        // =======================
        // SERVICES
        // =======================
        private readonly DoctorOverrideSessionService _sessionService = new();

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
        public ucDoctorOverrideSession()
        {
            InitializeComponent();

            if (IsDesignTime)
                return;

            LoadSlotMinutes();
            WireDirtyEvents();
            //  LoadNew();
        }

        // =======================
        // PUBLIC API
        // =======================
        public void LoadEntityData(int sessionId, enMode mode = enMode.View)
        {
            ClearErrors();

            if (sessionId <= 0)
            {
                LoadNew();
                return;
            }

            CurrentMode = mode;

            var res = _sessionService.GetById(sessionId);
            if (!res.IsSuccess || res.Value is null)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Session not found.");
                //LoadNew();
                return;
            }

            Session = res.Value;

            BindEntityToUI();
            SetDirty(false);
        }

        public void LoadNewForOverride(int overrideId)
        {
            LoadNew();

            if (overrideId > 0)
            {
                Session.OverrideId = overrideId;
                lblOverrideID.Text = overrideId.ToString();
                SetDirty(true);
            }
        }

        public void LoadNew()
        {
            ClearErrors();

            Session = new DoctorDayOverrideSession
            {
                SessionId = 0,
                OverrideId = 0,
                StartTime = TimeSpan.FromHours(9),
                EndTime = TimeSpan.FromHours(10),
                SlotMinutes = 15

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
                var res = _sessionService.Create(Session);

                if (!res.IsSuccess || res.Value <= 0)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to create session.");
                    return false;
                }

                Session.SessionId = res.Value;

                BindEntityToUI();
                SetDirty(false);
                CurrentMode = enMode.View;

                OnSessionCreated?.Invoke(res.Value);
                return true;
            }
            else
            {
                var res = _sessionService.Update(Session);

                if (!res.IsSuccess)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to update session.");
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
            if (SessionID <= 0)
                return false;

            if (!clsMessage.ConfirmDelete("override session"))
                return false;

            var res = _sessionService.Delete(SessionID);
            if (!res.IsSuccess)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Delete failed.");
                return false;
            }

            LoadNew();
            clsMessage.ShowSuccess("Session deleted successfully.");
            return true;
        }

        // =======================
        // UI CORE
        // =======================
        private void ResetUI()
        {
            lblOverrideID.Text = Session.OverrideId > 0 ? Session.OverrideId.ToString() : "[N/A]";
            lblSessionId.Text = "[N/A]";

            dtpStartTime.Value = DateTime.Today.Add(Session.StartTime);
            dtpEndTime.Value = DateTime.Today.Add(Session.EndTime);

            cbSlotMinutes.SelectedItem = Session.SlotMinutes;

            ApplyMode();
        }

        private void BindEntityToUI()
        {
            lblOverrideID.Text = Session.OverrideId.ToString();
            lblSessionId.Text = Session.SessionId.ToString();

            dtpStartTime.Value = DateTime.Today.Add(Session.StartTime);
            dtpEndTime.Value = DateTime.Today.Add(Session.EndTime);

            cbSlotMinutes.SelectedItem = Session.SlotMinutes;

            ApplyMode();
        }

        private void MapUIToEntity()
        {
            Session.StartTime = dtpStartTime.Value.TimeOfDay;
            Session.EndTime = dtpEndTime.Value.TimeOfDay;
            Session.SlotMinutes = cbSlotMinutes.SelectedItem is int m ? (short)m : (short)15;
        }

        private void ApplyMode()
        {
            bool editable = (CurrentMode != enMode.View);

            dtpStartTime.Enabled = editable;
            dtpEndTime.Enabled = editable;
            cbSlotMinutes.Enabled = editable;

            btnSave.Enabled = editable;
            linkEdit.Visible = (CurrentMode == enMode.View && SessionID > 0);
            linkLabel1.Visible = (CurrentMode == enMode.AddNew);

        }

        // =======================
        // VALIDATION
        // =======================
        private bool ValidateUI()
        {
            ClearErrors();
            bool ok = true;

            if (Session.OverrideId <= 0)
            {
                SetError(lblOverrideID, "Override must be selected first.");
                ok = false;
            }

            var start = dtpStartTime.Value.TimeOfDay;
            var end = dtpEndTime.Value.TimeOfDay;

            if (end <= start)
            {
                SetError(dtpEndTime, "End time must be after start time.");
                ok = false;
            }

            if (!(cbSlotMinutes.SelectedItem is int slot) || slot <= 0)
            {
                SetError(cbSlotMinutes, "Select slot minutes.");
                ok = false;
            }

            return ok;
        }

        // =======================
        // SLOT COMBO
        // =======================
        private void LoadSlotMinutes()
        {
            cbSlotMinutes.Items.Clear();
            cbSlotMinutes.Items.AddRange(new object[] { 10, 15, 20, 30, 45, 60 });
            cbSlotMinutes.SelectedIndex = 1; // default 15
        }

        // =======================
        // DIRTY
        // =======================
        private void WireDirtyEvents()
        {
            dtpStartTime.ValueChanged += (_, __) => SetDirty(true);
            dtpEndTime.ValueChanged += (_, __) => SetDirty(true);
            cbSlotMinutes.SelectedIndexChanged += (_, __) => SetDirty(true);
        }

        // =======================
        // UI EVENTS
        // =======================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!SaveCurrent())
            {
                clsMessage.ShowError("Session failed to save.");
                return;
            }

            clsMessage.ShowSuccess("Session saved successfully.");
        }

        private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentMode = enMode.Edit;
        }

        public void SetOverride(int overrideId)
        {
            if (overrideId <= 0) return;

            Session.OverrideId = overrideId;
            lblOverrideID.Text = overrideId.ToString();

            if (CurrentMode == enMode.View)
                CurrentMode = enMode.Edit;

            SetDirty(true);
        }

        void OnOverrideSelected(int  overrideId)
        {
            SetOverride(overrideId);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDoctorDayOverrideFinder frm = new frmDoctorDayOverrideFinder();
            frm.OnOverrideSelected += OnOverrideSelected;
            frm.OnOverrideSaved += OnOverrideSelected;
            frm.ShowDialog();
        }
    }


}
