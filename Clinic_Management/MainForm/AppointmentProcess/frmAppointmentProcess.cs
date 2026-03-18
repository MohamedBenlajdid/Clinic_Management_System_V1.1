using Clinic_Management.Appointment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.MainForm.AppointmentProcess
{
    public partial class frmAppointmentProcess : Form
    {
        public frmAppointmentProcess()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAppointment frm = new frmAppointment();
            frm.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmAppointmentTable frm = new frmAppointmentTable();
            frm.ShowDialog();
        }




    }



}
