using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Diagnostics.DiagnosticResult
{
    using System;
    using System.Windows.Forms;

    public partial class frmDiagnosticResult : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnDiagnosticResultSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int DiagnosticResultID => this.ucDiagnosticResult1.DiagnosticResultID;
        public int DiagnosticRequestItemID => this.ucDiagnosticResult1.DiagnosticRequestItemID;
        public Clinic_Management_Entities.Entities.DiagnosticResult DiagnosticResult => this.ucDiagnosticResult1.DiagnosticResult;
        public ucDiagnosticResult.enMode Mode => this.ucDiagnosticResult1.CurrentMode;

        // =========================
        // CTORS
        // =========================

        // ➕ Create new result for a diagnostic request item
        public frmDiagnosticResult(int diagnosticRequestItemId)
        {
            InitializeComponent();

            WireUp();

            this.ucDiagnosticResult1.LoadNewForRequestItem(diagnosticRequestItemId);
        }

        // 👁 / ✏ View or Edit existing diagnostic result
        public frmDiagnosticResult(int diagnosticResultId,
            ucDiagnosticResult.enMode mode = ucDiagnosticResult.enMode.View)
        {
            InitializeComponent();

            WireUp();

            this.ucDiagnosticResult1.LoadEntityData(diagnosticResultId, mode);
        }

        // Optional (designer support)
        public frmDiagnosticResult()
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
            this.ucDiagnosticResult1.OnDiagnosticResultCreated += RaiseDiagnosticResultSaved;

            // Optional: unsaved changes guard later
            this.FormClosing += FrmDiagnosticResult_FormClosing;
        }

        // =========================
        // EVENT FORWARDER
        // =========================
        private void RaiseDiagnosticResultSaved(int diagnosticResultId)
        {
            // Always trust UC as source of truth
            this.OnDiagnosticResultSaved?.Invoke(this.ucDiagnosticResult1.DiagnosticResultID);

            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        // =========================
        // CLOSE BEHAVIOR (OPTIONAL)
        // =========================
        private void FrmDiagnosticResult_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // if (ucDiagnosticResult1.IsDirty) { ... }
        }
    }
}
