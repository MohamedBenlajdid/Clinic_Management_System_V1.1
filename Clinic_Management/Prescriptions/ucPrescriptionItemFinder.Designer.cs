namespace Clinic_Management.Prescriptions
{
    partial class ucPrescriptionItemFinder
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
            ucPrescriptionItem1 = new ucPrescriptionItem();
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
            // ucPrescriptionItem1
            // 
            ucPrescriptionItem1.Location = new Point(39, 61);
            ucPrescriptionItem1.Name = "ucPrescriptionItem1";
            ucPrescriptionItem1.Size = new Size(478, 336);
            ucPrescriptionItem1.TabIndex = 1;
            ucPrescriptionItem1.CurrentMode = ucPrescriptionItem.enMode.View;
            // 
            // ucPrescriptionItemFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ucPrescriptionItem1);
            Controls.Add(ucFinderBox1);
            Name = "ucPrescriptionItemFinder";
            Size = new Size(569, 413);
            ResumeLayout(false);
        }

        #endregion

        private UcHelpers.ucFinderBox ucFinderBox1;
        private ucPrescriptionItem ucPrescriptionItem1;
    }
}
