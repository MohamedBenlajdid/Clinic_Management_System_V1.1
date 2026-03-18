namespace Clinic_Management.MedicalRecord
{
    partial class ucMedicalRecordFinder
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
            ucMedicalRecord1 = new ucMedicalRecord();
            SuspendLayout();
            // 
            // ucFinderBox1
            // 
            ucFinderBox1.FilterValuePlaceholder = "";
            ucFinderBox1.InputMode = UcHelpers.ucFinderBox.enInputMode.Any;
            ucFinderBox1.Location = new Point(3, 3);
            ucFinderBox1.Name = "ucFinderBox1";
            ucFinderBox1.ReadOnlyFilterValue = false;
            ucFinderBox1.Size = new Size(551, 52);
            ucFinderBox1.TabIndex = 0;
            // 
            // ucMedicalRecord1
            // 
            ucMedicalRecord1.Location = new Point(32, 61);
            ucMedicalRecord1.Name = "ucMedicalRecord1";
            ucMedicalRecord1.Size = new Size(477, 282);
            ucMedicalRecord1.TabIndex = 1;
            // 
            // ucMedicalRecordFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ucMedicalRecord1);
            Controls.Add(ucFinderBox1);
            Name = "ucMedicalRecordFinder";
            Size = new Size(559, 358);
            ResumeLayout(false);
        }

        #endregion

        private UcHelpers.ucFinderBox ucFinderBox1;
        private ucMedicalRecord ucMedicalRecord1;
    }
}
