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

    public partial class frmPermissionFinder : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnPermissionSelected;
        public event Action<int>? OnPermissionSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int PermissionID => this.ucPermissionFinder1.PermissionID;
        public Permission Permission => this.ucPermissionFinder1.Permission;

        // =========================
        // CTOR
        // =========================
        public frmPermissionFinder()
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
            this.ucPermissionFinder1.OnPermissionSelected += id =>
            {
                OnPermissionSelected?.Invoke(id);
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            };

            this.ucPermissionFinder1.OnPermissionSaved += id =>
            {
                OnPermissionSaved?.Invoke(id);
                OnPermissionSelected?.Invoke(id); // after save, permission is also selected
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            };
        }
    }



}
