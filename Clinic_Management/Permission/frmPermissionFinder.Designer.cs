namespace Clinic_Management.Permission
{
    partial class frmPermissionFinder
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
            ucPermissionFinder1 = new ucPermissionFinder();
            SuspendLayout();
            // 
            // ucPermissionFinder1
            // 
            ucPermissionFinder1.Location = new Point(1, 1);
            ucPermissionFinder1.Name = "ucPermissionFinder1";
            ucPermissionFinder1.Size = new Size(550, 374);
            ucPermissionFinder1.TabIndex = 0;
            // 
            // frmPermissionFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(553, 372);
            Controls.Add(ucPermissionFinder1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmPermissionFinder";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmPermissionFinder";
            ResumeLayout(false);
        }

        #endregion

        private ucPermissionFinder ucPermissionFinder1;
    }
}