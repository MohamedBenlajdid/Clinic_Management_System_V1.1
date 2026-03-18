namespace Clinic_Management.MedicalCertificate
{
    partial class frmMedicalCertificate
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
            ucMedicalCertificate1 = new ucMedicalCertificate();
            SuspendLayout();
            // 
            // ucMedicalCertificate1
            // 
            ucMedicalCertificate1.Location = new Point(7, 7);
            ucMedicalCertificate1.Name = "ucMedicalCertificate1";
            ucMedicalCertificate1.Size = new Size(466, 325);
            ucMedicalCertificate1.TabIndex = 0;
            // 
            // frmMedicalCertificate
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(480, 338);
            Controls.Add(ucMedicalCertificate1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmMedicalCertificate";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmMedicalCertificate";
            ResumeLayout(false);
        }

        #endregion

        private ucMedicalCertificate ucMedicalCertificate1;
    }
}