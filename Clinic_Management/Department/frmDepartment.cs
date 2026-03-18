using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Department
{
    using Clinic_Management_Entities;
    using System;
    using System.Windows.Forms;

    public partial class frmDepartment : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnDepartmentSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int DepartmentID => this.ucDepartment1.DepartmentID;
        public Department Department => this.ucDepartment1.Department;
        public ucDepartment.enMode Mode => this.ucDepartment1.CurrentMode;

        // =========================
        // CTORS
        // =========================

        // ➕ Create new Department
        public frmDepartment()
        {
            InitializeComponent();

            WireUp();

            this.ucDepartment1.LoadNew();
        }

        // 👁 / ✏ View or Edit existing Department
        public frmDepartment(int departmentID, ucDepartment.enMode mode = ucDepartment.enMode.View)
        {
            InitializeComponent();

            WireUp();

            this.ucDepartment1.LoadEntityData(departmentID, mode);
        }

        // =========================
        // INTERNAL WIRING
        // =========================
        private void WireUp()
        {
            // Forward UC event → Form event
            this.ucDepartment1.OnDepartmentCreated += RaiseDepartmentSaved;

            // Optional: close behavior / unsaved guard
            this.FormClosing += FrmDepartment_FormClosing;
        }

        // =========================
        // EVENT FORWARDER
        // =========================
        private void RaiseDepartmentSaved(int departmentId)
        {
            // Always trust UC as source of truth
            this.OnDepartmentSaved?.Invoke(this.ucDepartment1.DepartmentID);

            // Optional auto close:
            // this.DialogResult = DialogResult.OK;
            // this.Close();
        }

        // =========================
        // CLOSE BEHAVIOR (OPTIONAL)
        // =========================
        private void FrmDepartment_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // If needed later: unsaved changes guard
            // if (ucDepartment1.IsDirty)
            // {
            //     var leave = clsMessage.Confirm("You have unsaved changes. Close anyway?");
            //     if (!leave) e.Cancel = true;
            // }
        }
    }

}
