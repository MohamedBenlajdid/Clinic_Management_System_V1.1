namespace Clinic_Management.Person
{
    partial class ucPersonFinder
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
            ucPerson1 = new ucPerson();
            ucFinderBox1 = new Clinic_Management.UcHelpers.ucFinderBox();
            SuspendLayout();
            // 
            // ucPerson1
            // 
            ucPerson1.Location = new Point(53, 58);
            ucPerson1.Name = "ucPerson1";
            ucPerson1.Size = new Size(442, 468);
            ucPerson1.TabIndex = 1;
            ucPerson1.CurrentMode = ucPerson.enMode.View;
            // 
            // ucFinderBox1
            // 
            ucFinderBox1.FilterValuePlaceholder = "";
            ucFinderBox1.InputMode = UcHelpers.ucFinderBox.enInputMode.Any;
            ucFinderBox1.Location = new Point(3, 0);
            ucFinderBox1.Name = "ucFinderBox1";
            ucFinderBox1.ReadOnlyFilterValue = false;
            ucFinderBox1.Size = new Size(551, 52);
            ucFinderBox1.TabIndex = 2;
            // 
            // ucPersonFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ucFinderBox1);
            Controls.Add(ucPerson1);
            Name = "ucPersonFinder";
            Size = new Size(559, 534);
            ResumeLayout(false);
        }

        #endregion
        private ucPerson ucPerson1;
        private UcHelpers.ucFinderBox ucFinderBox1;
    }
}
