namespace Clinic_Management.Diagnostics.DiagnosticTest
{
    partial class ucDiagnosticTestFinder
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
            ucDiagnosticTest1 = new ucDiagnosticTest();
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
            // ucDiagnosticTest1
            // 
            ucDiagnosticTest1.Location = new Point(32, 61);
            ucDiagnosticTest1.Name = "ucDiagnosticTest1";
            ucDiagnosticTest1.Size = new Size(490, 309);
            ucDiagnosticTest1.TabIndex = 1;
            ucDiagnosticTest1.CurrentMode = ucDiagnosticTest.enMode.View;
            // 
            // ucDiagnosticTestFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ucDiagnosticTest1);
            Controls.Add(ucFinderBox1);
            Name = "ucDiagnosticTestFinder";
            Size = new Size(557, 386);
            ResumeLayout(false);
        }

        #endregion

        private UcHelpers.ucFinderBox ucFinderBox1;
        private ucDiagnosticTest ucDiagnosticTest1;
    }
}
