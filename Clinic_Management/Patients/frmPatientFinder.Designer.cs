namespace Clinic_Management.Patients
{
    partial class frmPatientFinder
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
            ucPatientFinder1 = new ucPatientFinder();
            SuspendLayout();
            // 
            // ucPatientFinder1
            // 
            ucPatientFinder1.Location = new Point(2, 1);
            ucPatientFinder1.Name = "ucPatientFinder1";
            ucPatientFinder1.Size = new Size(552, 401);
            ucPatientFinder1.TabIndex = 0;
            // 
            // frmPatientFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(551, 406);
            Controls.Add(ucPatientFinder1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmPatientFinder";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmPatientFinder";
            ResumeLayout(false);
        }

        #endregion

        private ucPatientFinder ucPatientFinder1;
    }
}