namespace Clinic_Management.Diagnostics.DiagnosticTest
{
    partial class frmDiagnosticTest
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
            ucDiagnosticTest1 = new ucDiagnosticTest();
            SuspendLayout();
            // 
            // ucDiagnosticTest1
            // 
            ucDiagnosticTest1.Location = new Point(9, 7);
            ucDiagnosticTest1.Name = "ucDiagnosticTest1";
            ucDiagnosticTest1.Size = new Size(490, 309);
            ucDiagnosticTest1.TabIndex = 0;
            // 
            // frmDiagnosticTest
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(516, 334);
            Controls.Add(ucDiagnosticTest1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmDiagnosticTest";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmDiagnosticTest";
            ResumeLayout(false);
        }

        #endregion

        private ucDiagnosticTest ucDiagnosticTest1;
    }
}