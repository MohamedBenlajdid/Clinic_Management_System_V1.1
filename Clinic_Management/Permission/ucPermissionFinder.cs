using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Permission
{
    using Clinic_Management.Helpers;
    using Clinic_Management.UcHelpers;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities;
    using System;
    using System.Windows.Forms;

    public partial class ucPermissionFinder : UserControl
    {
        // =========================
        // EVENTS (Delegation outward)
        // =========================
        public event Action<int>? OnPermissionSelected;
        public event Action<int>? OnPermissionSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int PermissionID => ucPermission1.PermissionID;
        public Permission Permission => ucPermission1.Permission;

        // =========================
        // SERVICES
        // =========================
        private readonly PermissionService _permissionService = new();

        // =========================
        // CTOR
        // =========================
        public ucPermissionFinder()
        {
            InitializeComponent();

            InitFinderBox();
            WireUp();
            this.ucPermission1.CurrentMode = ucPermission.enMode.View;
        }

        // =========================
        // INIT
        // =========================
        private void InitFinderBox()
        {
            ucFinderBox1.SetFilterByItems(
                "Permission ID",
                "Permission Code");

            ucFinderBox1.FilterValuePlaceholder = "Type value then press Enter...";
            ucFinderBox1.ShowAddNew = true;
            ucFinderBox1.InputMode = ucFinderBox.enInputMode.Any;
        }

        private void WireUp()
        {
            // Finder events
            ucFinderBox1.FindClicked += DoFind;
            ucFinderBox1.AddNewClicked += DoAddNew;

            // Permission control events
            ucPermission1.OnPermissionCreated += id =>
            {
                OnPermissionSaved?.Invoke(id);
                OnPermissionSelected?.Invoke(id);

                // after create, show in view mode
                ucPermission1.LoadEntityData(id, ucPermission.enMode.View);

                // update finder value to the created ID for clarity
                ucFinderBox1.FilterBySelectedIndex = 0; // Permission ID
                ucFinderBox1.FilterValue = id.ToString();
            };
        }

        // =========================
        // ACTIONS
        // =========================
        private void DoAddNew()
        {
            ucPermission1.LoadNew();

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
                // 1) Permission ID
                // =========================
                if (filterBy == "Permission ID")
                {
                    if (!int.TryParse(value, out int permissionId) || permissionId <= 0)
                    {
                        clsMessage.ShowWarning("Permission ID must be a valid number.");
                        return;
                    }

                    ucPermission1.LoadEntityData(permissionId, ucPermission.enMode.View);

                    if (ucPermission1.PermissionID <= 0)
                    {
                        clsMessage.ShowInfo("No permission found.");
                        return;
                    }

                    OnPermissionSelected?.Invoke(ucPermission1.PermissionID);
                    return;
                }

                // =========================
                // 2) Permission Code
                // =========================
                if (filterBy == "Permission Code")
                {
                    var res = _permissionService.FindByCode(value.Trim()); // implement in service/DAL

                    if (!res.IsSuccess || res.Value == null || res.Value.PermissionId <= 0)
                    {
                        clsMessage.ShowInfo("No permission found.");
                        return;
                    }

                    ucPermission1.LoadEntityData(res.Value.PermissionId, ucPermission.enMode.View);
                    OnPermissionSelected?.Invoke(res.Value.PermissionId);

                    // reflect found id
                    ucFinderBox1.FilterBySelectedIndex = 0;
                    ucFinderBox1.FilterValue = res.Value.PermissionId.ToString();
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
