using Clinic_Management_Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Users
{
    public partial class frmUser : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnUserSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int UserID => this.ucUser1.UserID;
        public int PersonID => this.ucUser1.PersonID;
        public User User => this.ucUser1.User;
        public ucUser.enMode Mode => this.ucUser1.CurrentMode;

        // =========================
        // CTORS
        // =========================

        // ➕ Create new User for a Person (PersonID = passport)
        public frmUser(int personID)
        {
            InitializeComponent();

            WireUp();

            this.ucUser1.LoadNew(personID);
        }

        // 👁 / ✏ View or Edit existing User
        public frmUser(int userID, ucUser.enMode mode = ucUser.enMode.View)
        {
            InitializeComponent();

            WireUp();

            this.ucUser1.LoadEntityData(userID, mode);
        }

        // =========================
        // INTERNAL WIRING
        // =========================
        private void WireUp()
        {
            // Forward UC event → Form event
            this.ucUser1.OnUserCreated += RaiseUserSaved;

            // Optional: close after save (same psychology as frmPerson)
            this.FormClosing += FrmUser_FormClosing;
        }

        // =========================
        // EVENT FORWARDER
        // =========================
        private void RaiseUserSaved(int userId)
        {
            // Always trust UC as source of truth
            this.OnUserSaved?.Invoke(this.ucUser1.UserID);

            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        // =========================
        // CLOSE BEHAVIOR (OPTIONAL)
        // =========================
        private void FrmUser_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // If needed later: unsaved changes guard
            // if (ucUser1.IsDirty) { ... }
        }
    }

}
