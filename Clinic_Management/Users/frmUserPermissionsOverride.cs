using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Users
{
    using Clinic_Management_BLL.Service;
    using System;
    using System.Windows.Forms;

    public partial class frmUserPermissionsOverride : Form
    {
        // =========================
        // DATA (Passport)
        // =========================
        public int UserID { get; private set; } = -1;

        public UserService userService { get; private set; } = new UserService();
        public PersonService personService { get; private set; } = new PersonService();
        public UserRoleService userRoleService { get; private set; } = new UserRoleService();

        // =========================
        // CTORs
        // =========================
        public frmUserPermissionsOverride()
        {
            InitializeComponent();
            InitializeForm();
        }

        public frmUserPermissionsOverride(int userId)
        {
            InitializeComponent();
            UserID = userId;
            InitializeForm();
        }

        // =========================
        // INIT
        // =========================
        private void InitializeForm()
        {
            // Assume you already dropped these 2 user controls on the form:
            // - ucUserDetails1
            // - ucPermissionsTable1
            //
            // And you have:
            // - btnFindUser (optional)
            // - btnClose

            this.Load += frmUserPermissionsOverride_Load;

            ApplyState();
        }

        private void frmUserPermissionsOverride_Load(object? sender, EventArgs e)
        {
            if (this.DesignMode) return;

            if (UserID <= 0)
            {
                if (!PickUser())
                {
                    //this.Close();
                    return;
                }
            }

            LoadUser(UserID);
        }

        // =========================
        // PICK USER
        // =========================
        private bool PickUser()
        {
            frmUserFinder frm = new frmUserFinder(); // You must have this form

            int selectedId = -1;

            frm.OnUserSelected += (id) =>
            {
                selectedId = id;
                //frm.Close();
            };

            frm.ShowDialog();

            if (selectedId > 0)
            {
                UserID = selectedId;
                return true;
            }

            return false;
        }

        // =========================
        // LOAD USER + OVERRIDES
        // =========================
        private void LoadUser(int userId)
        {
            UserID = userId;

            // 1) User details (smart read-only control)
            // Recommended: let the control be smart by itself
            // so the form stays clean.
            ucUserDetails1.LoadUser(UserID);

            // If you insist to build user object here:
            // var userResult = userService.GetById(UserID);
            // if (!userResult.IsSuccess) { clsMessage.ShowWarning(userResult.ErrorMessage); return; }
            // ucUserDetails1.SetUser(userResult.Value); // only if you have such method

            // 2) Permissions table in USER mode (override)
            ucPermissionsTable1.LoadForUser(UserID);

            ApplyState();
        }

        // =========================
        // UI STATE
        // =========================
        private void ApplyState()
        {
            bool hasUser = UserID > 0;

            ucUserDetails1.Enabled = hasUser;
            ucPermissionsTable1.Enabled = hasUser;
        }

        // =========================
        // OPTIONAL BUTTONS (if exist)
        // =========================
        private void btnFindUser_Click(object sender, EventArgs e)
        {
            if (!PickUser())
                return;

            LoadUser(UserID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }



}
