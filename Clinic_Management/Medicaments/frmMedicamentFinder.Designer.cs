namespace Clinic_Management.Medicaments
{
    partial class frmMedicamentFinder
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
            ucMedicamentFinder1 = new ucMedicamentFinder();
            SuspendLayout();
            // 
            // ucMedicamentFinder1
            // 
            ucMedicamentFinder1.Location = new Point(5, 5);
            ucMedicamentFinder1.Name = "ucMedicamentFinder1";
            ucMedicamentFinder1.Size = new Size(557, 413);
            ucMedicamentFinder1.TabIndex = 0;
            // 
            // frmMedicamentFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(570, 419);
            Controls.Add(ucMedicamentFinder1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmMedicamentFinder";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmMedicamentFinder";
            ResumeLayout(false);
        }

        #endregion

        private ucMedicamentFinder ucMedicamentFinder1;
    }
}