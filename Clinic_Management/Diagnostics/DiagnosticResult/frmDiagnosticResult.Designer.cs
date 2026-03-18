namespace Clinic_Management.Diagnostics.DiagnosticResult
{
    partial class frmDiagnosticResult
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
            ucDiagnosticResult1 = new ucDiagnosticResult();
            SuspendLayout();
            // 
            // ucDiagnosticResult1
            // 
            ucDiagnosticResult1.Location = new Point(6, 4);
            ucDiagnosticResult1.Name = "ucDiagnosticResult1";
            ucDiagnosticResult1.Size = new Size(477, 307);
            ucDiagnosticResult1.TabIndex = 0;
            // 
            // frmDiagnosticResult
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(491, 317);
            Controls.Add(ucDiagnosticResult1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmDiagnosticResult";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmDiagnosticResult";
            ResumeLayout(false);
        }

        #endregion

        private ucDiagnosticResult ucDiagnosticResult1;
    }
}