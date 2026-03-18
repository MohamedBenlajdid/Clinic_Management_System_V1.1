using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Diagnostics.DiagnosticRequest
{
    using Clinic_Management_Entities.Entities;
    using System;
    using System.Windows.Forms;

    public partial class frmDiagnosticRequestItem : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnDiagnosticRequestItemSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int DiagnosticRequestItemID => this.ucDiagnosticRequestItem1.DiagnosticRequestItemID;
        public int DiagnosticRequestID => this.ucDiagnosticRequestItem1.DiagnosticRequestID;
        public int DiagnosticTestID => this.ucDiagnosticRequestItem1.DiagnosticTestID;
        public DiagnosticRequestItem DiagnosticRequestItem => this.ucDiagnosticRequestItem1.DiagnosticRequestItem;
        public ucDiagnosticRequestItem.enMode Mode => this.ucDiagnosticRequestItem1.CurrentMode;

        // =========================
        // CTORS
        // =========================

        // ➕ Create new item for a diagnostic request
        public frmDiagnosticRequestItem(int diagnosticRequestId)
        {
            InitializeComponent();

            WireUp();

            this.ucDiagnosticRequestItem1.LoadNewForRequest(diagnosticRequestId);
        }

        // 👁 / ✏ View or Edit existing item
        public frmDiagnosticRequestItem(int diagnosticRequestItemId,
            ucDiagnosticRequestItem.enMode mode = ucDiagnosticRequestItem.enMode.View)
        {
            InitializeComponent();

            WireUp();

            this.ucDiagnosticRequestItem1.LoadEntityData(diagnosticRequestItemId, mode);
        }

        // Optional (designer support)
        public frmDiagnosticRequestItem()
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
            this.ucDiagnosticRequestItem1.OnDiagnosticRequestItemCreated += RaiseDiagnosticRequestItemSaved;

            // Optional: unsaved changes guard later
            this.FormClosing += FrmDiagnosticRequestItem_FormClosing;
        }

        // =========================
        // EVENT FORWARDER
        // =========================
        private void RaiseDiagnosticRequestItemSaved(int id)
        {
            // Always trust UC as source of truth
            this.OnDiagnosticRequestItemSaved?.Invoke(this.ucDiagnosticRequestItem1.DiagnosticRequestItemID);

            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        // =========================
        // CLOSE BEHAVIOR (OPTIONAL)
        // =========================
        private void FrmDiagnosticRequestItem_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // if (ucDiagnosticRequestItem1.IsDirty) { ... }
        }
    }


}
