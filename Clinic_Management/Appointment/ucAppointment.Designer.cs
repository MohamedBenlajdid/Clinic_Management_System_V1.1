namespace Clinic_Management.Appointment
{
    partial class ucAppointment
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
            cbAppointmentSlots = new ComboBox();
            lblDoctorId = new Label();
            label10 = new Label();
            linkEdit = new LinkLabel();
            btnSave = new Button();
            pictureBox4 = new PictureBox();
            label3 = new Label();
            pictureBox3 = new PictureBox();
            label1 = new Label();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            pbPersonImage = new PictureBox();
            lblPatientId = new Label();
            label5 = new Label();
            lblAppointmentId = new Label();
            label7 = new Label();
            dtpStartAt = new DateTimePicker();
            dtpEndAt = new DateTimePicker();
            label8 = new Label();
            cbStatus = new ComboBox();
            pictureBox1 = new PictureBox();
            label9 = new Label();
            txtReason = new TextBox();
            pictureBox5 = new PictureBox();
            label11 = new Label();
            txtCancelReason = new TextBox();
            pictureBox6 = new PictureBox();
            label12 = new Label();
            chkIsCancelled = new CheckBox();
            pictureBox7 = new PictureBox();
            label4 = new Label();
            dtpAppointmentDate = new DateTimePicker();
            errorProvider1 = new ErrorProvider(components);
            linkLabel1 = new LinkLabel();
            label6 = new Label();
            pictureBox8 = new PictureBox();
            ucDoctorSelecter2 = new Clinic_Management.Doctors.ucDoctorSelecter();
            gbCancellingInfos = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            gbCancellingInfos.SuspendLayout();
            SuspendLayout();
            // 
            // cbAppointmentSlots
            // 
            cbAppointmentSlots.FormattingEnabled = true;
            cbAppointmentSlots.Location = new Point(227, 231);
            cbAppointmentSlots.Name = "cbAppointmentSlots";
            cbAppointmentSlots.Size = new Size(271, 23);
            cbAppointmentSlots.TabIndex = 107;
            // 
            // lblDoctorId
            // 
            lblDoctorId.AutoSize = true;
            lblDoctorId.Location = new Point(91, 3);
            lblDoctorId.Name = "lblDoctorId";
            lblDoctorId.Size = new Size(37, 15);
            lblDoctorId.TabIndex = 106;
            lblDoctorId.Text = "[N/A]";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(19, 3);
            label10.Name = "label10";
            label10.Size = new Size(66, 15);
            label10.TabIndex = 105;
            label10.Text = "Doctor ID : ";
            // 
            // linkEdit
            // 
            linkEdit.AutoSize = true;
            linkEdit.Location = new Point(397, 45);
            linkEdit.Name = "linkEdit";
            linkEdit.Size = new Size(101, 15);
            linkEdit.TabIndex = 104;
            linkEdit.TabStop = true;
            linkEdit.Text = "Edit Appointment";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(463, 412);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(61, 38);
            btnSave.TabIndex = 103;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox4.Location = new Point(174, 293);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(28, 22);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 102;
            pictureBox4.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(47, 298);
            label3.Name = "label3";
            label3.Size = new Size(128, 15);
            label3.TabIndex = 101;
            label3.Text = "Appointment Status : ";
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox3.Location = new Point(174, 265);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(28, 22);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 99;
            pictureBox3.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(47, 270);
            label1.Name = "label1";
            label1.Size = new Size(123, 15);
            label1.TabIndex = 98;
            label1.Text = "Start And End Time : ";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox2.Location = new Point(174, 234);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(28, 22);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 97;
            pictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(47, 239);
            label2.Name = "label2";
            label2.Size = new Size(115, 15);
            label2.TabIndex = 96;
            label2.Text = "Appointment Slot : ";
            // 
            // pbPersonImage
            // 
            pbPersonImage.Image = Properties.Resources.medical_appointment;
            pbPersonImage.Location = new Point(213, 3);
            pbPersonImage.Name = "pbPersonImage";
            pbPersonImage.Size = new Size(140, 81);
            pbPersonImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbPersonImage.TabIndex = 95;
            pbPersonImage.TabStop = false;
            // 
            // lblPatientId
            // 
            lblPatientId.AutoSize = true;
            lblPatientId.Location = new Point(91, 23);
            lblPatientId.Name = "lblPatientId";
            lblPatientId.Size = new Size(37, 15);
            lblPatientId.TabIndex = 111;
            lblPatientId.Text = "[N/A]";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(19, 23);
            label5.Name = "label5";
            label5.Size = new Size(67, 15);
            label5.TabIndex = 110;
            label5.Text = "Patient ID : ";
            // 
            // lblAppointmentId
            // 
            lblAppointmentId.AutoSize = true;
            lblAppointmentId.Location = new Point(116, 45);
            lblAppointmentId.Name = "lblAppointmentId";
            lblAppointmentId.Size = new Size(37, 15);
            lblAppointmentId.TabIndex = 113;
            lblAppointmentId.Text = "[N/A]";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(19, 45);
            label7.Name = "label7";
            label7.Size = new Size(101, 15);
            label7.TabIndex = 112;
            label7.Text = "Appointment ID : ";
            // 
            // dtpStartAt
            // 
            dtpStartAt.Format = DateTimePickerFormat.Time;
            dtpStartAt.Location = new Point(227, 262);
            dtpStartAt.Name = "dtpStartAt";
            dtpStartAt.Size = new Size(76, 23);
            dtpStartAt.TabIndex = 114;
            // 
            // dtpEndAt
            // 
            dtpEndAt.Format = DateTimePickerFormat.Time;
            dtpEndAt.Location = new Point(358, 262);
            dtpEndAt.Name = "dtpEndAt";
            dtpEndAt.Size = new Size(76, 23);
            dtpEndAt.TabIndex = 115;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(319, 265);
            label8.Name = "label8";
            label8.Size = new Size(23, 15);
            label8.TabIndex = 116;
            label8.Text = "=>";
            // 
            // cbStatus
            // 
            cbStatus.FormattingEnabled = true;
            cbStatus.Location = new Point(227, 292);
            cbStatus.Name = "cbStatus";
            cbStatus.Size = new Size(207, 23);
            cbStatus.TabIndex = 117;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox1.Location = new Point(175, 326);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(28, 22);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 119;
            pictureBox1.TabStop = false;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(47, 333);
            label9.Name = "label9";
            label9.Size = new Size(56, 15);
            label9.TabIndex = 118;
            label9.Text = "Reason : ";
            // 
            // txtReason
            // 
            txtReason.Location = new Point(227, 325);
            txtReason.Name = "txtReason";
            txtReason.Size = new Size(207, 23);
            txtReason.TabIndex = 120;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox5.Location = new Point(139, 19);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(28, 22);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 122;
            pictureBox5.TabStop = false;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.Location = new Point(12, 21);
            label11.Name = "label11";
            label11.Size = new Size(81, 15);
            label11.TabIndex = 121;
            label11.Text = "Is Cancelled : ";
            // 
            // txtCancelReason
            // 
            txtCancelReason.Location = new Point(193, 42);
            txtCancelReason.Name = "txtCancelReason";
            txtCancelReason.Size = new Size(207, 23);
            txtCancelReason.TabIndex = 126;
            // 
            // pictureBox6
            // 
            pictureBox6.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox6.Location = new Point(140, 43);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(28, 22);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 125;
            pictureBox6.TabStop = false;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.Location = new Point(13, 48);
            label12.Name = "label12";
            label12.Size = new Size(95, 15);
            label12.TabIndex = 124;
            label12.Text = "Cancel Reason : ";
            // 
            // chkIsCancelled
            // 
            chkIsCancelled.AutoSize = true;
            chkIsCancelled.Location = new Point(192, 20);
            chkIsCancelled.Name = "chkIsCancelled";
            chkIsCancelled.Size = new Size(78, 19);
            chkIsCancelled.TabIndex = 127;
            chkIsCancelled.Text = "Cancelled";
            chkIsCancelled.UseVisualStyleBackColor = true;
            chkIsCancelled.CheckedChanged += chkIsCancelled_CheckedChanged;
            // 
            // pictureBox7
            // 
            pictureBox7.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox7.Location = new Point(174, 203);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(28, 22);
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox7.TabIndex = 130;
            pictureBox7.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(47, 208);
            label4.Name = "label4";
            label4.Size = new Size(120, 15);
            label4.TabIndex = 129;
            label4.Text = "Appointment Date : ";
            // 
            // dtpAppointmentDate
            // 
            dtpAppointmentDate.Format = DateTimePickerFormat.Short;
            dtpAppointmentDate.Location = new Point(224, 203);
            dtpAppointmentDate.Name = "dtpAppointmentDate";
            dtpAppointmentDate.Size = new Size(207, 23);
            dtpAppointmentDate.TabIndex = 131;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(227, 445);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(108, 15);
            linkLabel1.TabIndex = 132;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Select/Add  Patient";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(47, 445);
            label6.Name = "label6";
            label6.Size = new Size(119, 15);
            label6.TabIndex = 133;
            label6.Text = "Booked By Patient : ";
            // 
            // pictureBox8
            // 
            pictureBox8.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox8.Location = new Point(174, 442);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new Size(28, 22);
            pictureBox8.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox8.TabIndex = 134;
            pictureBox8.TabStop = false;
            // 
            // ucDoctorSelecter2
            // 
            ucDoctorSelecter2.Location = new Point(2, 86);
            ucDoctorSelecter2.Name = "ucDoctorSelecter2";
            ucDoctorSelecter2.Size = new Size(531, 111);
            ucDoctorSelecter2.TabIndex = 135;
            // 
            // gbCancellingInfos
            // 
            gbCancellingInfos.Controls.Add(label11);
            gbCancellingInfos.Controls.Add(pictureBox5);
            gbCancellingInfos.Controls.Add(chkIsCancelled);
            gbCancellingInfos.Controls.Add(pictureBox6);
            gbCancellingInfos.Controls.Add(txtCancelReason);
            gbCancellingInfos.Controls.Add(label12);
            gbCancellingInfos.Location = new Point(28, 354);
            gbCancellingInfos.Name = "gbCancellingInfos";
            gbCancellingInfos.Size = new Size(429, 72);
            gbCancellingInfos.TabIndex = 136;
            gbCancellingInfos.TabStop = false;
            gbCancellingInfos.Text = "Cancelling Informations .";
            // 
            // ucAppointment
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pictureBox1);
            Controls.Add(label9);
            Controls.Add(ucDoctorSelecter2);
            Controls.Add(txtReason);
            Controls.Add(pictureBox8);
            Controls.Add(label6);
            Controls.Add(linkLabel1);
            Controls.Add(dtpAppointmentDate);
            Controls.Add(pictureBox7);
            Controls.Add(label4);
            Controls.Add(cbStatus);
            Controls.Add(label8);
            Controls.Add(dtpEndAt);
            Controls.Add(dtpStartAt);
            Controls.Add(lblAppointmentId);
            Controls.Add(label7);
            Controls.Add(lblPatientId);
            Controls.Add(label5);
            Controls.Add(cbAppointmentSlots);
            Controls.Add(lblDoctorId);
            Controls.Add(label10);
            Controls.Add(linkEdit);
            Controls.Add(btnSave);
            Controls.Add(pictureBox4);
            Controls.Add(label3);
            Controls.Add(pictureBox3);
            Controls.Add(label1);
            Controls.Add(pictureBox2);
            Controls.Add(label2);
            Controls.Add(pbPersonImage);
            Controls.Add(gbCancellingInfos);
            Name = "ucAppointment";
            Size = new Size(536, 480);
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            gbCancellingInfos.ResumeLayout(false);
            gbCancellingInfos.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox cbAppointmentSlots;
        private Label lblDoctorId;
        private Label label10;
        private LinkLabel linkEdit;
        private Button btnSave;
        private PictureBox pictureBox4;
        private Label label3;
        private PictureBox pictureBox3;
        private Label label1;
        private PictureBox pictureBox2;
        private Label label2;
        private PictureBox pbPersonImage;
        private Label lblPatientId;
        private Label label5;
        private Label lblAppointmentId;
        private Label label7;
        private DateTimePicker dtpStartAt;
        private DateTimePicker dtpEndAt;
        private Label label8;
        private ComboBox cbStatus;
        private PictureBox pictureBox1;
        private Label label9;
        private TextBox txtReason;
        private PictureBox pictureBox5;
        private Label label11;
        private TextBox txtCancelReason;
        private PictureBox pictureBox6;
        private Label label12;
        private CheckBox chkIsCancelled;
        private PictureBox pictureBox7;
        private Label label4;
        private DateTimePicker dtpAppointmentDate;
        private ErrorProvider errorProvider1;
        private LinkLabel linkLabel1;
        private PictureBox pictureBox8;
        private Label label6;
        private Doctors.ucDoctorSelecter ucDoctorSelecter2;
        private GroupBox gbCancellingInfos;
    }
}
