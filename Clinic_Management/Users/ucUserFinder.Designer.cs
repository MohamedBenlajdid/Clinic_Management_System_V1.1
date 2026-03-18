namespace Clinic_Management.Users
{
    partial class ucUserFinder
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ucFinderBox1 = new Clinic_Management.UcHelpers.ucFinderBox();
            ucUser1 = new ucUser();
            SuspendLayout();
            // 
            // ucFinderBox1
            // 
            ucFinderBox1.FilterValuePlaceholder = "";
            ucFinderBox1.InputMode = UcHelpers.ucFinderBox.enInputMode.Any;
            ucFinderBox1.Location = new Point(0, 0);
            ucFinderBox1.Name = "ucFinderBox1";
            ucFinderBox1.ReadOnlyFilterValue = false;
            ucFinderBox1.Size = new Size(543, 52);
            ucFinderBox1.TabIndex = 0;
            // 
            // ucUser1
            // 
            ucUser1.Enabled = false;
            ucUser1.Location = new Point(63, 58);
            ucUser1.Name = "ucUser1";
            ucUser1.Size = new Size(433, 278);
            ucUser1.TabIndex = 1;
            ucUser1.CurrentMode = ucUser.enMode.View;
            // 
            // ucUserFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ucUser1);
            Controls.Add(ucFinderBox1);
            Name = "ucUserFinder";
            Size = new Size(546, 341);
            ResumeLayout(false);
        }

        #endregion

        private UcHelpers.ucFinderBox ucFinderBox1;
        private ucUser ucUser1;
    }
}
