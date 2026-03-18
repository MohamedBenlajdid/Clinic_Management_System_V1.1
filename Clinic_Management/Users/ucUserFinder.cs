using Clinic_Management.Helpers;
using Clinic_Management.UcHelpers;
using Clinic_Management_BLL.ResultWraper;
using Clinic_Management_BLL.Service;
using Clinic_Management_DAL.Data;
using Clinic_Management_Entities;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Users
{
    public partial class ucUserFinder : UserControl
    {
        // =========================
        // EVENTS (Delegation outward)
        // =========================
        public event Action<int>? OnUserSelected;   // when found/loaded successfully
        public event Action<int>? OnUserSaved;      // when created/saved

        // =========================
        // EXPOSITION
        // =========================
        public int UserID => ucUser1.UserID;
        public int PersonID => ucUser1.PersonID;
        public User User => ucUser1.User;

        // =========================
        // SERVICES
        // =========================
        private readonly UserService _userService = new();

        // =========================
        // CTOR
        // =========================
        public ucUserFinder()
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
                "User ID",
                "Person ID",
                "Username",
                "Email");

            ucFinderBox1.FilterValuePlaceholder = "Type value then press Enter...";
            ucFinderBox1.ShowAddNew = true;
            ucFinderBox1.InputMode = ucFinderBox.enInputMode.Any;
        }

        private void WireUp()
        {
            // Finder events
            ucFinderBox1.FindClicked += DoFind;
            ucFinderBox1.AddNewClicked += DoAddNew;

            // User control events
            ucUser1.OnUserCreated += id =>
            {
                OnUserSaved?.Invoke(id);
                OnUserSelected?.Invoke(id);

                // after create, show in view mode
                ucUser1.LoadEntityData(id, ucUser.enMode.View);

                // update finder value to the created ID for clarity
                ucFinderBox1.FilterBySelectedIndex = 0; // User ID
                ucFinderBox1.FilterValue = id.ToString();
            };
        }

        // =========================
        // ACTIONS
        // =========================
        private void DoAddNew()
        {
            // If you want AddNew to require PersonId first, keep it empty until found.
            ucUser1.LoadNew(0);

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
                // 1) User ID (direct load)
                // =========================
                if (filterBy == "User ID")
                {
                    if (!int.TryParse(value, out int userId) || userId <= 0)
                    {
                        clsMessage.ShowWarning("User ID must be a valid number.");
                        return;
                    }

                    ucUser1.LoadEntityData(userId, ucUser.enMode.View);

                    if (ucUser1.UserID <= 0)
                    {
                        clsMessage.ShowInfo("No user found.");
                        return;
                    }

                    OnUserSelected?.Invoke(ucUser1.UserID);
                    return;
                }

                // =========================
                // 2) Person ID → Find User by PersonId
                // =========================
                if (filterBy == "Person ID")
                {
                    if (!int.TryParse(value, out int personId) || personId <= 0)
                    {
                        clsMessage.ShowWarning("Person ID must be a valid number.");
                        return;
                    }

                    // You need this finder in service/DAL:
                    var res = _userService.FindByPersonId(personId);

                    if (!res.IsSuccess || res.Value == null || res.Value.UserId <= 0)
                    {
                        clsMessage.ShowInfo("No user found for this person.");
                        return;
                    }

                    ucUser1.LoadEntityData(res.Value.UserId, ucUser.enMode.View);

                    OnUserSelected?.Invoke(res.Value.UserId);

                    ucFinderBox1.FilterBySelectedIndex = 0; // User ID
                    ucFinderBox1.FilterValue = res.Value.UserId.ToString();
                    return;
                }

                // =========================
                // 3) Username / Email → Find User
                // =========================
                Result<User>? found = null;

                if (filterBy == "Username")
                    found = _userService.FindByUsername(value.Trim()); // implement in service/DAL
                else if (filterBy == "Email")
                    found = _userService.FindByEmail(value.Trim());    // implement in service/DAL

                if (found == null || !found.IsSuccess || found.Value == null || found.Value.UserId <= 0)
                {
                    clsMessage.ShowInfo("No user found.");
                    return;
                }

                ucUser1.LoadEntityData(found.Value.UserId, ucUser.enMode.View);
                OnUserSelected?.Invoke(found.Value.UserId);

                // reflect found user id
                ucFinderBox1.FilterBySelectedIndex = 0;
                ucFinderBox1.FilterValue = found.Value.UserId.ToString();
            }
            catch (Exception ex)
            {
                clsMessage.ShowError(ex.Message);
            }
        }
    }

}
