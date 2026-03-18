using Clinic_Management.Helpers;
using Clinic_Management_BLL.Service;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Patients
{
    public partial class ucPatient : UserControl
    {
        // =======================
        // ErrorProvider helpers
        // =======================
        private void ClearErrors() => errorProvider1.Clear();

        private void SetError(Control ctrl, string message)
            => errorProvider1.SetError(ctrl, message);

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
        public int PatientID => Patient?.PatientId ?? -1;
        public int PersonID => Patient?.PersonId ?? -1;

        public Patient Patient { get; private set; } = new Patient();
        

        // =======================
        // EVENTS
        // =======================
        public event Action<int>? OnPatientCreated;
        public event Action<bool>? DirtyChanged;

        // =======================
        // SERVICES
        // =======================
        private readonly PatientService _patientService = new();

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
        public ucPatient()
        {
            InitializeComponent();

            if (IsDesignTime)
                return;

            WireDirtyEvents();
            WirePhoneGuards();
            LoadBloodTypes(); // simple: fill cbBloodTypeId
            LoadNew();        // default
        }

        // =======================
        // PUBLIC API
        // =======================
        public void LoadNew(int personId = 0)
        {
            ClearErrors();

            Patient = new Patient
            {
                PatientId = 0,
                PersonId = personId,
                MedicalRecordNumber = null,
                BloodTypeId = null,
                EmergencyContactName = null,
                EmergencyContactPhone = null,
                Notes = null

            };

            ResetUI();
            CurrentMode = enMode.AddNew;
            SetDirty(false);
        }

        public void LoadEntityData(int patientId, enMode mode = enMode.View)
        {
            ClearErrors();

            if (patientId <= 0)
            {
                LoadNew();
                return;
            }

            CurrentMode = mode;

            var res = _patientService.GetById(patientId); // Result<Patient>
            if (!res.IsSuccess || res.Value == null)
                throw new InvalidOperationException(res.ErrorMessage ?? "Patient not found.");

            Patient = res.Value;

            BindEntityToUI();
            SetDirty(false);
        }

        public bool SaveCurrent()
        {
            // Passport rule
            if (PersonID <= 0)
            {
                clsMessage.ShowWarning("PersonID is required to create or edit a patient.");
                ClearErrors();
                SetError(lblPersonId, "PersonID is required (Person is the passport).");
                return false;
            }

            if (!ValidateUI())
                return false;

            MapUIToEntity();

            bool wasAdd = (CurrentMode == enMode.AddNew);

            if (wasAdd)
            {
                var created = _patientService.Create(Patient); // Result<int>
                if (!created.IsSuccess || created.Value <= 0)
                {
                    clsMessage.ShowError(created.ErrorMessage ?? "Failed to create patient.");
                    return false;
                }

                Patient.PatientId = created.Value;

                CurrentMode = enMode.View;
                SetDirty(false);

                OnPatientCreated?.Invoke(Patient.PatientId);
                return true;
            }
            else
            {
                var updated = _patientService.Update(Patient); // Result
                if (!updated.IsSuccess)
                {
                    clsMessage.ShowError(updated.ErrorMessage ?? "Failed to update patient.");
                    return false;
                }

                CurrentMode = enMode.View;
                SetDirty(false);
                return true;
            }
        }

        // =======================
        // UI CORE
        // =======================
        private void ResetUI()
        {
            ClearErrors();

            lblPatientID.Text = "[N/A]";
            lblPersonId.Text = (PersonID > 0) ? PersonID.ToString() : "[N/A]";

            txtMedicalRecordNumber.Clear();
            txtEmergencyContactName.Clear();
            txtEmergencyPhone.Clear();
            txtNotes.Clear();

            if (cbBloodTypeId.Items.Count > 0)
                cbBloodTypeId.SelectedIndex = 0;

            ApplyPassportRule();
        }

        private void BindEntityToUI()
        {
            lblPatientID.Text = PatientID > 0 ? PatientID.ToString() : "[N/A]";
            lblPersonId.Text = PersonID > 0 ? PersonID.ToString() : "[N/A]";

            txtMedicalRecordNumber.Text = Patient.MedicalRecordNumber ?? "";

            SetComboSelectedValueSafe(cbBloodTypeId, Patient.BloodTypeId);

            txtEmergencyContactName.Text = Patient.EmergencyContactName ?? "";
            txtEmergencyPhone.Text = Patient.EmergencyContactPhone ?? "";
            txtNotes.Text = Patient.Notes ?? "";

            ApplyPassportRule();
        }

        private void MapUIToEntity()
        {
            Patient.MedicalRecordNumber = txtMedicalRecordNumber.Text.Trim();

            Patient.BloodTypeId = GetComboSelectedByteOrNull(cbBloodTypeId);

            Patient.EmergencyContactName = txtEmergencyContactName.Text.Trim();
            Patient.EmergencyContactPhone = txtEmergencyPhone.Text.Trim();
            Patient.Notes = txtNotes.Text.Trim();



        }

        private void ApplyMode()
        {
            bool view = (CurrentMode == enMode.View);
            bool editable = !view;

            // View mode: hide Save
            btnSave.Visible = !view;
            btnSave.Enabled = !view;

            txtMedicalRecordNumber.ReadOnly = !editable;
            cbBloodTypeId.Enabled = editable;
            txtEmergencyContactName.ReadOnly = !editable;
            txtEmergencyPhone.ReadOnly = !editable;
            txtNotes.ReadOnly = !editable;

            // edit link visible in view if patient exists
            linkEdit.Visible = (view && PatientID > 0);

            ApplyPassportRule();
        }

        private void ApplyPassportRule()
        {
            bool hasPerson = PersonID > 0;

            // PersonID is passport: if missing, disable everything and hide save
            this.Enabled = hasPerson; // If you have a pnlPatientDetails, set pnlPatientDetails.Enabled instead
            btnSave.Visible = hasPerson && (CurrentMode != enMode.View);

            if (!hasPerson)
                lblPersonId.Text = "[N/A]";
        }

        // =======================
        // VALIDATION
        // =======================
        private bool ValidateUI()
        {
            ClearErrors();
            bool ok = true;

           // Medical Record Number(optional) - but if you want it required, uncomment:
            if (string.IsNullOrWhiteSpace(txtMedicalRecordNumber.Text))
            {
                SetError(txtMedicalRecordNumber, "Medical record number is required.");
                ok = false;
            }

            // Blood type (optional) - if selected index 0 is placeholder, then allow; else validate as needed

            // Emergency contact: if one filled, require both + phone format minimal
            bool anyEmergency =
                !string.IsNullOrWhiteSpace(txtEmergencyContactName.Text) ||
                !string.IsNullOrWhiteSpace(txtEmergencyPhone.Text);

            if (anyEmergency)
            {
                if (string.IsNullOrWhiteSpace(txtEmergencyContactName.Text))
                {
                    SetError(txtEmergencyContactName, "Emergency contact name is required.");
                    ok = false;
                }

                if (string.IsNullOrWhiteSpace(txtEmergencyPhone.Text))
                {
                    SetError(txtEmergencyPhone, "Emergency phone is required.");
                    ok = false;
                }
                else if (!IsPhoneLike(txtEmergencyPhone.Text.Trim()))
                {
                    SetError(txtEmergencyPhone, "Invalid phone number.");
                    ok = false;
                }
            }

            // Focus first invalid
            if (!ok)
            {
                if (!string.IsNullOrEmpty(errorProvider1.GetError(txtMedicalRecordNumber))) txtMedicalRecordNumber.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(txtEmergencyContactName))) txtEmergencyContactName.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(txtEmergencyPhone))) txtEmergencyPhone.Focus();
            }

            return ok;
        }

        private static bool IsPhoneLike(string phone)
        {
            // simple guard: digits, space, +, -, (, )
            foreach (char c in phone)
            {
                if (!(char.IsDigit(c) || c == ' ' || c == '+' || c == '-' || c == '(' || c == ')'))
                    return false;
            }
            return phone.Length >= 6;
        }

        // =======================
        // DIRTY WIRING
        // =======================
        private void WireDirtyEvents()
        {
            txtMedicalRecordNumber.TextChanged += (_, __) =>
            {
                SetDirty(true);
                errorProvider1.SetError(txtMedicalRecordNumber, "");
            };

            cbBloodTypeId.SelectedIndexChanged += (_, __) => SetDirty(true);

            txtEmergencyContactName.TextChanged += (_, __) =>
            {
                SetDirty(true);
                errorProvider1.SetError(txtEmergencyContactName, "");
            };

            txtEmergencyPhone.TextChanged += (_, __) =>
            {
                SetDirty(true);
                errorProvider1.SetError(txtEmergencyPhone, "");
            };

            txtNotes.TextChanged += (_, __) => SetDirty(true);
        }

        // =======================
        // SMALL INPUT GUARDS
        // =======================
        private void WirePhoneGuards()
        {
            txtEmergencyPhone.KeyPress += (s, e) =>
            {
                // allow control keys
                if (char.IsControl(e.KeyChar)) return;

                // allow digits and phone chars
                char c = e.KeyChar;
                bool ok =
                    char.IsDigit(c) || c == '+' || c == '-' || c == ' ' || c == '(' || c == ')';

                e.Handled = !ok;
            };
        }

        // =======================
        // COMBO HELPERS
        // =======================
        private void LoadBloodTypes()
        {
            var list = BloodTypeService.GetAll().ToList();

            // Insert placeholder at top
            list.Insert(0, new BloodType
            {
                BloodTypeId = 0,
                Name = "-- Select Blood Type --"
            });

            cbBloodTypeId.DataSource = list;
            cbBloodTypeId.DisplayMember = "Name";
            cbBloodTypeId.ValueMember = "BloodTypeId";

            cbBloodTypeId.SelectedIndex = 0;
        }


        private byte? GetComboSelectedByteOrNull(ComboBox cb)
        {
            return cb.SelectedValue is byte id && id > 0
                ? id
                : null;
        }



        private void SetComboSelectedValueSafe(ComboBox cb, byte? value)
        {
            if (cb.DataSource == null) return;

            byte target = value.GetValueOrDefault(0);

            cb.SelectedValue = target;

            if (cb.SelectedIndex < 0)
                cb.SelectedValue = (byte)0;
        }





        private sealed class ComboItem
        {
            public int Value { get; set; }
            public string Text { get; set; } = "";
            public override string ToString() => Text;
        }

        // =======================
        // UI EVENTS
        // =======================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!SaveCurrent())
            {
                clsMessage.ShowError("Patient details failed to save.");
                return;
            }

            clsMessage.ShowSuccess("Patient details saved successfully.");
        }

        private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentMode = enMode.Edit;
        }


    }

}
