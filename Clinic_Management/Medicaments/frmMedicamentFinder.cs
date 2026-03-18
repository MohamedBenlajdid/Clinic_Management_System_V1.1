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

    public partial class frmMedicamentFinder : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnMedicamentSelected;
        public event Action<int>? OnMedicamentSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int MedicamentID => this.ucMedicamentFinder1.MedicamentID;
        public Medicament Medicament => this.ucMedicamentFinder1.Medicament;

        // =========================
        // CTOR
        // =========================
        public frmMedicamentFinder()
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
            this.ucMedicamentFinder1.OnMedicamentSelected += id =>
            {
                OnMedicamentSelected?.Invoke(id);
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            };

            this.ucMedicamentFinder1.OnMedicamentSaved += id =>
            {
                OnMedicamentSaved?.Invoke(id);
                OnMedicamentSelected?.Invoke(id); // after save, medicament is also selected
            };
        }
    }



}
