namespace Clinic_Management.Users
{
    partial class frmUserFinder
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
            ucUserFinder1 = new ucUserFinder();
            SuspendLayout();
            // 
            // ucUserFinder1
            // 
            ucUserFinder1.Location = new Point(1, 1);
            ucUserFinder1.Name = "ucUserFinder1";
            ucUserFinder1.Size = new Size(546, 341);
            ucUserFinder1.TabIndex = 0;
            // 
            // frmUserFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(556, 357);
            Controls.Add(ucUserFinder1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmUserFinder";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmUserFinder";
            ResumeLayout(false);
        }

        #endregion

        private ucUserFinder ucUserFinder1;
    }
}