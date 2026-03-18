using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Diagnostics.DiagnosticRequest
{
    using Clinic_Management.Helpers;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities.Entities;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public partial class ucDiagnosticRequest : UserControl
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
        public int DiagnosticRequestID => DiagnosticRequest?.DiagnosticRequestId ?? -1;
        public int AppointmentID => DiagnosticRequest?.AppointmentId ?? -1;
        public int PatientID => DiagnosticRequest?.PatientId ?? -1;
        public int DoctorID => DiagnosticRequest?.DoctorId ?? -1;

        public Clinic_Management_Entities.Entities.DiagnosticRequest DiagnosticRequest { get; private set; } =
            new Clinic_Management_Entities.Entities.DiagnosticRequest();

        // =======================
        // EVENTS
        // =======================
        public event Action<int>? OnDiagnosticRequestCreated;
        public event Action<bool>? DirtyChanged;

        // =======================
        // SERVICES
        // =======================
        private readonly DiagnosticRequestService _diagnosticRequestService = new();
        private readonly DiagnosticRequestItemService _diagnosticRequestItemService = new();

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
        public ucDiagnosticRequest()
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
            cbPriority.Items.Clear();
            cbPriority.Items.AddRange(new object[] { "Routine", "Urgent", "Stat" });
            cbPriority.SelectedIndex = 0;

            cbStatus.Items.Clear();
            cbStatus.Items.AddRange(new object[] { "Pending", "In Progress", "Completed", "Cancelled" });
            cbStatus.SelectedIndex = 0;
        }

        // =======================
        // PUBLIC API
        // =======================
        public void LoadEntityData(int diagnosticRequestId, enMode mode = enMode.View)
        {
            ClearErrors();

            if (diagnosticRequestId <= 0)
            {
                LoadNew();
                return;
            }

            CurrentMode = mode;

            var res = _diagnosticRequestService.GetById(diagnosticRequestId);
            if (!res.IsSuccess || res.Value is null)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Diagnostic request not found.");
                return;
            }

            DiagnosticRequest = res.Value;

            BindEntityToUI();
            SetDirty(false);
        }

        public void LoadNewForAppointment(int appointmentId, int patientId, int doctorId)
        {
            LoadNew();

            DiagnosticRequest.AppointmentId = appointmentId;
            DiagnosticRequest.PatientId = patientId;
            DiagnosticRequest.DoctorId = doctorId;

            lblAppointmentId.Text = appointmentId > 0 ? appointmentId.ToString() : "[N/A]";
            lblPatientId.Text = patientId > 0 ? patientId.ToString() : "[N/A]";
            lblDoctorId.Text = doctorId > 0 ? doctorId.ToString() : "[N/A]";

            SetDirty(true);
        }

        public void LoadNew()
        {
            ClearErrors();

            DiagnosticRequest = new Clinic_Management_Entities.Entities.DiagnosticRequest
            {
                DiagnosticRequestId = 0,
                AppointmentId = 0,
                PatientId = 0,
                DoctorId = 0,
                ClinicalInfo = "",
                Priority = 0,
                Status = 0
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
                var res = _diagnosticRequestService.Create(DiagnosticRequest);

                if (!res.IsSuccess || res.Value <= 0)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to create diagnostic request.");
                    return false;
                }

                DiagnosticRequest.DiagnosticRequestId = res.Value;

                BindEntityToUI();
                SetDirty(false);
                CurrentMode = enMode.View;

                OnDiagnosticRequestCreated?.Invoke(res.Value);
                return true;
            }
            else
            {
                var res = _diagnosticRequestService.Update(DiagnosticRequest);

                if (!res.IsSuccess)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to update diagnostic request.");
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
            if (DiagnosticRequestID <= 0)
                return false;

            if (!clsMessage.ConfirmDelete("diagnostic request"))
                return false;

            var res = _diagnosticRequestService.Delete(DiagnosticRequestID);
            if (!res.IsSuccess)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Delete failed.");
                return false;
            }

            LoadNew();
            clsMessage.ShowSuccess("Diagnostic request deleted successfully.");
            return true;
        }

        // =======================
        // UI CORE
        // =======================
        private void ResetUI()
        {
            ClearErrors();

            lblDiagnosticRequestId.Text = "[N/A]";
            lblAppointmentId.Text = "[N/A]";
            lblPatientId.Text = "[N/A]";
            lblDoctorId.Text = "[N/A]";
            txtClinicalInfo.Text = string.Empty;
            cbPriority.SelectedIndex = 0;
            cbStatus.SelectedIndex = 0;

            ApplyMode();
        }

        private void BindEntityToUI()
        {
            lblDiagnosticRequestId.Text = DiagnosticRequestID > 0 ? DiagnosticRequestID.ToString() : "[N/A]";
            lblAppointmentId.Text = AppointmentID > 0 ? AppointmentID.ToString() : "[N/A]";
            lblPatientId.Text = PatientID > 0 ? PatientID.ToString() : "[N/A]";
            lblDoctorId.Text = DoctorID > 0 ? DoctorID.ToString() : "[N/A]";

            txtClinicalInfo.Text = DiagnosticRequest.ClinicalInfo ?? "";
            cbPriority.SelectedIndex = (DiagnosticRequest.Priority - 1);
            cbStatus.SelectedIndex = (DiagnosticRequest.Status - 1);

            ApplyMode();
        }

        private void MapUIToEntity()
        {
            DiagnosticRequest.ClinicalInfo = txtClinicalInfo.Text.Trim();
            DiagnosticRequest.Priority = (byte)(cbPriority.SelectedIndex + 1);
            DiagnosticRequest.Status = (byte)(cbStatus.SelectedIndex + 1);
        }

        private void ApplyMode()
        {
            bool editable = (CurrentMode != enMode.View);

            txtClinicalInfo.ReadOnly = !editable;
            cbPriority.Enabled = editable;
            cbStatus.Enabled = false;

            btnSave.Enabled = editable;
            linkEdit.Visible = (CurrentMode == enMode.View && DiagnosticRequestID > 0);

            linkViewDiagnosticItems.Visible = CurrentMode != enMode.AddNew;
            linkAddDiagnosticItem.Visible = CurrentMode != enMode.View;

            lblDiagnosticRequestId.Text = DiagnosticRequestID > 0 ? DiagnosticRequestID.ToString() : "[N/A]";
        }

        // =======================
        // VALIDATION
        // =======================
        private bool ValidateUI()
        {
            ClearErrors();
            bool ok = true;

            if (DiagnosticRequest.AppointmentId <= 0)
            {
                SetError(lblAppointmentId, "Appointment is required.");
                ok = false;
            }

            if (string.IsNullOrWhiteSpace(txtClinicalInfo.Text))
            {
                SetError(txtClinicalInfo, "Clinical information is required.");
                ok = false;
            }

            if (!ok)
            {
                if (!string.IsNullOrEmpty(errorProvider1.GetError(lblAppointmentId))) lblAppointmentId.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(txtClinicalInfo))) txtClinicalInfo.Focus();
            }

            return ok;
        }

        // =======================
        // DIRTY WIRING
        // =======================
        private void WireDirtyEvents()
        {
            txtClinicalInfo.TextChanged += (_, __) => SetDirty(true);
            cbPriority.SelectedIndexChanged += (_, __) => SetDirty(true);
            cbStatus.SelectedIndexChanged += (_, __) => SetDirty(true);
        }

        // =======================
        // UI EVENTS
        // =======================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!SaveCurrent())
            {
                clsMessage.ShowError("Diagnostic request failed to save.");
                return;
            }

            clsMessage.ShowSuccess("Diagnostic request saved successfully.");
        }

        private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentMode = enMode.Edit;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDiagnosticRequestItem frm =
                new frmDiagnosticRequestItem(this.DiagnosticRequestID);
            frm.ShowDialog();
        }


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
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (DiagnosticRequestID <= 0)
            {
                MessageBox.Show("Please select a diagnostic request first.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Call service
            var result = _diagnosticRequestService
                            .GetDiagnosticRequestItems(DiagnosticRequestID);

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.ErrorMessage ?? "Cannot load diagnostic tests.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var items = result.Value?.ToList() ?? new List<DiagnosticRequestItemDetail>();

            // Build a lightweight "virtual" form
            using var frm = new Form
            {
                Text = $"Diagnostic Tests (#{DiagnosticRequestID})",
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
                Text = $"Tests: {items.Count}",
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
            HideIfExists(dgv, "DiagnosticRequestItemId");
            HideIfExists(dgv, "DiagnosticRequestId");
            HideIfExists(dgv, "DiagnosticTestId");

            RenameIfExists(dgv, "Name", "Test");
            RenameIfExists(dgv, "Notes", "Notes");

            // Add to form
            frm.Controls.Add(dgv);
            frm.Controls.Add(pnlTop);

            // Show dialog
            frm.ShowDialog(this);
        }


    }



}
