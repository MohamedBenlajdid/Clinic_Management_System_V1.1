using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Permission
{
    using Clinic_Management_Entities;
    using System;
    using System.Windows.Forms;

    public partial class frmPermission : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnPermissionSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int PermissionID => this.ucPermission1.PermissionID;
        public Permission Permission => this.ucPermission1.Permission;
        public ucPermission.enMode Mode => this.ucPermission1.CurrentMode;

        // =========================
        // CTORS
        // =========================

        // ➕ Create new Permission
        public frmPermission()
        {
            InitializeComponent();

            WireUp();

            this.ucPermission1.LoadNew();
        }

        // 👁 / ✏ View or Edit existing Permission
        public frmPermission(int permissionID, ucPermission.enMode mode = ucPermission.enMode.View)
        {
            InitializeComponent();

            WireUp();

            this.ucPermission1.LoadEntityData(permissionID, mode);
        }

        // =========================
        // INTERNAL WIRING
        // =========================
        private void WireUp()
        {
            // Forward UC event → Form event
            this.ucPermission1.OnPermissionCreated += RaisePermissionSaved;

            // Optional: close behavior / unsaved guard
            this.FormClosing += FrmPermission_FormClosing;
        }

        // =========================
        // EVENT FORWARDER
        // =========================
        private void RaisePermissionSaved(int permissionId)
        {
            // Always trust UC as source of truth
            this.OnPermissionSaved?.Invoke(this.ucPermission1.PermissionID);

            // Optional auto close:
            // this.DialogResult = DialogResult.OK;
            // this.Close();
        }

        // =========================
        // CLOSE BEHAVIOR (OPTIONAL)
        // =========================
        private void FrmPermission_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // If needed later: unsaved changes guard
            // if (ucPermission1.IsDirty)
            // {
            //     var leave = clsMessage.Confirm("You have unsaved changes. Close anyway?");
            //     if (!leave) e.Cancel = true;
            // }
        }
    }




}
