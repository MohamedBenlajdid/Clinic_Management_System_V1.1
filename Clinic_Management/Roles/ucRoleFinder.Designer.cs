namespace Clinic_Management.Roles
{
    partial class ucRoleFinder
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
            ucRole1 = new ucRole();
            SuspendLayout();
            // 
            // ucFinderBox1
            // 
            ucFinderBox1.FilterValuePlaceholder = "";
            ucFinderBox1.InputMode = UcHelpers.ucFinderBox.enInputMode.Any;
            ucFinderBox1.Location = new Point(2, 2);
            ucFinderBox1.Name = "ucFinderBox1";
            ucFinderBox1.ReadOnlyFilterValue = false;
            ucFinderBox1.Size = new Size(551, 52);
            ucFinderBox1.TabIndex = 0;
            // 
            // ucRole1
            // 
            ucRole1.Location = new Point(46, 60);
            ucRole1.Name = "ucRole1";
            ucRole1.Size = new Size(462, 292);
            ucRole1.TabIndex = 1;
            ucRole1.CurrentMode = ucRole.enMode.View;
            // 
            // ucRoleFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ucRole1);
            Controls.Add(ucFinderBox1);
            Name = "ucRoleFinder";
            Size = new Size(553, 360);
            ResumeLayout(false);
        }

        #endregion

        private UcHelpers.ucFinderBox ucFinderBox1;
        private ucRole ucRole1;
    }
}
