using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Payment
{
    using System;
    using System.Windows.Forms;

    public partial class frmPayment : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnPaymentSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int PaymentID => this.ucPayment1.PaymentID;
        public int InvoiceID => this.ucPayment1.InvoiceID;
        public Clinic_Management_Entities.Entities.Payment Payment => this.ucPayment1.Payment;
        public ucPayment.enMode Mode => this.ucPayment1.CurrentMode;

        // =========================
        // CTORS
        // =========================

        // ➕ Create new payment for invoice
        public frmPayment(int invoiceId)
        {
            InitializeComponent();

            WireUp();

            this.ucPayment1.LoadNewForInvoice(invoiceId);
        }

        // 👁 / ✏ View or Edit existing payment
        public frmPayment(int paymentId, ucPayment.enMode mode = ucPayment.enMode.View)
        {
            InitializeComponent();

            WireUp();

            this.ucPayment1.LoadEntityData(paymentId, mode);
        }

        // Optional (designer support)
        public frmPayment()
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
            this.ucPayment1.OnPaymentCreated += RaisePaymentSaved;

            // Optional: unsaved changes guard later
            this.FormClosing += FrmPayment_FormClosing;
        }

        // =========================
        // EVENT FORWARDER
        // =========================
        private void RaisePaymentSaved(int paymentId)
        {
            // Always trust UC as source of truth
            this.OnPaymentSaved?.Invoke(this.ucPayment1.PaymentID);

            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        // =========================
        // CLOSE BEHAVIOR (OPTIONAL)
        // =========================
        private void FrmPayment_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // if (ucPayment1.IsDirty) { ... }
        }
    }
}
