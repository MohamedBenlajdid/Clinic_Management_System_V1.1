namespace Clinic_Management.Roles
{
    partial class frmRoleFinder
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
            ucRoleFinder1 = new ucRoleFinder();
            SuspendLayout();
            // 
            // ucRoleFinder1
            // 
            ucRoleFinder1.Location = new Point(1, 1);
            ucRoleFinder1.Name = "ucRoleFinder1";
            ucRoleFinder1.Size = new Size(553, 360);
            ucRoleFinder1.TabIndex = 0;
            // 
            // frmRoleFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(555, 357);
            Controls.Add(ucRoleFinder1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmRoleFinder";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmRoleFinder";
            ResumeLayout(false);
        }

        #endregion

        private ucRoleFinder ucRoleFinder1;
    }
}