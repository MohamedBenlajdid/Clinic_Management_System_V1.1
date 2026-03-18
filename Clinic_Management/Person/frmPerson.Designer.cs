namespace Clinic_Management.Person
{
    partial class frmPerson
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
            ucPerson1 = new ucPerson();
            SuspendLayout();
            // 
            // ucPerson1
            // 
            ucPerson1.Location = new Point(15, 12);
            ucPerson1.Name = "ucPerson1";
            ucPerson1.Size = new Size(442, 468);
            ucPerson1.TabIndex = 0;
            // 
            // frmPerson
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(469, 483);
            Controls.Add(ucPerson1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmPerson";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmPerson";
            ResumeLayout(false);
        }

        #endregion

        private ucPerson ucPerson1;
    }
}