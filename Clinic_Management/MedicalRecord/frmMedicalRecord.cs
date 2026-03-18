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

    public partial class frmMedicalRecord : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnMedicalRecordSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int MedicalRecordID => this.ucMedicalRecord1.MedicalRecordID;
        public int AppointmentID => this.ucMedicalRecord1.AppointmentID;
        public int PatientID => this.ucMedicalRecord1.PatientID;
        public int DoctorID => this.ucMedicalRecord1.DoctorID;
        public Clinic_Management_Entities.Entities.MedicalRecord MedicalRecord => this.ucMedicalRecord1.MedicalRecord;
        public ucMedicalRecord.enMode Mode => this.ucMedicalRecord1.CurrentMode;

        // =========================
        // CTORS
        // =========================

        // ➕ Create new medical record for appointment
        public frmMedicalRecord(int appointmentId, int patientId, int doctorId)
        {
            InitializeComponent();

            WireUp();

            this.ucMedicalRecord1.LoadNewForAppointment(appointmentId, patientId, doctorId);
        }

        // 👁 / ✏ View or Edit existing medical record
        public frmMedicalRecord(int medicalRecordId, ucMedicalRecord.enMode mode = ucMedicalRecord.enMode.View)
        {
            InitializeComponent();

            WireUp();

            this.ucMedicalRecord1.LoadEntityData(medicalRecordId, mode);
        }

        // Optional (designer support)
        public frmMedicalRecord()
        {
            InitializeComponent();
            // keep empty for designer safety
        }

        // =========================
        // INTERNAL WIRING
        // =========================
        private void WireUp()
        {
            // Forward UC event → Form event
            this.ucMedicalRecord1.OnMedicalRecordCreated += RaiseMedicalRecordSaved;

            // Optional: unsaved changes guard later
            this.FormClosing += FrmMedicalRecord_FormClosing;
        }

        // =========================
        // EVENT FORWARDER
        // =========================
        private void RaiseMedicalRecordSaved(int medicalRecordId)
        {
            // Always trust UC as source of truth
            this.OnMedicalRecordSaved?.Invoke(this.ucMedicalRecord1.MedicalRecordID);

            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        // =========================
        // CLOSE BEHAVIOR (OPTIONAL)
        // =========================
        private void FrmMedicalRecord_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // if (ucMedicalRecord1.IsDirty) { ... }
        }
    }



}
