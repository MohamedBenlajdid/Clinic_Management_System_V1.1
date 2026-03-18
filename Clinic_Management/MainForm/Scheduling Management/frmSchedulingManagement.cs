using Clinic_Management.DoctorDayOverride;
using Clinic_Management.DoctorOverrideSession;
using Clinic_Management.Schedule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.MainForm.Scheduling_Management
{
    public partial class frmSchedulingManagement : Form
    {
        public frmSchedulingManagement()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmDoctorSchedule frm = new frmDoctorSchedule();
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmDoctorScheduleFinder frm = new frmDoctorScheduleFinder();
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmDoctorDayOverride frm = new frmDoctorDayOverride();
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmDoctorDayOverrideFinder frm = new frmDoctorDayOverrideFinder();
            frm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmDoctorOverrideSession frm = new frmDoctorOverrideSession();
            frm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmDoctorOverrideSessionFinder frm = new frmDoctorOverrideSessionFinder();  
            frm.ShowDialog();
        }
    }
}
