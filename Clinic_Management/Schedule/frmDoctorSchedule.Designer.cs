namespace Clinic_Management.Schedule
{
    partial class frmDoctorSchedule
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ucDoctorSchedule1 = new ucDoctorSchedule();
            SuspendLayout();
            // 
            // ucDoctorSchedule1
            // 
            ucDoctorSchedule1.Location = new Point(3, 2);
            ucDoctorSchedule1.Name = "ucDoctorSchedule1";
            ucDoctorSchedule1.Size = new Size(497, 322);
            ucDoctorSchedule1.TabIndex = 0;
            // 
            // frmDoctorSchedule
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(507, 331);
            Controls.Add(ucDoctorSchedule1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmDoctorSchedule";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmDoctorSchedule";
            ResumeLayout(false);
        }

        #endregion

        private ucDoctorSchedule ucDoctorSchedule1;
    }
}