namespace Clinic_Management.DoctorDayOverride
{
    partial class ucDoctorDayOverride
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
            gbOverride = new GroupBox();
            chkIsOverride = new CheckBox();
            chkIsDayOff = new CheckBox();
            txtNotes = new TextBox();
            dtpDate = new DateTimePicker();
            pictureBox1 = new PictureBox();
            label7 = new Label();
            label4 = new Label();
            pictureBox5 = new PictureBox();
            label5 = new Label();
            pictureBox7 = new PictureBox();
            pictureBox6 = new PictureBox();
            label6 = new Label();
            lblOverrideId = new Label();
            label10 = new Label();
            linkEdit = new LinkLabel();
            btnSave = new Button();
            pbPersonImage = new PictureBox();
            lblDoctorId = new Label();
            label11 = new Label();
            errorProvider1 = new ErrorProvider(components);
            linkLabel1 = new LinkLabel();
            gbOverride.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // gbOverride
            // 
            gbOverride.Controls.Add(chkIsOverride);
            gbOverride.Controls.Add(chkIsDayOff);
            gbOverride.Controls.Add(txtNotes);
            gbOverride.Controls.Add(dtpDate);
            gbOverride.Controls.Add(pictureBox1);
            gbOverride.Controls.Add(label7);
            gbOverride.Controls.Add(label4);
            gbOverride.Controls.Add(pictureBox5);
            gbOverride.Controls.Add(label5);
            gbOverride.Controls.Add(pictureBox7);
            gbOverride.Controls.Add(pictureBox6);
            gbOverride.Controls.Add(label6);
            gbOverride.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbOverride.Location = new Point(5, 90);
            gbOverride.Name = "gbOverride";
            gbOverride.Size = new Size(473, 166);
            gbOverride.TabIndex = 109;
            gbOverride.TabStop = false;
            gbOverride.Text = "Override Day Infos : ";
            // 
            // chkIsOverride
            // 
            chkIsOverride.AutoSize = true;
            chkIsOverride.Location = new Point(189, 53);
            chkIsOverride.Name = "chkIsOverride";
            chkIsOverride.Size = new Size(88, 19);
            chkIsOverride.TabIndex = 94;
            chkIsOverride.Text = "Is Override";
            chkIsOverride.UseVisualStyleBackColor = true;
            chkIsOverride.CheckedChanged += chkIsOverride_CheckedChanged;
            // 
            // chkIsDayOff
            // 
            chkIsDayOff.AutoSize = true;
            chkIsDayOff.Location = new Point(189, 80);
            chkIsDayOff.Name = "chkIsDayOff";
            chkIsDayOff.Size = new Size(81, 19);
            chkIsDayOff.TabIndex = 90;
            chkIsDayOff.Text = "Is Day Off";
            chkIsDayOff.UseVisualStyleBackColor = true;
            chkIsDayOff.CheckedChanged += chkIsDayOff_CheckedChanged;
            // 
            // txtNotes
            // 
            txtNotes.Location = new Point(187, 105);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new Size(213, 45);
            txtNotes.TabIndex = 93;
            // 
            // dtpDate
            // 
            dtpDate.Format = DateTimePickerFormat.Short;
            dtpDate.Location = new Point(187, 19);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(207, 23);
            dtpDate.TabIndex = 89;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox1.Location = new Point(136, 22);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(28, 22);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 92;
            pictureBox1.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(9, 27);
            label7.Name = "label7";
            label7.Size = new Size(43, 15);
            label7.TabIndex = 91;
            label7.Text = "Date : ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(9, 54);
            label4.Name = "label4";
            label4.Size = new Size(78, 15);
            label4.TabIndex = 55;
            label4.Text = "Is Override : ";
            // 
            // pictureBox5
            // 
            pictureBox5.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox5.Location = new Point(136, 49);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(28, 22);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 56;
            pictureBox5.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(9, 82);
            label5.Name = "label5";
            label5.Size = new Size(71, 15);
            label5.TabIndex = 57;
            label5.Text = "Is Day Off : ";
            // 
            // pictureBox7
            // 
            pictureBox7.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox7.Location = new Point(136, 105);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(28, 22);
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox7.TabIndex = 61;
            pictureBox7.TabStop = false;
            // 
            // pictureBox6
            // 
            pictureBox6.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox6.Location = new Point(136, 77);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(28, 22);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 58;
            pictureBox6.TabStop = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(9, 110);
            label6.Name = "label6";
            label6.Size = new Size(49, 15);
            label6.TabIndex = 60;
            label6.Text = "Notes : ";
            // 
            // lblOverrideId
            // 
            lblOverrideId.AutoSize = true;
            lblOverrideId.Location = new Point(77, 3);
            lblOverrideId.Name = "lblOverrideId";
            lblOverrideId.Size = new Size(37, 15);
            lblOverrideId.TabIndex = 106;
            lblOverrideId.Text = "[N/A]";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(5, 3);
            label10.Name = "label10";
            label10.Size = new Size(75, 15);
            label10.TabIndex = 105;
            label10.Text = "Override ID : ";
            // 
            // linkEdit
            // 
            linkEdit.AutoSize = true;
            linkEdit.Location = new Point(40, 58);
            linkEdit.Name = "linkEdit";
            linkEdit.Size = new Size(75, 15);
            linkEdit.TabIndex = 104;
            linkEdit.TabStop = true;
            linkEdit.Text = "Edit Override";
            linkEdit.LinkClicked += linkEdit_LinkClicked;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(432, 262);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(61, 38);
            btnSave.TabIndex = 103;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // pbPersonImage
            // 
            pbPersonImage.Image = Properties.Resources.expired;
            pbPersonImage.Location = new Point(192, 3);
            pbPersonImage.Name = "pbPersonImage";
            pbPersonImage.Size = new Size(129, 81);
            pbPersonImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbPersonImage.TabIndex = 95;
            pbPersonImage.TabStop = false;
            // 
            // lblDoctorId
            // 
            lblDoctorId.AutoSize = true;
            lblDoctorId.Location = new Point(77, 23);
            lblDoctorId.Name = "lblDoctorId";
            lblDoctorId.Size = new Size(37, 15);
            lblDoctorId.TabIndex = 111;
            lblDoctorId.Text = "[N/A]";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(5, 23);
            label11.Name = "label11";
            label11.Size = new Size(66, 15);
            label11.TabIndex = 110;
            label11.Text = "Doctor ID : ";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(359, 23);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(77, 15);
            linkLabel1.TabIndex = 112;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Select Doctor";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // ucDoctorDayOverride
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(linkLabel1);
            Controls.Add(lblDoctorId);
            Controls.Add(label11);
            Controls.Add(gbOverride);
            Controls.Add(lblOverrideId);
            Controls.Add(label10);
            Controls.Add(linkEdit);
            Controls.Add(btnSave);
            Controls.Add(pbPersonImage);
            Name = "ucDoctorDayOverride";
            Size = new Size(496, 304);
            gbOverride.ResumeLayout(false);
            gbOverride.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox gbOverride;
        private Label lbl4;
        private CheckBox chkIsDayOff;
        private TextBox txtNotes;
        private DateTimePicker dtpDate;
        private Label lblPersonId;
        private PictureBox pictureBox1;
        private Label label8;
        private Label label7;
        private Label lblStaffID;
        private ComboBox cbDepartmentID;
        private Label label4;
        private PictureBox pictureBox5;
        private Label label5;
        private PictureBox pictureBox7;
        private PictureBox pictureBox6;
        private Label label6;
        private NumericUpDown nudConsultationFee;
        private ComboBox cbSpecialityID;
        private Label lblOverrideId;
        private Label label10;
        private LinkLabel linkEdit;
        private Button btnSave;
        private PictureBox pictureBox4;
        private Label label3;
        private TextBox txtLicenseNumber;
        private PictureBox pictureBox3;
        private Label label1;
        private PictureBox pictureBox2;
        private Label label2;
        private PictureBox pbPersonImage;
        private Label lblDoctorId;
        private Label label11;
        private CheckBox chkIsOverride;
        private ErrorProvider errorProvider1;
        private LinkLabel linkLabel1;
    }
}
