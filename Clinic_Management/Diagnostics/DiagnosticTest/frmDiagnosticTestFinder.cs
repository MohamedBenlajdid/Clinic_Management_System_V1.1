using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Diagnostics.DiagnosticTest
{
    using System;
    using System.Windows.Forms;

    public partial class frmDiagnosticTestFinder : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnDiagnosticTestSelected;
        public event Action<int>? OnDiagnosticTestSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int DiagnosticTestID => this.ucDiagnosticTestFinder1.DiagnosticTestID;
        public Clinic_Management_Entities.Entities.DiagnosticTest DiagnosticTest => this.ucDiagnosticTestFinder1.DiagnosticTest;

        // =========================
        // CTOR
        // =========================
        public frmDiagnosticTestFinder()
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
            this.ucDiagnosticTestFinder1.OnDiagnosticTestSelected += id =>
            {
                OnDiagnosticTestSelected?.Invoke(id);
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            };

            this.ucDiagnosticTestFinder1.OnDiagnosticTestSaved += id =>
            {
                OnDiagnosticTestSaved?.Invoke(id);
                OnDiagnosticTestSelected?.Invoke(id); // after save, test is also selected
            };
        }
    }
}
