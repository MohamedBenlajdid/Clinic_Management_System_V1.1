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

    public partial class frmDoctorScheduleFinder : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnScheduleSelected;
        public event Action<int>? OnScheduleSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int ScheduleID => this.ucDoctorScheduleFinder1.ScheduleID;
        public int DoctorID => this.ucDoctorScheduleFinder1.DoctorID;
        public DoctorSchedule Schedule => this.ucDoctorScheduleFinder1.Schedule;

        // =========================
        // CTOR
        // =========================
        public frmDoctorScheduleFinder()
        {
            InitializeComponent();
            WireUp();
        }

        // =========================
        // INTERNAL WIRING
        // =========================
        private void WireUp()
        {
            // Forward UserControl events → Form events
            this.ucDoctorScheduleFinder1.OnScheduleSelected += id =>
            {
                OnScheduleSelected?.Invoke(id);
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            };

            this.ucDoctorScheduleFinder1.OnScheduleSaved += id =>
            {
                OnScheduleSaved?.Invoke(id);
                OnScheduleSelected?.Invoke(id); // after save, schedule is also selected
            };
        }
    }




}
