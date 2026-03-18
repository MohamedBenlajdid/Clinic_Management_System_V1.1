using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Doctors
{
    using Clinic_Management.Helpers;
    using Clinic_Management_BLL.ResultWraper;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Forms;

    public partial class ucDoctorSelecter : UserControl
    {
        // =========================
        // EVENTS (delegation outward)
        // =========================
        public event Action<int>? OnDoctorSelected; // doctorId (StaffId)

        // =========================
        // EXPOSITION
        // =========================
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int DoctorID => SelectedDoctor?.StaffId ?? -1;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Doctor? SelectedDoctor { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int? SelectedSpecialtyID
            => cbSpecialities.SelectedValue is int id && id > 0 ? id : (int?)null;

        // =========================
        // SERVICES (create at runtime only)
        // =========================
        private DoctorService? _doctorService;
        private SpecialtyService? _specialtyService;

        // =========================
        // INIT FLAG
        // =========================
        private bool _initialized;

        // =========================
        // DESIGN TIME GUARD (works with out-of-proc designer)
        // =========================
        private bool IsInDesigner()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return true;

            if (Site?.DesignMode == true)
                return true;

            // WinForms out-of-proc designer hosts your control here
            string? dn = AppDomain.CurrentDomain.FriendlyName;
            if (!string.IsNullOrWhiteSpace(dn) &&
                (dn.Contains("DesignToolsServer", StringComparison.OrdinalIgnoreCase) ||
                 dn.Contains("WinFormsDesigner", StringComparison.OrdinalIgnoreCase)))
                return true;

            return false;
        }

        // =========================
        // CTOR
        // =========================
        public ucDoctorSelecter()
        {
            InitializeComponent();

            // IMPORTANT: do nothing that touches DB/services here
            WireUp();
            Reset();
        }

        // =========================
        // RUNTIME INIT (safe)
        // =========================
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (_initialized) return;
            if (IsInDesigner()) return;

            _initialized = true;

            try
            {
                _doctorService = new DoctorService();
                _specialtyService = new SpecialtyService();

                LoadSpecialities();
                LoadDoctors(null); // all
            }
            catch (Exception ex)
            {
                // Prevent designer/runtime crash if something goes wrong
                clsMessage.ShowError(ex.Message);
            }
        }

        // =========================
        // RESET
        // =========================
        private void Reset()
        {
            cbSpecialities.DataSource = null;
            cbDoctors.DataSource = null;

            cbSpecialities.Items.Clear();
            cbDoctors.Items.Clear();

            SelectedDoctor = null;
        }

        // =========================
        // WIRING
        // =========================
        private void WireUp()
        {
            cbSpecialities.SelectionChangeCommitted += (_, __) =>
            {
                LoadDoctors(SelectedSpecialtyID);
            };

            cbDoctors.SelectionChangeCommitted += (_, __) =>
            {
                UpdateSelectedDoctorFromCombo();
            };
        }

        // =========================
        // LOADERS
        // =========================
        private void LoadSpecialities()
        {
            if (_specialtyService is null) return;

            var res = _specialtyService.GetAll();
            if (!res.IsSuccess || res.Value is null)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Failed to load specialties.");
                return;
            }

            var list = res.Value
                .Prepend(new Specialty { SpecialtyId = 0, Name = "-- All Specialities --" })
                .ToList();

            cbSpecialities.DataSource = list;
            cbSpecialities.ValueMember = nameof(Specialty.SpecialtyId);
            cbSpecialities.DisplayMember = nameof(Specialty.Name);
            cbSpecialities.SelectedValue = 0;
        }

        private void LoadDoctors(int? specialtyId)
        {
            if (_doctorService is null) return;

            Result<IEnumerable<Doctor>> res;

            if (specialtyId.HasValue && specialtyId.Value > 0)
                res = _doctorService.GetAllBySpecialtyId(specialtyId.Value);
            else
                res = _doctorService.GetAll();

            if (!res.IsSuccess || res.Value is null)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Failed to load doctors.");
                cbDoctors.DataSource = null;
                SelectedDoctor = null;
                return;
            }

            var list = res.Value
                .Prepend(new Doctor
                {
                    StaffId = 0,
                    LicenseNumber = "-- Select Doctor --"
                })
                .ToList();

            cbDoctors.DataSource = list;
            cbDoctors.ValueMember = nameof(Doctor.StaffId);

            // Change this if you have FullName/DisplayName
            cbDoctors.DisplayMember = nameof(Doctor.LicenseNumber);

            cbDoctors.SelectedValue = 0;
            SelectedDoctor = null;
        }

        // =========================
        // SELECTION
        // =========================
        private void UpdateSelectedDoctorFromCombo()
        {
            if (cbDoctors.SelectedItem is Doctor d && d.StaffId > 0)
            {
                SelectedDoctor = d;
                OnDoctorSelected?.Invoke(d.StaffId);
            }
            else
            {
                SelectedDoctor = null;
            }
        }

        // =========================
        // PUBLIC HELPERS
        // =========================
        public void SetSelectedDoctor(int doctorId)
        {
            if (doctorId <= 0) return;

            cbDoctors.SelectedValue = doctorId;
            UpdateSelectedDoctorFromCombo();
        }

        public void Reload()
        {
            LoadSpecialities();
            LoadDoctors(SelectedSpecialtyID);
        }
    }


}
