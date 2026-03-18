using Clinic_Management.Diagnostics.DiagnosticRequest;
using Clinic_Management.Diagnostics.DiagnosticResult;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.MainForm.DiagnosticResultsPrecess
{
    public partial class frmDiagnosticsProcess : Form
    {
        public frmDiagnosticsProcess()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmDiagnosticRequestsTable frm =
                new frmDiagnosticRequestsTable();
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmDiagnosticResultsTable frm = 
                new frmDiagnosticResultsTable();
            frm.ShowDialog();   
        }
    }
}
