using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Clinic_Management_Entities;

namespace Clinic_Management.DoctorOverrideSession
{
    using Clinic_Management_Entities.Entities;
    using System;
    using System.Windows.Forms;

    public partial class frmDoctorOverrideSessionFinder : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnSessionSelected;
        public event Action<int>? OnSessionSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int SessionID => this.ucDoctorOverrideSessionFinder1.SessionID;
        public int OverrideID => this.ucDoctorOverrideSessionFinder1.OverrideID;
        public DoctorDayOverrideSession Session => this.ucDoctorOverrideSessionFinder1.Session;

        // =========================
        // CTOR
        // =========================
        public frmDoctorOverrideSessionFinder()
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
            this.ucDoctorOverrideSessionFinder1.OnSessionSelected += id =>
            {
                OnSessionSelected?.Invoke(id);
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            };

            this.ucDoctorOverrideSessionFinder1.OnSessionSaved += id =>
            {
                OnSessionSaved?.Invoke(id);
                OnSessionSelected?.Invoke(id); // after save, session is also selected
            };
        }


    }


}




