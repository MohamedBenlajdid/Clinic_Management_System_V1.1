using Clinic_Management.Helpers;
using Clinic_Management.UcHelpers;
using Clinic_Management_BLL.Service;
using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Doctors
{
    public partial class ucDoctorFinder : UserControl
    {
        // =========================
        // EVENTS (Delegation outward)
        // =========================
        public event Action<int>? OnDoctorSelected;   // when found/loaded successfully
        public event Action<int>? OnDoctorSaved;      // when created/saved

        // =========================
        // EXPOSITION
        // =========================
        public int DoctorID => ucDoctor1.StaffID;
        public int PersonID => ucDoctor1.PersonID;
        public Doctor Doctor => ucDoctor1.Doctor;

        // =========================
        // SERVICES
        // =========================
        private readonly DoctorService _doctorService = new(); // Assuming a DoctorService exists
        private readonly StaffService _staffService = new();

        // =========================
        // CTOR
        // =========================
        public ucDoctorFinder()
        {
            InitializeComponent();

            InitFinderBox();
            WireUp();
            this.ucDoctor1.CurrentMode = ucDoctor.enMode.View;
        }

        // =========================
        // INIT
        // =========================
        private void InitFinderBox()
        {
            // Simple options (no extra classes)
            ucFinderBox1.SetFilterByItems(
                "Doctor ID",
                "Person ID",
                "Staff ID"); // Added Staff ID as per request

            ucFinderBox1.FilterValuePlaceholder = "Type value then press Enter...";
            ucFinderBox1.ShowAddNew = true;
            ucFinderBox1.InputMode = ucFinderBox.enInputMode.Any;
        }

        private void WireUp()
        {
            // Finder events
            ucFinderBox1.FindClicked += DoFind;
            ucFinderBox1.AddNewClicked += DoAddNew;

            // Doctor control events
            ucDoctor1.OnDoctorCreated += id =>
            {
                OnDoctorSaved?.Invoke(id);
                OnDoctorSelected?.Invoke(id);

                // after create, show in view mode
                ucDoctor1.LoadEntityData(id, ucDoctor.enMode.View);

                // update finder value to the created ID for clarity
                ucFinderBox1.FilterBySelectedIndex = 0; // Doctor ID
                ucFinderBox1.FilterValue = id.ToString();
            };
        }

        // =========================
        // ACTIONS
        // =========================
        private void DoAddNew()
        {
            // If you want AddNew to require PersonId first, keep it empty until found.
            ucDoctor1.LoadNew();

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
                // 1) Doctor ID (direct load)
                // =========================
                if (filterBy == "Doctor ID")
                {
                    if (!int.TryParse(value, out int doctorId) || doctorId <= 0)
                    {
                        clsMessage.ShowWarning("Doctor ID must be a valid number.");
                        return;
                    }

                    ucDoctor1.LoadEntityData(doctorId, ucDoctor.enMode.View);

                    if (ucDoctor1.StaffID <= 0)
                    {
                        clsMessage.ShowInfo("No doctor found.");
                        return;
                    }

                    OnDoctorSelected?.Invoke(ucDoctor1.StaffID);
                    return;
                }

                // =========================
                // 2) Person ID → Find Doctor by PersonId
                // =========================
                if (filterBy == "Person ID")
                {
                    if (!int.TryParse(value, out int personId) || personId <= 0)
                    {
                        clsMessage.ShowWarning("Person ID must be a valid number.");
                        return;
                    }

                    // You need this finder in service/DAL:
                    // Assuming FindByPersonId returns a Result<Doctor> with a Doctor.DoctorId
                    var res = _doctorService.GetByStaffId(
                        _staffService.GetByPersonID(PersonID).Value.StaffId);

                    if (!res.IsSuccess || res.Value == null || res.Value.StaffId <= 0)
                    {
                        clsMessage.ShowInfo("No doctor found for this person.");
                        return;
                    }

                    ucDoctor1.LoadEntityData(res.Value.StaffId, ucDoctor.enMode.View);

                    OnDoctorSelected?.Invoke(res.Value.StaffId);

                    ucFinderBox1.FilterBySelectedIndex = 0; // Doctor ID
                    ucFinderBox1.FilterValue = res.Value.StaffId.ToString();
                    return;
                }

                // =========================
                // 3) Staff ID → Find Doctor by Staff ID
                // =========================
                if (filterBy == "Staff ID")
                {
                    // Assuming Staff ID is an int, adjust if string
                    if (!int.TryParse(value, out int staffId) || staffId <= 0)
                    {
                        clsMessage.ShowWarning("Staff ID must be a valid number.");
                        return;
                    }

                    // You need this finder in service/DAL:
                    // Assuming FindByStaffId returns a Result<Doctor> with a Doctor.DoctorId
                    var res = _doctorService.GetByStaffId(staffId);

                    if (!res.IsSuccess || res.Value == null || res.Value.StaffId <= 0)
                    {
                        clsMessage.ShowInfo("No doctor found with this Staff ID.");
                        return;
                    }

                    ucDoctor1.LoadEntityData(res.Value.StaffId, ucDoctor.enMode.View);

                    OnDoctorSelected?.Invoke(res.Value.StaffId);

                    ucFinderBox1.FilterBySelectedIndex = 0; // Doctor ID
                    ucFinderBox1.FilterValue = res.Value.StaffId.ToString();
                    return;
                }
            }
            catch (Exception ex)
            {
                clsMessage.ShowError(ex.Message);
            }
        }
    }
}
