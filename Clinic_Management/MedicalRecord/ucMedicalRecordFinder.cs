using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.MedicalRecord
{
    using Clinic_Management.Helpers;
    using Clinic_Management.UcHelpers;
    using Clinic_Management_BLL.ResultWraper;
    using Clinic_Management_BLL.Service;
    using System;
    using System.Windows.Forms;

    public partial class ucMedicalRecordFinder : UserControl
    {
        // =========================
        // EVENTS (Delegation outward)
        // =========================
        public event Action<int>? OnMedicalRecordSelected; // when found/loaded successfully
        public event Action<int>? OnMedicalRecordSaved;    // when created/saved

        // =========================
        // EXPOSITION
        // =========================
        public int MedicalRecordID => ucMedicalRecord1.MedicalRecordID;
        public int AppointmentID => ucMedicalRecord1.AppointmentID;
        public int PatientID => ucMedicalRecord1.PatientID;
        public int DoctorID => ucMedicalRecord1.DoctorID;

        public Clinic_Management_Entities.Entities.MedicalRecord MedicalRecord => ucMedicalRecord1.MedicalRecord;

        // =========================
        // CTOR
        // =========================
        public ucMedicalRecordFinder()
        {
            InitializeComponent();

            InitFinderBox();
            WireUp();
        }

        // =========================
        // INIT
        // =========================
        private void InitFinderBox()
        {
            // Simple options (no extra classes)
            ucFinderBox1.SetFilterByItems(
                "Medical Record ID",
                "Appointment ID",
                "Patient ID",
                "Doctor ID");

            ucFinderBox1.FilterValuePlaceholder = "Type value then press Enter...";
            ucFinderBox1.ShowAddNew = true;
            ucFinderBox1.InputMode = ucFinderBox.enInputMode.Any;
        }

        private void WireUp()
        {
            // Finder events
            ucFinderBox1.FindClicked += DoFind;
            ucFinderBox1.AddNewClicked += DoAddNew;

            // MedicalRecord control events
            ucMedicalRecord1.OnMedicalRecordCreated += id =>
            {
                OnMedicalRecordSaved?.Invoke(id);
                OnMedicalRecordSelected?.Invoke(id);

                // after create, show in view mode
                ucMedicalRecord1.LoadEntityData(id, ucMedicalRecord.enMode.View);

                // update finder value to the created ID for clarity
                ucFinderBox1.FilterBySelectedIndex = 0; // Medical Record ID
                ucFinderBox1.FilterValue = id.ToString();
            };
        }

        // =========================
        // ACTIONS
        // =========================
        private void DoAddNew()
        {
            ucMedicalRecord1.LoadNew();
            ucFinderBox1.FilterValue = "";
            ucFinderBox1.FocusFilterValue();
        }

        private void DoFind()
        {
            string filterBy = ucFinderBox1.FilterBySelectedText;
            string value = ucFinderBox1.FilterValue;

            if (string.IsNullOrWhiteSpace(value))
            {
                clsMessage.ShowWarning("Enter a value to search.");
                ucFinderBox1.FocusFilterValue();
                return;
            }

            try
            {
                var service = new MedicalRecordService();

                // Decide search type
                if (filterBy == "Medical Record ID")
                {
                    if (!int.TryParse(value, out int medicalRecordId) || medicalRecordId <= 0)
                    {
                        clsMessage.ShowWarning("Medical Record ID must be a valid number.");
                        return;
                    }

                    ucMedicalRecord1.LoadEntityData(medicalRecordId, ucMedicalRecord.enMode.View);

                    if (ucMedicalRecord1.MedicalRecordID <= 0)
                    {
                        clsMessage.ShowInfo("No medical record found.");
                        return;
                    }

                    OnMedicalRecordSelected?.Invoke(ucMedicalRecord1.MedicalRecordID);
                    return;
                }

                // For other filters: find first matching record, then load by ID
                Result<Clinic_Management_Entities.Entities.MedicalRecord> r = null;

                //if (filterBy == "Appointment ID")
                //    r = service.FindByAppointmentId(ParsePositiveInt(value, "Appointment ID"));
                //else if (filterBy == "Patient ID")
                //    r = service.FindByPatientId(ParsePositiveInt(value, "Patient ID"));
                //else if (filterBy == "Doctor ID")
                //    r = service.FindByDoctorId(ParsePositiveInt(value, "Doctor ID"));

                if (r?.Value == null || r.Value.MedicalRecordId <= 0)
                {
                    clsMessage.ShowInfo("No medical record found.");
                    return;
                }

                ucMedicalRecord1.LoadEntityData(r.Value.MedicalRecordId, ucMedicalRecord.enMode.View);
                OnMedicalRecordSelected?.Invoke(r.Value.MedicalRecordId);

                // update UI to reflect found id
                ucFinderBox1.FilterBySelectedIndex = 0;
                ucFinderBox1.FilterValue = r.Value.MedicalRecordId.ToString();
            }
            catch (Exception ex)
            {
                clsMessage.ShowError(ex.Message);
            }
        }

        private int ParsePositiveInt(string value, string label)
        {
            if (!int.TryParse(value, out int id) || id <= 0)
                throw new ArgumentException($"{label} must be a valid number.");
            return id;
        }
    }
}
