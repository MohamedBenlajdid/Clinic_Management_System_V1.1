namespace Clinic_Management.DoctorDayOverride
{
    partial class frmDoctorDayOverrideFinder
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
            ucDoctorDayOverrideFinder1 = new ucDoctorDayOverrideFinder();
            SuspendLayout();
            // 
            // ucDoctorDayOverrideFinder1
            // 
            ucDoctorDayOverrideFinder1.Location = new Point(2, 1);
            ucDoctorDayOverrideFinder1.Name = "ucDoctorDayOverrideFinder1";
            ucDoctorDayOverrideFinder1.Size = new Size(563, 363);
            ucDoctorDayOverrideFinder1.TabIndex = 0;
            // 
            // frmDoctorDayOverrideFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(563, 373);
            Controls.Add(ucDoctorDayOverrideFinder1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmDoctorDayOverrideFinder";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmDoctorDayOverrideFinder";
            ResumeLayout(false);
        }

        #endregion

        private ucDoctorDayOverrideFinder ucDoctorDayOverrideFinder1;
    }
}