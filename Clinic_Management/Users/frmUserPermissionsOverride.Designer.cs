namespace Clinic_Management.Users
{
    partial class frmUserPermissionsOverride
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
            ucUserDetails1 = new ucUserDetails();
            ucPermissionsTable1 = new Clinic_Management.Permission.ucPermissionsTable();
            SuspendLayout();
            // 
            // ucUserDetails1
            // 
            ucUserDetails1.Location = new Point(104, 2);
            ucUserDetails1.Name = "ucUserDetails1";
            ucUserDetails1.Size = new Size(540, 223);
            ucUserDetails1.TabIndex = 0;
            ucUserDetails1.TabStop = false;
            // 
            // ucPermissionsTable1
            // 
            ucPermissionsTable1.Enabled = false;
            ucPermissionsTable1.Location = new Point(5, 216);
            ucPermissionsTable1.Name = "ucPermissionsTable1";
            ucPermissionsTable1.Size = new Size(727, 317);
            ucPermissionsTable1.TabIndex = 1;
            // 
            // frmUserPermissionsOverride
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(744, 527);
            Controls.Add(ucPermissionsTable1);
            Controls.Add(ucUserDetails1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmUserPermissionsOverride";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmUserPermissionsOverride";
            ResumeLayout(false);
        }

        #endregion

        private ucUserDetails ucUserDetails1;
        private Permission.ucPermissionsTable ucPermissionsTable1;
    }
}