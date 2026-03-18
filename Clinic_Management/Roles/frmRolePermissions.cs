using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Roles
{
    using Clinic_Management_BLL.Service;
    using System;
    using System.Windows.Forms;

    public partial class frmRolePermissions : Form
    {
        // =========================
        // DATA (Passport)
        // =========================
        public int RoleID { get; private set; } = -1;
        public RoleService roleService = new RoleService();
        // =========================
        // CTORs
        // =========================
        public frmRolePermissions()
        {
            InitializeComponent();
            InitializeForm();
        }

        public frmRolePermissions(int roleId)
        {
            InitializeComponent();
            RoleID = roleId;
            InitializeForm();
        }

        // =========================
        // INIT
        // =========================
        private void InitializeForm()
        {
            // Assume you already dropped these 2 user controls on the form:
            // - ucRoleDetails1
            // - ucPermissionsTable1
            //
            // And you have:
            // - btnFindRole (optional)
            // - btnClose

            this.Load += frmRolePermissions_Load;

            // Nice: when permissions changed, you can enable a Save button if you have one in the form
            // (but ucPermissionsTable already has btnSave internally if you added it)
            // ucPermissionsTable1.DirtyChanged += (dirty) => btnSave.Enabled = dirty;

            ApplyState();
        }

        private void frmRolePermissions_Load(object? sender, EventArgs e)
        {
            if (this.DesignMode) return;

            if (RoleID <= 0)
            {
                if (!PickRole())
                {
                    // user cancelled selecting role
                    this.Close();
                    return;
                }
            }

            LoadRole(RoleID);
        }

        // =========================
        // PICK ROLE
        // =========================
        private bool PickRole()
        {
            using var frm = new frmRoleFinder(); // You must have this form

            // expected behavior:
            // frm.OnRoleSelected += id => { RoleID = id; frm.Close(); };
            // Here is a safe generic pattern:

            int selectedId = -1;

            frm.OnRoleSelected += (id) =>
            {
                selectedId = id;
                frm.Close();
            };

            frm.ShowDialog();

            if (selectedId > 0)
            {
                RoleID = selectedId;
                return true;
            }

            return false;
        }

        // =========================
        // LOAD ROLE + PERMISSIONS
        // =========================
        private void LoadRole(int roleId)
        {
            RoleID = roleId;

            // 1) Role details (read-only or view mode)
            // If your ucRoleDetails has LoadEntityData(RoleID, Mode):
            Clinic_Management_Entities.Role  role = roleService.GetById(RoleID).Value;
            ucRoleDetails1.LoadEntityData(role);

            // 2) Permissions table in ROLE mode
            ucPermissionsTable1.LoadForRole(roleId);

            ApplyState();
        }

        // =========================
        // UI STATE
        // =========================
        private void ApplyState()
        {
            bool hasRole = RoleID > 0;

            // If you have labels on form:
            // lblRoleId.Text = hasRole ? RoleID.ToString() : "[???]";

            ucRoleDetails1.Enabled = hasRole;          // if you want it disabled until role selected
            ucPermissionsTable1.Enabled = hasRole;     // same
        }

        // =========================
        // OPTIONAL BUTTONS (if exist)
        // =========================
        private void btnFindRole_Click(object sender, EventArgs e)
        {
            if (!PickRole())
                return;

            LoadRole(RoleID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }

}
