using Clinic_Management.MainForm.AppointmentProcess;
using Clinic_Management.MainForm.DiagnosticResultsPrecess;
using Clinic_Management.MainForm.ManageRolesUsersPermissions;
using Clinic_Management.MainForm.ManageSystemMembers;
using Clinic_Management.MainForm.Scheduling_Management;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.MainForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmManageRolesUsersPermissions frm = new frmManageRolesUsersPermissions();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmManageSystemMemebers frm = new frmManageSystemMemebers();
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmSchedulingManagement frm = new frmSchedulingManagement();
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmAppointmentProcess frm = new frmAppointmentProcess();
            frm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmDiagnosticsProcess frm =
                new frmDiagnosticsProcess();
            frm.ShowDialog();
        }
    }

}
