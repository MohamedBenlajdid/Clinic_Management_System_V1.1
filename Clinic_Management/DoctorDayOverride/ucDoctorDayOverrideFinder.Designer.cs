namespace Clinic_Management.DoctorDayOverride
{
    partial class ucDoctorDayOverrideFinder
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
            ucDoctorDayOverride1 = new ucDoctorDayOverride();
            ucFinderBox1 = new Clinic_Management.UcHelpers.ucFinderBox();
            SuspendLayout();
            // 
            // ucDoctorDayOverride1
            // 
            ucDoctorDayOverride1.Location = new Point(28, 53);
            ucDoctorDayOverride1.Name = "ucDoctorDayOverride1";
            ucDoctorDayOverride1.Size = new Size(496, 304);
            ucDoctorDayOverride1.TabIndex = 0;
            ucDoctorDayOverride1.CurrentMode = ucDoctorDayOverride.enMode.View;
            
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
            // ucDoctorDayOverrideFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ucFinderBox1);
            Controls.Add(ucDoctorDayOverride1);
            Name = "ucDoctorDayOverrideFinder";
            Size = new Size(563, 363);
            ResumeLayout(false);
        }

        #endregion

        private ucDoctorDayOverride ucDoctorDayOverride1;
        private UcHelpers.ucFinderBox ucFinderBox1;
    }
}
