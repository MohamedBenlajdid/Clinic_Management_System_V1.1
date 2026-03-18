using Clinic_Management.Permission;
using Clinic_Management.Roles;
using Clinic_Management.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.MainForm.ManageRolesUsersPermissions
{
    public partial class frmManageRolesUsersPermissions : Form
    {
        public frmManageRolesUsersPermissions()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmRole frm = new frmRole();
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmRoleFinder frm = new frmRoleFinder();
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmCreateUser frm = new frmCreateUser();
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmUserFinder frm = new frmUserFinder();
            frm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmPermission frm = new frmPermission();
            frm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmPermissionFinder frm = new frmPermissionFinder();
            frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmRolePermissions frm = new frmRolePermissions();
            frm.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmUserPermissionsOverride frm = new frmUserPermissionsOverride();
            frm.ShowDialog();
        }
    }
}
