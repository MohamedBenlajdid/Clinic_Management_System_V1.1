namespace Clinic_Management.Users
{
    partial class ucUser
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
            btnSave = new Button();
            txtConfirmPassword = new TextBox();
            pictureBox6 = new PictureBox();
            label5 = new Label();
            txtPassword = new TextBox();
            pictureBox3 = new PictureBox();
            label1 = new Label();
            txtUsername = new TextBox();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            lblUserId = new Label();
            lbl4 = new Label();
            pbPersonImage = new PictureBox();
            btnShowHide = new Button();
            lblPersonId = new Label();
            label4 = new Label();
            chkIsActive = new CheckBox();
            errorProvider1 = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // linkEdit
            // 
            linkEdit.AutoSize = true;
            linkEdit.Location = new Point(35, 79);
            linkEdit.Name = "linkEdit";
            linkEdit.Size = new Size(53, 15);
            linkEdit.TabIndex = 80;
            linkEdit.TabStop = true;
            linkEdit.Text = "Edit User";
            linkEdit.LinkClicked += linkEdit_LinkClicked;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(332, 218);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 47);
            btnSave.TabIndex = 79;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Location = new Point(200, 178);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(207, 23);
            txtConfirmPassword.TabIndex = 59;
            // 
            // pictureBox6
            // 
            pictureBox6.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox6.Location = new Point(147, 179);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(28, 22);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 58;
            pictureBox6.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(20, 184);
            label5.Name = "label5";
            label5.Size = new Size(116, 15);
            label5.TabIndex = 57;
            label5.Text = "Confirm Password : ";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(200, 148);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(207, 23);
            txtPassword.TabIndex = 52;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox3.Location = new Point(147, 149);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(28, 22);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 51;
            pictureBox3.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(20, 154);
            label1.Name = "label1";
            label1.Size = new Size(68, 15);
            label1.TabIndex = 50;
            label1.Text = "Password : ";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(200, 120);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(207, 23);
            txtUsername.TabIndex = 49;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox2.Location = new Point(147, 121);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(28, 22);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 48;
            pictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(20, 126);
            label2.Name = "label2";
            label2.Size = new Size(75, 15);
            label2.TabIndex = 47;
            label2.Text = "UserName : ";
            // 
            // lblUserId
            // 
            lblUserId.AutoSize = true;
            lblUserId.Location = new Point(76, 3);
            lblUserId.Name = "lblUserId";
            lblUserId.Size = new Size(37, 15);
            lblUserId.TabIndex = 46;
            lblUserId.Text = "[N/A]";
            // 
            // lbl4
            // 
            lbl4.AutoSize = true;
            lbl4.Location = new Point(4, 3);
            lbl4.Name = "lbl4";
            lbl4.Size = new Size(53, 15);
            lbl4.TabIndex = 45;
            lbl4.Text = "User ID : ";
            // 
            // pbPersonImage
            // 
            pbPersonImage.Image = Properties.Resources.Aha_Soft_Free_Large_Boss_Admin_513;
            pbPersonImage.Location = new Point(147, 0);
            pbPersonImage.Name = "pbPersonImage";
            pbPersonImage.Size = new Size(129, 81);
            pbPersonImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbPersonImage.TabIndex = 44;
            pbPersonImage.TabStop = false;
            // 
            // btnShowHide
            // 
            btnShowHide.BackgroundImage = Properties.Resources.ShowPass;
            btnShowHide.BackgroundImageLayout = ImageLayout.Zoom;
            btnShowHide.FlatStyle = FlatStyle.Flat;
            btnShowHide.Location = new Point(356, 79);
            btnShowHide.Name = "btnShowHide";
            btnShowHide.Size = new Size(51, 35);
            btnShowHide.TabIndex = 81;
            btnShowHide.UseVisualStyleBackColor = true;
            // 
            // lblPersonId
            // 
            lblPersonId.AutoSize = true;
            lblPersonId.Location = new Point(76, 24);
            lblPersonId.Name = "lblPersonId";
            lblPersonId.Size = new Size(37, 15);
            lblPersonId.TabIndex = 83;
            lblPersonId.Text = "[N/A]";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(4, 24);
            label4.Name = "label4";
            label4.Size = new Size(66, 15);
            label4.TabIndex = 82;
            label4.Text = "Person ID : ";
            // 
            // chkIsActive
            // 
            chkIsActive.AutoSize = true;
            chkIsActive.Location = new Point(147, 233);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(59, 19);
            chkIsActive.TabIndex = 84;
            chkIsActive.Text = "Active";
            chkIsActive.UseVisualStyleBackColor = true;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // ucUser
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(chkIsActive);
            Controls.Add(lblPersonId);
            Controls.Add(label4);
            Controls.Add(btnShowHide);
            Controls.Add(linkEdit);
            Controls.Add(btnSave);
            Controls.Add(txtConfirmPassword);
            Controls.Add(pictureBox6);
            Controls.Add(label5);
            Controls.Add(txtPassword);
            Controls.Add(pictureBox3);
            Controls.Add(label1);
            Controls.Add(txtUsername);
            Controls.Add(pictureBox2);
            Controls.Add(label2);
            Controls.Add(lblUserId);
            Controls.Add(lbl4);
            Controls.Add(pbPersonImage);
            Name = "ucUser";
            Size = new Size(433, 278);
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private LinkLabel linkEdit;
        private Button btnSave;
        private TextBox txtConfirmPassword;
        private PictureBox pictureBox6;
        private Label label5;
        private TextBox txtPassword;
        private PictureBox pictureBox3;
        private Label label1;
        private TextBox txtUsername;
        private PictureBox pictureBox2;
        private Label label2;
        private Label lblUserId;
        private Label lbl4;
        private PictureBox pbPersonImage;
        private Button btnShowHide;
        private Label lblPersonId;
        private Label label4;
        private CheckBox chkIsActive;
        private ErrorProvider errorProvider1;
    }
}
