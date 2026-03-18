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

    public partial class frmPrescriptionItem : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnPrescriptionItemSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int PrescriptionItemID => this.ucPrescriptionItem1.PrescriptionItemID;
        public int PrescriptionID => this.ucPrescriptionItem1.PrescriptionID;
        public int MedicamentID => this.ucPrescriptionItem1.MedicamentID;
        public PrescriptionItem PrescriptionItem => this.ucPrescriptionItem1.PrescriptionItem;
        public ucPrescriptionItem.enMode Mode => this.ucPrescriptionItem1.CurrentMode;

        // =========================
        // CTORS
        // =========================

        // ➕ Create new item for a prescription
        public frmPrescriptionItem(int prescriptionId)
        {
            InitializeComponent();

            WireUp();
            
            this.ucPrescriptionItem1.LoadNewForPrescription(prescriptionId);
        }

        // 👁 / ✏ View or Edit existing item
        public frmPrescriptionItem(int prescriptionItemId, ucPrescriptionItem.enMode mode = ucPrescriptionItem.enMode.View)
        {
            InitializeComponent();

            WireUp();

            this.ucPrescriptionItem1.LoadEntityData(prescriptionItemId, mode);
        }

        // Optional (designer support)
        public frmPrescriptionItem()
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
            this.ucPrescriptionItem1.OnPrescriptionItemCreated += RaisePrescriptionItemSaved;

            // Optional: unsaved changes guard later
            this.FormClosing += FrmPrescriptionItem_FormClosing;
        }

        // =========================
        // EVENT FORWARDER
        // =========================
        private void RaisePrescriptionItemSaved(int prescriptionItemId)
        {
            // Always trust UC as source of truth
            this.OnPrescriptionItemSaved?.Invoke(this.ucPrescriptionItem1.PrescriptionItemID);

            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        // =========================
        // CLOSE BEHAVIOR (OPTIONAL)
        // =========================
        private void FrmPrescriptionItem_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // if (ucPrescriptionItem1.IsDirty) { ... }
        }
    }



}
