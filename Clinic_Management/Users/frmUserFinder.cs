using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Clinic_Management_Entities;

namespace Clinic_Management.Users
{
    public partial class frmUserFinder : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnUserSelected;
        public event Action<int>? OnUserSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int UserID => this.ucUserFinder1.UserID;
        public int PersonID => this.ucUserFinder1.PersonID;
        public User User => this.ucUserFinder1.User;

        // =========================
        // CTOR
        // =========================
        public frmUserFinder()
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
            this.ucUserFinder1.OnUserSelected += id =>
            {
                OnUserSelected?.Invoke(id);
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            };

            this.ucUserFinder1.OnUserSaved += id =>
            {
                OnUserSaved?.Invoke(id);
                OnUserSelected?.Invoke(id); // after save, user is also selected
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            };
        }
    }

}
