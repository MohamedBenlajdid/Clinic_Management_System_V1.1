namespace Clinic_Management.Department
{
    partial class frmDepartment
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
            ucDepartment1 = new ucDepartment();
            SuspendLayout();
            // 
            // ucDepartment1
            // 
            ucDepartment1.Location = new Point(1, 2);
            ucDepartment1.Name = "ucDepartment1";
            ucDepartment1.Size = new Size(504, 187);
            ucDepartment1.TabIndex = 0;
            // 
            // frmDepartment
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(516, 207);
            Controls.Add(ucDepartment1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmDepartment";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmDepartment";
            ResumeLayout(false);
        }

        #endregion

        private ucDepartment ucDepartment1;
    }
}