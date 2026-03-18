namespace Clinic_Management.Prescriptions
{
    partial class frmPrescriptionItem
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
            ucPrescriptionItem1 = new ucPrescriptionItem();
            SuspendLayout();
            // 
            // ucPrescriptionItem1
            // 
            ucPrescriptionItem1.Location = new Point(8, 7);
            ucPrescriptionItem1.Name = "ucPrescriptionItem1";
            ucPrescriptionItem1.Size = new Size(478, 336);
            ucPrescriptionItem1.TabIndex = 0;
            // 
            // frmPrescriptionItem
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(498, 351);
            Controls.Add(ucPrescriptionItem1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmPrescriptionItem";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmPrescriptionItem";
            ResumeLayout(false);
        }

        #endregion

        private ucPrescriptionItem ucPrescriptionItem1;
    }
}