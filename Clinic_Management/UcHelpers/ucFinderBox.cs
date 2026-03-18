using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.UcHelpers
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;

    public partial class ucFinderBox : UserControl
    {
        // =========================
        // EVENTS (Delegation)
        // =========================
        public event Action? FindClicked;
        public event Action? AddNewClicked;

        public event Action<string>? FilterValueChanged;
        public event Action<object?>? FilterByChanged;

        public event Action? EnterPressed;

        // =========================
        // EXPOSE CONTROLS (Get/Set)
        // =========================
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string FilterValue
        {
            get => txtFilterValue.Text.Trim();
            set => txtFilterValue.Text = value ?? string.Empty;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int FilterBySelectedIndex
        {
            get => cbFilterBy.SelectedIndex;
            set => cbFilterBy.SelectedIndex = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public object? FilterBySelectedValue
        {
            get => cbFilterBy.SelectedValue;
            set => cbFilterBy.SelectedValue = value;
        }

        [Browsable(false)]
        public string FilterBySelectedText
            => cbFilterBy.SelectedItem?.ToString() ?? string.Empty;

        // =========================
        // FLEXIBILITY SETTINGS
        // =========================
        [Category("Finder")]
        [DefaultValue(true)]
        public bool ShowAddNew
        {
            get => btnAddNew.Visible;
            set => btnAddNew.Visible = value;
        }

        [Category("Finder")]
        [DefaultValue(true)]
        public bool ShowFind
        {
            get => btnFind.Visible;
            set => btnFind.Visible = value;
        }

        [Category("Finder")]
        [DefaultValue(true)]
        public bool ReadOnlyFilterValue
        {
            get => txtFilterValue.ReadOnly;
            set => txtFilterValue.ReadOnly = value;
        }

        [Category("Finder")]
        [DefaultValue(true)]
        public string FilterValuePlaceholder
        {
            get => txtFilterValue.PlaceholderText;
            set => txtFilterValue.PlaceholderText = value ?? "";
        }

        public enum enInputMode { Any, NumbersOnly }
        private enInputMode _inputMode = enInputMode.Any;

        [Category("Finder")]
        [DefaultValue(true)]
        public enInputMode InputMode
        {
            get => _inputMode;
            set => _inputMode = value;
        }

        // =========================
        // CTOR
        // =========================
        public ucFinderBox()
        {
            InitializeComponent();

            WireUp();
        }

        // =========================
        // PUBLIC HELPERS
        // =========================
        public void SetFilterByItems(params string[] items)
        {
            cbFilterBy.DataSource = null;
            cbFilterBy.Items.Clear();

            if (items == null || items.Length == 0)
                return;

            cbFilterBy.Items.AddRange(items.Cast<object>().ToArray());
            cbFilterBy.SelectedIndex = 0;
        }

        public void Clear()
        {
            cbFilterBy.SelectedIndex = cbFilterBy.Items.Count > 0 ? 0 : -1;
            txtFilterValue.Clear();
        }

        public void FocusFilterValue() => txtFilterValue.Focus();

        // =========================
        // WIRING
        // =========================
        private void WireUp()
        {
            btnFind.Click += (_, __) => FindClicked?.Invoke();
            btnAddNew.Click += (_, __) => AddNewClicked?.Invoke();

            txtFilterValue.TextChanged += (_, __) =>
                FilterValueChanged?.Invoke(FilterValue);

            cbFilterBy.SelectedIndexChanged += (_, __) =>
                FilterByChanged?.Invoke(FilterBySelectedValue);

            txtFilterValue.KeyDown += TxtFilterValue_KeyDown;
            txtFilterValue.KeyPress += TxtFilterValue_KeyPress;
        }

        private void TxtFilterValue_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EnterPressed?.Invoke();
                FindClicked?.Invoke();   // common UX: Enter triggers Find
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void TxtFilterValue_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (InputMode == enInputMode.NumbersOnly)
            {
                bool isControl = char.IsControl(e.KeyChar);
                bool isDigit = char.IsDigit(e.KeyChar);

                if (!isControl && !isDigit)
                    e.Handled = true;
            }
        }
    }

}
