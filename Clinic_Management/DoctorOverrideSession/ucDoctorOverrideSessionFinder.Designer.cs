namespace Clinic_Management.DoctorOverrideSession
{
    partial class ucDoctorOverrideSessionFinder
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ucDoctorOverrideSession1 = new ucDoctorOverrideSession();
            ucFinderBox1 = new Clinic_Management.UcHelpers.ucFinderBox();
            SuspendLayout();
            // 
            // ucDoctorOverrideSession1
            // 
            ucDoctorOverrideSession1.Location = new Point(25, 58);
            ucDoctorOverrideSession1.Name = "ucDoctorOverrideSession1";
            ucDoctorOverrideSession1.Size = new Size(496, 332);
            ucDoctorOverrideSession1.TabIndex = 0;
            ucDoctorOverrideSession1.CurrentMode = ucDoctorOverrideSession.enMode.View;
            // 
            // ucFinderBox1
            // 
            ucFinderBox1.FilterValuePlaceholder = "";
            ucFinderBox1.InputMode = UcHelpers.ucFinderBox.enInputMode.Any;
            ucFinderBox1.Location = new Point(2, 2);
            ucFinderBox1.Name = "ucFinderBox1";
            ucFinderBox1.ReadOnlyFilterValue = false;
            ucFinderBox1.Size = new Size(551, 52);
            ucFinderBox1.TabIndex = 1;
            // 
            // ucDoctorOverrideSessionFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ucFinderBox1);
            Controls.Add(ucDoctorOverrideSession1);
            Name = "ucDoctorOverrideSessionFinder";
            Size = new Size(557, 393);
            ResumeLayout(false);
        }

        #endregion

        private ucDoctorOverrideSession ucDoctorOverrideSession1;
        private UcHelpers.ucFinderBox ucFinderBox1;
    }
}
