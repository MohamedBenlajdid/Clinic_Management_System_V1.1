namespace Clinic_Management.Patients
{
    partial class frmPatient
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
            ucPatient1 = new ucPatient();
            SuspendLayout();
            // 
            // ucPatient1
            // 
            ucPatient1.Enabled = false;
            ucPatient1.Location = new Point(2, 2);
            ucPatient1.Name = "ucPatient1";
            ucPatient1.Size = new Size(466, 330);
            ucPatient1.TabIndex = 0;
            // 
            // frmPatient
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(470, 334);
            Controls.Add(ucPatient1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmPatient";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmPatient";
            ResumeLayout(false);
        }

        #endregion

        private ucPatient ucPatient1;
    }
}