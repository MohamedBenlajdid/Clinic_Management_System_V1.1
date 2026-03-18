namespace Clinic_Management.Doctors
{
    partial class frmDoctor
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
            ucDoctor1 = new ucDoctor();
            SuspendLayout();
            // 
            // ucDoctor1
            // 
            ucDoctor1.Location = new Point(2, 2);
            ucDoctor1.Name = "ucDoctor1";
            ucDoctor1.Size = new Size(480, 382);
            ucDoctor1.TabIndex = 0;
            // 
            // frmDoctor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(492, 386);
            Controls.Add(ucDoctor1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmDoctor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmDoctor";
            ResumeLayout(false);
        }

        #endregion

        private ucDoctor ucDoctor1;
    }
}