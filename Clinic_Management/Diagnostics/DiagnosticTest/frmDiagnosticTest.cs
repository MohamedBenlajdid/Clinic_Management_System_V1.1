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

    public partial class frmDiagnosticTest : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnDiagnosticTestSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int DiagnosticTestID => this.ucDiagnosticTest1.DiagnosticTestID;
        public Clinic_Management_Entities.Entities.DiagnosticTest DiagnosticTest => this.ucDiagnosticTest1.DiagnosticTest;
        public ucDiagnosticTest.enMode Mode => this.ucDiagnosticTest1.CurrentMode;

        // =========================
        // CTORS
        // =========================

        // ➕ Create new diagnostic test
        public frmDiagnosticTest()
        {
            InitializeComponent();

            WireUp();

            this.ucDiagnosticTest1.LoadNew();
        }

        // 👁 / ✏ View or Edit existing diagnostic test
        public frmDiagnosticTest(int diagnosticTestId, ucDiagnosticTest.enMode mode = ucDiagnosticTest.enMode.View)
        {
            InitializeComponent();

            WireUp();

            this.ucDiagnosticTest1.LoadEntityData(diagnosticTestId, mode);
        }

        // Optional (designer support)
        // public frmDiagnosticTest()
        // {
        //     InitializeComponent();
        // }

        // =========================
        // INTERNAL WIRING
        // =========================
        private void WireUp()
        {
            // Forward UC event → Form event
            this.ucDiagnosticTest1.OnDiagnosticTestCreated += RaiseDiagnosticTestSaved;

            // Optional: unsaved changes guard later
            this.FormClosing += FrmDiagnosticTest_FormClosing;
        }

        // =========================
        // EVENT FORWARDER
        // =========================
        private void RaiseDiagnosticTestSaved(int diagnosticTestId)
        {
            // Always trust UC as source of truth
            this.OnDiagnosticTestSaved?.Invoke(this.ucDiagnosticTest1.DiagnosticTestID);

            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        // =========================
        // CLOSE BEHAVIOR (OPTIONAL)
        // =========================
        private void FrmDiagnosticTest_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // if (ucDiagnosticTest1.IsDirty) { ... }
        }
    }
}
