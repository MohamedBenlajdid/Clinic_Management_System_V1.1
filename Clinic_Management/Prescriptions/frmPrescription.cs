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

    public partial class frmPrescription : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnPrescriptionSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int PrescriptionID => this.ucPrescription1.PrescriptionID;
        public int AppointmentID => this.ucPrescription1.AppointmentID;
        public int PatientID => this.ucPrescription1.PatientID;
        public int DoctorID => this.ucPrescription1.DoctorID;
        public Prescription Prescription => this.ucPrescription1.Prescription;
        public ucPrescription.enMode Mode => this.ucPrescription1.CurrentMode;

        // =========================
        // CTORS
        // =========================

        // ➕ Create new prescription for appointment
        public frmPrescription(int appointmentId, int patientId, int doctorId)
        {
            InitializeComponent();

            WireUp();

            this.ucPrescription1.LoadNewForAppointment(appointmentId, patientId, doctorId);
        }

        // 👁 / ✏ View or Edit existing prescription
        public frmPrescription(int prescriptionId, ucPrescription.enMode mode = ucPrescription.enMode.View)
        {
            InitializeComponent();

            WireUp();

            this.ucPrescription1.LoadEntityData(prescriptionId, mode);
        }

        // Optional (designer support)
        public frmPrescription()
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
            this.ucPrescription1.OnPrescriptionCreated += RaisePrescriptionSaved;

            // Optional: unsaved changes guard later
            this.FormClosing += FrmPrescription_FormClosing;
        }

        // =========================
        // EVENT FORWARDER
        // =========================
        private void RaisePrescriptionSaved(int prescriptionId)
        {
            // Always trust UC as source of truth
            this.OnPrescriptionSaved?.Invoke(this.ucPrescription1.PrescriptionID);

            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        // =========================
        // CLOSE BEHAVIOR (OPTIONAL)
        // =========================
        private void FrmPrescription_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // if (ucPrescription1.IsDirty) { ... }
        }
    }
}
