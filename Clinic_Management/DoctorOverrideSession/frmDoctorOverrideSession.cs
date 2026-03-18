using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.DoctorOverrideSession
{
    using System;
    using System.Windows.Forms;

    public partial class frmDoctorOverrideSession : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnSessionSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int SessionID => this.ucDoctorOverrideSession1.SessionID;
        public int OverrideID => this.ucDoctorOverrideSession1.OverrideID;
        public Clinic_Management_Entities.Entities.DoctorDayOverrideSession Session => this.ucDoctorOverrideSession1.Session;
        public ucDoctorOverrideSession.enMode Mode => this.ucDoctorOverrideSession1.CurrentMode;

        // =========================
        // CTORS
        // =========================

        // ➕ Create new session for an override
        public frmDoctorOverrideSession(int overrideId)
        {
            InitializeComponent();

            WireUp();

            this.ucDoctorOverrideSession1.LoadNewForOverride(overrideId);
        }

        // 👁 / ✏ View or Edit existing session
        public frmDoctorOverrideSession(int sessionId, ucDoctorOverrideSession.enMode mode = ucDoctorOverrideSession.enMode.View)
        {
            InitializeComponent();

            WireUp();

            this.ucDoctorOverrideSession1.LoadEntityData(sessionId, mode);
        }

        // Optional (designer support)
        public frmDoctorOverrideSession()
        {
            InitializeComponent();
            WireUp();
            this.ucDoctorOverrideSession1.CurrentMode = ucDoctorOverrideSession.enMode.AddNew;
        }

        // =========================
        // INTERNAL WIRING
        // =========================
        private void WireUp()
        {
            // Forward UC event → Form event
            this.ucDoctorOverrideSession1.OnSessionCreated += RaiseSessionSaved;

            // Optional: unsaved changes guard later
            this.FormClosing += FrmDoctorOverrideSession_FormClosing;
        }

        // =========================
        // EVENT FORWARDER
        // =========================
        private void RaiseSessionSaved(int sessionId)
        {
            // Always trust UC as source of truth
            this.OnSessionSaved?.Invoke(this.ucDoctorOverrideSession1.SessionID);

            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        // =========================
        // CLOSE BEHAVIOR (OPTIONAL)
        // =========================
        private void FrmDoctorOverrideSession_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // if (ucDoctorOverrideSession1.IsDirty) { ... }
        }
    }

}
