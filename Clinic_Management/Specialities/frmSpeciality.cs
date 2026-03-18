using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Specialities
{
    using Clinic_Management_Entities;
    using System;
    using System.Windows.Forms;

    public partial class frmSpeciality : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnSpecialitySaved;

        // =========================
        // EXPOSITION
        // =========================
        public int SpecialityID => this.ucSpeciality1.SpecialityID;
        public Specialty Speciality => this.ucSpeciality1.Speciality;
        public ucSpeciality.enMode Mode => this.ucSpeciality1.CurrentMode;

        // =========================
        // CTORS
        // =========================

        // ➕ Create new Speciality
        public frmSpeciality()
        {
            InitializeComponent();

            WireUp();

            this.ucSpeciality1.LoadNew();
        }

        // 👁 / ✏ View or Edit existing Speciality
        public frmSpeciality(int specialityID, ucSpeciality.enMode mode = ucSpeciality.enMode.View)
        {
            InitializeComponent();

            WireUp();

            this.ucSpeciality1.LoadEntityData(specialityID, mode);
        }

        // =========================
        // INTERNAL WIRING
        // =========================
        private void WireUp()
        {
            // Forward UC event → Form event
            this.ucSpeciality1.OnSpecialityCreated += RaiseSpecialitySaved;

            // Optional: close behavior / unsaved guard
            this.FormClosing += FrmSpeciality_FormClosing;
        }

        // =========================
        // EVENT FORWARDER
        // =========================
        private void RaiseSpecialitySaved(int specialityId)
        {
            // Always trust UC as source of truth
            this.OnSpecialitySaved?.Invoke(this.ucSpeciality1.SpecialityID);

            // Optional auto close:
            // this.DialogResult = DialogResult.OK;
            // this.Close();
        }

        // =========================
        // CLOSE BEHAVIOR (OPTIONAL)
        // =========================
        private void FrmSpeciality_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // If needed later: unsaved changes guard
            // if (ucSpeciality1.IsDirty)
            // {
            //     var leave = clsMessage.Confirm("You have unsaved changes. Close anyway?");
            //     if (!leave) e.Cancel = true;
            // }
        }
    }



}
