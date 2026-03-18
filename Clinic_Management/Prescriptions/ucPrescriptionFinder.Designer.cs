namespace Clinic_Management.Prescriptions
{
    partial class ucPrescriptionFinder
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
            ucPrescription1 = new ucPrescription();
            SuspendLayout();
            // 
            // ucFinderBox1
            // 
            ucFinderBox1.FilterValuePlaceholder = "";
            ucFinderBox1.InputMode = UcHelpers.ucFinderBox.enInputMode.Any;
            ucFinderBox1.Location = new Point(1, 0);
            ucFinderBox1.Name = "ucFinderBox1";
            ucFinderBox1.ReadOnlyFilterValue = false;
            ucFinderBox1.Size = new Size(551, 52);
            ucFinderBox1.TabIndex = 0;
            // 
            // ucPrescription1
            // 
            ucPrescription1.Location = new Point(38, 58);
            ucPrescription1.Name = "ucPrescription1";
            ucPrescription1.Size = new Size(475, 228);
            ucPrescription1.TabIndex = 1;
            ucPrescription1.CurrentMode = ucPrescription.enMode.View;
            // 
            // ucPrescriptionFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ucPrescription1);
            Controls.Add(ucFinderBox1);
            Name = "ucPrescriptionFinder";
            Size = new Size(554, 303);
            ResumeLayout(false);
        }

        #endregion

        private UcHelpers.ucFinderBox ucFinderBox1;
        private ucPrescription ucPrescription1;
    }
}
