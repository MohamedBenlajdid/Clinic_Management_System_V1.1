using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.MedicalRecord
{
    using System;
    using System.Windows.Forms;

    public partial class frmMedicalRecordFinder : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnMedicalRecordSelected;
        public event Action<int>? OnMedicalRecordSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int MedicalRecordID => this.ucMedicalRecordFinder1.MedicalRecordID;
        public int AppointmentID => this.ucMedicalRecordFinder1.AppointmentID;
        public int PatientID => this.ucMedicalRecordFinder1.PatientID;
        public int DoctorID => this.ucMedicalRecordFinder1.DoctorID;
        public Clinic_Management_Entities.Entities.MedicalRecord MedicalRecord => this.ucMedicalRecordFinder1.MedicalRecord;

        // =========================
        // CTOR
        // =========================
        public frmMedicalRecordFinder()
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
            this.ucMedicalRecordFinder1.OnMedicalRecordSelected += id =>
            {
                OnMedicalRecordSelected?.Invoke(id);
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            };

            this.ucMedicalRecordFinder1.OnMedicalRecordSaved += id =>
            {
                OnMedicalRecordSaved?.Invoke(id);
                OnMedicalRecordSelected?.Invoke(id); // after save, record is also selected
            };
        }
    }


}
