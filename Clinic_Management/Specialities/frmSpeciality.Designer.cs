namespace Clinic_Management.Specialities
{
    partial class frmSpeciality
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
            ucSpeciality1 = new ucSpeciality();
            SuspendLayout();
            // 
            // ucSpeciality1
            // 
            ucSpeciality1.Location = new Point(2, 0);
            ucSpeciality1.Name = "ucSpeciality1";
            ucSpeciality1.Size = new Size(512, 194);
            ucSpeciality1.TabIndex = 0;
            // 
            // frmSpeciality
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(519, 208);
            Controls.Add(ucSpeciality1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmSpeciality";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmSpeciality";
            ResumeLayout(false);
        }

        #endregion

        private ucSpeciality ucSpeciality1;
    }
}