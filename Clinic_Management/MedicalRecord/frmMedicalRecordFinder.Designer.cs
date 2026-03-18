namespace Clinic_Management.MedicalRecord
{
    partial class frmMedicalRecordFinder
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
            ucMedicalRecordFinder1 = new ucMedicalRecordFinder();
            SuspendLayout();
            // 
            // ucMedicalRecordFinder1
            // 
            ucMedicalRecordFinder1.Location = new Point(5, 4);
            ucMedicalRecordFinder1.Name = "ucMedicalRecordFinder1";
            ucMedicalRecordFinder1.Size = new Size(559, 358);
            ucMedicalRecordFinder1.TabIndex = 0;
            // 
            // frmMedicalRecordFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(569, 364);
            Controls.Add(ucMedicalRecordFinder1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmMedicalRecordFinder";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmMedicalRecordFinder";
            ResumeLayout(false);
        }

        #endregion

        private ucMedicalRecordFinder ucMedicalRecordFinder1;
    }
}