namespace Clinic_Management.Roles
{
    partial class frmRole
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
            ucRole1 = new ucRole();
            SuspendLayout();
            // 
            // ucRole1
            // 
            ucRole1.Location = new Point(2, 2);
            ucRole1.Name = "ucRole1";
            ucRole1.Size = new Size(462, 292);
            ucRole1.TabIndex = 0;
            // 
            // frmRole
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(472, 300);
            Controls.Add(ucRole1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmRole";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmRole";
            ResumeLayout(false);
        }

        #endregion

        private ucRole ucRole1;
    }
}