using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Schedule
{
    using Clinic_Management.Helpers;
    using Clinic_Management.UcHelpers;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities.Entities;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public partial class ucDoctorScheduleFinder : UserControl
    {
        // =========================
        // EVENTS (Delegation outward)
        // =========================
        public event Action<int>? OnScheduleSelected;   // when found/loaded successfully
        public event Action<int>? OnScheduleSaved;      // when created/saved

        // =========================
        // EXPOSITION
        // =========================
        public int ScheduleID => ucDoctorSchedule1.ScheduleID;
        public int DoctorID => ucDoctorSchedule1.DoctorID;
        public DoctorSchedule Schedule => ucDoctorSchedule1.Schedule;

        // =========================
        // SERVICES
        // =========================
        private readonly DoctorScheduleService _scheduleService = new();

        // =========================
        // CTOR
        // =========================
        public ucDoctorScheduleFinder()
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
                "Schedule ID",
                "Doctor ID");

            ucFinderBox1.FilterValuePlaceholder = "Type value then press Enter...";
            ucFinderBox1.ShowAddNew = true;
            ucFinderBox1.InputMode = ucFinderBox.enInputMode.NumbersOnly; // IDs only
        }

        private void WireUp()
        {
            // Finder events
            ucFinderBox1.FindClicked += DoFind;
            ucFinderBox1.AddNewClicked += DoAddNew;

            // Schedule control events
            ucDoctorSchedule1.OnScheduleCreated += id =>
            {
                OnScheduleSaved?.Invoke(id);
                OnScheduleSelected?.Invoke(id);

                // after create, show in view mode
                ucDoctorSchedule1.LoadEntityData(id, ucDoctorSchedule.enMode.View);

                // update finder value to the created ID for clarity
                ucFinderBox1.FilterBySelectedIndex = 0; // Schedule ID
                ucFinderBox1.FilterValue = id.ToString();
            };
        }

        // =========================
        // ACTIONS
        // =========================
        private void DoAddNew()
        {
            // New schedule needs DoctorId; allow user to set it later
            ucDoctorSchedule1.LoadNew();

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
                // 1) Schedule ID (direct load)
                // =========================
                if (filterBy == "Schedule ID")
                {
                    if (!int.TryParse(value, out int scheduleId) || scheduleId <= 0)
                    {
                        clsMessage.ShowWarning("Schedule ID must be a valid number.");
                        return;
                    }

                    ucDoctorSchedule1.LoadEntityData(scheduleId, ucDoctorSchedule.enMode.View);

                    if (ucDoctorSchedule1.ScheduleID <= 0)
                    {
                        clsMessage.ShowInfo("No schedule found.");
                        return;
                    }

                    OnScheduleSelected?.Invoke(ucDoctorSchedule1.ScheduleID);
                    return;
                }

                // =========================
                // 2) Doctor ID → find a schedule for this doctor
                // =========================
                if (filterBy == "Doctor ID")
                {
                    if (!int.TryParse(value, out int doctorId) || doctorId <= 0)
                    {
                        clsMessage.ShowWarning("Doctor ID must be a valid number.");
                        return;
                    }

                    // You need a finder in service/DAL:
                    // Option A (recommended): return "latest active" schedule id, or first schedule
                    var res = _scheduleService.GetByDoctorId(doctorId);

                    if (!res.IsSuccess || res.Value == null || res.Value.First().ScheduleId <= 0)
                    {
                        clsMessage.ShowInfo("No schedule found for this doctor.");
                        return;
                    }

                    ucDoctorSchedule1.LoadEntityData(res.Value.First().ScheduleId, ucDoctorSchedule.enMode.View);

                    OnScheduleSelected?.Invoke(res.Value.First().ScheduleId);

                    // reflect found schedule id
                    ucFinderBox1.FilterBySelectedIndex = 0; // Schedule ID
                    ucFinderBox1.FilterValue = res.Value.First().ScheduleId.ToString();
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
