using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Person
{
    public partial class frmPerson : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnPersonSaved;      // forward from uc
        public event Action? OnCancelled;             // optional (form closed without save)

        // =========================
        // EXPOSITION
        // =========================
        public int PersonID => this.ucPerson1.PersonID;
        public Clinic_Management_Entities.Person Person => this.ucPerson1.Person;
        public ucPerson.enMode Mode => this.ucPerson1.CurrentMode;

        // =========================
        // CTORS
        // =========================
        public frmPerson()
        {
            InitializeComponent();

            WireUpDelegation();

            this.ucPerson1.LoadNew();
        }

        public frmPerson(int personID, ucPerson.enMode mode = ucPerson.enMode.View)
        {
            InitializeComponent();

            WireUpDelegation();

            this.ucPerson1.LoadEntityData(personID, mode);
        }

        // =========================
        // WIRING
        // =========================
        private void WireUpDelegation()
        {
            // Mirror psychology: control is the source of truth
            this.ucPerson1.OnPersonCreated += RaiseSaved;

            // If you also want "saved" for edits, add an event in ucPerson like OnPersonSaved
            // and forward it here too:
            // this.ucPerson1.OnPersonSaved += RaiseSaved;

            this.FormClosing += FrmPerson_FormClosing;
        }

        // =========================
        // FORWARD EVENTS
        // =========================
        private void RaiseSaved(int personId)
        {
            // Always expose the actual ID from uc
            this.OnPersonSaved?.Invoke(this.ucPerson1.PersonID);

            
        }

        private void FrmPerson_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // notify cancel if not saved
            if (this.DialogResult != DialogResult.OK)
                this.OnCancelled?.Invoke();
        }
    }


}
