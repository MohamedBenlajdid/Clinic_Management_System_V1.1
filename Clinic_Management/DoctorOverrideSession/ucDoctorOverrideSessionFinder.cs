using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.DoctorOverrideSession
{
    using Clinic_Management.Helpers;
    using Clinic_Management.UcHelpers;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities.Entities;
    using System;
    using System.Windows.Forms;

    public partial class ucDoctorOverrideSessionFinder : UserControl
    {
        // =========================
        // EVENTS (Delegation outward)
        // =========================
        public event Action<int>? OnSessionSelected;
        public event Action<int>? OnSessionSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int SessionID => ucDoctorOverrideSession1.SessionID;
        public int OverrideID => ucDoctorOverrideSession1.OverrideID;
        public DoctorDayOverrideSession Session => ucDoctorOverrideSession1.Session;

        // =========================
        // SERVICES
        // =========================
        private readonly DoctorOverrideSessionService _sessionService = new();

        // =========================
        // CTOR
        // =========================
        public ucDoctorOverrideSessionFinder()
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
                "Session ID",
                "Override ID");

            ucFinderBox1.FilterValuePlaceholder = "Type value then press Enter...";
            ucFinderBox1.ShowAddNew = true;
            ucFinderBox1.InputMode = ucFinderBox.enInputMode.NumbersOnly;
        }

        private void WireUp()
        {
            // Finder events
            ucFinderBox1.FindClicked += DoFind;
            ucFinderBox1.AddNewClicked += DoAddNew;

            // Forward session creation event
            ucDoctorOverrideSession1.OnSessionCreated += id =>
            {
                OnSessionSaved?.Invoke(id);
                OnSessionSelected?.Invoke(id);

                // After create → show View mode
                ucDoctorOverrideSession1.LoadEntityData(id, ucDoctorOverrideSession.enMode.View);

                // Update finder UI
                ucFinderBox1.FilterBySelectedIndex = 0; // Session ID
                ucFinderBox1.FilterValue = id.ToString();
            };
        }

        // =========================
        // ACTIONS
        // =========================
        private void DoAddNew()
        {
            ucDoctorOverrideSession1.LoadNew();

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
                // 1) Session ID
                // =========================
                if (filterBy == "Session ID")
                {
                    if (!int.TryParse(value, out int sessionId) || sessionId <= 0)
                    {
                        clsMessage.ShowWarning("Session ID must be a valid number.");
                        return;
                    }

                    ucDoctorOverrideSession1.LoadEntityData(sessionId, ucDoctorOverrideSession.enMode.View);

                    if (ucDoctorOverrideSession1.SessionID <= 0)
                    {
                        clsMessage.ShowInfo("No session found.");
                        return;
                    }

                    OnSessionSelected?.Invoke(sessionId);
                    return;
                }

                // =========================
                // 2) Override ID → find first session
                // =========================
                if (filterBy == "Override ID")
                {
                    if (!int.TryParse(value, out int overrideId) || overrideId <= 0)
                    {
                        clsMessage.ShowWarning("Override ID must be a valid number.");
                        return;
                    }

                    var res = _sessionService.GetByOverrideId(overrideId);

                    if (!res.IsSuccess || res.Value == null || res.Value.First().SessionId <= 0)
                    {
                        clsMessage.ShowInfo("No session found for this override.");
                        return;
                    }

                    ucDoctorOverrideSession1.LoadEntityData(res.Value.First().SessionId, ucDoctorOverrideSession.enMode.View);

                    OnSessionSelected?.Invoke(res.Value.First().SessionId);

                    // Reflect found session ID
                    ucFinderBox1.FilterBySelectedIndex = 0;
                    ucFinderBox1.FilterValue = res.Value.First().SessionId.ToString();
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
