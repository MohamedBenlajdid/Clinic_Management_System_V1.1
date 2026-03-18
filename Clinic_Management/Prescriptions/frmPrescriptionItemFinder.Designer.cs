namespace Clinic_Management.Prescriptions
{
    partial class frmPrescriptionItemFinder
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
            ucPrescriptionItemFinder1 = new ucPrescriptionItemFinder();
            SuspendLayout();
            // 
            // ucPrescriptionItemFinder1
            // 
            ucPrescriptionItemFinder1.Location = new Point(7, 5);
            ucPrescriptionItemFinder1.Name = "ucPrescriptionItemFinder1";
            ucPrescriptionItemFinder1.Size = new Size(569, 413);
            ucPrescriptionItemFinder1.TabIndex = 0;
            // 
            // frmPrescriptionItemFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 423);
            Controls.Add(ucPrescriptionItemFinder1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmPrescriptionItemFinder";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmPrescriptionItemFinder";
            ResumeLayout(false);
        }

        #endregion

        private ucPrescriptionItemFinder ucPrescriptionItemFinder1;
    }
}