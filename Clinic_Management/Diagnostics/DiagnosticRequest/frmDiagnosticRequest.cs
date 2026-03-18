using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Diagnostics.DiagnosticRequest
{
    using System;
    using System.Windows.Forms;

    public partial class frmDiagnosticRequest : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnDiagnosticRequestSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int DiagnosticRequestID => this.ucDiagnosticRequest1.DiagnosticRequestID;
        public int AppointmentID => this.ucDiagnosticRequest1.AppointmentID;
        public int PatientID => this.ucDiagnosticRequest1.PatientID;
        public int DoctorID => this.ucDiagnosticRequest1.DoctorID;
        public Clinic_Management_Entities.Entities.DiagnosticRequest DiagnosticRequest => this.ucDiagnosticRequest1.DiagnosticRequest;
        public ucDiagnosticRequest.enMode Mode => this.ucDiagnosticRequest1.CurrentMode;

        // =========================
        // CTORS
        // =========================

        // ➕ Create new diagnostic request for appointment
        public frmDiagnosticRequest(int appointmentId, int patientId, int doctorId)
        {
            InitializeComponent();

            WireUp();

            this.ucDiagnosticRequest1.LoadNewForAppointment(appointmentId, patientId, doctorId);
        }

        // 👁 / ✏ View or Edit existing diagnostic request
        public frmDiagnosticRequest(int diagnosticRequestId, ucDiagnosticRequest.enMode mode = ucDiagnosticRequest.enMode.View)
        {
            InitializeComponent();

            WireUp();

            this.ucDiagnosticRequest1.LoadEntityData(diagnosticRequestId, mode);
        }

        // Optional (designer support)
        public frmDiagnosticRequest()
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
            this.ucDiagnosticRequest1.OnDiagnosticRequestCreated += RaiseDiagnosticRequestSaved;

            // Optional: unsaved changes guard later
            this.FormClosing += FrmDiagnosticRequest_FormClosing;
        }

        // =========================
        // EVENT FORWARDER
        // =========================
        private void RaiseDiagnosticRequestSaved(int diagnosticRequestId)
        {
            // Always trust UC as source of truth
            this.OnDiagnosticRequestSaved?.Invoke(this.ucDiagnosticRequest1.DiagnosticRequestID);

            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        // =========================
        // CLOSE BEHAVIOR (OPTIONAL)
        // =========================
        private void FrmDiagnosticRequest_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // if (ucDiagnosticRequest1.IsDirty) { ... }
        }
    }
}
