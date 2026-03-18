namespace Clinic_Management.Schedule
{
    partial class ucDoctorScheduleFinder
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ucDoctorSchedule1 = new ucDoctorSchedule();
            ucFinderBox1 = new Clinic_Management.UcHelpers.ucFinderBox();
            SuspendLayout();
            // 
            // ucDoctorSchedule1
            // 
            ucDoctorSchedule1.Location = new Point(24, 52);
            ucDoctorSchedule1.Name = "ucDoctorSchedule1";
            ucDoctorSchedule1.Size = new Size(497, 322);
            ucDoctorSchedule1.TabIndex = 0;
            ucDoctorSchedule1.CurrentMode = ucDoctorSchedule.enMode.View;
                 
            // 
            // ucFinderBox1
            // 
            ucFinderBox1.FilterValuePlaceholder = "";
            ucFinderBox1.InputMode = UcHelpers.ucFinderBox.enInputMode.Any;
            ucFinderBox1.Location = new Point(0, 0);
            ucFinderBox1.Name = "ucFinderBox1";
            ucFinderBox1.ReadOnlyFilterValue = false;
            ucFinderBox1.Size = new Size(551, 52);
            ucFinderBox1.TabIndex = 1;
            // 
            // ucDoctorScheduleFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ucFinderBox1);
            Controls.Add(ucDoctorSchedule1);
            Name = "ucDoctorScheduleFinder";
            Size = new Size(550, 380);
            ResumeLayout(false);
        }

        #endregion

        private ucDoctorSchedule ucDoctorSchedule1;
        private UcHelpers.ucFinderBox ucFinderBox1;
    }
}
