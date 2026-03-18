using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Users
{
    using Clinic_Management.Helpers;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities;
    using System;
    using System.Windows.Forms;

    public partial class ucUserDetails : UserControl
    {
        // =========================
        // PASSPORT
        // =========================
        public int? UserID { get; private set; }

        // Optional: expose loaded objects (read-only)
        public User? CurrentUser = new User();
        public Person? CurrentPerson = new Person();
        public Role? CurrentRole = new Role();
        public UserRoleService userRoleService = new UserRoleService();
        public UserService userService = new UserService();
        public PersonService personService = new PersonService();
        public RoleService roleService = new RoleService(); 

        // =========================
        // EVENTS
        // =========================
        public event Action<int?>? UserChanged;

        // =========================
        // CTOR
        // =========================
        public ucUserDetails()
        {
            InitializeComponent();
            Reset();
            ApplyReadOnlyUI();
        }

        // =========================
        // PUBLIC API
        // =========================
        public void LoadUser(int userId)
        {
            Reset();

            try
            {
                // 1) Load User (passport)
                var user = userService.GetById(userId);
                if (user == null)
                {
                    clsMessage.ShowWarning("User not found.");
                    return;
                }

                CurrentUser = user.Value;
                UserID = user.Value.UserId;

                // 2) Load Person for email (smart)
                // Email = PersonService.GetByID(User.PersonID).Email;
                CurrentPerson = personService.GetById(user.Value.PersonId).Value;

                // 3) Load Role (1 to 1)
                // Role = UserRoleService.GetByID(User.ID)
                CurrentRole = roleService.GetById( userRoleService.GetById(user.Value.UserId).Value.RoleId).Value;

                // 4) Bind UI
                BindUI();

                UserChanged?.Invoke(UserID);
            }
            catch (Exception ex)
            {
                clsMessage.ShowError($"Failed to load user details.\n{ex.Message}");
            }
        }

        public void Clear()
        {
            UserID = null;
            CurrentUser = null;
            CurrentPerson = null;
            CurrentRole = null;
            Reset();
            UserChanged?.Invoke(UserID);
        }

        // =========================
        // UI
        // =========================
        private void Reset()
        {
            lblUserID.Text = "[???]";
            lblPersonID.Text = "[???]";
            lblUsername.Text = "[???]";
            lblEmail.Text = "[???]";
            lblRoleCode.Text = "[???]";
            lblRoleDescription.Text = "[???]";
        }

        private void BindUI()
        {
            if (CurrentUser == null)
            {
                Reset();
                return;
            }

            lblUserID.Text = CurrentUser.UserId.ToString();
            lblPersonID.Text = CurrentUser.PersonId.ToString();
            lblUsername.Text = string.IsNullOrWhiteSpace(CurrentUser.Username) ? "[N/A]" : CurrentUser.Username;

            // Email from person (smart)
            lblEmail.Text = (CurrentPerson != null && !string.IsNullOrWhiteSpace(CurrentPerson.Email))
                ? CurrentPerson.Email
                : "[N/A]";

            // Role info (1:1)
            if (CurrentRole != null)
            {
                lblRoleCode.Text = string.IsNullOrWhiteSpace(CurrentRole.Code) ? "[N/A]" : CurrentRole.Code;
                lblRoleDescription.Text = string.IsNullOrWhiteSpace(CurrentRole.Description) ? "[N/A]" : CurrentRole.Description;
            }
            else
            {
                lblRoleCode.Text = "[N/A]";
                lblRoleDescription.Text = "[N/A]";
            }
        }

        private void ApplyReadOnlyUI()
        {
            // This control is read-only by design.
            // If you have TextBoxes, set ReadOnly=true; if you have Links/Buttons disable them.
            // Here we just ensure it can't receive focus accidentally.
            this.TabStop = false;

            foreach (Control c in this.Controls)
            {
                c.TabStop = false;
            }
        }
    }


}
