using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Roles
{
    using Clinic_Management_Entities;
    using System;
    using System.Windows.Forms;

    public partial class frmRole : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnRoleSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int RoleID => this.ucRole1.RoleID;
        public Role Role => this.ucRole1.Role;
        public ucRole.enMode Mode => this.ucRole1.CurrentMode;

        // =========================
        // CTORS
        // =========================

        // ➕ Create new Role
        public frmRole()
        {
            InitializeComponent();

            WireUp();

            this.ucRole1.LoadNew();
        }

        // 👁 / ✏ View or Edit existing Role
        public frmRole(int roleID, ucRole.enMode mode = ucRole.enMode.View)
        {
            InitializeComponent();

            WireUp();

            this.ucRole1.LoadEntityData(roleID, mode);
        }

        // =========================
        // INTERNAL WIRING
        // =========================
        private void WireUp()
        {
            // Forward UC event → Form event
            this.ucRole1.OnRoleCreated += RaiseRoleSaved;

            // Optional: close behavior / unsaved guard
            this.FormClosing += FrmRole_FormClosing;
        }

        // =========================
        // EVENT FORWARDER
        // =========================
        private void RaiseRoleSaved(int roleId)
        {
            // Always trust UC as source of truth
            this.OnRoleSaved?.Invoke(this.ucRole1.RoleID);

            // Optional auto close:
            // this.DialogResult = DialogResult.OK;
            // this.Close();
        }

        // =========================
        // CLOSE BEHAVIOR (OPTIONAL)
        // =========================
        private void FrmRole_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // If needed later: unsaved changes guard
            // if (ucRole1.IsDirty)
            // {
            //     var leave = clsMessage.Confirm("You have unsaved changes. Close anyway?");
            //     if (!leave) e.Cancel = true;
            // }
        }
    }


}
