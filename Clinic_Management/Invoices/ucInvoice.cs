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

    public partial class ucInvoice : UserControl
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
        public int InvoiceID => Invoice?.InvoiceId ?? -1;
        public int PatientID => Invoice?.PatientId ?? -1;
        public int AppointmentID => Invoice?.AppointmentId ?? -1;

        public Invoice Invoice { get; private set; } = new Invoice();

        // =======================
        // EVENTS
        // =======================
        public event Action<int>? OnInvoiceCreated;
        public event Action<bool>? DirtyChanged;

        // =======================
        // SERVICES
        // =======================
        private readonly InvoiceService _service = new();
        private readonly InvoiceItemService _InvoiceItemService = new();


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
        public ucInvoice()
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
            cbStatus.Items.Clear();
            cbStatus.Items.AddRange(new object[]
            {
            "Draft",
            "Issued",
            "Partially Paid",
            "Paid",
            "Cancelled"
            });

            cbStatus.SelectedIndex = 0;
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
                clsMessage.ShowError(res.ErrorMessage ?? "Invoice not found.");
                return;
            }

            Invoice = res.Value;

            BindEntityToUI();
            SetDirty(false);
        }

        public void LoadNewForAppointment(int appointmentId, int patientId)
        {
            LoadNew();

            Invoice.AppointmentId = appointmentId;
            Invoice.PatientId = patientId;

            lblAppointmentId.Text = appointmentId > 0 ? appointmentId.ToString() : "[A/N]";
            lblPatientId.Text = patientId > 0 ? patientId.ToString() : "[A/N]";

            SetDirty(true);
        }

        public void LoadNew()
        {
            ClearErrors();

            Invoice = new Invoice
            {
                InvoiceId = 0,
                InvoiceNumber = "",
                PatientId = 0,
                AppointmentId = 0,
                IssueDate = DateTime.Now,
                DueDate = DateTime.Now,
                SubTotal = 0,
                DiscountAmount = 0,
                TaxAmount = 0,
                TotalAmount = 0,
                PaidAmount = 0,
                RemainingAmount = 0,
                Status = 0,
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

            CalculateTotals();
            MapUIToEntity();

            bool wasAdd = (CurrentMode == enMode.AddNew);

            if (wasAdd)
            {
                var res = _service.Create(Invoice);

                if (!res.IsSuccess || res.Value <= 0)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to create invoice.");
                    return false;
                }

                Invoice.InvoiceId = res.Value;

                BindEntityToUI();
                SetDirty(false);
                CurrentMode = enMode.View;

                OnInvoiceCreated?.Invoke(res.Value);
                return true;
            }
            else
            {
                var res = _service.Update(Invoice);

                if (!res.IsSuccess)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to update invoice.");
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

            lblInvoiceId.Text = "[A/N]";
            txtInvoiceNumber.Text = string.Empty;
            lblPatientId.Text = "[A/N]";
            lblAppointmentId.Text = "[A/N]";
            dtpIssueDate.Value = DateTime.Now;
            dtpDueDate.Value = DateTime.Now;
            nudSubTotal.Value = 0;
            nudDiscountAmount.Value = 0;
            nudTaxAmount.Value = 0;
            nudTotalAmount.Value = 0;
            nudPaidAmount.Value = 0;
            nudRemainingAmount.Value = 0;
            cbStatus.SelectedIndex = 0;
            txtNotes.Text = string.Empty;

            ApplyMode();
        }

        private void BindEntityToUI()
        {
            lblInvoiceId.Text = InvoiceID > 0 ? InvoiceID.ToString() : "[A/N]";
            txtInvoiceNumber.Text = Invoice.InvoiceNumber ?? "";
            lblPatientId.Text = PatientID > 0 ? PatientID.ToString() : "[A/N]";
            lblAppointmentId.Text = AppointmentID > 0 ? AppointmentID.ToString() : "[A/N]";
            dtpIssueDate.Value = Invoice.IssueDate;
            dtpDueDate.Value = (DateTime)Invoice.DueDate;
            nudSubTotal.Value = Invoice.SubTotal;
            nudDiscountAmount.Value = Invoice.DiscountAmount;
            nudTaxAmount.Value = Invoice.TaxAmount;
            nudTotalAmount.Value = Invoice.TotalAmount;
            nudPaidAmount.Value = Invoice.PaidAmount;
            nudRemainingAmount.Value = Invoice.RemainingAmount;
            cbStatus.SelectedIndex = (byte)(Invoice.Status - 1 );
            txtNotes.Text = Invoice.Notes ?? "";

            ApplyMode();
        }

        private void MapUIToEntity()
        {
            Invoice.InvoiceNumber = txtInvoiceNumber.Text.Trim();
            Invoice.IssueDate = dtpIssueDate.Value;
            Invoice.DueDate = dtpDueDate.Value;
            Invoice.SubTotal = nudSubTotal.Value;
            Invoice.DiscountAmount = nudDiscountAmount.Value;
            Invoice.TaxAmount = nudTaxAmount.Value;
            Invoice.TotalAmount = nudTotalAmount.Value;
            Invoice.PaidAmount = nudPaidAmount.Value;
            Invoice.RemainingAmount = nudRemainingAmount.Value;
            Invoice.Status = (byte)(cbStatus.SelectedIndex + 1);
            Invoice.Notes = txtNotes.Text.Trim();
        }

        private void CalculateTotals()
        {
            decimal total = nudSubTotal.Value - nudDiscountAmount.Value + nudTaxAmount.Value;
            nudTotalAmount.Value = total < 0 ? 0 : total;

            decimal remaining = nudTotalAmount.Value - nudPaidAmount.Value;
            nudRemainingAmount.Value = remaining < 0 ? 0 : remaining;
        }

        private void ApplyMode()
        {
            bool editable = (CurrentMode != enMode.View);

            txtInvoiceNumber.ReadOnly = !editable;
            dtpIssueDate.Enabled = editable;
            dtpDueDate.Enabled = editable;
            nudSubTotal.Enabled = editable;
            nudDiscountAmount.Enabled = editable;
            nudTaxAmount.Enabled = editable;
            nudPaidAmount.Enabled = editable;
            cbStatus.Enabled = editable;
            txtNotes.ReadOnly = !editable;

            linkAddInvoiceItem.Visible = CurrentMode == enMode.Edit;
            linkViewInvoiceItems.Visible = CurrentMode != enMode.AddNew;

            btnSave.Enabled = editable;
            linkEdit.Visible = (CurrentMode == enMode.View && InvoiceID > 0);
        }

        // =======================
        // VALIDATION
        // =======================
        private bool ValidateUI()
        {
            ClearErrors();
            bool ok = true;

            if (Invoice.PatientId <= 0)
            {
                SetError(lblPatientId, "Patient is required.");
                ok = false;
            }

            if (string.IsNullOrWhiteSpace(txtInvoiceNumber.Text))
            {
                SetError(txtInvoiceNumber, "Invoice number is required.");
                ok = false;
            }

            if (dtpDueDate.Value.Date < dtpIssueDate.Value.Date)
            {
                SetError(dtpDueDate, "Due date must be after issue date.");
                ok = false;
            }

            return ok;
        }

        // =======================
        // DIRTY WIRING
        // =======================
        private void WireDirtyEvents()
        {
            txtInvoiceNumber.TextChanged += (_, __) => SetDirty(true);
            dtpIssueDate.ValueChanged += (_, __) => SetDirty(true);
            dtpDueDate.ValueChanged += (_, __) => SetDirty(true);
            nudSubTotal.ValueChanged += (_, __) => { CalculateTotals(); SetDirty(true); };
            nudDiscountAmount.ValueChanged += (_, __) => { CalculateTotals(); SetDirty(true); };
            nudTaxAmount.ValueChanged += (_, __) => { CalculateTotals(); SetDirty(true); };
            nudPaidAmount.ValueChanged += (_, __) => { CalculateTotals(); SetDirty(true); };
            cbStatus.SelectedIndexChanged += (_, __) => SetDirty(true);
            txtNotes.TextChanged += (_, __) => SetDirty(true);
        }

        // =======================
        // UI EVENTS
        // =======================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!SaveCurrent())
            {
                clsMessage.ShowError("Invoice failed to save.");
                return;
            }

            clsMessage.ShowSuccess("Invoice saved successfully.");
        }

        private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentMode = enMode.Edit;
        }

        private void linkAddInvoiceItem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInvoiceItem frm = new frmInvoiceItem(this.InvoiceID);
            frm.ShowDialog();
        }

        // --------------------
        // Small helpers
        // --------------------
        private static void HideIfExists(DataGridView dgv, string colName)
        {
            if (dgv.Columns.Contains(colName))
                dgv.Columns[colName].Visible = false;
        }

        private static void RenameIfExists(DataGridView dgv, string colName, string header)
        {
            if (dgv.Columns.Contains(colName))
                dgv.Columns[colName].HeaderText = header;
        }


        private void linkViewInvoiceItems_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
         
            if (InvoiceID <= 0)
            {
                MessageBox.Show("Please select an invoice first.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Call service
            var result = _InvoiceItemService.GetByInvoiceId(InvoiceID);

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.ErrorMessage ?? "Cannot load invoice items.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var items = result.Value?.ToList() ?? new List<InvoiceItem>();

            // Build a lightweight "virtual" form
            using var frm = new Form
            {
                Text = $"Invoice Items (#{InvoiceID})",
                StartPosition = FormStartPosition.CenterParent,
                Width = 900,
                Height = 520,
                MinimizeBox = false,
                MaximizeBox = true,
                ShowInTaskbar = false
            };

            var pnlTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 44,
                Padding = new Padding(10),
            };

            var lbl = new Label
            {
                Dock = DockStyle.Left,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Text = $"Items: {items.Count}",
                Width = 200,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
            };

            var btnClose = new Button
            {
                Text = "Close",
                Dock = DockStyle.Right,
                Width = 100
            };
            btnClose.Click += (_, __) => frm.Close();

            pnlTop.Controls.Add(btnClose);
            pnlTop.Controls.Add(lbl);

            var dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToOrderColumns = true,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Optional styling
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10f, FontStyle.Regular);
            dgv.DefaultCellStyle.SelectionBackColor = Color.Gainsboro;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Bind
            dgv.DataSource = items;

            // Hide / rename columns safely
            HideIfExists(dgv, "InvoiceItemId");
            HideIfExists(dgv, "InvoiceId");

            RenameIfExists(dgv, "ItemName", "Item");
            RenameIfExists(dgv, "Quantity", "Qty");
            RenameIfExists(dgv, "UnitPrice", "Unit Price");
            RenameIfExists(dgv, "TotalPrice", "Total");
            RenameIfExists(dgv, "Notes", "Notes");

            // Add to form
            frm.Controls.Add(dgv);
            frm.Controls.Add(pnlTop);

            // Show dialog
            frm.ShowDialog(this);
        
        }


    }


}
