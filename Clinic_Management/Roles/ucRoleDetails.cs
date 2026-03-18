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
    using System.ComponentModel;
    using System.Windows.Forms;

    

    public partial class ucRoleDetails : UserControl
    {
        // =======================
        // EXPOSITION
        // =======================
        // This will hold the role data being displayed
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Role Role { get; private set; } = new Role();

        public int RoleID => Role?.RoleId ?? -1;

        // =======================
        // DESIGN TIME GUARD
        // =======================
        private bool IsInDesigner =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
            (Site?.DesignMode ?? false);

        // =======================
        // CTOR
        // =======================
        public ucRoleDetails()
        {
            InitializeComponent();
            this.Load += ucRoleDetails_Load; // Hook up load event
        }

        private void ucRoleDetails_Load(object? sender, EventArgs e)
        {
            if (IsInDesigner)
                return;

            // Initialize with default/empty state
            ResetUI();
        }

        // =======================
        // PUBLIC API
        // =======================
        /// <summary>
        /// Loads and displays the data for a specific role.
        /// </summary>
        /// <param name="role">The Role object to display.</param>
        public void LoadEntityData(Role role)
        {
            if (role == null)
            {
                ResetUI();
                return;
            }

            Role = role; // Store the provided role
            BindEntityToUI(); // Update the UI with the role data
        }

        /// <summary>
        /// Resets the UI to an empty or default state.
        /// </summary>
        public void Reset()
        {
            Role = new Role(); // Clear the internal role object
            ResetUI();         // Reset the UI labels
        }

        // =======================
        // UI CORE
        // =======================
        private void ResetUI()
        {
            lblRoleID.Text = "[???]"; // Changed from lblID for consistency with ucRole
            lblCode.Text = "[???]";
            lblName.Text = "[???]";
            lblDescription.Text = "[???]";
            lblIsActive.Text = "[???]"; // New label for IsActive status
        }

        private void BindEntityToUI()
        {
            lblRoleID.Text = Role.RoleId > 0 ? Role.RoleId.ToString() : "[N/A]";
            lblCode.Text = Role.Code ?? "";
            lblName.Text = Role.Name ?? "";
            lblDescription.Text = Role.Description ?? "";
            lblIsActive.Text = Role.IsActive ? "Yes" : "No"; // Display IsActive status
        }
    }
}
