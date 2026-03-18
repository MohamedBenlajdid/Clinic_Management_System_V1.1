namespace Clinic_Management.Person
{
    partial class frmPersonFinder
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
            ucPersonFinder1 = new ucPersonFinder();
            SuspendLayout();
            // 
            // ucPersonFinder1
            // 
            ucPersonFinder1.Location = new Point(2, 0);
            ucPersonFinder1.Name = "ucPersonFinder1";
            ucPersonFinder1.Size = new Size(559, 534);
            ucPersonFinder1.TabIndex = 0;
            // 
            // frmPersonFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(567, 528);
            Controls.Add(ucPersonFinder1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmPersonFinder";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmPersonFinder";
            ResumeLayout(false);
        }

        #endregion

        private ucPersonFinder ucPersonFinder1;
    }
}