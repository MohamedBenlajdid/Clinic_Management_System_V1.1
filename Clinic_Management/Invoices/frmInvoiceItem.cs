using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Invoices
{
    using Clinic_Management_Entities.Entities;
    using System;
    using System.Windows.Forms;

    public partial class frmInvoiceItem : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnInvoiceItemSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int InvoiceItemID => this.ucInvoiceItem1.InvoiceItemID;
        public int InvoiceID => this.ucInvoiceItem1.InvoiceID;
        public InvoiceItem InvoiceItem => this.ucInvoiceItem1.InvoiceItem;
        public ucInvoiceItem.enMode Mode => this.ucInvoiceItem1.CurrentMode;

        // =========================
        // CTORS
        // =========================

        // ➕ Create new item for invoice
        public frmInvoiceItem(int invoiceId)
        {
            InitializeComponent();

            WireUp();

            this.ucInvoiceItem1.LoadNewForInvoice(invoiceId);
        }

        // 👁 / ✏ View or Edit existing invoice item
        public frmInvoiceItem(int invoiceItemId,
            ucInvoiceItem.enMode mode = ucInvoiceItem.enMode.View)
        {
            InitializeComponent();

            WireUp();

            this.ucInvoiceItem1.LoadEntityData(invoiceItemId, mode);
        }

        // Optional (designer support)
        public frmInvoiceItem()
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
            this.ucInvoiceItem1.OnInvoiceItemCreated += RaiseInvoiceItemSaved;

            // Optional: unsaved changes guard later
            this.FormClosing += FrmInvoiceItem_FormClosing;
        }

        // =========================
        // EVENT FORWARDER
        // =========================
        private void RaiseInvoiceItemSaved(int id)
        {
            // Always trust UC as source of truth
            this.OnInvoiceItemSaved?.Invoke(this.ucInvoiceItem1.InvoiceItemID);

            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        // =========================
        // CLOSE BEHAVIOR (OPTIONAL)
        // =========================
        private void FrmInvoiceItem_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // if (ucInvoiceItem1.IsDirty) { ... }
        }
    }
}
