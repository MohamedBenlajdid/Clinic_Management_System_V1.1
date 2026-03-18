using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Medicaments
{
    using Clinic_Management_Entities.Entities;
    using System;
    using System.Windows.Forms;

    public partial class frmMedicament : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnMedicamentSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int MedicamentID => this.ucMedicament1.MedicamentID;
        public Medicament Medicament => this.ucMedicament1.Medicament;
        public ucMedicament.enMode Mode => this.ucMedicament1.CurrentMode;

        // =========================
        // CTORS
        // =========================

        // ➕ Create new medicament
        public frmMedicament()
        {
            InitializeComponent();

            WireUp();

            this.ucMedicament1.LoadNew();
        }

        // 👁 / ✏ View or Edit existing medicament
        public frmMedicament(int medicamentId, ucMedicament.enMode mode = ucMedicament.enMode.View)
        {
            InitializeComponent();

            WireUp();

            this.ucMedicament1.LoadEntityData(medicamentId, mode);
        }

        // Optional (designer support)
        // (If you already used frmMedicament() above for LoadNew,
        //  keep this overload only if you want a "designer-safe empty" ctor.
        //  Otherwise you can remove it.)
        // public frmMedicament()
        // {
        //     InitializeComponent();
        // }

        // =========================
        // INTERNAL WIRING
        // =========================
        private void WireUp()
        {
            // Forward UC event → Form event
            this.ucMedicament1.OnMedicamentCreated += RaiseMedicamentSaved;

            // Optional: unsaved changes guard later
            this.FormClosing += FrmMedicament_FormClosing;
        }

        // =========================
        // EVENT FORWARDER
        // =========================
        private void RaiseMedicamentSaved(int medicamentId)
        {
            // Always trust UC as source of truth
            this.OnMedicamentSaved?.Invoke(this.ucMedicament1.MedicamentID);

            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        // =========================
        // CLOSE BEHAVIOR (OPTIONAL)
        // =========================
        private void FrmMedicament_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // if (ucMedicament1.IsDirty) { ... }
        }
    }



}
