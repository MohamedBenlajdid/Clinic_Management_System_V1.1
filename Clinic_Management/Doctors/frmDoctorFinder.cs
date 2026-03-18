using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Doctors
{
    public partial class frmDoctorFinder : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnDoctorSelected;
        public event Action<int>? OnDoctorSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int DoctorID => this.ucDoctorFinder1.DoctorID;
        public int PersonID => this.ucDoctorFinder1.PersonID;
        public Doctor Doctor => this.ucDoctorFinder1.Doctor;

        // =========================
        // CTOR
        // =========================
        public frmDoctorFinder()
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
            this.ucDoctorFinder1.OnDoctorSelected += id =>
            {
                OnDoctorSelected?.Invoke(id);
                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            this.ucDoctorFinder1.OnDoctorSaved += id =>
            {
                OnDoctorSaved?.Invoke(id);
                OnDoctorSelected?.Invoke(id); // after save, doctor is also selected
                
            };
        }
    }
}
