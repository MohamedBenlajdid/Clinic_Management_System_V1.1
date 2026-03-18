using Clinic_Management.Helpers;
using Clinic_Management_BLL.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Diagnostics.DiagnosticResult
{
    public partial class frmDiagnosticResultsTable : Form
    {
        // ================================
        // SERVICES
        // ================================
        private readonly DiagnosticResultService _service =
            new DiagnosticResultService();

        // ================================
        // DATA
        // ================================
        private BindingList<DiagnosticResultRow> _rows =
            new BindingList<DiagnosticResultRow>();

        private List<DiagnosticResultRow> _all =
            new List<DiagnosticResultRow>();

        public frmDiagnosticResultsTable()
        {
            InitializeComponent();

            this.Load += frmDiagnosticResultsTable_Load;

            cbFilterBy.SelectedIndexChanged += cbFilterBy_SelectedIndexChanged;
            txtFilterValue.TextChanged += txtFilterValue_TextChanged;

            btnRefresh.Click += btnRefresh_Click;
        }

        // ================================
        // FORM LOAD
        // ================================
        private void frmDiagnosticResultsTable_Load(object? sender, EventArgs e)
        {
            SetupFilterBy();
            SetupGrid();
            RefreshGrid();
        }

        // ================================
        // FILTER SETUP
        // ================================
        private void SetupFilterBy()
        {
            cbFilterBy.DropDownStyle = ComboBoxStyle.DropDownList;

            cbFilterBy.Items.Clear();
            cbFilterBy.Items.AddRange(new object[]
            {
            "All",
            "DiagnosticResultId",
            "DiagnosticRequestItemId",
            "Unit"
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

        // ================================
        // GRID SETUP
        // ================================
        private void SetupGrid()
        {
            dgvResults.AutoGenerateColumns = false;
            dgvResults.AllowUserToAddRows = false;
            dgvResults.AllowUserToDeleteRows = false;
            dgvResults.ReadOnly = true;

            dgvResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResults.MultiSelect = false;
            dgvResults.RowHeadersVisible = false;

            dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvResults.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvResults.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);

            dgvResults.ColumnHeadersHeight = 34;
            dgvResults.RowTemplate.Height = 30;

            dgvResults.Columns.Clear();

            dgvResults.Columns.Add(
                MakeTextCol(nameof(DiagnosticResultRow.DiagnosticResultId), "ID", 80));

            dgvResults.Columns.Add(
                MakeTextCol(nameof(DiagnosticResultRow.DiagnosticRequestItemId), "Request Item", 120));

            dgvResults.Columns.Add(
                MakeTextCol(nameof(DiagnosticResultRow.ResultText), "Result Text", 200));

            dgvResults.Columns.Add(
                MakeTextCol(nameof(DiagnosticResultRow.ResultNumeric), "Numeric", 100));

            dgvResults.Columns.Add(
                MakeTextCol(nameof(DiagnosticResultRow.Unit), "Unit", 100));

            dgvResults.Columns.Add(
                MakeTextCol(nameof(DiagnosticResultRow.RefRange), "Ref Range", 120));

            dgvResults.Columns.Add(
                MakeTextCol(nameof(DiagnosticResultRow.PerformedAt), "Performed At", 150));

            dgvResults.Columns.Add(
                MakeTextCol(nameof(DiagnosticResultRow.VerifiedAt), "Verified At", 150));

            dgvResults.DataSource = _rows;
        }

        private DataGridViewTextBoxColumn MakeTextCol(
            string property,
            string header,
            int minWidth,
            string? format = null)
        {
            var col = new DataGridViewTextBoxColumn
            {
                DataPropertyName = property,
                HeaderText = header,
                MinimumWidth = minWidth,
                SortMode = DataGridViewColumnSortMode.Automatic
            };

            if (!string.IsNullOrWhiteSpace(format))
                col.DefaultCellStyle.Format = format;

            return col;
        }

        // ================================
        // LOAD DATA
        // ================================
        private void RefreshGrid()
        {
            var result = _service.GetAllSafe();

            if (!result.IsSuccess)
            {
                clsMessage.ShowError(result.ErrorMessage);
                return;
            }

            var list = result.Value!.ToList();

            _all = list
                .Select(x => new DiagnosticResultRow(x))
                .ToList();

            ApplyFilters();
        }

        // ================================
        // FILTER LOGIC
        // ================================
        private void ApplyFilters()
        {
            IEnumerable<DiagnosticResultRow> filtered = _all;

            string filterBy = cbFilterBy.SelectedItem?.ToString() ?? "All";
            string value = (txtFilterValue.Text ?? "").Trim();

            if (filterBy != "All" && !string.IsNullOrWhiteSpace(value))
            {
                switch (filterBy)
                {
                    case "DiagnosticResultId":
                        if (int.TryParse(value, out int id))
                            filtered = filtered.Where(x => x.DiagnosticResultId == id);
                        else
                            filtered = Enumerable.Empty<DiagnosticResultRow>();
                        break;

                    case "DiagnosticRequestItemId":
                        if (int.TryParse(value, out int itemId))
                            filtered = filtered.Where(x => x.DiagnosticRequestItemId == itemId);
                        else
                            filtered = Enumerable.Empty<DiagnosticResultRow>();
                        break;

                    case "Unit":
                        filtered = filtered.Where(x =>
                            (x.Unit ?? "").IndexOf(value,
                            StringComparison.OrdinalIgnoreCase) >= 0);
                        break;
                }
            }

            _rows = new BindingList<DiagnosticResultRow>(filtered.ToList());

            dgvResults.DataSource = _rows;

            UpdateRecordNumber();
        }

        private void UpdateRecordNumber()
        {
            lblRecordNumber.Text = _rows.Count.ToString();
        }

        // ================================
        // REFRESH BUTTON
        // ================================
        private void btnRefresh_Click(object? sender, EventArgs e)
        {
            RefreshGrid();
        }

        // ================================
        // ROW VIEWMODEL
        // ================================
        private sealed class DiagnosticResultRow
        {
            public int DiagnosticResultId { get; }
            public int DiagnosticRequestItemId { get; }

            public string? ResultText { get; }
            public decimal? ResultNumeric { get; }

            public string? Unit { get; }
            public string? RefRange { get; }

            public string? ReportText { get; }

            public DateTime? PerformedAt { get; }
            public DateTime? VerifiedAt { get; }

            public DiagnosticResultRow(Clinic_Management_Entities.Entities.DiagnosticResult r)
            {
                DiagnosticResultId = r.DiagnosticResultId;
                DiagnosticRequestItemId = r.DiagnosticRequestItemId;

                ResultText = r.ResultText;
                ResultNumeric = r.ResultNumeric;

                Unit = r.Unit;
                RefRange = r.RefRange;

                ReportText = r.ReportText;

                PerformedAt = r.PerformedAt;
                VerifiedAt = r.VerifiedAt;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dgvResults.CurrentRow == null)
                return;

            var row = dgvResults.CurrentRow.DataBoundItem as DiagnosticResultRow;

            if (row == null)
                return;

            int ResultID = row.DiagnosticResultId;


            frmDiagnosticResult frm = new frmDiagnosticResult(ResultID,
                ucDiagnosticResult.enMode.View);
            frm.ShowDialog();

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (dgvResults.CurrentRow == null)
                return;

            var row = dgvResults.CurrentRow.DataBoundItem as DiagnosticResultRow;

            if (row == null)
                return;

            int ResultID = row.DiagnosticResultId;


            frmDiagnosticResult frm = new frmDiagnosticResult(ResultID,
                ucDiagnosticResult.enMode.Edit);
            frm.ShowDialog();

        }

        DiagnosticResultService _DiagResultService = new DiagnosticResultService();

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (dgvResults.CurrentRow == null)
                return;

            var row = dgvResults.CurrentRow.DataBoundItem as DiagnosticResultRow;

            if (row == null)
                return;

            int ResultID = row.DiagnosticResultId;


            if(!clsMessage.ConfirmDelete())
            {
                return;
            }

            if(_DiagResultService.Delete(ResultID).IsSuccess)
            {
                clsMessage.ShowSuccess("Item Result Deleted Successfuly.");
                return;
            }

            clsMessage.ShowSuccess("Item Result NOt Deleted !.");
            return;

        }
    }

}
