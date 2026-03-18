using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Prescriptions
{
    using Clinic_Management.Helpers;
    using Clinic_Management.UcHelpers;
    using Clinic_Management_BLL.ResultWraper;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities.Entities;
    using System;
    using System.Windows.Forms;

    public partial class ucPrescriptionItemFinder : UserControl
    {
        // =========================
        // EVENTS (Delegation outward)
        // =========================
        public event Action<int>? OnPrescriptionItemSelected; // when found/loaded successfully
        public event Action<int>? OnPrescriptionItemSaved;    // when created/saved

        // =========================
        // EXPOSITION
        // =========================
        public int PrescriptionItemID => ucPrescriptionItem1.PrescriptionItemID;
        public int PrescriptionID => ucPrescriptionItem1.PrescriptionID;
        public int MedicamentID => ucPrescriptionItem1.MedicamentID;

        public PrescriptionItem PrescriptionItem => ucPrescriptionItem1.PrescriptionItem;

        // =========================
        // CTOR
        // =========================
        public ucPrescriptionItemFinder()
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
                "Prescription Item ID",
                "Prescription ID",
                "Medicament ID");

            ucFinderBox1.FilterValuePlaceholder = "Type value then press Enter...";
            ucFinderBox1.ShowAddNew = true;
            ucFinderBox1.InputMode = ucFinderBox.enInputMode.Any;
        }

        private void WireUp()
        {
            // Finder events
            ucFinderBox1.FindClicked += DoFind;
            ucFinderBox1.AddNewClicked += DoAddNew;

            // PrescriptionItem control events
            ucPrescriptionItem1.OnPrescriptionItemCreated += id =>
            {
                OnPrescriptionItemSaved?.Invoke(id);
                OnPrescriptionItemSelected?.Invoke(id);

                // after create, show in view mode
                ucPrescriptionItem1.LoadEntityData(id, ucPrescriptionItem.enMode.View);

                // update finder value to the created ID for clarity
                ucFinderBox1.FilterBySelectedIndex = 0; // Prescription Item ID
                ucFinderBox1.FilterValue = id.ToString();
            };
        }

        // =========================
        // ACTIONS
        // =========================
        private void DoAddNew()
        {
            ucPrescriptionItem1.LoadNew();
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
                var service = new PrescriptionItemService();

                // Decide search type
                if (filterBy == "Prescription Item ID")
                {
                    if (!int.TryParse(value, out int itemId) || itemId <= 0)
                    {
                        clsMessage.ShowWarning("Prescription Item ID must be a valid number.");
                        return;
                    }

                    ucPrescriptionItem1.LoadEntityData(itemId, ucPrescriptionItem.enMode.View);

                    if (ucPrescriptionItem1.PrescriptionItemID <= 0)
                    {
                        clsMessage.ShowInfo("No prescription item found.");
                        return;
                    }

                    OnPrescriptionItemSelected?.Invoke(ucPrescriptionItem1.PrescriptionItemID);
                    return;
                }

                // For other filters: find first matching item, then load by ID
                Result<PrescriptionItem> r = null;

                //if (filterBy == "Prescription ID")
                //    r = service.FindFirstByPrescriptionId(ParsePositiveInt(value, "Prescription ID")); // adapt to your method
                //else if (filterBy == "Medicament ID")
                //    r = service.FindFirstByMedicamentId(ParsePositiveInt(value, "Medicament ID"));     // adapt to your method

                if (r?.Value == null || r.Value.PrescriptionItemId <= 0)
                {
                    clsMessage.ShowInfo("No prescription item found.");
                    return;
                }

                ucPrescriptionItem1.LoadEntityData(r.Value.PrescriptionItemId, ucPrescriptionItem.enMode.View);
                OnPrescriptionItemSelected?.Invoke(r.Value.PrescriptionItemId);

                // update UI to reflect found id
                ucFinderBox1.FilterBySelectedIndex = 0;
                ucFinderBox1.FilterValue = r.Value.PrescriptionItemId.ToString();
            }
            catch (Exception ex)
            {
                clsMessage.ShowError(ex.Message);
            }
        }

        private int ParsePositiveInt(string value, string label)
        {
            if (!int.TryParse(value, out int id) || id <= 0)
                throw new ArgumentException($"{label} must be a valid number.");
            return id;
        }
    }
}
