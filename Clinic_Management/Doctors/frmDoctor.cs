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
    public partial class frmDoctor : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnDoctorSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int DoctorID => this.ucDoctor1.StaffID;
        public int PersonID => this.ucDoctor1.PersonID;
        public Doctor Doctor => this.ucDoctor1.Doctor;
        public ucDoctor.enMode Mode => this.ucDoctor1.CurrentMode;

        // =========================
        // CTORS
        // =========================

        // ➕ Create new Doctor for a Person (PersonID = passport)
        public frmDoctor(int personID)
        {
            InitializeComponent();

            WireUp();

            this.ucDoctor1.LoadNewForPerson(personID); 
            
        }

        // 👁 / ✏ View or Edit existing Doctor
        public frmDoctor(int doctorID, ucDoctor.enMode mode = ucDoctor.enMode.View)
        {
            InitializeComponent();

            WireUp();

            this.ucDoctor1.LoadEntityData(doctorID, mode);
        }

        // =========================
        // INTERNAL WIRING
        // =========================
        private void WireUp()
        {
            // Forward UC event → Form event
            this.ucDoctor1.OnDoctorCreated += RaiseDoctorSaved;

            // Optional: close after save (same psychology as frmPerson)
            this.FormClosing += FrmDoctor_FormClosing;
        }

        // =========================
        // EVENT FORWARDER
        // =========================
        private void RaiseDoctorSaved(int doctorId)
        {
            // Always trust UC as source of truth
            this.OnDoctorSaved?.Invoke(this.ucDoctor1.StaffID);

            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        // =========================
        // CLOSE BEHAVIOR (OPTIONAL)
        // =========================
        private void FrmDoctor_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // If needed later: unsaved changes guard
            // if (ucDoctor1.IsDirty) { ... }
        }
    }


}
