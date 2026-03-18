namespace Clinic_Management.Permission
{
    partial class ucPermission
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
            txtName = new TextBox();
            chkIsActive = new CheckBox();
            linkEdit = new LinkLabel();
            btnSave = new Button();
            txtDescription = new TextBox();
            pictureBox6 = new PictureBox();
            label5 = new Label();
            pictureBox3 = new PictureBox();
            label1 = new Label();
            txtCode = new TextBox();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            lblPermission = new Label();
            lbl4 = new Label();
            pbPersonImage = new PictureBox();
            txtModule = new TextBox();
            pictureBox1 = new PictureBox();
            label3 = new Label();
            errorProvider1 = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Location = new Point(205, 152);
            txtName.Name = "txtName";
            txtName.Size = new Size(207, 23);
            txtName.TabIndex = 118;
            // 
            // chkIsActive
            // 
            chkIsActive.AutoSize = true;
            chkIsActive.Location = new Point(121, 263);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(59, 19);
            chkIsActive.TabIndex = 117;
            chkIsActive.Text = "Active";
            chkIsActive.UseVisualStyleBackColor = true;
            // 
            // linkEdit
            // 
            linkEdit.AutoSize = true;
            linkEdit.Location = new Point(40, 82);
            linkEdit.Name = "linkEdit";
            linkEdit.Size = new Size(88, 15);
            linkEdit.TabIndex = 116;
            linkEdit.TabStop = true;
            linkEdit.Text = "Edit Permission";
            linkEdit.LinkClicked += linkEdit_LinkClicked;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(409, 248);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 47);
            btnSave.TabIndex = 115;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(205, 181);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(207, 23);
            txtDescription.TabIndex = 114;
            // 
            // pictureBox6
            // 
            pictureBox6.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox6.Location = new Point(152, 182);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(28, 22);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 113;
            pictureBox6.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(25, 187);
            label5.Name = "label5";
            label5.Size = new Size(80, 15);
            label5.TabIndex = 112;
            label5.Text = "Description : ";
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox3.Location = new Point(152, 152);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(28, 22);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 111;
            pictureBox3.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(25, 157);
            label1.Name = "label1";
            label1.Size = new Size(77, 15);
            label1.TabIndex = 110;
            label1.Text = "Role Name : ";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // txtCode
            // 
            txtCode.Location = new Point(205, 123);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(207, 23);
            txtCode.TabIndex = 109;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox2.Location = new Point(152, 124);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(28, 22);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 108;
            pictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(25, 129);
            label2.Name = "label2";
            label2.Size = new Size(72, 15);
            label2.TabIndex = 107;
            label2.Text = "Role Code : ";
            // 
            // lblPermission
            // 
            lblPermission.AutoSize = true;
            lblPermission.Location = new Point(91, 6);
            lblPermission.Name = "lblPermission";
            lblPermission.Size = new Size(37, 15);
            lblPermission.TabIndex = 106;
            lblPermission.Text = "[N/A]";
            // 
            // lbl4
            // 
            lbl4.AutoSize = true;
            lbl4.Location = new Point(9, 6);
            lbl4.Name = "lbl4";
            lbl4.Size = new Size(88, 15);
            lbl4.TabIndex = 105;
            lbl4.Text = "Permission ID : ";
            // 
            // pbPersonImage
            // 
            pbPersonImage.Image = Properties.Resources.Everaldo_Kids_Icons_K_services_128;
            pbPersonImage.Location = new Point(197, 3);
            pbPersonImage.Name = "pbPersonImage";
            pbPersonImage.Size = new Size(129, 81);
            pbPersonImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbPersonImage.TabIndex = 104;
            pbPersonImage.TabStop = false;
            // 
            // txtModule
            // 
            txtModule.Location = new Point(205, 209);
            txtModule.Name = "txtModule";
            txtModule.Size = new Size(207, 23);
            txtModule.TabIndex = 121;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox1.Location = new Point(152, 210);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(28, 22);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 120;
            pictureBox1.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(25, 215);
            label3.Name = "label3";
            label3.Size = new Size(58, 15);
            label3.TabIndex = 119;
            label3.Text = "Module : ";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // ucPermission
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(txtModule);
            Controls.Add(pictureBox1);
            Controls.Add(label3);
            Controls.Add(txtName);
            Controls.Add(chkIsActive);
            Controls.Add(linkEdit);
            Controls.Add(btnSave);
            Controls.Add(txtDescription);
            Controls.Add(pictureBox6);
            Controls.Add(label5);
            Controls.Add(pictureBox3);
            Controls.Add(label1);
            Controls.Add(txtCode);
            Controls.Add(pictureBox2);
            Controls.Add(label2);
            Controls.Add(lblPermission);
            Controls.Add(lbl4);
            Controls.Add(pbPersonImage);
            Name = "ucPermission";
            Size = new Size(487, 298);
            Load += ucPermission_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtName;
        private CheckBox chkIsActive;
        private LinkLabel linkEdit;
        private Button btnSave;
        private TextBox txtDescription;
        private PictureBox pictureBox6;
        private Label label5;
        private PictureBox pictureBox3;
        private Label label1;
        private TextBox txtCode;
        private PictureBox pictureBox2;
        private Label label2;
        private Label lblPermission;
        private Label lbl4;
        private PictureBox pbPersonImage;
        private TextBox txtModule;
        private PictureBox pictureBox1;
        private Label label3;
        private ErrorProvider errorProvider1;
    }
}
