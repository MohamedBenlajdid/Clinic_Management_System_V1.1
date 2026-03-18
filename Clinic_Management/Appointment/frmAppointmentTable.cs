using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Appointment
{
    using Clinic_Management.Diagnostics.DiagnosticRequest;
    using Clinic_Management.Helpers;
    using Clinic_Management.Invoices;
    using Clinic_Management.MedicalCertificate;
    using Clinic_Management.MedicalRecord;
    using Clinic_Management.Prescriptions;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    public partial class frmAppointmentTable : Form
    {
        // =======================
        // SERVICES
        // =======================
        private readonly AppointmentService _service = new AppointmentService();
        private readonly MedicalRecordService _MedRecordService = new MedicalRecordService();
        private readonly DiagnosticRequestService _DiagnosticRequestService =
            new DiagnosticRequestService();
        private readonly MedicalCertificateService _MedicalCertificateService =
            new MedicalCertificateService();
        private readonly InvoiceService _InvoiceService =
            new InvoiceService();

        // =======================
        // DATA (bindable)
        // =======================
        private BindingList<AppointmentRow> _rows = new BindingList<AppointmentRow>();
        private List<AppointmentRow> _all = new List<AppointmentRow>();

        public frmAppointmentTable()
        {
            InitializeComponent();

            // recommended (optional)
            this.Load += frmAppointmentTable_Load;

            // wire your existing controls (rename if different)
            // button1 = Add new
            button1.Click += button1_Click;

            // button2 = Reset
            button2.Click += button2_Click;

            // filtering
            cbFilterBy.SelectedIndexChanged += cbFilterBy_SelectedIndexChanged;
            txtFilterValue.TextChanged += txtFilterValue_TextChanged;
        }

        private void frmAppointmentTable_Load(object? sender, EventArgs e)
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
            "AppointmentId",
            "PatientId",
            "DoctorId",
            "Status",
            "Date (StartAt)" // filter by date prefix (yyyy-mm-dd) or dd/mm depending on your input
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
        // UI: GRID SETUP / FORMAT
        // =======================
        private void SetupGrid()
        {
            dgvAppointments.AutoGenerateColumns = false;
            dgvAppointments.AllowUserToAddRows = false;
            dgvAppointments.AllowUserToDeleteRows = false;
            dgvAppointments.ReadOnly = true;
            dgvAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAppointments.MultiSelect = false;
            dgvAppointments.RowHeadersVisible = false;

            dgvAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAppointments.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvAppointments.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvAppointments.ColumnHeadersHeight = 34;
            dgvAppointments.RowTemplate.Height = 30;

            dgvAppointments.Columns.Clear();

            dgvAppointments.Columns.Add(MakeTextCol(nameof(AppointmentRow.AppointmentId), "ID", 70));
            dgvAppointments.Columns.Add(MakeTextCol(nameof(AppointmentRow.PatientId), "Patient", 80));
            dgvAppointments.Columns.Add(MakeTextCol(nameof(AppointmentRow.DoctorId), "Doctor", 80));

            dgvAppointments.Columns.Add(MakeTextCol(nameof(AppointmentRow.StartAt), "Start", 140, "g"));
            dgvAppointments.Columns.Add(MakeTextCol(nameof(AppointmentRow.EndAt), "End", 140, "g"));

            dgvAppointments.Columns.Add(MakeTextCol(nameof(AppointmentRow.StatusText), "Status", 110));
            dgvAppointments.Columns.Add(MakeTextCol(nameof(AppointmentRow.Reason), "Reason", 200));

            dgvAppointments.Columns.Add(MakeTextCol(nameof(AppointmentRow.CreatedAt), "Created", 140, "g"));

            // optional visual: color status
            dgvAppointments.CellFormatting += dgvAppointments_CellFormatting;

            dgvAppointments.DataSource = _rows;
        }

        private DataGridViewTextBoxColumn MakeTextCol(string dataProperty, string header, int minWidth,
            string? format = null)
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

        private void dgvAppointments_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvAppointments.Columns[e.ColumnIndex].DataPropertyName != nameof(AppointmentRow.StatusText))
                return;

            string status = Convert.ToString(e.Value) ?? "";

            // Light coloring per status text (adjust to your enum mapping)
            var row = dgvAppointments.Rows[e.RowIndex];

            if (status.Equals("Cancelled", StringComparison.OrdinalIgnoreCase))
                row.DefaultCellStyle.ForeColor = Color.Firebrick;
            else if (status.Equals("Completed", StringComparison.OrdinalIgnoreCase))
                row.DefaultCellStyle.ForeColor = Color.SeaGreen;
            else if (status.Equals("Scheduled", StringComparison.OrdinalIgnoreCase))
                row.DefaultCellStyle.ForeColor = Color.DodgerBlue;
            else
                row.DefaultCellStyle.ForeColor = dgvAppointments.DefaultCellStyle.ForeColor;
        }

        // =======================
        // DATA LOAD
        // =======================
        private void RefreshGrid()
        {
            var result = _service.GetAll();

            if (!result.IsSuccess)
            {
                clsMessage.ShowError(result.ErrorMessage);
                return;
            }

            var list = result.Value!
                .Where(a => !a.IsDeleted)
                .ToList();

            _all = list
                .Select(a => new AppointmentRow(a))
                .OrderByDescending(r => r.StartAt)
                .ToList();

            ApplyFilters();
        }


        private void ApplyFilters()
        {
            IEnumerable<AppointmentRow> filtered = _all;

            string filterBy = cbFilterBy.SelectedItem?.ToString() ?? "All";
            string value = (txtFilterValue.Text ?? "").Trim();

            if (filterBy != "All" && !string.IsNullOrWhiteSpace(value))
            {
                switch (filterBy)
                {
                    case "AppointmentId":
                        if (int.TryParse(value, out int apptId))
                            filtered = filtered.Where(x => x.AppointmentId == apptId);
                        else
                            filtered = Enumerable.Empty<AppointmentRow>();
                        break;

                    case "PatientId":
                        if (int.TryParse(value, out int patientId))
                            filtered = filtered.Where(x => x.PatientId == patientId);
                        else
                            filtered = Enumerable.Empty<AppointmentRow>();
                        break;

                    case "DoctorId":
                        if (int.TryParse(value, out int doctorId))
                            filtered = filtered.Where(x => x.DoctorId == doctorId);
                        else
                            filtered = Enumerable.Empty<AppointmentRow>();
                        break;

                    case "Status":
                        filtered = filtered.Where(x =>
                            (x.StatusText ?? "").IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0);
                        break;

                    case "Date (StartAt)":
                        // Allow user to type: 2026-02-19 OR 19/02/2026 OR "19-02"
                        filtered = filtered.Where(x =>
                            x.StartAt.ToString("yyyy-MM-dd").Contains(value, StringComparison.OrdinalIgnoreCase) ||
                            x.StartAt.ToString("dd/MM/yyyy").Contains(value, StringComparison.OrdinalIgnoreCase));
                        break;
                }
            }

            // rebind
            _rows = new BindingList<AppointmentRow>(filtered.ToList());
            dgvAppointments.DataSource = _rows;

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
            // Open your appointment form (adapt constructor if needed)
            using var frm = new frmAppointment();
            frm.ShowDialog();

            // After closing, refresh table
            RefreshGrid();
        }

        private void button2_Click(object? sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0; // All
            txtFilterValue.Clear();
            ApplyFilters();
        }

        // =======================
        // VIEWMODEL ROW (for display)
        // =======================
        private sealed class AppointmentRow
        {
            public int AppointmentId { get; }
            public int PatientId { get; }
            public int DoctorId { get; }
            public DateTime StartAt { get; }
            public DateTime EndAt { get; }
            public byte Status { get; }
            public string? StatusText { get; }
            public string? Reason { get; }
            public DateTime CreatedAt { get; }

            public AppointmentRow(Clinic_Management_Entities.Entities.Appointment a)
            {
                AppointmentId = a.AppointmentId;
                PatientId = a.PatientId;
                DoctorId = a.DoctorId;
                StartAt = a.StartAt;
                EndAt = a.EndAt;
                Status = a.Status;
                StatusText = MapStatus(Status);
                Reason = a.Reason;
                CreatedAt = a.CreatedAt;
            }

            private static string MapStatus(byte status)
            {
                // adjust to your real enum meaning
                return status switch
                {
                    1 => "Scheduled",
                    2 => "Completed",
                    3 => "NoShow",
                    4 => "Cancelled",
                    5 => "InProgress",
                    _ => "Unknown"
                };
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.CurrentRow == null)
                return;

            var row = dgvAppointments.CurrentRow.DataBoundItem as AppointmentRow;

            if (row == null)
                return;

            int appointmentId = row.AppointmentId;

            frmAppointment frm = new frmAppointment(appointmentId, ucAppointment.enMode.View);
            frm.ShowDialog();


        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.CurrentRow == null)
                return;

            var row = dgvAppointments.CurrentRow.DataBoundItem as AppointmentRow;

            if (row == null)
                return;

            int appointmentId = row.AppointmentId;

            frmAppointment frm = new frmAppointment(appointmentId, ucAppointment.enMode.Edit);
            frm.ShowDialog();

        }





        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.CurrentRow == null)
                return;

            var row = dgvAppointments.CurrentRow.DataBoundItem as AppointmentRow;

            if (row == null)
                return;

            int appointmentId = row.AppointmentId;

            if (!clsMessage.Confirm("Are you sure you want to cancel this appointment?"))
                return;

            string cancelReason = string.Empty;

            frmCancelledAppointmentReason frmCancel
                = new frmCancelledAppointmentReason();
            frmCancel.ShowDialog();

            cancelReason = frmCancel.Reason;

            var result = _service.Cancel(appointmentId, cancelReason);

            if (result.IsSuccess)
            {
                clsMessage.ShowSuccess("Cancelled Successfully");
                RefreshGrid();
                return;
            }

            clsMessage.ShowError(result.ErrorMessage ?? "Cancelled Fail");
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.CurrentRow == null)
                return;

            var row = dgvAppointments.CurrentRow.DataBoundItem as AppointmentRow;

            if (row == null)
                return;

            int appointmentId = row.AppointmentId;


            if (!clsMessage.Confirm("Are you sure you want to Delete this appointment?"))
                return;

            var result = _service.Delete(appointmentId);

            if (result.IsSuccess)
            {
                clsMessage.ShowSuccess("Deleted Successfully");
                RefreshGrid();
                return;
            }

            clsMessage.ShowError(result.ErrorMessage ?? "Deleted Fail");

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.CurrentRow == null)
                return;

            var row = dgvAppointments.CurrentRow.DataBoundItem as AppointmentRow;

            if (row == null)
                return;

            int appointmentId = row.AppointmentId;


            if (!clsMessage.Confirm("Are you sure you want Change State To No ShowUP.?"))
                return;

            var result = _service.SetStatus(appointmentId,
                Clinic_Management_BLL.Service.enAppointmentStatus.NoShow);

            if (result.IsSuccess)
            {
                clsMessage.ShowSuccess("Status Change Successfully");
                RefreshGrid();
                return;
            }

            clsMessage.ShowError(result.ErrorMessage ?? "State Fail");
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.CurrentRow == null)
                return;

            var row = dgvAppointments.CurrentRow.DataBoundItem as AppointmentRow;

            if (row == null)
                return;

            int appointmentId = row.AppointmentId;


            if (!clsMessage.Confirm("Are you sure you want Change State To InProgress.?"))
                return;

            var result = _service.SetStatus(appointmentId,
                Clinic_Management_BLL.Service.enAppointmentStatus.InProgress);

            if (result.IsSuccess)
            {
                clsMessage.ShowSuccess("Status Change Successfully");
                RefreshGrid();
                return;
            }

            clsMessage.ShowError(result.ErrorMessage ?? "State Fail");
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.CurrentRow == null)
                return;

            var row = dgvAppointments.CurrentRow.DataBoundItem as AppointmentRow;

            if (row == null)
                return;

            int appointmentId = row.AppointmentId;


            if (!clsMessage.Confirm("Are you sure you want Change State To Completed.?"))
                return;

            var result = _service.SetStatus(appointmentId,
                Clinic_Management_BLL.Service.enAppointmentStatus.Completed);

            if (result.IsSuccess)
            {
                clsMessage.ShowSuccess("Status Change Successfully");
                RefreshGrid();
                return;
            }

            clsMessage.ShowError(result.ErrorMessage ?? "State Fail");
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.CurrentRow == null)
                return;

            var row = dgvAppointments.CurrentRow.DataBoundItem as AppointmentRow;

            if (row == null)
                return;

            int appointmentId = row.AppointmentId;

            int MedicalRecordId =
                (_MedRecordService.GetByAppointmentIdSafe(appointmentId).IsSuccess) ?
                _MedRecordService.GetByAppointmentIdSafe(appointmentId).Value.MedicalRecordId : -1;

            if (MedicalRecordId > 0)
            {
                frmMedicalRecord frmMedRecord = new frmMedicalRecord(
                    MedicalRecordId, ucMedicalRecord.enMode.View);
                frmMedRecord.ShowDialog();
                return;
            }

            Appointment CurrentAppoint = _service.GetById(appointmentId).Value;

            frmMedicalRecord frm = new frmMedicalRecord(appointmentId,
                CurrentAppoint.PatientId, CurrentAppoint.DoctorId);
            frm.ShowDialog();



        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {

            if (dgvAppointments.CurrentRow == null)
                return;

            var row = dgvAppointments.CurrentRow.DataBoundItem as AppointmentRow;

            if (row == null)
                return;

            int appointmentId = row.AppointmentId;

            Appointment CurrentAppoint = _service.GetById(appointmentId).Value;

            frmPrescription frm = new frmPrescription(appointmentId,
                CurrentAppoint.PatientId, CurrentAppoint.DoctorId);
            frm.ShowDialog();

        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.CurrentRow == null)
                return;

            var row = dgvAppointments.CurrentRow.DataBoundItem as AppointmentRow;

            if (row == null)
                return;

            int appointmentId = row.AppointmentId;

            int DiagRequestId =
                (_DiagnosticRequestService.GetByAppointmentId(appointmentId).IsSuccess) ?
                _DiagnosticRequestService.GetByAppointmentId(appointmentId).Value.First().DiagnosticRequestId : -1;

            if (DiagRequestId > 0)
            {
                frmDiagnosticRequest frmDiagRequest = new frmDiagnosticRequest(
                    DiagRequestId, ucDiagnosticRequest.enMode.View);
                frmDiagRequest.ShowDialog();
                return;
            }


            Appointment CurrentAppoint = _service.GetById(appointmentId).Value;

            frmDiagnosticRequest frm = new frmDiagnosticRequest(appointmentId,
                CurrentAppoint.PatientId, CurrentAppoint.DoctorId);
            frm.ShowDialog();


        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.CurrentRow == null)
                return;

            var row = dgvAppointments.CurrentRow.DataBoundItem as AppointmentRow;

            if (row == null)
                return;

            int appointmentId = row.AppointmentId;

            int MedicalCertificateId =
                (_MedicalCertificateService.GetByAppointmentId(appointmentId).IsSuccess) ?
                _MedicalCertificateService.GetByAppointmentId(appointmentId).Value.First().MedicalCertificateId : -1;

            if (MedicalCertificateId > 0)
            {
                frmMedicalCertificate frmMedicalCert = new frmMedicalCertificate(
                    MedicalCertificateId, ucMedicalCertificate.enMode.View);
                frmMedicalCert.ShowDialog();
                return;
            }


            Appointment CurrentAppoint = _service.GetById(appointmentId).Value;

            frmMedicalCertificate frm = new frmMedicalCertificate(appointmentId,
                CurrentAppoint.PatientId, CurrentAppoint.DoctorId);
            frm.ShowDialog();

        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.CurrentRow == null)
                return;

            var row = dgvAppointments.CurrentRow.DataBoundItem as AppointmentRow;

            if (row == null)
                return;

            int appointmentId = row.AppointmentId;

            int InvoiceID =
                (_InvoiceService.GetByAppointmentId(appointmentId).IsSuccess) ?
                _InvoiceService.GetByAppointmentId(appointmentId).Value.First().InvoiceId : -1;

            if (InvoiceID > 0)
            {
                frmInvoice frmInvoice = new frmInvoice(
                    InvoiceID, ucInvoice.enMode.View);
                frmInvoice.ShowDialog();
                return;
            }


            Appointment CurrentAppoint = _service.GetById(appointmentId).Value;

            frmInvoice frm = new frmInvoice(appointmentId,
                CurrentAppoint.PatientId);
            frm.ShowDialog();
        }

        private void cmsAppointments_Opening(object sender, CancelEventArgs e)
        {
            if (dgvAppointments.CurrentRow == null)
                return;

            var row = dgvAppointments.CurrentRow.DataBoundItem as AppointmentRow;

            if (row == null)
                return;

            int appointmentId = row.AppointmentId;

            byte AppointementStatus =
                _service.GetById(appointmentId).Value.Status;

            bool InProgress = AppointementStatus == 5;

            toolStripMenuItem8.Enabled = InProgress;
            toolStripMenuItem9.Enabled = InProgress;
            toolStripMenuItem10.Enabled = InProgress;
            toolStripMenuItem11.Enabled = InProgress;
            toolStripMenuItem12.Enabled = InProgress;
         

        }



    }



}
