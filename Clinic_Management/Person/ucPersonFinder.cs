using Clinic_Management.Helpers;
using Clinic_Management.UcHelpers;
using Clinic_Management_BLL.ResultWraper;
using Clinic_Management_BLL.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Person
{
    public partial class ucPersonFinder : UserControl
    {
        // =========================
        // EVENTS (Delegation outward)
        // =========================
        public event Action<int>? OnPersonSelected;   // when found/loaded successfully
        public event Action<int>? OnPersonSaved;      // when created/saved

        // =========================
        // EXPOSITION
        // =========================
        public int PersonID => ucPerson1.PersonID;
        public Clinic_Management_Entities.Person Person => ucPerson1.Person;

        // =========================
        // CTOR
        // =========================
        public ucPersonFinder()
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
                "Person ID",
                "National ID",
                "Phone",
                "Email");

            ucFinderBox1.FilterValuePlaceholder = "Type value then press Enter...";
            ucFinderBox1.ShowAddNew = true;
            ucFinderBox1.InputMode = ucFinderBox.enInputMode.Any;
        }

        private void WireUp()
        {
            // Finder events
            ucFinderBox1.FindClicked += DoFind;
            ucFinderBox1.AddNewClicked += DoAddNew;

            // Enter key triggers Find already in your ucFinderBox
            // ucFinderBox1.EnterPressed += DoFind; // optional if you didn't auto-call FindClicked

            // Person control events
            ucPerson1.OnPersonCreated += id =>
            {
                OnPersonSaved?.Invoke(id);
                OnPersonSelected?.Invoke(id);

                // after create, show in view mode
                ucPerson1.LoadEntityData(id, ucPerson.enMode.View);

                // update finder value to the created ID for clarity
                ucFinderBox1.FilterBySelectedIndex = 0; // Person ID
                ucFinderBox1.FilterValue = id.ToString();
            };
        }

        // =========================
        // ACTIONS
        // =========================
        private void DoAddNew()
        {
            ucPerson1.LoadNew();
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

            int personId = -1;

            try
            {
                // Decide search type
                if (filterBy == "Person ID")
                {
                    if (!int.TryParse(value, out personId) || personId <= 0)
                    {
                        clsMessage.ShowWarning("Person ID must be a valid number.");
                        return;
                    }

                    // load directly
                    ucPerson1.LoadEntityData(personId, ucPerson.enMode.View);

                    // if your ucPerson1 loads and can expose PersonID = -1 if not found,
                    // you can check like:
                    if (ucPerson1.PersonID <= 0)
                    {
                        clsMessage.ShowInfo("No person found.");
                        return;
                    }

                    OnPersonSelected?.Invoke(ucPerson1.PersonID);
                    return;
                }

                // For other filters, find ID first using PersonService
                // (simple & real-world: Finder finds ID, then loads details)
                var service = new PersonService();

                Result<Clinic_Management_Entities.Person> p = null;

                if (filterBy == "National ID")
                    p = service.FindByNationalId(value);   // adapt to your method
                else if (filterBy == "Phone")
                    p = service.FindByPhone(value);        // adapt to your method
                else if (filterBy == "Email")
                    p = service.FindByEmail(value);        // adapt to your method

                if (p.Value == null || p.Value.PersonId <= 0)
                {
                    clsMessage.ShowInfo("No person found.");
                    return;
                }

                ucPerson1.LoadEntityData(p.Value.PersonId, ucPerson.enMode.View);
                OnPersonSelected?.Invoke(p.Value.PersonId);

                // update UI to reflect found id
                ucFinderBox1.FilterBySelectedIndex = 0;
                ucFinderBox1.FilterValue = p.Value.PersonId.ToString();
            }
            catch (Exception ex)
            {
                clsMessage.ShowError(ex.Message);
            }
        }
    }
}
