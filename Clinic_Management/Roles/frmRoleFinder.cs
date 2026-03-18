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

    public partial class frmRoleFinder : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnRoleSelected;
        public event Action<int>? OnRoleSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int RoleID => this.ucRoleFinder1.RoleID;
        public Role Role => this.ucRoleFinder1.Role;

        // =========================
        // CTOR
        // =========================
        public frmRoleFinder()
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
            this.ucRoleFinder1.OnRoleSelected += id =>
            {
                OnRoleSelected?.Invoke(id);
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            };

            this.ucRoleFinder1.OnRoleSaved += id =>
            {
                OnRoleSaved?.Invoke(id);
                OnRoleSelected?.Invoke(id); // after save, role is also selected
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            };
        }
    }




}
