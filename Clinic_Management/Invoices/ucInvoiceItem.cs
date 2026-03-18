using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Invoices
{
    using Clinic_Management.Helpers;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities.Entities;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public partial class ucInvoiceItem : UserControl
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
        public int InvoiceItemID => InvoiceItem?.InvoiceItemId ?? -1;
        public int InvoiceID => InvoiceItem?.InvoiceId ?? -1;

        public InvoiceItem InvoiceItem { get; private set; } = new InvoiceItem();

        // =======================
        // EVENTS
        // =======================
        public event Action<int>? OnInvoiceItemCreated;
        public event Action<bool>? DirtyChanged;

        // =======================
        // SERVICES
        // =======================
        private readonly InvoiceItemService _service = new();

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
        public ucInvoiceItem()
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
            cbItemType.Items.Clear();
            cbItemType.Items.AddRange(new object[]
            {
            "Service",
            "Medicament",
            "Test",
            "Other"
            });

            cbItemType.SelectedIndex = 0;
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
                clsMessage.ShowError(res.ErrorMessage ?? "Invoice item not found.");
                return;
            }

            InvoiceItem = res.Value;

            BindEntityToUI();
            SetDirty(false);
        }

        public void LoadNewForInvoice(int invoiceId)
        {
            LoadNew();

            InvoiceItem.InvoiceId = invoiceId;
            lblInvoiceId.Text = invoiceId > 0 ? invoiceId.ToString() : "[N/A]";

            SetDirty(true);
        }

        public void LoadNew()
        {
            ClearErrors();

            InvoiceItem = new InvoiceItem
            {
                InvoiceItemId = 0,
                InvoiceId = 0,
                ItemType = 0,
                ReferenceId = null,
                Description = "",
                Quantity = 0,
                UnitPrice = 0,
                Discount = 0,
                Total = 0
            };

            ResetUI();
            CurrentMode = enMode.AddNew;
            SetDirty(false);
        }

        public bool SaveCurrent()
        {
            if (!ValidateUI())
                return false;

            CalculateTotal();
            MapUIToEntity();

            bool wasAdd = (CurrentMode == enMode.AddNew);

            if (wasAdd)
            {
                var res = _service.Create(InvoiceItem);

                if (!res.IsSuccess || res.Value <= 0)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to create invoice item.");
                    return false;
                }

                InvoiceItem.InvoiceItemId = res.Value;

                BindEntityToUI();
                SetDirty(false);
                CurrentMode = enMode.View;

                OnInvoiceItemCreated?.Invoke(res.Value);
                return true;
            }
            else
            {
                var res = _service.Update(InvoiceItem);

                if (!res.IsSuccess)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to update invoice item.");
                    return false;
                }

                BindEntityToUI();
                SetDirty(false);
                CurrentMode = enMode.View;
                return true;
            }
        }

        // =======================
        // UI CORE
        // =======================
        private void ResetUI()
        {
            ClearErrors();

            lblInvoiceItemId.Text = "[N/A]";
            lblInvoiceId.Text = "[N/A]";
            cbItemType.SelectedIndex = 0;
            lblReferenceId.Text = "[N/A]";
            txtDescription.Text = string.Empty;
            nudQuantity.Value = 0;
            nudUnitPrice.Value = 0;
            nudDiscount.Value = 0;
            nudTotal.Value = 0;

            ApplyMode();
        }

        private void BindEntityToUI()
        {
            lblInvoiceItemId.Text = InvoiceItemID > 0 ? InvoiceItemID.ToString() : "[N/A]";
            lblInvoiceId.Text = InvoiceID > 0 ? InvoiceID.ToString() : "[N/A]";
            cbItemType.SelectedIndex =(byte) (InvoiceItem.ItemType -1);
            lblReferenceId.Text = InvoiceItem.ReferenceId?.ToString() ?? "[N/A]";
            txtDescription.Text = InvoiceItem.Description ?? "";
            nudQuantity.Value = InvoiceItem.Quantity;
            nudUnitPrice.Value = InvoiceItem.UnitPrice;
            nudDiscount.Value = InvoiceItem.Discount;
            nudTotal.Value = InvoiceItem.Total;

            ApplyMode();
        }

        private void MapUIToEntity()
        {
            InvoiceItem.ItemType = (byte)(cbItemType.SelectedIndex +1);
            InvoiceItem.Description = txtDescription.Text.Trim();
            InvoiceItem.Quantity = nudQuantity.Value;
            InvoiceItem.UnitPrice = nudUnitPrice.Value;
            InvoiceItem.Discount = nudDiscount.Value;
            InvoiceItem.Total = nudTotal.Value;
        }

        private void CalculateTotal()
        {
            decimal total = (nudQuantity.Value * nudUnitPrice.Value) - nudDiscount.Value;
            nudTotal.Value = total < 0 ? 0 : total;
        }

        private void ApplyMode()
        {
            bool editable = (CurrentMode != enMode.View);

            cbItemType.Enabled = editable;
            txtDescription.ReadOnly = !editable;
            nudQuantity.Enabled = editable;
            nudUnitPrice.Enabled = editable;
            nudDiscount.Enabled = editable;

            btnSave.Enabled = editable;
            linkEdit.Visible = (CurrentMode == enMode.View && InvoiceItemID > 0);
        }

        // =======================
        // VALIDATION
        // =======================
        private bool ValidateUI()
        {
            ClearErrors();
            bool ok = true;

            if (InvoiceItem.InvoiceId <= 0)
            {
                SetError(lblInvoiceId, "Invoice is required.");
                ok = false;
            }

            if (nudQuantity.Value <= 0)
            {
                SetError(nudQuantity, "Quantity must be greater than zero.");
                ok = false;
            }

            if (nudUnitPrice.Value < 0)
            {
                SetError(nudUnitPrice, "Unit price cannot be negative.");
                ok = false;
            }

            return ok;
        }

        // =======================
        // DIRTY WIRING
        // =======================
        private void WireDirtyEvents()
        {
            cbItemType.SelectedIndexChanged += (_, __) => SetDirty(true);
            txtDescription.TextChanged += (_, __) => SetDirty(true);
            nudQuantity.ValueChanged += (_, __) => { CalculateTotal(); SetDirty(true); };
            nudUnitPrice.ValueChanged += (_, __) => { CalculateTotal(); SetDirty(true); };
            nudDiscount.ValueChanged += (_, __) => { CalculateTotal(); SetDirty(true); };
        }

        // =======================
        // UI EVENTS
        // =======================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!SaveCurrent())
            {
                clsMessage.ShowError("Invoice item failed to save.");
                return;
            }

            clsMessage.ShowSuccess("Invoice item saved successfully.");
        }

        private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentMode = enMode.Edit;
        }
    }
}
