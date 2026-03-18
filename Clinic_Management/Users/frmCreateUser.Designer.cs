namespace Clinic_Management.Users
{
    partial class frmCreateUser
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
            pnlContainer = new Panel();
            ucPersonFinder1 = new Clinic_Management.Person.ucPersonFinder();
            btnPerson = new Button();
            btnUser = new Button();
            lbl = new Label();
            cbUserRole = new ComboBox();
            ucUser1 = new ucUser();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlContainer.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Gakuseisean_Aire_Add_Folder_256;
            pictureBox1.Location = new Point(227, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 75);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pnlContainer
            // 
            pnlContainer.Controls.Add(ucPersonFinder1);
            pnlContainer.Controls.Add(ucUser1);
            pnlContainer.Location = new Point(12, 103);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new Size(580, 535);
            pnlContainer.TabIndex = 1;
            // 
            // ucPersonFinder1
            // 
            ucPersonFinder1.Location = new Point(0, 0);
            ucPersonFinder1.Name = "ucPersonFinder1";
            ucPersonFinder1.Size = new Size(559, 534);
            ucPersonFinder1.TabIndex = 0;
            // 
            // btnPerson
            // 
            btnPerson.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPerson.Location = new Point(12, 74);
            btnPerson.Name = "btnPerson";
            btnPerson.Size = new Size(75, 23);
            btnPerson.TabIndex = 2;
            btnPerson.Text = "Person";
            btnPerson.UseVisualStyleBackColor = true;
            // 
            // btnUser
            // 
            btnUser.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnUser.Location = new Point(84, 74);
            btnUser.Name = "btnUser";
            btnUser.Size = new Size(75, 23);
            btnUser.TabIndex = 3;
            btnUser.Text = "User";
            btnUser.UseVisualStyleBackColor = true;
            // 
            // lbl
            // 
            lbl.AutoSize = true;
            lbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl.Location = new Point(30, 9);
            lbl.Name = "lbl";
            lbl.Size = new Size(70, 15);
            lbl.TabIndex = 4;
            lbl.Text = "User Role : ";
            // 
            // cbUserRole
            // 
            cbUserRole.FormattingEnabled = true;
            cbUserRole.Location = new Point(30, 27);
            cbUserRole.Name = "cbUserRole";
            cbUserRole.Size = new Size(121, 23);
            cbUserRole.TabIndex = 5;
            // 
            // ucUser1
            // 
            ucUser1.Enabled = false;
            ucUser1.Location = new Point(3, 3);
            ucUser1.Name = "ucUser1";
            ucUser1.Size = new Size(433, 278);
            ucUser1.TabIndex = 6;
            // 
            // frmCreateUser
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(595, 630);
            Controls.Add(cbUserRole);
            Controls.Add(lbl);
            Controls.Add(btnUser);
            Controls.Add(btnPerson);
            Controls.Add(pnlContainer);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmCreateUser";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmCreateUser";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pnlContainer.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Panel pnlContainer;
        private Button btnPerson;
        private Button btnUser;
        private Label lbl;
        private ComboBox cbUserRole;
        private Person.ucPersonFinder ucPersonFinder1;
        private ucUser ucUser1;
    }
}