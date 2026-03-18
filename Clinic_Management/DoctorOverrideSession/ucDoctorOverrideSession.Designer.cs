namespace Clinic_Management.DoctorOverrideSession
{
    partial class ucDoctorOverrideSession
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
            lblSessionId = new Label();
            label11 = new Label();
            gbSessionInfos = new GroupBox();
            dtpStartTime = new DateTimePicker();
            cbSlotMinutes = new ComboBox();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            dtpEndTime = new DateTimePicker();
            label4 = new Label();
            pictureBox5 = new PictureBox();
            label5 = new Label();
            pictureBox6 = new PictureBox();
            lblOverrideID = new Label();
            label10 = new Label();
            linkEdit = new LinkLabel();
            btnSave = new Button();
            pbPersonImage = new PictureBox();
            errorProvider1 = new ErrorProvider(components);
            linkLabel1 = new LinkLabel();
            gbSessionInfos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // lblSessionId
            // 
            lblSessionId.AutoSize = true;
            lblSessionId.Location = new Point(78, 25);
            lblSessionId.Name = "lblSessionId";
            lblSessionId.Size = new Size(37, 15);
            lblSessionId.TabIndex = 119;
            lblSessionId.Text = "[N/A]";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(6, 25);
            label11.Name = "label11";
            label11.Size = new Size(66, 15);
            label11.TabIndex = 118;
            label11.Text = "Session ID :";
            // 
            // gbSessionInfos
            // 
            gbSessionInfos.Controls.Add(dtpStartTime);
            gbSessionInfos.Controls.Add(cbSlotMinutes);
            gbSessionInfos.Controls.Add(pictureBox2);
            gbSessionInfos.Controls.Add(label2);
            gbSessionInfos.Controls.Add(dtpEndTime);
            gbSessionInfos.Controls.Add(label4);
            gbSessionInfos.Controls.Add(pictureBox5);
            gbSessionInfos.Controls.Add(label5);
            gbSessionInfos.Controls.Add(pictureBox6);
            gbSessionInfos.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbSessionInfos.Location = new Point(10, 90);
            gbSessionInfos.Name = "gbSessionInfos";
            gbSessionInfos.Size = new Size(473, 175);
            gbSessionInfos.TabIndex = 117;
            gbSessionInfos.TabStop = false;
            gbSessionInfos.Text = "Session : ";
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
            // dtpEndTime
            // 
            dtpEndTime.Format = DateTimePickerFormat.Time;
            dtpEndTime.Location = new Point(189, 76);
            dtpEndTime.Name = "dtpEndTime";
            dtpEndTime.Size = new Size(207, 23);
            dtpEndTime.TabIndex = 89;
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
            // lblOverrideID
            // 
            lblOverrideID.AutoSize = true;
            lblOverrideID.Location = new Point(78, 3);
            lblOverrideID.Name = "lblOverrideID";
            lblOverrideID.Size = new Size(37, 15);
            lblOverrideID.TabIndex = 116;
            lblOverrideID.Text = "[N/A]";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(6, 3);
            label10.Name = "label10";
            label10.Size = new Size(75, 15);
            label10.TabIndex = 115;
            label10.Text = "Override ID : ";
            // 
            // linkEdit
            // 
            linkEdit.AutoSize = true;
            linkEdit.Location = new Point(49, 57);
            linkEdit.Name = "linkEdit";
            linkEdit.Size = new Size(69, 15);
            linkEdit.TabIndex = 114;
            linkEdit.TabStop = true;
            linkEdit.Text = "Edit Session";
            linkEdit.LinkClicked += linkEdit_LinkClicked;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(418, 281);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(61, 38);
            btnSave.TabIndex = 113;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // pbPersonImage
            // 
            pbPersonImage.Image = Properties.Resources.schedule;
            pbPersonImage.Location = new Point(190, 3);
            pbPersonImage.Name = "pbPersonImage";
            pbPersonImage.Size = new Size(129, 81);
            pbPersonImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbPersonImage.TabIndex = 112;
            pbPersonImage.TabStop = false;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(349, 12);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(109, 15);
            linkLabel1.TabIndex = 120;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Select Override Day";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // ucDoctorOverrideSession
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(linkLabel1);
            Controls.Add(lblSessionId);
            Controls.Add(label11);
            Controls.Add(gbSessionInfos);
            Controls.Add(lblOverrideID);
            Controls.Add(label10);
            Controls.Add(linkEdit);
            Controls.Add(btnSave);
            Controls.Add(pbPersonImage);
            Name = "ucDoctorOverrideSession";
            Size = new Size(496, 332);
            gbSessionInfos.ResumeLayout(false);
            gbSessionInfos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSessionId;
        private Label label11;
        private GroupBox gbSessionInfos;
        private DateTimePicker dtpStartTime;
        private ComboBox cbSlotMinutes;
        private PictureBox pictureBox2;
        private Label label2;
        private DateTimePicker dtpEndTime;
        private Label label4;
        private PictureBox pictureBox5;
        private Label label5;
        private PictureBox pictureBox6;
        private Label lblOverrideID;
        private Label label10;
        private LinkLabel linkEdit;
        private Button btnSave;
        private PictureBox pbPersonImage;
        private ErrorProvider errorProvider1;
        private LinkLabel linkLabel1;
    }
}
