namespace Clinic_Management.Diagnostics.DiagnosticTest
{
    partial class frmDiagnosticTestFinder
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
            ucDiagnosticTestFinder1 = new ucDiagnosticTestFinder();
            SuspendLayout();
            // 
            // ucDiagnosticTestFinder1
            // 
            ucDiagnosticTestFinder1.Location = new Point(6, 2);
            ucDiagnosticTestFinder1.Name = "ucDiagnosticTestFinder1";
            ucDiagnosticTestFinder1.Size = new Size(557, 386);
            ucDiagnosticTestFinder1.TabIndex = 0;
            // 
            // frmDiagnosticTestFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(571, 387);
            Controls.Add(ucDiagnosticTestFinder1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmDiagnosticTestFinder";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmDiagnosticTestFinder";
            ResumeLayout(false);
        }

        #endregion

        private ucDiagnosticTestFinder ucDiagnosticTestFinder1;
    }
}