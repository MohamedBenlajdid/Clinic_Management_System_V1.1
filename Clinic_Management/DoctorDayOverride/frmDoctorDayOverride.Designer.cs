namespace Clinic_Management.DoctorDayOverride
{
    partial class frmDoctorDayOverride
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
            ucDoctorDayOverride1 = new ucDoctorDayOverride();
            SuspendLayout();
            // 
            // ucDoctorDayOverride1
            // 
            ucDoctorDayOverride1.Location = new Point(3, 2);
            ucDoctorDayOverride1.Name = "ucDoctorDayOverride1";
            ucDoctorDayOverride1.Size = new Size(496, 304);
            ucDoctorDayOverride1.TabIndex = 0;
            // 
            // frmDoctorDayOverride
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(502, 311);
            Controls.Add(ucDoctorDayOverride1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmDoctorDayOverride";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmDoctorDayOverride";
            ResumeLayout(false);
        }

        #endregion

        private ucDoctorDayOverride ucDoctorDayOverride1;
    }
}