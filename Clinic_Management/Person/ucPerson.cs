using Clinic_Management.Helpers;
using Clinic_Management_BLL.ImageHelper;
using Clinic_Management_BLL.Service;
using Clinic_Management_Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Person
{
    using Clinic_Management.EntityUc;
    using Clinic_Management_BLL.ResultWraper;
    using Clinic_Management_Entities;
    using System;
    using System.Linq;
    using System.Windows.Forms;

        public partial class ucPerson : UserControl
        {

        private void ClearErrors() => errorProvider1.Clear();

        private void SetError(Control ctrl, string message)
            => errorProvider1.SetError(ctrl, message);


        // =======================
        // MODE
        // =======================
        public enum enMode { AddNew, View, Edit }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public enMode CurrentMode
            {
                get => _mode;
                set { _mode = value; ApplyMode(); }
            }
            private enMode _mode = enMode.AddNew;

            // =======================
            // EXPOSITION
            // =======================
            public int PersonID => Person?.PersonId ?? -1;
            public Person Person { get; private set; } = new Person();

            // =======================
            // EVENTS
            // =======================
            public event Action<int>? OnPersonCreated;     // when AddNew saved
            public event Action<bool>? DirtyChanged;       // optional

            // =======================
            // SERVICES
            // =======================
            private readonly PersonService _personService = new();
            private readonly ImageService _imageService = new();

            // =======================
            // DIRTY
            // =======================
            private bool _isDirty;
            public bool IsDirty => _isDirty;

            private void SetDirty(bool dirty)
            {
                if (_isDirty == dirty) return;
                _isDirty = dirty;
                DirtyChanged?.Invoke(dirty);
            }

            // =======================
            // Image state (UI intent)
            // =======================
            private string? _selectedImageSourcePath;
            private bool _removeImageRequested;
            private Clinic_Management_Entities.Entities.Image? _currentImage;

            // =======================
            // DESIGN TIME GUARD
            // =======================
            private static bool IsDesignTime =>
                LicenseManager.UsageMode == LicenseUsageMode.Designtime;

            // =======================
            // CTOR
            // =======================
            public ucPerson()
            {
                InitializeComponent();

                if (IsDesignTime)
                {
                    // keep designer stable: don’t call DB/services here
                    return;
                }

                WireDirtyEvents();
                LoadCombos();
                LoadNew(); // default fresh
            }

            // =======================
            // PUBLIC API (like your other controls)
            // =======================
            public void LoadEntityData(int personId, enMode mode = enMode.View)
            {
            ClearErrors();

            if (personId <= 0)
                {
                    LoadNew();
                    return;
                }

                CurrentMode = mode;

                var p = _personService.GetById(personId);   // <-- adapt to your real method
                if (p == null)
                    throw new InvalidOperationException("Person not found.");

                Person = p.Value;
                BindEntityToUI();
                SetDirty(false);
            }

            public void LoadNew()
            {
            ClearErrors();

            Person = new Person();
                ResetUI();
                CurrentMode = enMode.AddNew;
                SetDirty(false);
            }

            public bool SaveCurrent()
            {
                if (!ValidateUI())
                    return false;

                MapUIToEntity();

                bool wasAdd = (CurrentMode == enMode.AddNew);

                Result<int> saved;
                if (wasAdd)
                {
                    saved = _personService.Create(Person); // returns new id
                    if (saved.Value <= 0) return false;

                    Person.PersonId = saved.Value;
                }
                else
                {
                    Result ok = _personService.Update(Person);
                    if (!ok.IsSuccess) return false;
                }

                // image after Person saved
                HandleImageAfterPersonSaved(Person.PersonId);
                LoadPersonImage();

                SetDirty(false);

                // After save: switch to view
                CurrentMode = enMode.View;

                if (wasAdd)
                    OnPersonCreated?.Invoke(Person.PersonId);

                return true;
            }

            // =======================
            // UI CORE
            // =======================
            private void ResetUI()
            {

            ClearErrors();

            lblPersonId.Text = "[N/A]";

                txtFirstName.Clear();
                txtLastName.Clear();
                dtpBirthDate.Value = DateTime.Now.AddYears(-18);

                cbGenderID.SelectedValue = 0;

                txtPhone1.Clear();
                txtPhone2.Clear();
                txtEmail.Clear();

                cbCountryId.SelectedValue = 0;

                txtCity.Clear();
                txtAddressLine.Clear();
                txtNationalID.Clear();

                _selectedImageSourcePath = null;
                _removeImageRequested = false;
                _currentImage = null;

                pbPersonImage.ImageLocation = null;
                SetDefaultImage();
            }

            private void BindEntityToUI()
            {
                lblPersonId.Text = Person.PersonId > 0 ? Person.PersonId.ToString() : "[N/A]";

                txtFirstName.Text = Person.FirstName ?? "";
                txtLastName.Text = Person.LastName ?? "";

                dtpBirthDate.Value = Person.BirthDate.HasValue
                    ? Person.BirthDate.Value
                    : DateTime.Now.AddYears(-18);

                cbGenderID.SelectedValue = Person.GenderId > 0 ? Person.GenderId : (byte)0;

                txtPhone1.Text = Person.Phone1 ?? "";
                txtPhone2.Text = Person.Phone2 ?? "";
                txtEmail.Text = Person.Email ?? "";

                cbCountryId.SelectedValue = Person.CountryId > 0 ? Person.CountryId : 0;

                txtCity.Text = Person.City ?? "";
                txtAddressLine.Text = Person.AddressLine ?? "";
                txtNationalID.Text = Person.NationalId ?? "";

                LoadPersonImage();
            }

            private void MapUIToEntity()
            {
                Person.FirstName = txtFirstName.Text.Trim();
                Person.LastName = txtLastName.Text.Trim();
                Person.BirthDate = dtpBirthDate.Value;

                Person.GenderId = cbGenderID.SelectedValue is byte gid ? gid : (byte)0;

                Person.Phone1 = txtPhone1.Text.Trim();
                Person.Phone2 = txtPhone2.Text.Trim();
                Person.Email = txtEmail.Text.Trim();

                Person.CountryId = cbCountryId.SelectedValue is int cid ? cid : 0;

                Person.City = txtCity.Text.Trim();
                Person.AddressLine = txtAddressLine.Text.Trim();
                Person.NationalId = txtNationalID.Text.Trim();

                Person.UpdatedAt = DateTime.Now;
                if (CurrentMode == enMode.AddNew)
                    Person.CreatedAt = DateTime.Now;
            }

            private void ApplyMode()
            {
                bool editable = CurrentMode != enMode.View;

                txtFirstName.ReadOnly = !editable;
                txtLastName.ReadOnly = !editable;
                dtpBirthDate.Enabled = editable;
                cbGenderID.Enabled = editable;

                txtPhone1.ReadOnly = !editable;
                txtPhone2.ReadOnly = !editable;
                txtEmail.ReadOnly = !editable;

                cbCountryId.Enabled = editable;
                txtCity.ReadOnly = !editable;
                txtAddressLine.ReadOnly = !editable;
                txtNationalID.ReadOnly = !editable;

                linkSetImage.Enabled = editable;
                linkRemoveImage.Enabled = editable;

                btnSave.Enabled = editable;

                linkEdit.Visible = (CurrentMode == enMode.View && PersonID > 0);

                lblPersonId.Text = PersonID > 0 ? PersonID.ToString() : "[N/A]";
            }

        private void LoadCombos()
        {
            // =========================
            // Gender
            // =========================
            cbGenderID.DataSource = GenderService
                .GetAll()
                .Prepend(new Gender
                {
                    GenderId = 0,
                    Name = "-- Select Gender --"
                })
                .ToList();

            cbGenderID.DisplayMember = "Name";     // ✅ FIXED
            cbGenderID.ValueMember = "GenderId";
            cbGenderID.SelectedValue = (byte)0;

            // =========================
            // Country
            // =========================
            cbCountryId.DataSource = CountryService
                .GetAll()
                .Prepend(new Country
                {
                    CountryId = 0,
                    Name = "-- Select Country --"
                })
                .ToList();

            cbCountryId.DisplayMember = "Name";    // ✅ FIXED
            cbCountryId.ValueMember = "CountryId";
            cbCountryId.SelectedValue = 0;

            // =========================
            // Gender → Default Image
            // =========================
            cbGenderID.SelectedIndexChanged += (_, __) =>
            {
                if (_currentImage == null &&
                    string.IsNullOrWhiteSpace(_selectedImageSourcePath) &&
                    !_removeImageRequested)
                {
                    SetDefaultImage();
                }
            };
        }



        // =======================
        // VALIDATION (simple)
        // =======================
        private bool ValidateUI()
        {
            ClearErrors();

            bool ok = true;

            // First name
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                SetError(txtFirstName, "First name is required.");
                ok = false;
            }

            // Last name
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                SetError(txtLastName, "Last name is required.");
                ok = false;
            }

            // BirthDate (optional rule)
            if (dtpBirthDate.Value.Date > DateTime.Today)
            {
                SetError(dtpBirthDate, "Birth date cannot be in the future.");
                ok = false;
            }

            // Gender (0 = placeholder)
            if (!(cbGenderID.SelectedValue is byte gid) || gid <= 0)
            {
                SetError(cbGenderID, "Please select gender.");
                ok = false;
            }

            // Email format (optional)
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                try
                {
                    _ = new System.Net.Mail.MailAddress(txtEmail.Text.Trim());
                }
                catch
                {
                    SetError(txtEmail, "Invalid email format.");
                    ok = false;
                }
            }

            // focus first invalid
            if (!ok)
            {
                if (!string.IsNullOrEmpty(errorProvider1.GetError(txtFirstName))) txtFirstName.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(txtLastName))) txtLastName.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(dtpBirthDate))) dtpBirthDate.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(cbGenderID))) cbGenderID.Focus();
                else if (!string.IsNullOrEmpty(errorProvider1.GetError(txtEmail))) txtEmail.Focus();
            }

            return ok;
        }


        // =======================
        // IMAGE
        // =======================
        private void LoadPersonImage()
            {
                if (PersonID <= 0)
                {
                    pbPersonImage.ImageLocation = null;
                    SetDefaultImage();
                    return;
                }

                _currentImage = ImageService.GetImageByPersonID(PersonID);

                if (_currentImage != null && !string.IsNullOrWhiteSpace(_currentImage.ImagePath))
                {
                    pbPersonImage.ImageLocation = clsImageHandler.GetFullPath(_currentImage.ImagePath);
                    return;
                }

                pbPersonImage.ImageLocation = null;
                SetDefaultImage();
            }

            private void HandleImageAfterPersonSaved(int personId)
            {
                if (_removeImageRequested)
                {
                    var img = ImageService.GetImageByPersonID(personId);
                    if (img != null)
                        _imageService.Delete(img.ImageID);

                    _removeImageRequested = false;
                    _selectedImageSourcePath = null;
                    _currentImage = null;
                    return;
                }

                if (!string.IsNullOrWhiteSpace(_selectedImageSourcePath))
                {
                    var existing = ImageService.GetImageByPersonID(personId);

                    if (existing == null)
                    {
                        var img = new Clinic_Management_Entities.Entities.Image
                        {
                            PersonID = personId,
                            ImagePath = _selectedImageSourcePath
                        };
                        _imageService.Create(img);
                    }
                    else
                    {
                        existing.ImagePath = _selectedImageSourcePath;
                        _imageService.Update(existing);
                    }

                    _selectedImageSourcePath = null;
                }
            }

            private void SetDefaultImage()
            {
                pbPersonImage.Image = GetDefaultImage();
            }

            private System.Drawing.Image GetDefaultImage()
            {
                int genderId =
                    cbGenderID.SelectedValue is byte b ? b :
                    cbGenderID.SelectedValue is int i ? i : 0;

                return (genderId == 1)
                    ? Properties.Resources.man
                    : Properties.Resources.woman;
            }

            // =======================
            // DIRTY WIRING
            // =======================
            private void WireDirtyEvents()
            {
                // any change marks dirty (simple)
                txtFirstName.TextChanged += (_, __) => SetDirty(true);
                txtLastName.TextChanged += (_, __) => SetDirty(true);
                dtpBirthDate.ValueChanged += (_, __) => SetDirty(true);

                cbGenderID.SelectedIndexChanged += (_, __) => SetDirty(true);

                txtPhone1.TextChanged += (_, __) => SetDirty(true);
                txtPhone2.TextChanged += (_, __) => SetDirty(true);
                txtEmail.TextChanged += (_, __) => SetDirty(true);

                cbCountryId.SelectedIndexChanged += (_, __) => SetDirty(true);

                txtCity.TextChanged += (_, __) => SetDirty(true);
                txtAddressLine.TextChanged += (_, __) => SetDirty(true);
                txtNationalID.TextChanged += (_, __) => SetDirty(true);
            }

            // =======================
            // UI EVENTS
            // =======================
            private void btnSave_Click(object sender, EventArgs e)
            {
                if (!SaveCurrent())
                {
                    clsMessage.ShowError("Person details failed to save.");
                    return;
                }

                clsMessage.ShowSuccess("Person details saved successfully.");
            }

            private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {
                CurrentMode = enMode.Edit;
            }

            private void linkSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {
                using OpenFileDialog dlg = new()
                {
                    Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp"
                };

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _selectedImageSourcePath = dlg.FileName;
                    _removeImageRequested = false;

                    pbPersonImage.ImageLocation = dlg.FileName;
                    SetDirty(true);
                }
            }

            private void linkRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {
                if (!clsMessage.ConfirmDelete("image"))
                    return;

                _removeImageRequested = true;
                _selectedImageSourcePath = null;

                pbPersonImage.ImageLocation = null;
                SetDefaultImage();

                SetDirty(true);
            }



        }




    }







