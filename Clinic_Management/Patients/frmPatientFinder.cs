using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Patients
{
    using Clinic_Management_Entities;
    using System;
    using System.Windows.Forms;

    public partial class frmPatientFinder : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnPatientSelected;
        public event Action<int>? OnPatientSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int PatientID => this.ucPatientFinder1.PatientID;
        public int PersonID => this.ucPatientFinder1.PersonID;
        public Patient Patient => this.ucPatientFinder1.Patient;

        // =========================
        // CTOR
        // =========================
        public frmPatientFinder()
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
            this.ucPatientFinder1.OnPatientSelected += id =>
            {
                OnPatientSelected?.Invoke(id);
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            };

            this.ucPatientFinder1.OnPatientSaved += id =>
            {
                OnPatientSaved?.Invoke(id);
                OnPatientSelected?.Invoke(id); // after save, patient is also selected
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            };
        }
    }

}
