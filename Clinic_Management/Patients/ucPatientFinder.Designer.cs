namespace Clinic_Management.Patients
{
    partial class ucPatientFinder
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
            ucFinderBox1 = new Clinic_Management.UcHelpers.ucFinderBox();
            ucPatient1 = new ucPatient();
            SuspendLayout();
            // 
            // ucFinderBox1
            // 
            ucFinderBox1.FilterValuePlaceholder = "";
            ucFinderBox1.InputMode = UcHelpers.ucFinderBox.enInputMode.Any;
            ucFinderBox1.Location = new Point(0, 0);
            ucFinderBox1.Name = "ucFinderBox1";
            ucFinderBox1.ReadOnlyFilterValue = false;
            ucFinderBox1.Size = new Size(551, 52);
            ucFinderBox1.TabIndex = 0;
            
            // 
            // ucPatient1
            // 
            ucPatient1.Location = new Point(38, 58);
            ucPatient1.Name = "ucPatient1";
            ucPatient1.Size = new Size(466, 325);
            ucPatient1.TabIndex = 1;
            ucPatient1.CurrentMode = ucPatient.enMode.View;
            
            // 
            // ucPatientFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ucPatient1);
            Controls.Add(ucFinderBox1);
            Name = "ucPatientFinder";
            Size = new Size(552, 401);
            ResumeLayout(false);
        }

        #endregion

        private UcHelpers.ucFinderBox ucFinderBox1;
        private ucPatient ucPatient1;
    }
}
