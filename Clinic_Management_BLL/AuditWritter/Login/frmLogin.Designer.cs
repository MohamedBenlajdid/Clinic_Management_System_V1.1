namespace Clinic_Management.Login
{
    partial class frmLogin
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
            pictureBox1 = new PictureBox();
            gbLoginInfos = new GroupBox();
            btnShowHide = new Button();
            btnLogin = new Button();
            chkRememberMe = new CheckBox();
            txtPassword = new TextBox();
            txtUserName = new TextBox();
            label2 = new Label();
            label1 = new Label();
            linkCreateNewAccount = new LinkLabel();
            pictureBox2 = new PictureBox();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            gbLoginInfos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.pngwing_com__1_;
            pictureBox1.Location = new Point(41, 89);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(253, 313);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // gbLoginInfos
            // 
            gbLoginInfos.Controls.Add(btnShowHide);
            gbLoginInfos.Controls.Add(btnLogin);
            gbLoginInfos.Controls.Add(chkRememberMe);
            gbLoginInfos.Controls.Add(txtPassword);
            gbLoginInfos.Controls.Add(txtUserName);
            gbLoginInfos.Controls.Add(label2);
            gbLoginInfos.Controls.Add(label1);
            gbLoginInfos.Location = new Point(320, 108);
            gbLoginInfos.Name = "gbLoginInfos";
            gbLoginInfos.Size = new Size(340, 191);
            gbLoginInfos.TabIndex = 1;
            gbLoginInfos.TabStop = false;
            gbLoginInfos.Text = "Login : ";
            // 
            // btnShowHide
            // 
            btnShowHide.FlatStyle = FlatStyle.Flat;
            btnShowHide.Location = new Point(304, 13);
            btnShowHide.Name = "btnShowHide";
            btnShowHide.Size = new Size(36, 26);
            btnShowHide.TabIndex = 6;
            btnShowHide.UseVisualStyleBackColor = true;
            btnShowHide.Click += btnShowHide_Click;
            // 
            // btnLogin
            // 
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Location = new Point(290, 146);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(44, 39);
            btnLogin.TabIndex = 5;
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // chkRememberMe
            // 
            chkRememberMe.AutoSize = true;
            chkRememberMe.Location = new Point(63, 133);
            chkRememberMe.Name = "chkRememberMe";
            chkRememberMe.Size = new Size(107, 19);
            chkRememberMe.TabIndex = 4;
            chkRememberMe.Text = "Remember Me ";
            chkRememberMe.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(128, 95);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(206, 23);
            txtPassword.TabIndex = 3;
            // 
            // txtUserName
            // 
            txtUserName.Location = new Point(128, 60);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(206, 23);
            txtUserName.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(35, 98);
            label2.Name = "label2";
            label2.Size = new Size(68, 15);
            label2.TabIndex = 1;
            label2.Text = "Password : ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(35, 63);
            label1.Name = "label1";
            label1.Size = new Size(75, 15);
            label1.TabIndex = 0;
            label1.Text = "UserName : ";
            // 
            // linkCreateNewAccount
            // 
            linkCreateNewAccount.AutoSize = true;
            linkCreateNewAccount.Location = new Point(383, 327);
            linkCreateNewAccount.Name = "linkCreateNewAccount";
            linkCreateNewAccount.Size = new Size(116, 15);
            linkCreateNewAccount.TabIndex = 2;
            linkCreateNewAccount.TabStop = true;
            linkCreateNewAccount.Text = "Create New Account";
            linkCreateNewAccount.LinkClicked += linkAddNewAccount_LinkClicked;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.sign;
            pictureBox2.Location = new Point(-1, 2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(104, 88);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(227, 9);
            label3.Name = "label3";
            label3.Size = new Size(251, 25);
            label3.TabIndex = 4;
            label3.Text = "Clinic Management System";
            // 
            // frmLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(672, 450);
            Controls.Add(label3);
            Controls.Add(pictureBox2);
            Controls.Add(linkCreateNewAccount);
            Controls.Add(gbLoginInfos);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmLogin";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            gbLoginInfos.ResumeLayout(false);
            gbLoginInfos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private GroupBox gbLoginInfos;
        private Button btnLogin;
        private CheckBox chkRememberMe;
        private TextBox txtPassword;
        private TextBox txtUserName;
        private Label label2;
        private Label label1;
        private Button btnShowHide;
        private LinkLabel linkCreateNewAccount;
        private PictureBox pictureBox2;
        private Label label3;
    }
}