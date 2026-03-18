using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.MedicalCertificate
{
    using System;
    using System.Windows.Forms;

    public partial class frmMedicalCertificate : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnMedicalCertificateSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int MedicalCertificateID => this.ucMedicalCertificate1.MedicalCertificateID;
        public int AppointmentID => this.ucMedicalCertificate1.AppointmentID;
        public int PatientID => this.ucMedicalCertificate1.PatientID;
        public int DoctorID => this.ucMedicalCertificate1.DoctorID;
        public Clinic_Management_Entities.Entities.MedicalCertificate MedicalCertificate => this.ucMedicalCertificate1.MedicalCertificate;
        public ucMedicalCertificate.enMode Mode => this.ucMedicalCertificate1.CurrentMode;

        // =========================
        // CTORS
        // =========================

        // ➕ Create new certificate for appointment
        public frmMedicalCertificate(int appointmentId, int patientId, int doctorId)
        {
            InitializeComponent();

            WireUp();

            this.ucMedicalCertificate1.LoadNewForAppointment(appointmentId, patientId, doctorId);
        }

        // 👁 / ✏ View or Edit existing certificate
        public frmMedicalCertificate(int medicalCertificateId,
            ucMedicalCertificate.enMode mode = ucMedicalCertificate.enMode.View)
        {
            InitializeComponent();

            WireUp();

            this.ucMedicalCertificate1.LoadEntityData(medicalCertificateId, mode);
        }

        // Optional (designer support)
        public frmMedicalCertificate()
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
            this.ucMedicalCertificate1.OnMedicalCertificateCreated += RaiseMedicalCertificateSaved;

            // Optional: unsaved changes guard later
            this.FormClosing += FrmMedicalCertificate_FormClosing;
        }

        // =========================
        // EVENT FORWARDER
        // =========================
        private void RaiseMedicalCertificateSaved(int id)
        {
            // Always trust UC as source of truth
            this.OnMedicalCertificateSaved?.Invoke(this.ucMedicalCertificate1.MedicalCertificateID);

            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        // =========================
        // CLOSE BEHAVIOR (OPTIONAL)
        // =========================
        private void FrmMedicalCertificate_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // if (ucMedicalCertificate1.IsDirty) { ... }
        }
    }
}
