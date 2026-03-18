namespace Clinic_Management.Doctors
{
    partial class frmDoctorFinder
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
            ucDoctorFinder1 = new ucDoctorFinder();
            SuspendLayout();
            // 
            // ucDoctorFinder1
            // 
            ucDoctorFinder1.Location = new Point(1, -1);
            ucDoctorFinder1.Name = "ucDoctorFinder1";
            ucDoctorFinder1.Size = new Size(550, 437);
            ucDoctorFinder1.TabIndex = 0;
            // 
            // frmDoctorFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(552, 437);
            Controls.Add(ucDoctorFinder1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmDoctorFinder";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmDoctorFinder";
            ResumeLayout(false);
        }

        #endregion

        private ucDoctorFinder ucDoctorFinder1;
    }
}