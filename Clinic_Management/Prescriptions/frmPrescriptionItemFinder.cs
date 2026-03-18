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

    public partial class frmPrescriptionItemFinder : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnPrescriptionItemSelected;
        public event Action<int>? OnPrescriptionItemSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int PrescriptionItemID => this.ucPrescriptionItemFinder1.PrescriptionItemID;
        public int PrescriptionID => this.ucPrescriptionItemFinder1.PrescriptionID;
        public int MedicamentID => this.ucPrescriptionItemFinder1.MedicamentID;
        public PrescriptionItem PrescriptionItem => this.ucPrescriptionItemFinder1.PrescriptionItem;

        // =========================
        // CTOR
        // =========================
        public frmPrescriptionItemFinder()
        {
            InitializeComponent();
            WireUp();
        }

        // =========================
        // INTERNAL WIRING
        // =========================
        private void WireUp()
        {
            // Forward UserControl events → Form events
            this.ucPrescriptionItemFinder1.OnPrescriptionItemSelected += id =>
            {
                OnPrescriptionItemSelected?.Invoke(id);
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            };

            this.ucPrescriptionItemFinder1.OnPrescriptionItemSaved += id =>
            {
                OnPrescriptionItemSaved?.Invoke(id);
                OnPrescriptionItemSelected?.Invoke(id); // after save, item is also selected
            };
        }
    }

}
