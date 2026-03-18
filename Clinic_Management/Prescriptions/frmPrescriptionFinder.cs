using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Prescriptions
{
    using Clinic_Management_Entities.Entities;
    using System;
    using System.Windows.Forms;

    public partial class frmPrescriptionFinder : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnPrescriptionSelected;
        public event Action<int>? OnPrescriptionSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int PrescriptionID => this.ucPrescriptionFinder1.PrescriptionID;
        public int AppointmentID => this.ucPrescriptionFinder1.AppointmentID;
        public int PatientID => this.ucPrescriptionFinder1.PatientID;
        public int DoctorID => this.ucPrescriptionFinder1.DoctorID;
        public Prescription Prescription => this.ucPrescriptionFinder1.Prescription;

        // =========================
        // CTOR
        // =========================
        public frmPrescriptionFinder()
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
            this.ucPrescriptionFinder1.OnPrescriptionSelected += id =>
            {
                OnPrescriptionSelected?.Invoke(id);
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            };

            this.ucPrescriptionFinder1.OnPrescriptionSaved += id =>
            {
                OnPrescriptionSaved?.Invoke(id);
                OnPrescriptionSelected?.Invoke(id); // after save, prescription is also selected
            };
        }
    }
}
