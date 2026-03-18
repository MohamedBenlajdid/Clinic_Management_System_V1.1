namespace Clinic_Management.Department
{
    partial class ucDepartment
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
            components = new System.ComponentModel.Container();
            linkEdit = new LinkLabel();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            lblDepartmentID = new Label();
            lbl4 = new Label();
            pbPersonImage = new PictureBox();
            btnSave = new Button();
            txtDepartmentName = new TextBox();
            errorProvider1 = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // linkEdit
            // 
            linkEdit.AutoSize = true;
            linkEdit.Location = new Point(46, 51);
            linkEdit.Name = "linkEdit";
            linkEdit.Size = new Size(93, 15);
            linkEdit.TabIndex = 90;
            linkEdit.TabStop = true;
            linkEdit.Text = "Edit Department";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox2.Location = new Point(151, 101);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(28, 22);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 88;
            pictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(24, 106);
            label2.Name = "label2";
            label2.Size = new Size(121, 15);
            label2.TabIndex = 87;
            label2.Text = "Department Name : ";
            // 
            // lblDepartmentID
            // 
            lblDepartmentID.AutoSize = true;
            lblDepartmentID.Location = new Point(107, 6);
            lblDepartmentID.Name = "lblDepartmentID";
            lblDepartmentID.Size = new Size(37, 15);
            lblDepartmentID.TabIndex = 86;
            lblDepartmentID.Text = "[N/A]";
            // 
            // lbl4
            // 
            lbl4.AutoSize = true;
            lbl4.Location = new Point(8, 6);
            lbl4.Name = "lbl4";
            lbl4.Size = new Size(93, 15);
            lbl4.TabIndex = 85;
            lbl4.Text = "Department ID : ";
            // 
            // pbPersonImage
            // 
            pbPersonImage.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pbPersonImage.Location = new Point(204, 0);
            pbPersonImage.Name = "pbPersonImage";
            pbPersonImage.Size = new Size(129, 81);
            pbPersonImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbPersonImage.TabIndex = 84;
            pbPersonImage.TabStop = false;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(429, 143);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(72, 41);
            btnSave.TabIndex = 91;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // txtDepartmentName
            // 
            txtDepartmentName.Location = new Point(194, 100);
            txtDepartmentName.Name = "txtDepartmentName";
            txtDepartmentName.Size = new Size(238, 23);
            txtDepartmentName.TabIndex = 92;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // ucDepartment
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(txtDepartmentName);
            Controls.Add(btnSave);
            Controls.Add(linkEdit);
            Controls.Add(pictureBox2);
            Controls.Add(label2);
            Controls.Add(lblDepartmentID);
            Controls.Add(lbl4);
            Controls.Add(pbPersonImage);
            Name = "ucDepartment";
            Size = new Size(504, 187);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPersonId;
        private Label label4;
        private Button btnShowHide;
        private LinkLabel linkEdit;
        private TextBox txtUsername;
        private PictureBox pictureBox2;
        private Label label2;
        private Label lblDepartmentID;
        private Label lbl4;
        private PictureBox pbPersonImage;
        private Button btnSave;
        private TextBox txtDepartmentName;
        private ErrorProvider errorProvider1;
    }
}
