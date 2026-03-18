using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Appointment
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public partial class frmCancelledAppointmentReason : Form
    {
        // =========================
        // EVENTS (delegation outward)
        // =========================
        public event Action<string>? OnReasonSubmitted;

        // =========================
        // EXPOSITION
        // =========================
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Reason => (txtReason.Text ?? "").Trim();

        // Optional: allow setting initial text (for edit / retry)
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string InitialReason
        {
            get => (txtReason.Text ?? "");
            set => txtReason.Text = value ?? "";
        }

        // =========================
        // CTOR
        // =========================
        public frmCancelledAppointmentReason()
        {
            InitializeComponent();

            // recommended UX
            this.AcceptButton = btnSend;
            // if you have btnCancel on the form:
            // this.CancelButton = btnCancel;

            btnSend.Click += btnSend_Click;
            // if you have btnCancel:
            // btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            this.Shown += (s, e) => txtReason.Focus();
        }

        // =========================
        // BUTTON HANDLERS
        // =========================
        private void btnSend_Click(object? sender, EventArgs e)
        {
            if (!ValidateReason())
                return;

            // fire outward
            OnReasonSubmitted?.Invoke(Reason);

            // close with OK
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // =========================
        // VALIDATION
        // =========================
        private bool ValidateReason()
        {
            string r = Reason;

            if (string.IsNullOrWhiteSpace(r))
            {
                MessageBox.Show(
                    "Cancel reason is required.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtReason.Focus();
                return false;
            }

            // optional: minimum length
            if (r.Length < 3)
            {
                MessageBox.Show(
                    "Reason is too short.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtReason.Focus();
                txtReason.SelectAll();
                return false;
            }

            return true;
        }

        // =========================
        // STATIC HELPER (nice usage)
        // =========================
        public static bool TryGetReason(IWin32Window owner, out string reason, string? initialReason = null)
        {
            reason = string.Empty;

            using var frm = new frmCancelledAppointmentReason();
            if (initialReason != null)
                frm.InitialReason = initialReason;

            if (frm.ShowDialog(owner) != DialogResult.OK)
                return false;

            reason = frm.Reason;
            return !string.IsNullOrWhiteSpace(reason);
        }
    }

}
