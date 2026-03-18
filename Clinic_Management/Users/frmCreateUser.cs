using Clinic_Management.Helpers;
using Clinic_Management_BLL.Service;
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
    public partial class frmCreateUser : Form
    {
        // =========================
        // STATE
        // =========================
        private enum enStep { Person, User }
        private enStep _currentStep = enStep.Person;

        private int _selectedPersonId = -1;

        // =========================
        // SERVICES
        // =========================
        private readonly UserRoleService _userRoleService = new();
        private readonly RoleService _RoleService = new();

        // =========================
        // CTOR
        // =========================
        public frmCreateUser()
        {
            InitializeComponent();

            Init();
            WireUp();
            GoToPersonStep();
        }

        // =========================
        // INIT
        // =========================
        private void Init()
        {
            // Make sure both controls are inside pnlContainer (designer or code)
            // If ucUser1 is not in panel, add it:
            if (!pnlContainer.Controls.Contains(ucPersonFinder1))
                pnlContainer.Controls.Add(ucPersonFinder1);

            if (!pnlContainer.Controls.Contains(ucUser1))
                pnlContainer.Controls.Add(ucUser1);

            ucPersonFinder1.Dock = DockStyle.Fill;
            ucUser1.Dock = DockStyle.Fill;

            LoadRolesCombo();
        }

        private void LoadRolesCombo()
        {
            // Simple binding (IEnumerable -> ToList)
            var roles = _RoleService.GetAll().Value.ToList();

            // Optional: insert placeholder
            roles.Insert(0, new Role
            {
                RoleId = 0,
                Name = "-- Select Role --"
            });

            cbUserRole.DataSource = roles;
            cbUserRole.DisplayMember = "Name";
            cbUserRole.ValueMember = "RoleId";
            cbUserRole.SelectedValue = 0;
        }

        // =========================
        // WIRING
        // =========================
        private void WireUp()
        {
            // Top navigation
            btnPerson.Click += (_, __) => GoToPersonStep();
            btnUser.Click += (_, __) =>
            {
                if (_selectedPersonId <= 0)
                {
                    clsMessage.ShowWarning("Select a person first.");
                    return;
                }
                GoToUserStep();
            };

            // Person selected from finder -> unlock user step
            ucPersonFinder1.OnPersonSelected += OnPersonChosen;
            ucPersonFinder1.OnPersonSaved += OnPersonChosen; // after save, person is selected too

            // User created -> assign role
            ucUser1.OnUserCreated += OnUserCreated;
        }

        // =========================
        // STEP NAV
        // =========================
        private void GoToPersonStep()
        {
            _currentStep = enStep.Person;

            ucPersonFinder1.Visible = true;
            ucUser1.Visible = false;

            btnPerson.Enabled = false;
            btnUser.Enabled = (_selectedPersonId > 0);

            // Role + User section should be locked until person chosen
            cbUserRole.Enabled = (_selectedPersonId > 0);
        }

        private void GoToUserStep()
        {
            _currentStep = enStep.User;

            ucPersonFinder1.Visible = false;
            ucUser1.Visible = true;

            btnPerson.Enabled = true;
            btnUser.Enabled = false;

            cbUserRole.Enabled = true;
        }

        // =========================
        // PERSON FLOW
        // =========================
        private void OnPersonChosen(int personId)
        {
            _selectedPersonId = personId;

            // optional UI label
            // lblSelectedPersonId.Text = personId.ToString();

            // passport into ucUser
            ucUser1.LoadNew(personId);

            // unlock user step
            btnUser.Enabled = true;
            cbUserRole.Enabled = true;

            // go automatically to user step (smart flow)
            GoToUserStep();
        }

        // =========================
        // USER FLOW
        // =========================
        private void OnUserCreated(int userId)
        {
            // must have selected role
            int roleId = (cbUserRole.SelectedValue is int r) ? r : 0;

            if (roleId <= 0)
            {
                clsMessage.
                    ShowWarning("User created, but role Selected as a Patient By default. Contact Your Admin To Re-Issignt.");
                //var result = _userRoleService.AssignRole(userId, roleId); // adapt if returns Result

                return;
            }

            try
            {
                // Assign role to user
                var result = _userRoleService.AssignRole(userId, roleId); // adapt if returns Result

                // If your Assign returns Result:
                if (!result.IsSuccess) { clsMessage.ShowError(result.ErrorMessage); return; }

                clsMessage.ShowSuccess("User created and role assigned successfully.");

                // Lock everything to prevent playing after success
                LockAfterSuccess();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                clsMessage.ShowError(ex.Message);
            }
        }

        private void LockAfterSuccess()
        {
            btnPerson.Enabled = false;
            btnUser.Enabled = false;

            cbUserRole.Enabled = false;
            ucPersonFinder1.Enabled = false;
            ucUser1.Enabled = false;
        }

    }
}
