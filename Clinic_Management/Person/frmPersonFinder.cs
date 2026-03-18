using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Person
{
    public partial class frmPersonFinder : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnPersonSelected;
        public event Action<int>? OnPersonSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int PersonID => this.ucPersonFinder1.PersonID;
        public Clinic_Management_Entities.Person Person => this.ucPersonFinder1.Person;

        // =========================
        // CTOR
        // =========================
        public frmPersonFinder()
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
            this.ucPersonFinder1.OnPersonSelected += id =>
            {
                OnPersonSelected?.Invoke(id);
                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            this.ucPersonFinder1.OnPersonSaved += id =>
            {
                OnPersonSaved?.Invoke(id);
                OnPersonSelected?.Invoke(id); // usually after save, it's also selected
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
        }
    }

}
