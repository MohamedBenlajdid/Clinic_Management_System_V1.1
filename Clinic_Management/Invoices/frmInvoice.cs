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

    public partial class frmInvoice : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnInvoiceSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int InvoiceID => this.ucInvoice1.InvoiceID;
        public int PatientID => this.ucInvoice1.PatientID;
        public int AppointmentID => this.ucInvoice1.AppointmentID;
        public Invoice Invoice => this.ucInvoice1.Invoice;
        public ucInvoice.enMode Mode => this.ucInvoice1.CurrentMode;

        // =========================
        // CTORS
        // =========================

        // ➕ Create new invoice for appointment
        public frmInvoice(int appointmentId, int patientId)
        {
            InitializeComponent();

            WireUp();

            this.ucInvoice1.LoadNewForAppointment(appointmentId, patientId);
        }

        // 👁 / ✏ View or Edit existing invoice
        public frmInvoice(int invoiceId, ucInvoice.enMode mode = ucInvoice.enMode.View)
        {
            InitializeComponent();

            WireUp();

            this.ucInvoice1.LoadEntityData(invoiceId, mode);
        }

        // Optional (designer support)
        public frmInvoice()
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
            this.ucInvoice1.OnInvoiceCreated += RaiseInvoiceSaved;

            // Optional: unsaved changes guard later
            this.FormClosing += FrmInvoice_FormClosing;
        }

        // =========================
        // EVENT FORWARDER
        // =========================
        private void RaiseInvoiceSaved(int invoiceId)
        {
            // Always trust UC as source of truth
            this.OnInvoiceSaved?.Invoke(this.ucInvoice1.InvoiceID);

            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        // =========================
        // CLOSE BEHAVIOR (OPTIONAL)
        // =========================
        private void FrmInvoice_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // if (ucInvoice1.IsDirty) { ... }
        }
    }
}
