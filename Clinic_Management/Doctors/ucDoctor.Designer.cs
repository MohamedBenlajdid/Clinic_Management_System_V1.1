namespace Clinic_Management.Doctors
{
    partial class ucDoctor
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
            cbDepartmentID = new ComboBox();
            linkEdit = new LinkLabel();
            btnSave = new Button();
            pictureBox7 = new PictureBox();
            label6 = new Label();
            pictureBox6 = new PictureBox();
            label5 = new Label();
            pictureBox5 = new PictureBox();
            label4 = new Label();
            pictureBox4 = new PictureBox();
            label3 = new Label();
            txtLicenseNumber = new TextBox();
            pictureBox3 = new PictureBox();
            label1 = new Label();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            lblPersonId = new Label();
            lbl4 = new Label();
            pbPersonImage = new PictureBox();
            lblStaffID = new Label();
            label8 = new Label();
            lblDoctorID = new Label();
            label10 = new Label();
            cbSpecialityID = new ComboBox();
            nudConsultationFee = new NumericUpDown();
            dtpHireDate = new DateTimePicker();
            chkIsActive = new CheckBox();
            txtStaffCode = new TextBox();
            pictureBox1 = new PictureBox();
            label7 = new Label();
            errorProvider1 = new ErrorProvider(components);
            gbStaffInfos = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudConsultationFee).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            gbStaffInfos.SuspendLayout();
            SuspendLayout();
            // 
            // cbDepartmentID
            // 
            cbDepartmentID.FormattingEnabled = true;
            cbDepartmentID.Location = new Point(189, 77);
            cbDepartmentID.Name = "cbDepartmentID";
            cbDepartmentID.Size = new Size(207, 23);
            cbDepartmentID.TabIndex = 82;
            // 
            // linkEdit
            // 
            linkEdit.AutoSize = true;
            linkEdit.Location = new Point(39, 41);
            linkEdit.Name = "linkEdit";
            linkEdit.Size = new Size(66, 15);
            linkEdit.TabIndex = 80;
            linkEdit.TabStop = true;
            linkEdit.Text = "Edit Doctor";
            linkEdit.LinkClicked += linkEdit_LinkClicked;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(416, 341);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(61, 38);
            btnSave.TabIndex = 79;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // pictureBox7
            // 
            pictureBox7.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox7.Location = new Point(136, 133);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(28, 22);
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox7.TabIndex = 61;
            pictureBox7.TabStop = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(9, 138);
            label6.Name = "label6";
            label6.Size = new Size(64, 15);
            label6.TabIndex = 60;
            label6.Text = "Is Active ; ";
            // 
            // pictureBox6
            // 
            pictureBox6.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox6.Location = new Point(136, 105);
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
            label5.Location = new Point(9, 110);
            label5.Name = "label5";
            label5.Size = new Size(70, 15);
            label5.TabIndex = 57;
            label5.Text = "Hire Date : ";
            // 
            // pictureBox5
            // 
            pictureBox5.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox5.Location = new Point(136, 77);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(28, 22);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 56;
            pictureBox5.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(9, 82);
            label4.Name = "label4";
            label4.Size = new Size(85, 15);
            label4.TabIndex = 55;
            label4.Text = "Department : ";
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox4.Location = new Point(140, 314);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(28, 22);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 54;
            pictureBox4.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(13, 319);
            label3.Name = "label3";
            label3.Size = new Size(108, 15);
            label3.TabIndex = 53;
            label3.Text = "Consultation Fee : ";
            // 
            // txtLicenseNumber
            // 
            txtLicenseNumber.Location = new Point(193, 285);
            txtLicenseNumber.Name = "txtLicenseNumber";
            txtLicenseNumber.Size = new Size(207, 23);
            txtLicenseNumber.TabIndex = 52;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox3.Location = new Point(140, 286);
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
            label1.Location = new Point(13, 291);
            label1.Name = "label1";
            label1.Size = new Size(106, 15);
            label1.TabIndex = 50;
            label1.Text = "License Number : ";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox2.Location = new Point(140, 260);
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
            label2.Location = new Point(13, 265);
            label2.Name = "label2";
            label2.Size = new Size(69, 15);
            label2.TabIndex = 47;
            label2.Text = "Speciality : ";
            // 
            // lblPersonId
            // 
            lblPersonId.AutoSize = true;
            lblPersonId.Location = new Point(281, 15);
            lblPersonId.Name = "lblPersonId";
            lblPersonId.Size = new Size(37, 15);
            lblPersonId.TabIndex = 46;
            lblPersonId.Text = "[N/A]";
            // 
            // lbl4
            // 
            lbl4.AutoSize = true;
            lbl4.Location = new Point(209, 15);
            lbl4.Name = "lbl4";
            lbl4.Size = new Size(70, 15);
            lbl4.TabIndex = 45;
            lbl4.Text = "Person ID : ";
            // 
            // pbPersonImage
            // 
            pbPersonImage.Image = Properties.Resources.doctor_2785482;
            pbPersonImage.Location = new Point(188, 3);
            pbPersonImage.Name = "pbPersonImage";
            pbPersonImage.Size = new Size(129, 81);
            pbPersonImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbPersonImage.TabIndex = 44;
            pbPersonImage.TabStop = false;
            // 
            // lblStaffID
            // 
            lblStaffID.AutoSize = true;
            lblStaffID.Location = new Point(120, 15);
            lblStaffID.Name = "lblStaffID";
            lblStaffID.Size = new Size(37, 15);
            lblStaffID.TabIndex = 84;
            lblStaffID.Text = "[N/A]";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(48, 15);
            label8.Name = "label8";
            label8.Size = new Size(61, 15);
            label8.TabIndex = 83;
            label8.Text = "Stuff ID : ";
            // 
            // lblDoctorID
            // 
            lblDoctorID.AutoSize = true;
            lblDoctorID.Location = new Point(76, 3);
            lblDoctorID.Name = "lblDoctorID";
            lblDoctorID.Size = new Size(37, 15);
            lblDoctorID.TabIndex = 86;
            lblDoctorID.Text = "[N/A]";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(4, 3);
            label10.Name = "label10";
            label10.Size = new Size(66, 15);
            label10.TabIndex = 85;
            label10.Text = "Doctor ID : ";
            // 
            // cbSpecialityID
            // 
            cbSpecialityID.FormattingEnabled = true;
            cbSpecialityID.Location = new Point(193, 257);
            cbSpecialityID.Name = "cbSpecialityID";
            cbSpecialityID.Size = new Size(207, 23);
            cbSpecialityID.TabIndex = 87;
            // 
            // nudConsultationFee
            // 
            nudConsultationFee.Location = new Point(193, 313);
            nudConsultationFee.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudConsultationFee.Name = "nudConsultationFee";
            nudConsultationFee.Size = new Size(207, 23);
            nudConsultationFee.TabIndex = 88;
            // 
            // dtpHireDate
            // 
            dtpHireDate.Format = DateTimePickerFormat.Short;
            dtpHireDate.Location = new Point(189, 105);
            dtpHireDate.Name = "dtpHireDate";
            dtpHireDate.Size = new Size(207, 23);
            dtpHireDate.TabIndex = 89;
            // 
            // chkIsActive
            // 
            chkIsActive.AutoSize = true;
            chkIsActive.Location = new Point(189, 138);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(62, 19);
            chkIsActive.TabIndex = 90;
            chkIsActive.Text = "Active";
            chkIsActive.UseVisualStyleBackColor = true;
            // 
            // txtStaffCode
            // 
            txtStaffCode.Location = new Point(187, 51);
            txtStaffCode.Name = "txtStaffCode";
            txtStaffCode.Size = new Size(213, 23);
            txtStaffCode.TabIndex = 93;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox1.Location = new Point(136, 50);
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
            label7.Location = new Point(9, 55);
            label7.Name = "label7";
            label7.Size = new Size(76, 15);
            label7.TabIndex = 91;
            label7.Text = "Stuff Code : ";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // gbStaffInfos
            // 
            gbStaffInfos.Controls.Add(lbl4);
            gbStaffInfos.Controls.Add(chkIsActive);
            gbStaffInfos.Controls.Add(txtStaffCode);
            gbStaffInfos.Controls.Add(dtpHireDate);
            gbStaffInfos.Controls.Add(lblPersonId);
            gbStaffInfos.Controls.Add(pictureBox1);
            gbStaffInfos.Controls.Add(label8);
            gbStaffInfos.Controls.Add(label7);
            gbStaffInfos.Controls.Add(lblStaffID);
            gbStaffInfos.Controls.Add(cbDepartmentID);
            gbStaffInfos.Controls.Add(label4);
            gbStaffInfos.Controls.Add(pictureBox5);
            gbStaffInfos.Controls.Add(label5);
            gbStaffInfos.Controls.Add(pictureBox7);
            gbStaffInfos.Controls.Add(pictureBox6);
            gbStaffInfos.Controls.Add(label6);
            gbStaffInfos.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbStaffInfos.Location = new Point(4, 90);
            gbStaffInfos.Name = "gbStaffInfos";
            gbStaffInfos.Size = new Size(473, 164);
            gbStaffInfos.TabIndex = 94;
            gbStaffInfos.TabStop = false;
            gbStaffInfos.Text = "Staff";
            // 
            // ucDoctor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gbStaffInfos);
            Controls.Add(nudConsultationFee);
            Controls.Add(cbSpecialityID);
            Controls.Add(lblDoctorID);
            Controls.Add(label10);
            Controls.Add(linkEdit);
            Controls.Add(btnSave);
            Controls.Add(pictureBox4);
            Controls.Add(label3);
            Controls.Add(txtLicenseNumber);
            Controls.Add(pictureBox3);
            Controls.Add(label1);
            Controls.Add(pictureBox2);
            Controls.Add(label2);
            Controls.Add(pbPersonImage);
            Name = "ucDoctor";
            Size = new Size(480, 380);
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudConsultationFee).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            gbStaffInfos.ResumeLayout(false);
            gbStaffInfos.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox cbDepartmentID;
        private LinkLabel linkEdit;
        private Button btnSave;
        private PictureBox pictureBox7;
        private Label label6;
        private PictureBox pictureBox6;
        private Label label5;
        private PictureBox pictureBox5;
        private Label label4;
        private PictureBox pictureBox4;
        private Label label3;
        private TextBox txtLicenseNumber;
        private PictureBox pictureBox3;
        private Label label1;
        private PictureBox pictureBox2;
        private Label label2;
        private Label lblPersonId;
        private Label lbl4;
        private PictureBox pbPersonImage;
        private Label lblStaffID;
        private Label label8;
        private Label lblDoctorID;
        private Label label10;
        private ComboBox cbSpecialityID;
        private NumericUpDown nudConsultationFee;
        private DateTimePicker dtpHireDate;
        private CheckBox chkIsActive;
        private TextBox txtStaffCode;
        private PictureBox pictureBox1;
        private Label label7;
        private ErrorProvider errorProvider1;
        private GroupBox gbStaffInfos;
    }
}
