namespace Clinic_Management.Schedule
{
    partial class ucDoctorSchedule
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
            gbScheduleInfos = new GroupBox();
            nudDayOfWeek = new NumericUpDown();
            dtpStartTime = new DateTimePicker();
            cbSlotMinutes = new ComboBox();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            chkIsActive = new CheckBox();
            dtpEndTime = new DateTimePicker();
            pictureBox1 = new PictureBox();
            label7 = new Label();
            label4 = new Label();
            pictureBox5 = new PictureBox();
            label5 = new Label();
            pictureBox7 = new PictureBox();
            pictureBox6 = new PictureBox();
            label6 = new Label();
            lblDoctorId = new Label();
            label10 = new Label();
            linkEdit = new LinkLabel();
            btnSave = new Button();
            pbPersonImage = new PictureBox();
            lblScheduleId = new Label();
            label11 = new Label();
            errorProvider1 = new ErrorProvider(components);
            linkLabel1 = new LinkLabel();
            gbScheduleInfos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudDayOfWeek).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // gbScheduleInfos
            // 
            gbScheduleInfos.Controls.Add(nudDayOfWeek);
            gbScheduleInfos.Controls.Add(dtpStartTime);
            gbScheduleInfos.Controls.Add(cbSlotMinutes);
            gbScheduleInfos.Controls.Add(pictureBox2);
            gbScheduleInfos.Controls.Add(label2);
            gbScheduleInfos.Controls.Add(chkIsActive);
            gbScheduleInfos.Controls.Add(dtpEndTime);
            gbScheduleInfos.Controls.Add(pictureBox1);
            gbScheduleInfos.Controls.Add(label7);
            gbScheduleInfos.Controls.Add(label4);
            gbScheduleInfos.Controls.Add(pictureBox5);
            gbScheduleInfos.Controls.Add(label5);
            gbScheduleInfos.Controls.Add(pictureBox7);
            gbScheduleInfos.Controls.Add(pictureBox6);
            gbScheduleInfos.Controls.Add(label6);
            gbScheduleInfos.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbScheduleInfos.Location = new Point(10, 90);
            gbScheduleInfos.Name = "gbScheduleInfos";
            gbScheduleInfos.Size = new Size(473, 175);
            gbScheduleInfos.TabIndex = 109;
            gbScheduleInfos.TabStop = false;
            gbScheduleInfos.Text = "Schedule : ";
            // 
            // nudDayOfWeek
            // 
            nudDayOfWeek.Location = new Point(189, 18);
            nudDayOfWeek.Maximum = new decimal(new int[] { 6, 0, 0, 0 });
            nudDayOfWeek.Name = "nudDayOfWeek";
            nudDayOfWeek.Size = new Size(207, 23);
            nudDayOfWeek.TabIndex = 112;
            // 
            // dtpStartTime
            // 
            dtpStartTime.Format = DateTimePickerFormat.Time;
            dtpStartTime.Location = new Point(189, 48);
            dtpStartTime.Name = "dtpStartTime";
            dtpStartTime.Size = new Size(207, 23);
            dtpStartTime.TabIndex = 111;
            // 
            // cbSlotMinutes
            // 
            cbSlotMinutes.FormattingEnabled = true;
            cbSlotMinutes.Location = new Point(189, 105);
            cbSlotMinutes.Name = "cbSlotMinutes";
            cbSlotMinutes.Size = new Size(207, 23);
            cbSlotMinutes.TabIndex = 110;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox2.Location = new Point(136, 105);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(28, 22);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 109;
            pictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(9, 110);
            label2.Name = "label2";
            label2.Size = new Size(86, 15);
            label2.TabIndex = 108;
            label2.Text = "Slot Minutes : ";
            // 
            // chkIsActive
            // 
            chkIsActive.AutoSize = true;
            chkIsActive.Location = new Point(189, 137);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(62, 19);
            chkIsActive.TabIndex = 90;
            chkIsActive.Text = "Active";
            chkIsActive.UseVisualStyleBackColor = true;
            // 
            // dtpEndTime
            // 
            dtpEndTime.Format = DateTimePickerFormat.Time;
            dtpEndTime.Location = new Point(189, 76);
            dtpEndTime.Name = "dtpEndTime";
            dtpEndTime.Size = new Size(207, 23);
            dtpEndTime.TabIndex = 89;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox1.Location = new Point(136, 21);
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
            label7.Location = new Point(9, 23);
            label7.Name = "label7";
            label7.Size = new Size(90, 15);
            label7.TabIndex = 91;
            label7.Text = "Day Of Week : ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(9, 53);
            label4.Name = "label4";
            label4.Size = new Size(75, 15);
            label4.TabIndex = 55;
            label4.Text = "Start Time : ";
            // 
            // pictureBox5
            // 
            pictureBox5.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox5.Location = new Point(136, 48);
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
            label5.Location = new Point(9, 81);
            label5.Name = "label5";
            label5.Size = new Size(67, 15);
            label5.TabIndex = 57;
            label5.Text = "End Time : ";
            // 
            // pictureBox7
            // 
            pictureBox7.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox7.Location = new Point(136, 132);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(28, 22);
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox7.TabIndex = 61;
            pictureBox7.TabStop = false;
            // 
            // pictureBox6
            // 
            pictureBox6.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox6.Location = new Point(136, 76);
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
            label6.Location = new Point(9, 137);
            label6.Name = "label6";
            label6.Size = new Size(64, 15);
            label6.TabIndex = 60;
            label6.Text = "Is Active ; ";
            // 
            // lblDoctorId
            // 
            lblDoctorId.AutoSize = true;
            lblDoctorId.Location = new Point(82, 3);
            lblDoctorId.Name = "lblDoctorId";
            lblDoctorId.Size = new Size(37, 15);
            lblDoctorId.TabIndex = 106;
            lblDoctorId.Text = "[N/A]";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(10, 3);
            label10.Name = "label10";
            label10.Size = new Size(66, 15);
            label10.TabIndex = 105;
            label10.Text = "Doctor ID : ";
            // 
            // linkEdit
            // 
            linkEdit.AutoSize = true;
            linkEdit.Location = new Point(53, 57);
            linkEdit.Name = "linkEdit";
            linkEdit.Size = new Size(78, 15);
            linkEdit.TabIndex = 104;
            linkEdit.TabStop = true;
            linkEdit.Text = "Edit Schedule";
            linkEdit.LinkClicked += linkEdit_LinkClicked;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(433, 281);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(61, 38);
            btnSave.TabIndex = 103;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // pbPersonImage
            // 
            pbPersonImage.Image = Properties.Resources.schedule;
            pbPersonImage.Location = new Point(194, 3);
            pbPersonImage.Name = "pbPersonImage";
            pbPersonImage.Size = new Size(129, 81);
            pbPersonImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbPersonImage.TabIndex = 95;
            pbPersonImage.TabStop = false;
            // 
            // lblScheduleId
            // 
            lblScheduleId.AutoSize = true;
            lblScheduleId.Location = new Point(82, 25);
            lblScheduleId.Name = "lblScheduleId";
            lblScheduleId.Size = new Size(37, 15);
            lblScheduleId.TabIndex = 111;
            lblScheduleId.Text = "[N/A]";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(10, 25);
            label11.Name = "label11";
            label11.Size = new Size(78, 15);
            label11.TabIndex = 110;
            label11.Text = "Schedule ID : ";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(360, 13);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(77, 15);
            linkLabel1.TabIndex = 112;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Select Doctor";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // ucDoctorSchedule
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(linkLabel1);
            Controls.Add(lblScheduleId);
            Controls.Add(label11);
            Controls.Add(gbScheduleInfos);
            Controls.Add(lblDoctorId);
            Controls.Add(label10);
            Controls.Add(linkEdit);
            Controls.Add(btnSave);
            Controls.Add(pbPersonImage);
            Name = "ucDoctorSchedule";
            Size = new Size(497, 322);
            gbScheduleInfos.ResumeLayout(false);
            gbScheduleInfos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudDayOfWeek).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
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

        private GroupBox gbScheduleInfos;
        private CheckBox chkIsActive;
        private TextBox txtStaffCode;
        private PictureBox pictureBox1;
        private Label label7;
        private ComboBox cbDepartmentID;
        private Label label4;
        private PictureBox pictureBox5;
        private Label label5;
        private PictureBox pictureBox7;
        private PictureBox pictureBox6;
        private Label label6;
        private NumericUpDown nudConsultationFee;
        private Label lblDoctorId;
        private Label label10;
        private LinkLabel linkEdit;
        private Button btnSave;
        private PictureBox pictureBox4;
        private Label label3;
        private TextBox txtLicenseNumber;
        private PictureBox pictureBox3;
        private Label label1;
        private PictureBox pbPersonImage;
        private Label lblScheduleId;
        private Label label11;
        private DateTimePicker dtpStartTime;
        private ComboBox cbSlotMinutes;
        private PictureBox pictureBox2;
        private Label label2;
        private DateTimePicker dtpEndTime;
        private NumericUpDown nudDayOfWeek;
        private ErrorProvider errorProvider1;
        private LinkLabel linkLabel1;
    }
}
