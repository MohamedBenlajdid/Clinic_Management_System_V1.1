using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Patients
{
    using Clinic_Management.Helpers;
    using Clinic_Management.UcHelpers;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities;
    using System;
    using System.Windows.Forms;

    public partial class ucPatientFinder : UserControl
    {
        // =========================
        // EVENTS (Delegation outward)
        // =========================
        public event Action<int>? OnPatientSelected;   // when found/loaded successfully
        public event Action<int>? OnPatientSaved;      // when created/saved

        // =========================
        // EXPOSITION
        // =========================
        public int PatientID => ucPatient1.PatientID;
        public int PersonID => ucPatient1.PersonID;
        public Patient Patient => ucPatient1.Patient;

        // =========================
        // SERVICES
        // =========================
        private readonly PatientService _patientService = new();

        // =========================
        // CTOR
        // =========================
        public ucPatientFinder()
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
                "Patient ID",
                "Person ID",
                "Medical Record Number");

            ucFinderBox1.FilterValuePlaceholder = "Type value then press Enter...";
            ucFinderBox1.ShowAddNew = true;
            ucFinderBox1.InputMode = ucFinderBox.enInputMode.Any;
        }

        private void WireUp()
        {
            // Finder events
            ucFinderBox1.FindClicked += DoFind;
            ucFinderBox1.AddNewClicked += DoAddNew;

            // Patient control events
            ucPatient1.OnPatientCreated += id =>
            {
                OnPatientSaved?.Invoke(id);
                OnPatientSelected?.Invoke(id);

                // after create, show in view mode
                ucPatient1.LoadEntityData(id, ucPatient.enMode.View);

                // update finder value to the created ID for clarity
                ucFinderBox1.FilterBySelectedIndex = 0; // Patient ID
                ucFinderBox1.FilterValue = id.ToString();
            };
        }

        // =========================
        // ACTIONS
        // =========================
        private void DoAddNew()
        {
            // Passport: patient must have PersonID (disable control until person chosen)
            ucPatient1.LoadNew(0);

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
                // =========================
                // 1) Patient ID (direct load)
                // =========================
                if (filterBy == "Patient ID")
                {
                    if (!int.TryParse(value, out int patientId) || patientId <= 0)
                    {
                        clsMessage.ShowWarning("Patient ID must be a valid number.");
                        return;
                    }

                    ucPatient1.LoadEntityData(patientId, ucPatient.enMode.View);

                    if (ucPatient1.PatientID <= 0)
                    {
                        clsMessage.ShowInfo("No patient found.");
                        return;
                    }

                    OnPatientSelected?.Invoke(ucPatient1.PatientID);
                    return;
                }

                // =========================
                // 2) Person ID → Find Patient by PersonId
                // =========================
                if (filterBy == "Person ID")
                {
                    if (!int.TryParse(value, out int personId) || personId <= 0)
                    {
                        clsMessage.ShowWarning("Person ID must be a valid number.");
                        return;
                    }

                    // You need this finder in service/DAL:
                    var res = _patientService.FindByPersonId(personId);

                    if (!res.IsSuccess || res.Value == null || res.Value.PatientId <= 0)
                    {
                        clsMessage.ShowInfo("No patient found for this person.");
                        return;
                    }

                    ucPatient1.LoadEntityData(res.Value.PatientId, ucPatient.enMode.View);

                    OnPatientSelected?.Invoke(res.Value.PatientId);

                    ucFinderBox1.FilterBySelectedIndex = 0; // Patient ID
                    ucFinderBox1.FilterValue = res.Value.PatientId.ToString();
                    return;
                }

                // =========================
                // 3) Medical Record Number → Find Patient
                // =========================
                if (filterBy == "Medical Record Number")
                {
                    var res = _patientService.FindByMedicalRecordNumber(value.Trim()); // implement in service/DAL

                    if (!res.IsSuccess || res.Value == null || res.Value.PatientId <= 0)
                    {
                        clsMessage.ShowInfo("No patient found.");
                        return;
                    }

                    ucPatient1.LoadEntityData(res.Value.PatientId, ucPatient.enMode.View);
                    OnPatientSelected?.Invoke(res.Value.PatientId);

                    // reflect found patient id
                    ucFinderBox1.FilterBySelectedIndex = 0; // Patient ID
                    ucFinderBox1.FilterValue = res.Value.PatientId.ToString();
                    return;
                }

                clsMessage.ShowWarning("Unknown filter option.");
            }
            catch (Exception ex)
            {
                clsMessage.ShowError(ex.Message);
            }
        }
    }



}
