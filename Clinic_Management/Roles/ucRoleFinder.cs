using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Roles
{
    using Clinic_Management.Helpers;
    using Clinic_Management.UcHelpers;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities;
    using System;
    using System.Windows.Forms;

    public partial class ucRoleFinder : UserControl
    {
        // =========================
        // EVENTS (Delegation outward)
        // =========================
        public event Action<int>? OnRoleSelected;
        public event Action<int>? OnRoleSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int RoleID => ucRole1.RoleID;
        public Role Role => ucRole1.Role;

        // =========================
        // SERVICES
        // =========================
        private readonly RoleService _roleService = new();

        // =========================
        // CTOR
        // =========================
        public ucRoleFinder()
        {
            InitializeComponent();

            InitFinderBox();
            WireUp();
            this.ucRole1.CurrentMode = ucRole.enMode.View;
        }

        // =========================
        // INIT
        // =========================
        private void InitFinderBox()
        {
            ucFinderBox1.SetFilterByItems(
                "Role ID",
                "Code");

            ucFinderBox1.FilterValuePlaceholder = "Type value then press Enter...";
            ucFinderBox1.ShowAddNew = true;
            ucFinderBox1.InputMode = ucFinderBox.enInputMode.Any;
        }

        private void WireUp()
        {
            ucFinderBox1.FindClicked += DoFind;
            ucFinderBox1.AddNewClicked += DoAddNew;

            ucRole1.OnRoleCreated += id =>
            {
                OnRoleSaved?.Invoke(id);
                OnRoleSelected?.Invoke(id);

                ucRole1.LoadEntityData(id, ucRole.enMode.View);

                ucFinderBox1.FilterBySelectedIndex = 0; // Role ID
                ucFinderBox1.FilterValue = id.ToString();
            };
        }

        // =========================
        // ACTIONS
        // =========================
        private void DoAddNew()
        {
            ucRole1.LoadNew();

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
                // 1) Role ID
                // =========================
                if (filterBy == "Role ID")
                {
                    if (!int.TryParse(value, out int roleId) || roleId <= 0)
                    {
                        clsMessage.ShowWarning("Role ID must be a valid number.");
                        return;
                    }

                    ucRole1.LoadEntityData(roleId, ucRole.enMode.View);

                    if (ucRole1.RoleID <= 0)
                    {
                        clsMessage.ShowInfo("No role found.");
                        return;
                    }

                    OnRoleSelected?.Invoke(ucRole1.RoleID);
                    return;
                }

                // =========================
                // 2) Code
                // =========================
                if (filterBy == "Code")
                {
                    var res = _roleService.FindByCode(value.Trim());

                    if (!res.IsSuccess || res.Value == null || res.Value.RoleId <= 0)
                    {
                        clsMessage.ShowInfo("No role found.");
                        return;
                    }

                    ucRole1.LoadEntityData(res.Value.RoleId, ucRole.enMode.View);
                    OnRoleSelected?.Invoke(res.Value.RoleId);

                    ucFinderBox1.FilterBySelectedIndex = 0;
                    ucFinderBox1.FilterValue = res.Value.RoleId.ToString();
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
