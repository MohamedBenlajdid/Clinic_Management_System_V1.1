namespace Clinic_Management.Diagnostics.DiagnosticRequest
{
    partial class frmDiagnosticRequest
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
            ucDiagnosticRequest1 = new ucDiagnosticRequest();
            SuspendLayout();
            // 
            // ucDiagnosticRequest1
            // 
            ucDiagnosticRequest1.Location = new Point(12, 12);
            ucDiagnosticRequest1.Name = "ucDiagnosticRequest1";
            ucDiagnosticRequest1.Size = new Size(474, 287);
            ucDiagnosticRequest1.TabIndex = 0;
            // 
            // frmDiagnosticRequest
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(498, 312);
            Controls.Add(ucDiagnosticRequest1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmDiagnosticRequest";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmDiagnosticRequest";
            ResumeLayout(false);
        }

        #endregion

        private ucDiagnosticRequest ucDiagnosticRequest1;
    }
}