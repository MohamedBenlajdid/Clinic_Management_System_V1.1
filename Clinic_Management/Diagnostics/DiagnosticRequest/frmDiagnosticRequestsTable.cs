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
    public partial class frmDiagnosticRequestsTable : Form
    {
        // =======================
        // SERVICES
        // =======================
        private readonly DiagnosticRequestService _service =
            new DiagnosticRequestService();

        // =======================
        // DATA (bindable)
        // =======================
        private BindingList<DiagnosticRequestRow> _rows = new BindingList<DiagnosticRequestRow>();
        private List<DiagnosticRequestRow> _all = new List<DiagnosticRequestRow>();

        private int _appointmentId;

        public frmDiagnosticRequestsTable()
        {
            InitializeComponent();



            this.Load += frmDiagnosticRequestsTable_Load;

            button1.Click += button1_Click;
            button2.Click += button2_Click;

            cbFilterBy.SelectedIndexChanged += cbFilterBy_SelectedIndexChanged;
            txtFilterValue.TextChanged += txtFilterValue_TextChanged;
        }

        private void frmDiagnosticRequestsTable_Load(object? sender, EventArgs e)
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
            "DiagnosticRequestId",
            "PatientName",
            "DoctorName",
            "Priority",
            "Status"
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
            dgvDiagnosticRequests.AutoGenerateColumns = false;
            dgvDiagnosticRequests.AllowUserToAddRows = false;
            dgvDiagnosticRequests.AllowUserToDeleteRows = false;
            dgvDiagnosticRequests.ReadOnly = true;

            dgvDiagnosticRequests.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDiagnosticRequests.MultiSelect = false;
            dgvDiagnosticRequests.RowHeadersVisible = false;

            dgvDiagnosticRequests.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvDiagnosticRequests.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvDiagnosticRequests.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvDiagnosticRequests.ColumnHeadersHeight = 34;
            dgvDiagnosticRequests.RowTemplate.Height = 30;

            dgvDiagnosticRequests.Columns.Clear();

            dgvDiagnosticRequests.Columns.Add(MakeTextCol(nameof(DiagnosticRequestRow.DiagnosticRequestId), "ID", 80));
            dgvDiagnosticRequests.Columns.Add(MakeTextCol(nameof(DiagnosticRequestRow.PatientName), "Patient", 150));
            dgvDiagnosticRequests.Columns.Add(MakeTextCol(nameof(DiagnosticRequestRow.DoctorName), "Doctor", 150));

            dgvDiagnosticRequests.Columns.Add(MakeTextCol(nameof(DiagnosticRequestRow.RequestedAt), "Requested At", 140, "g"));

            dgvDiagnosticRequests.Columns.Add(MakeTextCol(nameof(DiagnosticRequestRow.PriorityText), "Priority", 100));
            dgvDiagnosticRequests.Columns.Add(MakeTextCol(nameof(DiagnosticRequestRow.StatusText), "Status", 110));

            dgvDiagnosticRequests.Columns.Add(MakeTextCol(nameof(DiagnosticRequestRow.ClinicalInfo), "Clinical Info", 250));

            dgvDiagnosticRequests.CellFormatting += dgvDiagnosticRequests_CellFormatting;

            dgvDiagnosticRequests.DataSource = _rows;
        }

        private DataGridViewTextBoxColumn MakeTextCol(string dataProperty, string header, int minWidth, string? format = null)
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

        private void dgvDiagnosticRequests_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvDiagnosticRequests.Columns[e.ColumnIndex].DataPropertyName != nameof(DiagnosticRequestRow.StatusText))
                return;

            string status = Convert.ToString(e.Value) ?? "";

            var row = dgvDiagnosticRequests.Rows[e.RowIndex];

            if (status.Equals("Cancelled", StringComparison.OrdinalIgnoreCase))
                row.DefaultCellStyle.ForeColor = Color.Firebrick;

            else if (status.Equals("Completed", StringComparison.OrdinalIgnoreCase))
                row.DefaultCellStyle.ForeColor = Color.SeaGreen;

            else if (status.Equals("Pending", StringComparison.OrdinalIgnoreCase))
                row.DefaultCellStyle.ForeColor = Color.DodgerBlue;

            else if (status.Equals("InProgress", StringComparison.OrdinalIgnoreCase))
                row.DefaultCellStyle.ForeColor = Color.DarkOrange;
        }

        // =======================
        // DATA LOAD
        // =======================
        private void RefreshGrid()
        {
            var result = _service.GetAllDetails();

            if (!result.IsSuccess)
            {
                clsMessage.ShowError(result.ErrorMessage);
                return;
            }

            var list = result.Value!.ToList();

            _all = list
                .Select(x => new DiagnosticRequestRow(x))
                .OrderByDescending(r => r.RequestedAt)
                .ToList();

            ApplyFilters();
        }

        private void ApplyFilters()
        {
            IEnumerable<DiagnosticRequestRow> filtered = _all;

            string filterBy = cbFilterBy.SelectedItem?.ToString() ?? "All";
            string value = (txtFilterValue.Text ?? "").Trim();

            if (filterBy != "All" && !string.IsNullOrWhiteSpace(value))
            {
                switch (filterBy)
                {
                    case "DiagnosticRequestId":
                        if (int.TryParse(value, out int id))
                            filtered = filtered.Where(x => x.DiagnosticRequestId == id);
                        else
                            filtered = Enumerable.Empty<DiagnosticRequestRow>();
                        break;

                    case "PatientName":
                        filtered = filtered.Where(x =>
                            (x.PatientName ?? "").IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0);
                        break;

                    case "DoctorName":
                        filtered = filtered.Where(x =>
                            (x.DoctorName ?? "").IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0);
                        break;

                    case "Priority":
                        filtered = filtered.Where(x =>
                            (x.PriorityText ?? "").IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0);
                        break;

                    case "Status":
                        filtered = filtered.Where(x =>
                            (x.StatusText ?? "").IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0);
                        break;
                }
            }

            _rows = new BindingList<DiagnosticRequestRow>(filtered.ToList());
            dgvDiagnosticRequests.DataSource = _rows;

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
            using var frm = new frmDiagnosticRequest(_appointmentId);
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
        private sealed class DiagnosticRequestRow
        {
            public int DiagnosticRequestId { get; }
            public int AppointmentId { get; }
            public DateTime RequestedAt { get; }
            public byte Priority { get; }
            public byte Status { get; }

            public string? PriorityText { get; }
            public string? StatusText { get; }

            public string? ClinicalInfo { get; }
            public string? PatientName { get; }
            public string? DoctorName { get; }

            public DiagnosticRequestRow(DiagnosticRequestDetail r)
            {
                DiagnosticRequestId = r.DiagnosticRequestId;
                AppointmentId = r.AppointmentId;
                RequestedAt = r.RequestedAt;

                Priority = (byte)r.Priority;
                Status = (byte)r.Status;

                PriorityText = MapPriority(Priority);
                StatusText = MapStatus(Status);

                ClinicalInfo = r.ClinicalInfo;
                PatientName = r.PatientName;
                DoctorName = r.DoctorName;
            }

            private static string MapPriority(byte p)
            {
                return p switch
                {
                    1 => "Routine",
                    2 => "Urgent",
                    3 => "Stat",
                    _ => "Unknown"
                };
            }

            private static string MapStatus(byte s)
            {
                return s switch
                {
                    1 => "Pending",
                    2 => "InProgress",
                    3 => "Completed",
                    4 => "Cancelled",
                    _ => "Unknown"
                };
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dgvDiagnosticRequests.CurrentRow == null)
                return;

            var row = dgvDiagnosticRequests.CurrentRow.DataBoundItem as DiagnosticRequestRow;

            if (row == null)
                return;

            int RequestID = row.DiagnosticRequestId;

            frmDiagnosticRequest frm = new frmDiagnosticRequest(RequestID,
                ucDiagnosticRequest.enMode.View);

            frm.ShowDialog();


        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (dgvDiagnosticRequests.CurrentRow == null)
                return;

            var row = dgvDiagnosticRequests.CurrentRow.DataBoundItem as DiagnosticRequestRow;

            if (row == null)
                return;

            int RequestID = row.DiagnosticRequestId;

            frmDiagnosticRequest frm = new frmDiagnosticRequest(RequestID,
                ucDiagnosticRequest.enMode.Edit);

            frm.ShowDialog();

        }

        DiagnosticRequestService _DiagRequestService = new DiagnosticRequestService();

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

            if (dgvDiagnosticRequests.CurrentRow == null)
                return;

            var row = dgvDiagnosticRequests.CurrentRow.DataBoundItem as DiagnosticRequestRow;

            if (row == null)
                return;

            int RequestID = row.DiagnosticRequestId;

            if (!clsMessage.Confirm("Are you sure you want to delete this Request?"))
                return;

            bool Deleted = _DiagRequestService.Delete(RequestID).IsSuccess;

            if (Deleted)
            {
                clsMessage.ShowSuccess("Request Deleted Successfuly .");
                return;
            }

            clsMessage.ShowSuccess("Request Not Deleted ! Something Wrong.");
            return;


        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (dgvDiagnosticRequests.CurrentRow == null)
                return;

            var row = dgvDiagnosticRequests.CurrentRow.DataBoundItem as DiagnosticRequestRow;

            if (row == null)
                return;

            int RequestID = row.DiagnosticRequestId;

            frmDiagnosticRequestItemsTable frm =
                new frmDiagnosticRequestItemsTable(RequestID);
            frm.ShowDialog();

        }

    }

}
