using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Prescriptions
{
    using Clinic_Management.Helpers;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities.Entities;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public partial class ucPrescription : UserControl
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
        public int PrescriptionID => Prescription?.PrescriptionId ?? -1;
        public int AppointmentID => Prescription?.AppointmentId ?? -1;
        public int PatientID => Prescription?.PatientId ?? -1;
        public int DoctorID => Prescription?.DoctorId ?? -1;

        public Prescription Prescription { get; private set; } = new Prescription();

        // =======================
        // EVENTS
        // =======================
        public event Action<int>? OnPrescriptionCreated;
        public event Action<bool>? DirtyChanged;

        // =======================
        // SERVICES
        // =======================
        private readonly PrescriptionService _prescriptionService = new();
        private readonly MedicalRecordService _medicalRecordService = new();    
        private readonly PrescriptionItemService _prescriptionItemService = new();
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
        public ucPrescription()
        {
            InitializeComponent();

            if (IsDesignTime)
                return;

            WireDirtyEvents();
            //LoadNew();
        }

        // =======================
        // PUBLIC API
        // =======================
        public void LoadEntityData(int prescriptionId, enMode mode = enMode.View)
        {
            ClearErrors();

            if (prescriptionId <= 0)
            {
                LoadNew();
                return;
            }

            CurrentMode = mode;

            var res = _prescriptionService.GetById(prescriptionId);
            if (!res.IsSuccess || res.Value is null)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Prescription not found.");
                return;
            }

            Prescription = res.Value;

            BindEntityToUI();
            SetDirty(false);
        }

        public void LoadNewForAppointment(int appointmentId, int patientId, int doctorId)
        {
            LoadNew();

            Prescription.AppointmentId = appointmentId;
            Prescription.PatientId = patientId;
            Prescription.DoctorId = doctorId;

            lblAppointmentId.Text = appointmentId > 0 ? appointmentId.ToString() : "[N/A]";
            lblPatientId.Text = patientId > 0 ? patientId.ToString() : "[N/A]";
            lblDoctorId.Text = doctorId > 0 ? doctorId.ToString() : "[N/A]";

            SetDirty(true);
        }

        public void LoadNew()
        {
            ClearErrors();

            Prescription = new Prescription
            {
                PrescriptionId = 0,
                AppointmentId = 0,
                PatientId = 0,
                DoctorId = 0,
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
                var res = _prescriptionService.Create(Prescription);

                if (!res.IsSuccess || res.Value <= 0)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to create prescription.");
                    return false;
                }

                Prescription.PrescriptionId = res.Value;

                BindEntityToUI();
                SetDirty(false);
                CurrentMode = enMode.View;

                OnPrescriptionCreated?.Invoke(res.Value);
                return true;
            }
            else
            {
                var res = _prescriptionService.Update(Prescription);

                if (!res.IsSuccess)
                {
                    clsMessage.ShowError(res.ErrorMessage ?? "Failed to update prescription.");
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
            if (PrescriptionID <= 0)
                return false;

            if (!clsMessage.ConfirmDelete("prescription"))
                return false;

            var res = _prescriptionService.Delete(PrescriptionID);
            if (!res.IsSuccess)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Delete failed.");
                return false;
            }

            LoadNew();
            clsMessage.ShowSuccess("Prescription deleted successfully.");
            return true;
        }

        // =======================
        // UI CORE
        // =======================
        private void ResetUI()
        {
            ClearErrors();

            lblPrescriptionId.Text = "[N/A]";
            lblAppointmentId.Text = "[N/A]";
            lblPatientId.Text = "[N/A]";
            lblDoctorId.Text = "[N/A]";
            txtNotes.Text = string.Empty;

            linkEdit.Visible = false;
            btnSave.Visible = false;

            ApplyMode();
        }

        private void BindEntityToUI()
        {
            lblPrescriptionId.Text = PrescriptionID > 0 ? PrescriptionID.ToString() : "[N/A]";
            lblAppointmentId.Text = AppointmentID > 0 ? AppointmentID.ToString() : "[N/A]";
            lblPatientId.Text = PatientID > 0 ? PatientID.ToString() : "[N/A]";
            lblDoctorId.Text = DoctorID > 0 ? DoctorID.ToString() : "[N/A]";
            txtNotes.Text = Prescription.Notes ?? "";

            ApplyMode();
        }

        private void MapUIToEntity()
        {
            Prescription.Notes = txtNotes.Text.Trim();
        }

        private void ApplyMode()
        {
            bool editable = (CurrentMode != enMode.View);

            txtNotes.ReadOnly = !editable;

            linkAddItem.Visible = this.PrescriptionID > 0;
            linkShowItems.Visible = this.PrescriptionID > 0;

            btnSave.Visible = editable;
            linkEdit.Visible = (CurrentMode == enMode.View && PrescriptionID > 0);

            lblPrescriptionId.Text = PrescriptionID > 0 ? PrescriptionID.ToString() : "[N/A]";
        }

        // =======================
        // VALIDATION
        // =======================
        private bool ValidateUI()
        {
            ClearErrors();
            bool ok = true;

            if (Prescription.AppointmentId <= 0)
            {
                SetError(lblAppointmentId, "Appointment is required.");
                ok = false;
            }

            if (!ok)
            {
                if (!string.IsNullOrEmpty(errorProvider1.GetError(lblAppointmentId)))
                    lblAppointmentId.Focus();
            }

            return ok;
        }

        // =======================
        // DIRTY WIRING
        // =======================
        private void WireDirtyEvents()
        {
            txtNotes.TextChanged += (_, __) => SetDirty(true);
        }

        // =======================
        // UI EVENTS
        // =======================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!SaveCurrent())
            {
                clsMessage.ShowError("Prescription failed to save.");
                return;
            }

            clsMessage.ShowSuccess("Prescription saved successfully.");
        }

        private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentMode = enMode.Edit;
        }

        private void linkAddItem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPrescriptionItem frm = new frmPrescriptionItem(this.PrescriptionID);
            frm.ShowDialog();
        }

        private void linkShowItems_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (PrescriptionID <= 0)
            {
                MessageBox.Show("Please select a prescription first.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Fetch items
            var items = _prescriptionItemService.GetByPrescriptionId(PrescriptionID).Value?.ToList()
                        ?? new List<PrescriptionItem>();

            // Build a lightweight "virtual" form
            using var frm = new Form
            {
                Text = $"Prescription Items (#{PrescriptionID})",
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

            // Optional: improve look
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10f, FontStyle.Regular);
            dgv.DefaultCellStyle.SelectionBackColor = Color.Gainsboro;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Bind
            dgv.DataSource = items;

            // Hide / rename columns safely (adjust to your entity)
            // These are common fields — keep/edit according to your PrescriptionItem model:
            HideIfExists(dgv, "PrescriptionItemId");
            HideIfExists(dgv, "PrescriptionId");

            RenameIfExists(dgv, "MedicamentId", "Medicament");
            RenameIfExists(dgv, "Dose", "Dose");
            RenameIfExists(dgv, "Frequency", "Frequency");
            RenameIfExists(dgv, "DurationDays", "Days");
            RenameIfExists(dgv, "Route", "Route");
            RenameIfExists(dgv, "Instructions", "Instructions");
            RenameIfExists(dgv, "Quantity", "Qty");

            // Add to form
            frm.Controls.Add(dgv);
            frm.Controls.Add(pnlTop);

            // Show as dialog (modal)
            frm.ShowDialog(this);
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
    }
}
