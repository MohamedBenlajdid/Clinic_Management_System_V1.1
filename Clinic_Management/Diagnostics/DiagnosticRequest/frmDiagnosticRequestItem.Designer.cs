namespace Clinic_Management.Diagnostics.DiagnosticRequest
{
    partial class frmDiagnosticRequestItem
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
            ucDiagnosticRequestItem1 = new ucDiagnosticRequestItem();
            SuspendLayout();
            // 
            // ucDiagnosticRequestItem1
            // 
            ucDiagnosticRequestItem1.Location = new Point(6, 4);
            ucDiagnosticRequestItem1.Name = "ucDiagnosticRequestItem1";
            ucDiagnosticRequestItem1.Size = new Size(480, 222);
            ucDiagnosticRequestItem1.TabIndex = 0;
            // 
            // frmDiagnosticRequestItem
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(487, 237);
            Controls.Add(ucDiagnosticRequestItem1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmDiagnosticRequestItem";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmDiagnosticRequestItem";
            ResumeLayout(false);
        }

        #endregion

        private ucDiagnosticRequestItem ucDiagnosticRequestItem1;
    }
}