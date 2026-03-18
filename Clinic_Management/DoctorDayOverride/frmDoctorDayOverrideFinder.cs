using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.DoctorDayOverride
{
    using System;
    using System.Windows.Forms;
    using Clinic_Management_Entities;
    using Clinic_Management_Entities.Entities;

    public partial class frmDoctorDayOverrideFinder : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnOverrideSelected;
        public event Action<int>? OnOverrideSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int OverrideID => this.ucDoctorDayOverrideFinder1.OverrideID;
        public int DoctorID => this.ucDoctorDayOverrideFinder1.DoctorID;
        public DoctorDayOverride Override => this.ucDoctorDayOverrideFinder1.Override;

        // =========================
        // CTOR
        // =========================
        public frmDoctorDayOverrideFinder()
        {
            InitializeComponent();
            WireUp();
        }

        // =========================
        // INTERNAL WIRING
        // =========================
        private void WireUp()
        {
            // Forward UserControl events → Form events
            this.ucDoctorDayOverrideFinder1.OnOverrideSelected += id =>
            {
                OnOverrideSelected?.Invoke(id);
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            };

            this.ucDoctorDayOverrideFinder1.OnOverrideSaved += id =>
            {
                OnOverrideSaved?.Invoke(id);
                OnOverrideSelected?.Invoke(id); // after save, override is also selected
            };
        }
    }

}
