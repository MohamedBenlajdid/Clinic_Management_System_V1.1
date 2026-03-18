namespace Clinic_Management.MedicalCertificate
{
    partial class ucMedicalCertificate
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
            lblDoctorId = new Label();
            label13 = new Label();
            lblPatientId = new Label();
            label11 = new Label();
            lblAppointmentId = new Label();
            label8 = new Label();
            txtNotes = new TextBox();
            lblMedicalCertificateId = new Label();
            label10 = new Label();
            linkEdit = new LinkLabel();
            btnSave = new Button();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            pbPersonImage = new PictureBox();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            cbCertificateType = new ComboBox();
            pictureBox3 = new PictureBox();
            label3 = new Label();
            dtpStartDate = new DateTimePicker();
            dtpEndDate = new DateTimePicker();
            pictureBox4 = new PictureBox();
            label4 = new Label();
            txtDiagnosticSummary = new TextBox();
            pictureBox5 = new PictureBox();
            label5 = new Label();
            errorProvider1 = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // lblDoctorId
            // 
            lblDoctorId.AutoSize = true;
            lblDoctorId.Location = new Point(103, 55);
            lblDoctorId.Name = "lblDoctorId";
            lblDoctorId.Size = new Size(37, 15);
            lblDoctorId.TabIndex = 186;
            lblDoctorId.Text = "[N/A]";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(6, 55);
            label13.Name = "label13";
            label13.Size = new Size(66, 15);
            label13.TabIndex = 185;
            label13.Text = "Doctor ID : ";
            // 
            // lblPatientId
            // 
            lblPatientId.AutoSize = true;
            lblPatientId.Location = new Point(103, 38);
            lblPatientId.Name = "lblPatientId";
            lblPatientId.Size = new Size(37, 15);
            lblPatientId.TabIndex = 184;
            lblPatientId.Text = "[N/A]";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(6, 38);
            label11.Name = "label11";
            label11.Size = new Size(67, 15);
            label11.TabIndex = 183;
            label11.Text = "Patient ID : ";
            // 
            // lblAppointmentId
            // 
            lblAppointmentId.AutoSize = true;
            lblAppointmentId.Location = new Point(102, 22);
            lblAppointmentId.Name = "lblAppointmentId";
            lblAppointmentId.Size = new Size(37, 15);
            lblAppointmentId.TabIndex = 182;
            lblAppointmentId.Text = "[N/A]";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(5, 22);
            label8.Name = "label8";
            label8.Size = new Size(98, 15);
            label8.TabIndex = 181;
            label8.Text = "Appointment ID :";
            // 
            // txtNotes
            // 
            txtNotes.Location = new Point(170, 126);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new Size(265, 33);
            txtNotes.TabIndex = 180;
            // 
            // lblMedicalCertificateId
            // 
            lblMedicalCertificateId.AutoSize = true;
            lblMedicalCertificateId.Location = new Point(102, 6);
            lblMedicalCertificateId.Name = "lblMedicalCertificateId";
            lblMedicalCertificateId.Size = new Size(37, 15);
            lblMedicalCertificateId.TabIndex = 179;
            lblMedicalCertificateId.Text = "[N/A]";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(5, 6);
            label10.Name = "label10";
            label10.Size = new Size(84, 15);
            label10.TabIndex = 178;
            label10.Text = "Certificate ID : ";
            // 
            // linkEdit
            // 
            linkEdit.AutoSize = true;
            linkEdit.Location = new Point(351, 22);
            linkEdit.Name = "linkEdit";
            linkEdit.Size = new Size(84, 15);
            linkEdit.TabIndex = 177;
            linkEdit.TabStop = true;
            linkEdit.Text = "Edit Certificate";
            linkEdit.LinkClicked += linkEdit_LinkClicked;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(402, 284);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(61, 38);
            btnSave.TabIndex = 176;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox2.Location = new Point(122, 126);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(28, 22);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 175;
            pictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(10, 133);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 174;
            label2.Text = "Notes : ";
            // 
            // pbPersonImage
            // 
            pbPersonImage.Image = Properties.Resources.prescription;
            pbPersonImage.Location = new Point(185, 3);
            pbPersonImage.Name = "pbPersonImage";
            pbPersonImage.Size = new Size(129, 81);
            pbPersonImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbPersonImage.TabIndex = 173;
            pbPersonImage.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox1.Location = new Point(122, 165);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(28, 22);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 188;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(10, 172);
            label1.Name = "label1";
            label1.Size = new Size(104, 15);
            label1.TabIndex = 187;
            label1.Text = "Certificate Type : ";
            // 
            // cbCertificateType
            // 
            cbCertificateType.FormattingEnabled = true;
            cbCertificateType.Location = new Point(170, 165);
            cbCertificateType.Name = "cbCertificateType";
            cbCertificateType.Size = new Size(265, 23);
            cbCertificateType.TabIndex = 189;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox3.Location = new Point(122, 193);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(28, 22);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 191;
            pictureBox3.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(10, 200);
            label3.Name = "label3";
            label3.Size = new Size(71, 15);
            label3.TabIndex = 190;
            label3.Text = "Start Date :";
            // 
            // dtpStartDate
            // 
            dtpStartDate.Format = DateTimePickerFormat.Short;
            dtpStartDate.Location = new Point(170, 194);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(200, 23);
            dtpStartDate.TabIndex = 192;
            // 
            // dtpEndDate
            // 
            dtpEndDate.Format = DateTimePickerFormat.Short;
            dtpEndDate.Location = new Point(170, 222);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(200, 23);
            dtpEndDate.TabIndex = 195;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox4.Location = new Point(122, 221);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(28, 22);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 194;
            pictureBox4.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(10, 228);
            label4.Name = "label4";
            label4.Size = new Size(66, 15);
            label4.TabIndex = 193;
            label4.Text = "End Date : ";
            // 
            // txtDiagnosticSummary
            // 
            txtDiagnosticSummary.Location = new Point(170, 251);
            txtDiagnosticSummary.Name = "txtDiagnosticSummary";
            txtDiagnosticSummary.Size = new Size(265, 23);
            txtDiagnosticSummary.TabIndex = 198;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox5.Location = new Point(122, 251);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(28, 22);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 197;
            pictureBox5.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(10, 258);
            label5.Name = "label5";
            label5.Size = new Size(97, 15);
            label5.TabIndex = 196;
            label5.Text = "Diag Summary : ";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // ucMedicalCertificate
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(txtDiagnosticSummary);
            Controls.Add(pictureBox5);
            Controls.Add(label5);
            Controls.Add(dtpEndDate);
            Controls.Add(pictureBox4);
            Controls.Add(label4);
            Controls.Add(dtpStartDate);
            Controls.Add(pictureBox3);
            Controls.Add(label3);
            Controls.Add(cbCertificateType);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(lblDoctorId);
            Controls.Add(label13);
            Controls.Add(lblPatientId);
            Controls.Add(label11);
            Controls.Add(lblAppointmentId);
            Controls.Add(label8);
            Controls.Add(txtNotes);
            Controls.Add(lblMedicalCertificateId);
            Controls.Add(label10);
            Controls.Add(linkEdit);
            Controls.Add(btnSave);
            Controls.Add(pictureBox2);
            Controls.Add(label2);
            Controls.Add(pbPersonImage);
            Name = "ucMedicalCertificate";
            Size = new Size(466, 325);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDoctorId;
        private Label label13;
        private Label lblPatientId;
        private Label label11;
        private Label lblAppointmentId;
        private Label label8;
        private TextBox txtNotes;
        private Label lblMedicalCertificateId;
        private Label label10;
        private LinkLabel linkEdit;
        private Button btnSave;
        private PictureBox pictureBox2;
        private Label label2;
        private PictureBox pbPersonImage;
        private PictureBox pictureBox1;
        private Label label1;
        private ComboBox cbCertificateType;
        private PictureBox pictureBox3;
        private Label label3;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private PictureBox pictureBox4;
        private Label label4;
        private TextBox txtDiagnosticSummary;
        private PictureBox pictureBox5;
        private Label label5;
        private ErrorProvider errorProvider1;
    }
}
