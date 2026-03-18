using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.DoctorDayOverride
{
    using System;
    using System.Windows.Forms;
    using Clinic_Management_Entities;
    using Clinic_Management_Entities.Entities;

    public partial class frmDoctorDayOverride : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnOverrideSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int OverrideID => this.ucDoctorDayOverride1.OverrideID;
        public int DoctorID => this.ucDoctorDayOverride1.DoctorID;
        public DoctorDayOverride Override => this.ucDoctorDayOverride1.Override;
        public ucDoctorDayOverride.enMode Mode => this.ucDoctorDayOverride1.CurrentMode;

        // =========================
        // CTORS
        // =========================

        // ➕ Create new override for a doctor
        public frmDoctorDayOverride(int doctorId)
        {
            InitializeComponent();

            WireUp();

            this.ucDoctorDayOverride1.LoadNewForDoctor(doctorId);
        }

        // 👁 / ✏ View or Edit existing override
        public frmDoctorDayOverride(int overrideId, ucDoctorDayOverride.enMode mode = ucDoctorDayOverride.enMode.View)
        {
            InitializeComponent();

            WireUp();

            this.ucDoctorDayOverride1.LoadEntityData(overrideId, mode);
        }

        // Optional (designer support)
        public frmDoctorDayOverride()
        {
            InitializeComponent();
            // keep it empty for designer safety
            // (you can call WireUp() here too if you want)
        }

        // =========================
        // INTERNAL WIRING
        // =========================
        private void WireUp()
        {
            // Forward UC event → Form event
            this.ucDoctorDayOverride1.OnOverrideCreated += RaiseOverrideSaved;

            // Optional: unsaved changes guard later
            this.FormClosing += FrmDoctorDayOverride_FormClosing;
        }

        // =========================
        // EVENT FORWARDER
        // =========================
        private void RaiseOverrideSaved(int overrideId)
        {
            // Always trust UC as source of truth
            this.OnOverrideSaved?.Invoke(this.ucDoctorDayOverride1.OverrideID);

            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        // =========================
        // CLOSE BEHAVIOR (OPTIONAL)
        // =========================
        private void FrmDoctorDayOverride_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // if (ucDoctorDayOverride1.IsDirty) { ... }
        }
    }

}
