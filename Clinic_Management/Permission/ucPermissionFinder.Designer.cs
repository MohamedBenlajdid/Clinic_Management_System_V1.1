namespace Clinic_Management.Permission
{
    partial class ucPermissionFinder
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
            ucPermission1 = new ucPermission();
            ucFinderBox1 = new Clinic_Management.UcHelpers.ucFinderBox();
            SuspendLayout();
            // 
            // ucPermission1
            // 
            ucPermission1.Location = new Point(30, 57);
            ucPermission1.Name = "ucPermission1";
            ucPermission1.Size = new Size(487, 298);
            ucPermission1.TabIndex = 0;
            // 
            // ucFinderBox1
            // 
            ucFinderBox1.FilterValuePlaceholder = "";
            ucFinderBox1.InputMode = UcHelpers.ucFinderBox.enInputMode.Any;
            ucFinderBox1.Location = new Point(0, 2);
            ucFinderBox1.Name = "ucFinderBox1";
            ucFinderBox1.ReadOnlyFilterValue = false;
            ucFinderBox1.Size = new Size(551, 52);
            ucFinderBox1.TabIndex = 1;
            // 
            // ucPermissionFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ucFinderBox1);
            Controls.Add(ucPermission1);
            Name = "ucPermissionFinder";
            Size = new Size(550, 374);
            ResumeLayout(false);
        }

        #endregion

        private ucPermission ucPermission1;
        private UcHelpers.ucFinderBox ucFinderBox1;
    }
}
