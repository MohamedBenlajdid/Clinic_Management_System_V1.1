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

    public partial class frmPatient : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnPatientSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int PatientID => this.ucPatient1.PatientID;
        public int PersonID => this.ucPatient1.PersonID;
        public Patient Patient => this.ucPatient1.Patient;
        public ucPatient.enMode Mode => this.ucPatient1.CurrentMode;

        // =========================
        // CTORS
        // =========================

        // ➕ Create new Patient for a Person (PersonID = passport)
        public frmPatient(int personID)
        {
            InitializeComponent();

            WireUp();

            this.ucPatient1.LoadNew(personID);
        }

        // 👁 / ✏ View or Edit existing Patient
        public frmPatient(int patientID, ucPatient.enMode mode = ucPatient.enMode.View)
        {
            InitializeComponent();

            WireUp();

            this.ucPatient1.LoadEntityData(patientID, mode);
        }

        // =========================
        // INTERNAL WIRING
        // =========================
        private void WireUp()
        {
            // Forward UC event → Form event
            this.ucPatient1.OnPatientCreated += RaisePatientSaved;

            // Optional: close behavior / unsaved guard
            this.FormClosing += FrmPatient_FormClosing;
        }

        // =========================
        // EVENT FORWARDER
        // =========================
        private void RaisePatientSaved(int patientId)
        {
            // Always trust UC as source of truth
            this.OnPatientSaved?.Invoke(this.ucPatient1.PatientID);

            // Optional auto close:
            // this.DialogResult = DialogResult.OK;
            // this.Close();
        }

        // =========================
        // CLOSE BEHAVIOR (OPTIONAL)
        // =========================
        private void FrmPatient_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // If needed later: unsaved changes guard
            // if (ucPatient1.IsDirty)
            // {
            //     var leave = clsMessage.Confirm("You have unsaved changes. Close anyway?");
            //     if (!leave) e.Cancel = true;
            // }
        }
    }

}
