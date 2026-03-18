using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinic_Management.Appointment
{
    using Clinic_Management.Helpers;
    using Clinic_Management_BLL.Service;
    using Clinic_Management_Entities;
    using System;
    using System.Windows.Forms;

    public partial class frmAppointment : Form
    {
        // =========================
        // DELEGATION
        // =========================
        public event Action<int>? OnAppointmentSaved;

        // =========================
        // EXPOSITION
        // =========================
        public int AppointmentID => this.ucAppointment1.AppointmentID;
        public int DoctorID => this.ucAppointment1.DoctorID;
        public int PatientID => this.ucAppointment1.PatientID;
        public Clinic_Management_Entities.Entities.Appointment Appointment => this.ucAppointment1.Appointment;
        public ucAppointment.enMode Mode => this.ucAppointment1.CurrentMode;

        // =========================
        // SERVICES
        // =========================
        private readonly AppointmentService _appointmentService = new();
        private readonly DoctorAvailabilityService _doctorAvailabilityService = new();

        // =========================
        // CTORS
        // =========================

        // ➕ New Appointment (empty passports)
        public frmAppointment()
        {
            InitializeComponent();
            WireUp();

            this.ucAppointment1.LoadNew();
        }

        // ➕ New Appointment with passports (DoctorID + PatientID)
        public frmAppointment(int doctorId, int patientId)
        {
            InitializeComponent();
            WireUp();

            this.ucAppointment1.SetDoctorID(doctorId);
            this.ucAppointment1.SetPatientID(patientId);    
        }

        // 👁 / ✏ View or Edit existing appointment
        public frmAppointment(int appointmentId, ucAppointment.enMode mode = ucAppointment.enMode.View)
        {
            InitializeComponent();
            WireUp();

            LoadExisting(appointmentId, mode);
        }

        // =========================
        // INTERNAL WIRING
        // =========================
        private void WireUp()
        {
            ucAppointment1.SlotProvider = (doctorId, date) =>
            {
                var res = _doctorAvailabilityService.GetDoctorAvailableSlots(doctorId, date);
                return res.IsSuccess
                    ? res.Value.Select(s => (s.StartAt, s.EndAt))
                    : Enumerable.Empty<(DateTime, DateTime)>();
            };


            // 1) UC raises SaveRequested(Appointment entity)
            this.ucAppointment1.SaveRequested += HandleSaveRequested;

            // 2) forward UC event -> Form event
            this.ucAppointment1.OnAppointmentCreated += RaiseAppointmentSaved;

            // Optional: close behavior later
            this.FormClosing += FrmAppointment_FormClosing;
        }

        // =========================
        // LOAD EXISTING
        // =========================
        private void LoadExisting(int appointmentId, ucAppointment.enMode mode)
        {
            if (appointmentId <= 0)
            {
                this.ucAppointment1.LoadNew();
                return;
            }

            var res = _appointmentService.GetById(appointmentId);

            if (!res.IsSuccess || res.Value is null)
            {
                clsMessage.ShowError(res.ErrorMessage ?? "Appointment not found.");
                this.ucAppointment1.LoadNew();
                return;
            }

            this.ucAppointment1.LoadEntityData(res.Value, mode);
        }

        // =========================
        // SAVE HANDLER (Form owns persistence)
        // =========================
        private void HandleSaveRequested(Clinic_Management_Entities.Entities.Appointment a)
        {
            if (a is null)
            {
                clsMessage.ShowError("Invalid appointment.");
                return;
            }

            bool isAdd = (a.AppointmentId <= 0);

            if (isAdd)
            {
                var create = _appointmentService.Create(a);
                if (!create.IsSuccess || create.Value <= 0)
                {
                    clsMessage.ShowError(create.ErrorMessage ?? "Failed to create appointment.");
                    return;
                }

                // reflect saved id back into UC
                this.ucAppointment1.SetAppointmentId(create.Value);
                this.ucAppointment1.Refresh();

                clsMessage.ShowSuccess("Appointment saved successfully.");
                RaiseAppointmentSaved(create.Value);
            }
            else
            {
                var update = _appointmentService.Update(a);
                if (!update.IsSuccess)
                {
                    clsMessage.ShowError(update.ErrorMessage ?? "Failed to update appointment.");
                    return;
                }

                this.ucAppointment1.Refresh();

                clsMessage.ShowSuccess("Appointment updated successfully.");
                RaiseAppointmentSaved(a.AppointmentId);
            }

            // Optional auto close:
            // this.DialogResult = DialogResult.OK;
            // this.Close();
        }

        // =========================
        // EVENT FORWARDER
        // =========================
        private void RaiseAppointmentSaved(int appointmentId)
        {
            this.OnAppointmentSaved?.Invoke(appointmentId);
        }

        // =========================
        // CLOSE BEHAVIOR (OPTIONAL)
        // =========================
        private void FrmAppointment_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // if (ucAppointment1.IsDirty)
            // {
            //     if (!clsMessage.Confirm("You have unsaved changes. Close anyway?"))
            //         e.Cancel = true;
            // }
        }
    }



}
