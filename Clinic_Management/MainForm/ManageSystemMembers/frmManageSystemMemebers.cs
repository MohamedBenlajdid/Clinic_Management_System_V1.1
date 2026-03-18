using Clinic_Management.Doctors;
using Clinic_Management.Helpers;
using Clinic_Management.Patients;
using Clinic_Management.Person;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.MainForm.ManageSystemMembers
{
    public partial class frmManageSystemMemebers : Form
    {
        public frmManageSystemMemebers()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int personId = -1;

            while (true)
            {
                using (var frmPerson = new frmPersonFinder())
                {
                    var result = frmPerson.ShowDialog();

                    if (result == DialogResult.Cancel)
                        return; // Exit completely

                    personId = frmPerson.PersonID;

                    if (personId > 0)
                        break;

                    clsMessage.ShowWarning("Person ID is required.");
                }
            }

            // Only reaches here if valid person selected
            using (var frm = new frmPatient(personId))
            {
                frm.ShowDialog();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmPatientFinder frmPatient = new frmPatientFinder();
            frmPatient.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int personId = -1;

            while (true)
            {
                using (var frmPerson = new frmPersonFinder())
                {
                    var result = frmPerson.ShowDialog();

                    if (result == DialogResult.Cancel)
                        return; // Exit completely

                    personId = frmPerson.PersonID;

                    if (personId > 0)
                        break;

                    clsMessage.ShowWarning("Person ID is required.");
                }
            }

            // Only reaches here if valid person selected
            using (var frm = new frmDoctor(personId))
            {
                frm.ShowDialog();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmDoctorFinder frmDoctor = new frmDoctorFinder();
            frmDoctor.ShowDialog();
        }

    }
}
