using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Diagnostics.DiagnosticTest
{
    using Clinic_Management.Helpers;
    using Clinic_Management.UcHelpers;
    using Clinic_Management_BLL.ResultWraper;
    using Clinic_Management_BLL.Service;
    using System;
    using System.Windows.Forms;

    public partial class ucDiagnosticTestFinder : UserControl
    {
        // =========================
        // EVENTS (Delegation outward)
        // =========================
        public event Action<int>? OnDiagnosticTestSelected; // when found/loaded successfully
        public event Action<int>? OnDiagnosticTestSaved;    // when created/saved

        // =========================
        // EXPOSITION
        // =========================
        public int DiagnosticTestID => ucDiagnosticTest1.DiagnosticTestID;
        public Clinic_Management_Entities.Entities.DiagnosticTest DiagnosticTest => ucDiagnosticTest1.DiagnosticTest;

        // =========================
        // CTOR
        // =========================
        public ucDiagnosticTestFinder()
        {
            InitializeComponent();

            InitFinderBox();
            WireUp();
        }

        // =========================
        // INIT
        // =========================
        private void InitFinderBox()
        {
            // Simple options (no extra classes)
            ucFinderBox1.SetFilterByItems(
                "Diagnostic Test ID",
                "Code",
                "Name",
                "Category");

            ucFinderBox1.FilterValuePlaceholder = "Type value then press Enter...";
            ucFinderBox1.ShowAddNew = true;
            ucFinderBox1.InputMode = ucFinderBox.enInputMode.Any;
        }

        private void WireUp()
        {
            // Finder events
            ucFinderBox1.FindClicked += DoFind;
            ucFinderBox1.AddNewClicked += DoAddNew;

            // DiagnosticTest control events
            ucDiagnosticTest1.OnDiagnosticTestCreated += id =>
            {
                OnDiagnosticTestSaved?.Invoke(id);
                OnDiagnosticTestSelected?.Invoke(id);

                // after create, show in view mode
                ucDiagnosticTest1.LoadEntityData(id, ucDiagnosticTest.enMode.View);

                // update finder value to the created ID for clarity
                ucFinderBox1.FilterBySelectedIndex = 0; // Diagnostic Test ID
                ucFinderBox1.FilterValue = id.ToString();
            };
        }

        // =========================
        // ACTIONS
        // =========================
        private void DoAddNew()
        {
            ucDiagnosticTest1.LoadNew();
            ucFinderBox1.FilterValue = "";
            ucFinderBox1.FocusFilterValue();
        }

        private void DoFind()
        {
            string filterBy = ucFinderBox1.FilterBySelectedText;
            string value = ucFinderBox1.FilterValue;

            if (string.IsNullOrWhiteSpace(value))
            {
                clsMessage.ShowWarning("Enter a value to search.");
                ucFinderBox1.FocusFilterValue();
                return;
            }

            try
            {
                var service = new DiagnosticTestService();

                // Decide search type
                if (filterBy == "Diagnostic Test ID")
                {
                    if (!int.TryParse(value, out int id) || id <= 0)
                    {
                        clsMessage.ShowWarning("Diagnostic Test ID must be a valid number.");
                        return;
                    }

                    ucDiagnosticTest1.LoadEntityData(id, ucDiagnosticTest.enMode.View);

                    if (ucDiagnosticTest1.DiagnosticTestID <= 0)
                    {
                        clsMessage.ShowInfo("No diagnostic test found.");
                        return;
                    }

                    OnDiagnosticTestSelected?.Invoke(ucDiagnosticTest1.DiagnosticTestID);
                    return;
                }

                // For other filters: find entity, then load by ID (same pattern)
                Result<Clinic_Management_Entities.Entities.DiagnosticTest> r = null;

                //if (filterBy == "Code")
                //    r = service.FindByCode(value);          // adapt to your method
                //else if (filterBy == "Name")
                //    r = service.FindByName(value);          // adapt to your method
                //else if (filterBy == "Category")
                //    r = service.FindByCategory(value);      // adapt to your method

                if (r?.Value == null || r.Value.DiagnosticTestId <= 0)
                {
                    clsMessage.ShowInfo("No diagnostic test found.");
                    return;
                }

                ucDiagnosticTest1.LoadEntityData(r.Value.DiagnosticTestId, ucDiagnosticTest.enMode.View);
                OnDiagnosticTestSelected?.Invoke(r.Value.DiagnosticTestId);

                // update UI to reflect found id
                ucFinderBox1.FilterBySelectedIndex = 0;
                ucFinderBox1.FilterValue = r.Value.DiagnosticTestId.ToString();
            }
            catch (Exception ex)
            {
                clsMessage.ShowError(ex.Message);
            }
        }
    }


}
