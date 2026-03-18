namespace Clinic_Management.MedicalRecord
{
    partial class frmMedicalRecord
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
            ucMedicalRecord1 = new ucMedicalRecord();
            SuspendLayout();
            // 
            // ucMedicalRecord1
            // 
            ucMedicalRecord1.Location = new Point(8, 6);
            ucMedicalRecord1.Name = "ucMedicalRecord1";
            ucMedicalRecord1.Size = new Size(477, 282);
            ucMedicalRecord1.TabIndex = 0;
            // 
            // frmMedicalRecord
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(493, 298);
            Controls.Add(ucMedicalRecord1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmMedicalRecord";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmMedicalRecord";
            ResumeLayout(false);
        }

        #endregion

        private ucMedicalRecord ucMedicalRecord1;
    }
}