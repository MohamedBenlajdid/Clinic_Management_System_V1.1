using Azure.Core;
using Clinic_Management.Diagnostics.DiagnosticResult;
using Clinic_Management.Helpers;
using Clinic_Management_BLL.Service;
using Clinic_Management_Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Diagnostics.DiagnosticRequest
{
    public partial class frmDiagnosticRequestItemsTable : Form
    {
        // =======================
        // SERVICES
        // =======================
        private readonly DiagnosticRequestService _service =
            new DiagnosticRequestService();

        // =======================
        // DATA (bindable)
        // =======================
        private BindingList<DiagnosticRequestItemRow> _rows =
            new BindingList<DiagnosticRequestItemRow>();

        private List<DiagnosticRequestItemRow> _all =
            new List<DiagnosticRequestItemRow>();

        private int _diagnosticRequestId;

        public frmDiagnosticRequestItemsTable(int diagnosticRequestId)
        {
            InitializeComponent();

            _diagnosticRequestId = diagnosticRequestId;

            this.Load += frmDiagnosticRequestItemsTable_Load;

            button1.Click += button1_Click;
            button2.Click += button2_Click;

            cbFilterBy.SelectedIndexChanged += cbFilterBy_SelectedIndexChanged;
            txtFilterValue.TextChanged += txtFilterValue_TextChanged;
        }

        private void frmDiagnosticRequestItemsTable_Load(object? sender, EventArgs e)
        {
            SetupFilterBy();
            SetupGrid();
            RefreshGrid();
        }

        // =======================
        // UI: FILTER SETUP
        // =======================
        private void SetupFilterBy()
        {
            cbFilterBy.DropDownStyle = ComboBoxStyle.DropDownList;

            cbFilterBy.Items.Clear();
            cbFilterBy.Items.AddRange(new object[]
            {
            "All",
            "DiagnosticRequestItemId",
            "Name"
            });

            cbFilterBy.SelectedIndex = 0;

            txtFilterValue.Enabled = false;
            txtFilterValue.Clear();
        }

        private void cbFilterBy_SelectedIndexChanged(object? sender, EventArgs e)
        {
            bool isAll = cbFilterBy.SelectedItem?.ToString() == "All";

            txtFilterValue.Enabled = !isAll;

            if (isAll)
                txtFilterValue.Clear();

            ApplyFilters();
        }

        private void txtFilterValue_TextChanged(object? sender, EventArgs e)
        {
            ApplyFilters();
        }

        // =======================
        // UI: GRID SETUP
        // =======================
        private void SetupGrid()
        {
            dgvDiagnosticRequestItems.AutoGenerateColumns = false;
            dgvDiagnosticRequestItems.AllowUserToAddRows = false;
            dgvDiagnosticRequestItems.AllowUserToDeleteRows = false;
            dgvDiagnosticRequestItems.ReadOnly = true;

            dgvDiagnosticRequestItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDiagnosticRequestItems.MultiSelect = false;
            dgvDiagnosticRequestItems.RowHeadersVisible = false;

            dgvDiagnosticRequestItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvDiagnosticRequestItems.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvDiagnosticRequestItems.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);

            dgvDiagnosticRequestItems.ColumnHeadersHeight = 34;
            dgvDiagnosticRequestItems.RowTemplate.Height = 30;

            dgvDiagnosticRequestItems.Columns.Clear();

            dgvDiagnosticRequestItems.Columns.Add(
                MakeTextCol(nameof(DiagnosticRequestItemRow.DiagnosticRequestItemId), "ID", 80));

            dgvDiagnosticRequestItems.Columns.Add(
                MakeTextCol(nameof(DiagnosticRequestItemRow.Name), "Test Name", 200));

            dgvDiagnosticRequestItems.Columns.Add(
                MakeTextCol(nameof(DiagnosticRequestItemRow.Notes), "Notes", 300));

            dgvDiagnosticRequestItems.DataSource = _rows;
        }

        private DataGridViewTextBoxColumn MakeTextCol(string dataProperty,
            string header, int minWidth, string? format = null)
        {
            var col = new DataGridViewTextBoxColumn
            {
                DataPropertyName = dataProperty,
                HeaderText = header,
                MinimumWidth = minWidth,
                SortMode = DataGridViewColumnSortMode.Automatic
            };

            if (!string.IsNullOrWhiteSpace(format))
                col.DefaultCellStyle.Format = format;

            return col;
        }

        // =======================
        // DATA LOAD
        // =======================
        private void RefreshGrid()
        {
            var result = _service.GetDiagnosticRequestItems(_diagnosticRequestId);

            if (!result.IsSuccess)
            {
                clsMessage.ShowError(result.ErrorMessage);
                return;
            }

            var list = result.Value!.ToList();

            _all = list
                .Select(x => new DiagnosticRequestItemRow(x))
                .OrderBy(r => r.Name)
                .ToList();

            ApplyFilters();
        }

        private void ApplyFilters()
        {
            IEnumerable<DiagnosticRequestItemRow> filtered = _all;

            string filterBy = cbFilterBy.SelectedItem?.ToString() ?? "All";
            string value = (txtFilterValue.Text ?? "").Trim();

            if (filterBy != "All" && !string.IsNullOrWhiteSpace(value))
            {
                switch (filterBy)
                {
                    case "DiagnosticRequestItemId":
                        if (int.TryParse(value, out int id))
                            filtered = filtered.Where(x => x.DiagnosticRequestItemId == id);
                        else
                            filtered = Enumerable.Empty<DiagnosticRequestItemRow>();
                        break;

                    case "Name":
                        filtered = filtered.Where(x =>
                            (x.Name ?? "").IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0);
                        break;
                }
            }

            _rows = new BindingList<DiagnosticRequestItemRow>(filtered.ToList());
            dgvDiagnosticRequestItems.DataSource = _rows;

            UpdateRecordNumber();
        }

        private void UpdateRecordNumber()
        {
            lblRecordNumber.Text = _rows.Count.ToString();
        }

        // =======================
        // BUTTONS
        // =======================
        private void button1_Click(object? sender, EventArgs e)
        {
            using var frm = new frmDiagnosticRequestItem(_diagnosticRequestId);
            frm.ShowDialog();

            RefreshGrid();
        }

        private void button2_Click(object? sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Clear();
            ApplyFilters();
        }

        // =======================
        // VIEWMODEL ROW
        // =======================
        private sealed class DiagnosticRequestItemRow
        {
            public int DiagnosticRequestItemId { get; }
            public int DiagnosticRequestId { get; }
            public int DiagnosticTestId { get; }

            public string? Name { get; }
            public string? Notes { get; }

            public DiagnosticRequestItemRow(DiagnosticRequestItemDetail r)
            {
                DiagnosticRequestItemId = r.DiagnosticRequestItemId;
                DiagnosticRequestId = r.DiagnosticRequestId;
                DiagnosticTestId = r.DiagnosticTestId;

                Name = r.Name;
                Notes = r.Notes;
            }
        }

        // =======================
        // CONTEXT MENU ACTIONS
        // =======================
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dgvDiagnosticRequestItems.CurrentRow == null)
                return;

            var row = dgvDiagnosticRequestItems.CurrentRow.DataBoundItem
                as DiagnosticRequestItemRow;

            if (row == null)
                return;

            frmDiagnosticRequestItem frm =
                new frmDiagnosticRequestItem(row.DiagnosticRequestItemId,
                ucDiagnosticRequestItem.enMode.View);

            frm.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (dgvDiagnosticRequestItems.CurrentRow == null)
                return;

            var row = dgvDiagnosticRequestItems.CurrentRow.DataBoundItem
                as DiagnosticRequestItemRow;

            if (row == null)
                return;

            frmDiagnosticRequestItem frm =
                new frmDiagnosticRequestItem(row.DiagnosticRequestItemId,
                ucDiagnosticRequestItem.enMode.Edit);

            frm.ShowDialog();
        }

        DiagnosticRequestItemService _DiagRequestItemService =
            new DiagnosticRequestItemService();
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

            if (dgvDiagnosticRequestItems.CurrentRow == null)
                return;

            var row = dgvDiagnosticRequestItems.CurrentRow.DataBoundItem
                as DiagnosticRequestItemRow;

            if (row == null)
                return;

            int RequestItemID = row.DiagnosticRequestItemId;

            if (!clsMessage.Confirm("Are You Sure You want to Delete Item ?"))
            {
                return;
            }

            bool Deleted = _DiagRequestItemService.Delete(RequestItemID).IsSuccess;

            if (Deleted)
            {
                clsMessage.ShowSuccess("Deleted Successfuly");
            }


        }


        DiagnosticResultService _DiagnosticResultService = new DiagnosticResultService();
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

            if (dgvDiagnosticRequestItems.CurrentRow == null)
                return;

            var row = dgvDiagnosticRequestItems.CurrentRow.DataBoundItem
                as DiagnosticRequestItemRow;

            if (row == null)
                return;

            int RequestItemID = row.DiagnosticRequestItemId;


            int ExistedResultID =
                _DiagnosticResultService.GetByRequestItemIdSafe(RequestItemID).IsSuccess ?
                _DiagnosticResultService.GetByRequestItemIdSafe(RequestItemID).Value.DiagnosticResultId :
                -1;

            if(ExistedResultID > 0)
            {
                frmDiagnosticResult frmR = new frmDiagnosticResult(ExistedResultID,
                    ucDiagnosticResult.enMode.View);
                frmR.ShowDialog();
                return;
            }


            frmDiagnosticResult frm = new frmDiagnosticResult(RequestItemID);
            frm.ShowDialog();




        }
    }
}
