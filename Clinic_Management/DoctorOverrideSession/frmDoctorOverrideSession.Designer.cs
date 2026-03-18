namespace Clinic_Management.DoctorOverrideSession
{
    partial class frmDoctorOverrideSession
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
            ucDoctorOverrideSession1 = new ucDoctorOverrideSession();
            SuspendLayout();
            // 
            // ucDoctorOverrideSession1
            // 
            ucDoctorOverrideSession1.Location = new Point(3, 4);
            ucDoctorOverrideSession1.Name = "ucDoctorOverrideSession1";
            ucDoctorOverrideSession1.Size = new Size(496, 332);
            ucDoctorOverrideSession1.TabIndex = 0;
            // 
            // frmDoctorOverrideSession
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(502, 338);
            Controls.Add(ucDoctorOverrideSession1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmDoctorOverrideSession";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmDoctorOverrideSession";
            ResumeLayout(false);
        }

        #endregion

        private ucDoctorOverrideSession ucDoctorOverrideSession1;
    }
}