using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Payment
{
    using Clinic_Management.Helpers;
    using Clinic_Management_BLL.Service;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public partial class ucPayment : UserControl
    {
        private void ClearErrors() => errorProvider1.Clear();
        private void SetError(Control ctrl, string message) => errorProvider1.SetError(ctrl, message);

        // =======================
        // MODE
        // =======================
        public enum enMode { AddNew, View, Edit }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public enMode CurrentMode
        {
            get => _mode;
            set { _mode = value; ApplyMode(); }
        }
        private enMode _mode = enMode.AddNew;

        // =======================
        // EXPOSITION
        // =======================
        public int PaymentID => Payment?.PaymentId ?? -1;
        public int InvoiceID => Payment?.InvoiceId ?? -1;

        public Clinic_Management_Entities.Entities.Payment Payment { get; private set; } = 
            new Clinic_Management_Entities.Entities.Payment();

        // =======================
        // EVENTS
        // =======================
        public event Action<int>? OnPaymentCreated;
        public event Action<bool>? DirtyChanged;

        // =======================
        // SERVICES
        // =======================
        private readonly PaymentService _service = new();

        // =======================
        // DIRTY
        // =======================
        private bool _isDirty;
        public bool IsDirty => _isDirty;

        private void SetDirty(bool dirty)
        {
            if (_isDirty == dirty) return;
            _isDirty = dirty;
            DirtyChanged?.Invoke(dirty);
        }

        // =======================
        // DESIGN TIME GUARD
        // =======================
        private static bool IsDesignTime =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime;

        // =======================
        // CTOR
        // =======================
        public ucPayment()
        {
            InitializeComponent();

            if (IsDesignTime)
                return;

            InitCombos();
            WireDirtyEvents();
            //LoadNew();
        }

        // =======================
        // INIT
        // =======================
        private void InitCombos()
        {
            cbPaymentMethodId.Items.Clear();
            cbPaymentMethodId.Items.AddRange(new object[]
            {
            "Cash",
            "Card",
            "Bank Transfer",
            "Online"
            });

            cbPaymentMethodId.SelectedIndex = 0;
        }

        // =======================
        // PUBLIC API
        // =======================
        public void LoadEntityData(int id, enMode mode = enMode.View)
        {
            ClearErrors();

            if (id <= 0)
            {
                LoadNew();
                return;
            }

            CurrentMode = mode;

            var res = _service.GetById(id);
            if (!res.IsSuccess || res.Value is null)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Payment not found.");
                return;
            }

            Payment = res.Value;

            BindEntityToUI();
            SetDirty(false);
        }

        public void LoadNewForInvoice(int invoiceId)
        {
            LoadNew();

            Payment.InvoiceId = invoiceId;
            lblInvoiceId.Text = invoiceId > 0 ? invoiceId.ToString() : "[N/A]";

            SetDirty(true);
        }

        public void LoadNew()
        {
            ClearErrors();

            Payment = new Clinic_Management_Entities.Entities.Payment
            {
                PaymentId = 0,
                InvoiceId = 0,
                PaymentMethodId = 0,
                Amount = 0,
                PaymentDate = DateTime.Now,
                TransactionReference = "",
                Notes = ""
            };

            ResetUI();
            CurrentMode = enMode.AddNew;
            SetDirty(false);
        }

        public bool SaveCurrent()
        {
            if (!ValidateUI())
                return false;

            MapUIToEntity();

            bool wasAdd = (CurrentMode == enMode.AddNew);

            if (wasAdd)
            {
                var res = _service.Create(Payment);

                if (!res.IsSuccess || res.Value <= 0)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to create payment.");
                    return false;
                }

                Payment.PaymentId = res.Value;

                BindEntityToUI();
                SetDirty(false);
                CurrentMode = enMode.View;

                OnPaymentCreated?.Invoke(res.Value);
                return true;
            }
            else
            {
                var res = _service.Update(Payment);

                if (!res.IsSuccess)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to update payment.");
                    return false;
                }

                BindEntityToUI();
                SetDirty(false);
                CurrentMode = enMode.View;
                return true;
            }
        }

        public bool DeleteCurrent()
        {
            if (PaymentID <= 0)
                return false;

            if (!clsMessage.ConfirmDelete("payment"))
                return false;

            var res = _service.Delete(PaymentID);
            if (!res.IsSuccess)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Delete failed.");
                return false;
            }

            LoadNew();
            clsMessage.ShowSuccess("Payment deleted successfully.");
            return true;
        }

        // =======================
        // UI CORE
        // =======================
        private void ResetUI()
        {
            ClearErrors();

            lblPaymentId.Text = "[N/A]";
            lblInvoiceId.Text = "[N/A]";
            cbPaymentMethodId.SelectedIndex = 0;
            nudAmount.Value = 0;
            dtpPaymentDate.Value = DateTime.Now;
            txtTransactionReference.Text = string.Empty;
            txtNotes.Text = string.Empty;

            ApplyMode();
        }

        private void BindEntityToUI()
        {
            lblPaymentId.Text = PaymentID > 0 ? PaymentID.ToString() : "[N/A]";
            lblInvoiceId.Text = InvoiceID > 0 ? InvoiceID.ToString() : "[N/A]";

            cbPaymentMethodId.SelectedIndex = Payment.PaymentMethodId;
            nudAmount.Value = Payment.Amount;
            dtpPaymentDate.Value = Payment.PaymentDate;
            txtTransactionReference.Text = Payment.TransactionReference ?? "";
            txtNotes.Text = Payment.Notes ?? "";

            ApplyMode();
        }

        private void MapUIToEntity()
        {
            Payment.PaymentMethodId = (byte) cbPaymentMethodId.SelectedIndex;
            Payment.Amount = nudAmount.Value;
            Payment.PaymentDate = dtpPaymentDate.Value;
            Payment.TransactionReference = txtTransactionReference.Text.Trim();
            Payment.Notes = txtNotes.Text.Trim();
        }

        private void ApplyMode()
        {
            bool editable = (CurrentMode != enMode.View);

            cbPaymentMethodId.Enabled = editable;
            nudAmount.Enabled = editable;
            dtpPaymentDate.Enabled = editable;
            txtTransactionReference.ReadOnly = !editable;
            txtNotes.ReadOnly = !editable;

            btnSave.Enabled = editable;
            linkEdit.Visible = (CurrentMode == enMode.View && PaymentID > 0);

            lblPaymentId.Text = PaymentID > 0 ? PaymentID.ToString() : "[N/A]";
        }

        // =======================
        // VALIDATION
        // =======================
        private bool ValidateUI()
        {
            ClearErrors();
            bool ok = true;

            if (Payment.InvoiceId <= 0)
            {
                SetError(lblInvoiceId, "Invoice is required.");
                ok = false;
            }

            if (nudAmount.Value <= 0)
            {
                SetError(nudAmount, "Amount must be greater than zero.");
                ok = false;
            }

            if (!ok)
            {
                if (!string.IsNullOrEmpty(errorProvider1.GetError(lblInvoiceId)))
                    lblInvoiceId.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(nudAmount)))
                    nudAmount.Focus();
            }

            return ok;
        }

        // =======================
        // DIRTY WIRING
        // =======================
        private void WireDirtyEvents()
        {
            cbPaymentMethodId.SelectedIndexChanged += (_, __) => SetDirty(true);
            nudAmount.ValueChanged += (_, __) => SetDirty(true);
            dtpPaymentDate.ValueChanged += (_, __) => SetDirty(true);
            txtTransactionReference.TextChanged += (_, __) => SetDirty(true);
            txtNotes.TextChanged += (_, __) => SetDirty(true);
        }

        // =======================
        // UI EVENTS
        // =======================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!SaveCurrent())
            {
                clsMessage.ShowError("Payment failed to save.");
                return;
            }

            clsMessage.ShowSuccess("Payment saved successfully.");
        }

        private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentMode = enMode.Edit;
        }
    }
}
