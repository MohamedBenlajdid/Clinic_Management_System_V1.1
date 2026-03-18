using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.DoctorDayOverride
{
    using Clinic_Management.Helpers;
    using Clinic_Management.UcHelpers;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities.Entities;
    using System;
    using System.Windows.Forms;

    public partial class ucDoctorDayOverrideFinder : UserControl
    {
        // =========================
        // EVENTS (Delegation outward)
        // =========================
        public event Action<int>? OnOverrideSelected;   // when found/loaded successfully
        public event Action<int>? OnOverrideSaved;      // when created/saved

        // =========================
        // EXPOSITION
        // =========================
        public int OverrideID => ucDoctorDayOverride1.OverrideID;
        public int DoctorID => ucDoctorDayOverride1.DoctorID;
        public DoctorDayOverride Override => ucDoctorDayOverride1.Override;

        // =========================
        // SERVICES
        // =========================
        private readonly DoctorDayOverrideService _overrideService = new();

        // =========================
        // CTOR
        // =========================
        public ucDoctorDayOverrideFinder()
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
            ucFinderBox1.SetFilterByItems(
                "Override ID",
                "Doctor ID",
                "Override Date");

            ucFinderBox1.FilterValuePlaceholder = "Type value then press Enter...";
            ucFinderBox1.ShowAddNew = true;
            ucFinderBox1.InputMode = ucFinderBox.enInputMode.Any;
        }

        private void WireUp()
        {
            // Finder events
            ucFinderBox1.FindClicked += DoFind;
            ucFinderBox1.AddNewClicked += DoAddNew;

            // Forward override control events
            ucDoctorDayOverride1.OnOverrideCreated += id =>
            {
                OnOverrideSaved?.Invoke(id);
                OnOverrideSelected?.Invoke(id);

                // After create → show in View mode
                ucDoctorDayOverride1.LoadEntityData(id, ucDoctorDayOverride.enMode.View);

                ucFinderBox1.FilterBySelectedIndex = 0; // Override ID
                ucFinderBox1.FilterValue = id.ToString();
            };
        }

        // =========================
        // ACTIONS
        // =========================
        private void DoAddNew()
        {
            //ucDoctorDayOverride1.LoadNew();

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
                // 1) Override ID
                // =========================
                if (filterBy == "Override ID")
                {
                    if (!int.TryParse(value, out int overrideId) || overrideId <= 0)
                    {
                        clsMessage.ShowWarning("Override ID must be a valid number.");
                        return;
                    }

                    ucDoctorDayOverride1.LoadEntityData(overrideId, ucDoctorDayOverride.enMode.View);

                    if (ucDoctorDayOverride1.OverrideID <= 0)
                    {
                        clsMessage.ShowInfo("No override found.");
                        return;
                    }

                    OnOverrideSelected?.Invoke(overrideId);
                    return;
                }

                //// =========================
                //// 2) Doctor ID
                //// =========================
                //if (filterBy == "Doctor ID")
                //{
                //    if (!int.TryParse(value, out int doctorId) || doctorId <= 0)
                //    {
                //        clsMessage.ShowWarning("Doctor ID must be a valid number.");
                //        return;
                //    }

                //    var res = _overrideService.FindFirstByDoctorId(doctorId);

                //    if (!res.IsSuccess || res.Value == null || res.Value.OverrideId <= 0)
                //    {
                //        clsMessage.ShowInfo("No override found for this doctor.");
                //        return;
                //    }

                //    ucDoctorDayOverride1.LoadEntityData(res.Value.OverrideId, ucDoctorDayOverride.enMode.View);

                //    OnOverrideSelected?.Invoke(res.Value.OverrideId);

                //    ucFinderBox1.FilterBySelectedIndex = 0;
                //    ucFinderBox1.FilterValue = res.Value.OverrideId.ToString();
                //    return;
                //}

                //// =========================
                //// 3) Override Date
                //// =========================
                //if (filterBy == "Override Date")
                //{
                //    if (!DateTime.TryParse(value, out DateTime date))
                //    {
                //        clsMessage.ShowWarning("Invalid date format.");
                //        return;
                //    }

                //    var res = _overrideService.FindByDate(date.Date);

                //    if (!res.IsSuccess || res.Value == null || res.Value.OverrideId <= 0)
                //    {
                //        clsMessage.ShowInfo("No override found for this date.");
                //        return;
                //    }

                //    ucDoctorDayOverride1.LoadEntityData(res.Value.OverrideId, ucDoctorDayOverride.enMode.View);

                //    OnOverrideSelected?.Invoke(res.Value.OverrideId);

                //    ucFinderBox1.FilterBySelectedIndex = 0;
                //    ucFinderBox1.FilterValue = res.Value.OverrideId.ToString();
                //    return;
                //}

                clsMessage.ShowWarning("Unknown filter option.");
            }
            catch (Exception ex)
            {
                clsMessage.ShowError(ex.Message);
            }
        }
    }

}
