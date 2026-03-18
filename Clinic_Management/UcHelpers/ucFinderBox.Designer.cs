namespace Clinic_Management.UcHelpers
{
    partial class ucFinderBox
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
            gbFinderBox = new GroupBox();
            btnAddNew = new Button();
            btnFind = new Button();
            txtFilterValue = new TextBox();
            label2 = new Label();
            cbFilterBy = new ComboBox();
            label1 = new Label();
            gbFinderBox.SuspendLayout();
            SuspendLayout();
            // 
            // gbFinderBox
            // 
            gbFinderBox.Controls.Add(btnAddNew);
            gbFinderBox.Controls.Add(btnFind);
            gbFinderBox.Controls.Add(txtFilterValue);
            gbFinderBox.Controls.Add(label2);
            gbFinderBox.Controls.Add(cbFilterBy);
            gbFinderBox.Controls.Add(label1);
            gbFinderBox.Location = new Point(3, 3);
            gbFinderBox.Name = "gbFinderBox";
            gbFinderBox.Size = new Size(540, 46);
            gbFinderBox.TabIndex = 0;
            gbFinderBox.TabStop = false;
            gbFinderBox.Text = "Finder Box : ";
            // 
            // btnAddNew
            // 
            btnAddNew.Location = new Point(465, 11);
            btnAddNew.Name = "btnAddNew";
            btnAddNew.Size = new Size(39, 27);
            btnAddNew.TabIndex = 5;
            btnAddNew.Text = "New";
            btnAddNew.UseVisualStyleBackColor = true;
            // 
            // btnFind
            // 
            btnFind.Location = new Point(420, 11);
            btnFind.Name = "btnFind";
            btnFind.Size = new Size(39, 27);
            btnFind.TabIndex = 4;
            btnFind.Text = "Find";
            btnFind.UseVisualStyleBackColor = true;
            // 
            // txtFilterValue
            // 
            txtFilterValue.Location = new Point(285, 16);
            txtFilterValue.Name = "txtFilterValue";
            txtFilterValue.Size = new Size(120, 23);
            txtFilterValue.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(201, 19);
            label2.Name = "label2";
            label2.Size = new Size(78, 15);
            label2.TabIndex = 2;
            label2.Text = "Filter Value : ";
            // 
            // cbFilterBy
            // 
            cbFilterBy.FormattingEnabled = true;
            cbFilterBy.Location = new Point(89, 16);
            cbFilterBy.Name = "cbFilterBy";
            cbFilterBy.Size = new Size(101, 23);
            cbFilterBy.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(21, 19);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 0;
            label1.Text = "Filter By : ";
            // 
            // ucFinderBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gbFinderBox);
            Name = "ucFinderBox";
            Size = new Size(551, 52);
            gbFinderBox.ResumeLayout(false);
            gbFinderBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gbFinderBox;
        private Button btnFind;
        private TextBox txtFilterValue;
        private Label label2;
        private ComboBox cbFilterBy;
        private Label label1;
        private Button btnAddNew;
    }
}
