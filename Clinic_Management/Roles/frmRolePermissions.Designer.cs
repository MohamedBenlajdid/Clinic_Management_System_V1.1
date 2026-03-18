namespace Clinic_Management.Roles
{
    partial class frmRolePermissions
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
            ucRoleDetails1 = new ucRoleDetails();
            ucPermissionsTable1 = new Clinic_Management.Permission.ucPermissionsTable();
            SuspendLayout();
            // 
            // ucRoleDetails1
            // 
            ucRoleDetails1.Location = new Point(103, 12);
            ucRoleDetails1.Name = "ucRoleDetails1";
            ucRoleDetails1.Size = new Size(538, 186);
            ucRoleDetails1.TabIndex = 0;
            // 
            // ucPermissionsTable1
            // 
            ucPermissionsTable1.Enabled = false;
            ucPermissionsTable1.Location = new Point(2, 189);
            ucPermissionsTable1.Name = "ucPermissionsTable1";
            ucPermissionsTable1.Size = new Size(724, 312);
            ucPermissionsTable1.TabIndex = 1;
            // 
            // frmRolePermissions
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(738, 509);
            Controls.Add(ucPermissionsTable1);
            Controls.Add(ucRoleDetails1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmRolePermissions";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmRolePermissions";
            ResumeLayout(false);
        }

        #endregion

        private ucRoleDetails ucRoleDetails1;
        private Permission.ucPermissionsTable ucPermissionsTable1;
    }
}