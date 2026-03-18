using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Medicaments
{
    using Clinic_Management.Helpers;
    using Clinic_Management.UcHelpers;
    using Clinic_Management_BLL.ResultWraper;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities.Entities;
    using System;
    using System.Windows.Forms;

    public partial class ucMedicamentFinder : UserControl
    {
        // =========================
        // EVENTS (Delegation outward)
        // =========================
        public event Action<int>? OnMedicamentSelected; // when found/loaded successfully
        public event Action<int>? OnMedicamentSaved;    // when created/saved

        // =========================
        // EXPOSITION
        // =========================
        public int MedicamentID => ucMedicament1.MedicamentID;
        public Medicament Medicament => ucMedicament1.Medicament;

        // =========================
        // CTOR
        // =========================
        public ucMedicamentFinder()
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
                "Medicament ID",
                "Name",
                "Generic Name",
                "Manufacturer");

            ucFinderBox1.FilterValuePlaceholder = "Type value then press Enter...";
            ucFinderBox1.ShowAddNew = true;
            ucFinderBox1.InputMode = ucFinderBox.enInputMode.Any;
        }

        private void WireUp()
        {
            // Finder events
            ucFinderBox1.FindClicked += DoFind;
            ucFinderBox1.AddNewClicked += DoAddNew;

            // Medicament control events
            ucMedicament1.OnMedicamentCreated += id =>
            {
                OnMedicamentSaved?.Invoke(id);
                OnMedicamentSelected?.Invoke(id);

                // after create, show in view mode
                ucMedicament1.LoadEntityData(id, ucMedicament.enMode.View);

                // update finder value to the created ID for clarity
                ucFinderBox1.FilterBySelectedIndex = 0; // Medicament ID
                ucFinderBox1.FilterValue = id.ToString();
            };
        }

        // =========================
        // ACTIONS
        // =========================
        private void DoAddNew()
        {
            ucMedicament1.LoadNew();
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

            int medicamentId = -1;

            try
            {
                // Decide search type
                if (filterBy == "Medicament ID")
                {
                    if (!int.TryParse(value, out medicamentId) || medicamentId <= 0)
                    {
                        clsMessage.ShowWarning("Medicament ID must be a valid number.");
                        return;
                    }

                    // load directly
                    ucMedicament1.LoadEntityData(medicamentId, ucMedicament.enMode.View);

                    // if your ucMedicament1 loads and can expose MedicamentID = -1 if not found,
                    // you can check like:
                    if (ucMedicament1.MedicamentID <= 0)
                    {
                        clsMessage.ShowInfo("No medicament found.");
                        return;
                    }

                    OnMedicamentSelected?.Invoke(ucMedicament1.MedicamentID);
                    return;
                }

                // For other filters, find ID first using MedicamentService
                // Finder finds entity, then loads details by ID (same pattern as PersonFinder)
                var service = new MedicamentService();

                Result<Medicament> m = null;

                //if (filterBy == "Name")
                //    m = service.FindByName(value);                 // adapt to your method
                //else if (filterBy == "Generic Name")
                //    m = service.FindByGenericName(value);          // adapt to your method
                //else if (filterBy == "Manufacturer")
                //    m = service.FindByManufacturer(value);         // adapt to your method

                if (m.Value == null || m.Value.MedicamentId <= 0)
                {
                    clsMessage.ShowInfo("No medicament found.");
                    return;
                }

                ucMedicament1.LoadEntityData(m.Value.MedicamentId, ucMedicament.enMode.View);
                OnMedicamentSelected?.Invoke(m.Value.MedicamentId);

                // update UI to reflect found id
                ucFinderBox1.FilterBySelectedIndex = 0;
                ucFinderBox1.FilterValue = m.Value.MedicamentId.ToString();
            }
            catch (Exception ex)
            {
                clsMessage.ShowError(ex.Message);
            }
        }
    }


}
