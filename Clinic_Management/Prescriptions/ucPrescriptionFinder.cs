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
    using Clinic_Management.UcHelpers;
    using Clinic_Management_BLL.ResultWraper;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities.Entities;
    using System;
    using System.Windows.Forms;

    public partial class ucPrescriptionFinder : UserControl
    {
        // =========================
        // EVENTS (Delegation outward)
        // =========================
        public event Action<int>? OnPrescriptionSelected; // when found/loaded successfully
        public event Action<int>? OnPrescriptionSaved;    // when created/saved

        // =========================
        // EXPOSITION
        // =========================
        public int PrescriptionID => ucPrescription1.PrescriptionID;
        public int AppointmentID => ucPrescription1.AppointmentID;
        public int PatientID => ucPrescription1.PatientID;
        public int DoctorID => ucPrescription1.DoctorID;

        public Prescription Prescription => ucPrescription1.Prescription;

        // =========================
        // CTOR
        // =========================
        public ucPrescriptionFinder()
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
                "Prescription ID",
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

            // Prescription control events
            ucPrescription1.OnPrescriptionCreated += id =>
            {
                OnPrescriptionSaved?.Invoke(id);
                OnPrescriptionSelected?.Invoke(id);

                // after create, show in view mode
                ucPrescription1.LoadEntityData(id, ucPrescription.enMode.View);

                // update finder value to the created ID for clarity
                ucFinderBox1.FilterBySelectedIndex = 0; // Prescription ID
                ucFinderBox1.FilterValue = id.ToString();
            };
        }

        // =========================
        // ACTIONS
        // =========================
        private void DoAddNew()
        {
            ucPrescription1.LoadNew();
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
                var service = new PrescriptionService();

                // Decide search type
                if (filterBy == "Prescription ID")
                {
                    if (!int.TryParse(value, out int prescriptionId) || prescriptionId <= 0)
                    {
                        clsMessage.ShowWarning("Prescription ID must be a valid number.");
                        return;
                    }

                    ucPrescription1.LoadEntityData(prescriptionId, ucPrescription.enMode.View);

                    if (ucPrescription1.PrescriptionID <= 0)
                    {
                        clsMessage.ShowInfo("No prescription found.");
                        return;
                    }

                    OnPrescriptionSelected?.Invoke(ucPrescription1.PrescriptionID);
                    return;
                }

                // For other filters: find first matching prescription, then load by ID
                Result<Prescription> r = null;

                //if (filterBy == "Appointment ID")
                //    r = service.FindByAppointmentId(ParsePositiveInt(value, "Appointment ID"));
                //else if (filterBy == "Patient ID")
                //    r = service.FindByPatientId(ParsePositiveInt(value, "Patient ID"));
                //else if (filterBy == "Doctor ID")
                //    r = service.FindByDoctorId(ParsePositiveInt(value, "Doctor ID"));

                if (r?.Value == null || r.Value.PrescriptionId <= 0)
                {
                    clsMessage.ShowInfo("No prescription found.");
                    return;
                }

                ucPrescription1.LoadEntityData(r.Value.PrescriptionId, ucPrescription.enMode.View);
                OnPrescriptionSelected?.Invoke(r.Value.PrescriptionId);

                // update UI to reflect found id
                ucFinderBox1.FilterBySelectedIndex = 0;
                ucFinderBox1.FilterValue = r.Value.PrescriptionId.ToString();
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
