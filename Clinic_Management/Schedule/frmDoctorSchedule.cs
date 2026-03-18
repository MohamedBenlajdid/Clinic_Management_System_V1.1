using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Schedule
{
    using Clinic_Management_Entities.Entities;
    using System;
    using System.Windows.Forms;

    public partial class frmDoctorSchedule : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnScheduleSaved;   // scheduleId

        // =========================
        // EXPOSITION
        // =========================
        public int ScheduleID => this.ucDoctorSchedule1.ScheduleID;
        public int DoctorID => this.ucDoctorSchedule1.DoctorID;
        public DoctorSchedule Schedule => this.ucDoctorSchedule1.Schedule;
        public ucDoctorSchedule.enMode Mode => this.ucDoctorSchedule1.CurrentMode;

        // =========================
        // CTORS
        // =========================

        // ➕ Create new Schedule for a Doctor
        public frmDoctorSchedule(int doctorId)
        {
            InitializeComponent();

            WireUp();

            this.ucDoctorSchedule1.LoadNewForDoctor(doctorId);
        }

        // 👁 / ✏ View or Edit existing Schedule
        public frmDoctorSchedule(int scheduleId, ucDoctorSchedule.enMode mode = ucDoctorSchedule.enMode.View)
        {
            InitializeComponent();

            WireUp();

            this.ucDoctorSchedule1.LoadEntityData(scheduleId, mode);
        }

        // Optional (designer support)
        public frmDoctorSchedule()
        {
            InitializeComponent();

            // don’t call WireUp or load methods here (keep designer safe)
            // you can still call WireUp() if you want, but it’s not necessary.
        }

        // =========================
        // INTERNAL WIRING
        // =========================
        private void WireUp()
        {
            // Forward UC event → Form event
            this.ucDoctorSchedule1.OnScheduleCreated += RaiseScheduleSaved;

            // Optional: close behavior later
            this.FormClosing += FrmDoctorSchedule_FormClosing;
        }

        // =========================
        // EVENT FORWARDER
        // =========================
        private void RaiseScheduleSaved(int scheduleId)
        {
            // Always trust UC as source of truth
            this.OnScheduleSaved?.Invoke(this.ucDoctorSchedule1.ScheduleID);

            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        // =========================
        // CLOSE BEHAVIOR (OPTIONAL)
        // =========================
        private void FrmDoctorSchedule_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // if (ucDoctorSchedule1.IsDirty) { ... }
        }
    }

}
