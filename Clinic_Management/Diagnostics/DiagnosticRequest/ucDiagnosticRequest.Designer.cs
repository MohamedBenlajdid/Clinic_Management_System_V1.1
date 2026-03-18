namespace Clinic_Management.Diagnostics.DiagnosticRequest
{
    partial class ucDiagnosticRequest
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
            txtClinicalInfo = new TextBox();
            lblDiagnosticRequestId = new Label();
            label10 = new Label();
            linkEdit = new LinkLabel();
            btnSave = new Button();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            pbPersonImage = new PictureBox();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            cbPriority = new ComboBox();
            cbStatus = new ComboBox();
            pictureBox3 = new PictureBox();
            label3 = new Label();
            errorProvider1 = new ErrorProvider(components);
            linkAddDiagnosticItem = new LinkLabel();
            linkViewDiagnosticItems = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // lblDoctorId
            // 
            lblDoctorId.AutoSize = true;
            lblDoctorId.Location = new Point(105, 55);
            lblDoctorId.Name = "lblDoctorId";
            lblDoctorId.Size = new Size(37, 15);
            lblDoctorId.TabIndex = 186;
            lblDoctorId.Text = "[N/A]";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(8, 55);
            label13.Name = "label13";
            label13.Size = new Size(66, 15);
            label13.TabIndex = 185;
            label13.Text = "Doctor ID : ";
            // 
            // lblPatientId
            // 
            lblPatientId.AutoSize = true;
            lblPatientId.Location = new Point(105, 38);
            lblPatientId.Name = "lblPatientId";
            lblPatientId.Size = new Size(37, 15);
            lblPatientId.TabIndex = 184;
            lblPatientId.Text = "[N/A]";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(8, 38);
            label11.Name = "label11";
            label11.Size = new Size(67, 15);
            label11.TabIndex = 183;
            label11.Text = "Patient ID : ";
            // 
            // lblAppointmentId
            // 
            lblAppointmentId.AutoSize = true;
            lblAppointmentId.Location = new Point(104, 22);
            lblAppointmentId.Name = "lblAppointmentId";
            lblAppointmentId.Size = new Size(37, 15);
            lblAppointmentId.TabIndex = 182;
            lblAppointmentId.Text = "[N/A]";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(7, 22);
            label8.Name = "label8";
            label8.Size = new Size(98, 15);
            label8.TabIndex = 181;
            label8.Text = "Appointment ID :";
            // 
            // txtClinicalInfo
            // 
            txtClinicalInfo.Location = new Point(153, 125);
            txtClinicalInfo.Multiline = true;
            txtClinicalInfo.Name = "txtClinicalInfo";
            txtClinicalInfo.Size = new Size(265, 36);
            txtClinicalInfo.TabIndex = 180;
            // 
            // lblDiagnosticRequestId
            // 
            lblDiagnosticRequestId.AutoSize = true;
            lblDiagnosticRequestId.Location = new Point(104, 6);
            lblDiagnosticRequestId.Name = "lblDiagnosticRequestId";
            lblDiagnosticRequestId.Size = new Size(37, 15);
            lblDiagnosticRequestId.TabIndex = 179;
            lblDiagnosticRequestId.Text = "[N/A]";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(7, 6);
            label10.Name = "label10";
            label10.Size = new Size(96, 15);
            label10.TabIndex = 178;
            label10.Text = "Diag Request ID :";
            // 
            // linkEdit
            // 
            linkEdit.AutoSize = true;
            linkEdit.Location = new Point(353, 22);
            linkEdit.Name = "linkEdit";
            linkEdit.Size = new Size(93, 15);
            linkEdit.TabIndex = 177;
            linkEdit.TabStop = true;
            linkEdit.Text = "Edit Prescription";
            linkEdit.LinkClicked += linkEdit_LinkClicked;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(410, 243);
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
            pictureBox2.Location = new Point(100, 128);
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
            label2.Location = new Point(12, 133);
            label2.Name = "label2";
            label2.Size = new Size(82, 15);
            label2.TabIndex = 174;
            label2.Text = "Clinical Infos :";
            // 
            // pbPersonImage
            // 
            pbPersonImage.Image = Properties.Resources.medical_diagnostics;
            pbPersonImage.Location = new Point(187, 3);
            pbPersonImage.Name = "pbPersonImage";
            pbPersonImage.Size = new Size(129, 81);
            pbPersonImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbPersonImage.TabIndex = 173;
            pbPersonImage.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox1.Location = new Point(100, 170);
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
            label1.Location = new Point(12, 175);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 187;
            label1.Text = "Priority : ";
            // 
            // cbPriority
            // 
            cbPriority.FormattingEnabled = true;
            cbPriority.Location = new Point(153, 167);
            cbPriority.Name = "cbPriority";
            cbPriority.Size = new Size(265, 23);
            cbPriority.TabIndex = 189;
            // 
            // cbStatus
            // 
            cbStatus.FormattingEnabled = true;
            cbStatus.Location = new Point(153, 198);
            cbStatus.Name = "cbStatus";
            cbStatus.Size = new Size(265, 23);
            cbStatus.TabIndex = 192;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox3.Location = new Point(100, 201);
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
            label3.Location = new Point(12, 206);
            label3.Name = "label3";
            label3.Size = new Size(48, 15);
            label3.TabIndex = 190;
            label3.Text = "Status :";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // linkAddDiagnosticItem
            // 
            linkAddDiagnosticItem.AutoSize = true;
            linkAddDiagnosticItem.Location = new Point(341, 55);
            linkAddDiagnosticItem.Name = "linkAddDiagnosticItem";
            linkAddDiagnosticItem.Size = new Size(115, 15);
            linkAddDiagnosticItem.TabIndex = 193;
            linkAddDiagnosticItem.TabStop = true;
            linkAddDiagnosticItem.Text = "Add Diagnostic Item";
            linkAddDiagnosticItem.LinkClicked += linkLabel1_LinkClicked;
            // 
            // linkViewDiagnosticItems
            // 
            linkViewDiagnosticItems.AutoSize = true;
            linkViewDiagnosticItems.Location = new Point(363, 72);
            linkViewDiagnosticItems.Name = "linkViewDiagnosticItems";
            linkViewDiagnosticItems.Size = new Size(64, 15);
            linkViewDiagnosticItems.TabIndex = 194;
            linkViewDiagnosticItems.TabStop = true;
            linkViewDiagnosticItems.Text = "View Items";
            linkViewDiagnosticItems.LinkClicked += linkLabel2_LinkClicked;
            // 
            // ucDiagnosticRequest
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(linkViewDiagnosticItems);
            Controls.Add(linkAddDiagnosticItem);
            Controls.Add(cbStatus);
            Controls.Add(pictureBox3);
            Controls.Add(label3);
            Controls.Add(cbPriority);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(lblDoctorId);
            Controls.Add(label13);
            Controls.Add(lblPatientId);
            Controls.Add(label11);
            Controls.Add(lblAppointmentId);
            Controls.Add(label8);
            Controls.Add(txtClinicalInfo);
            Controls.Add(lblDiagnosticRequestId);
            Controls.Add(label10);
            Controls.Add(linkEdit);
            Controls.Add(btnSave);
            Controls.Add(pictureBox2);
            Controls.Add(label2);
            Controls.Add(pbPersonImage);
            Name = "ucDiagnosticRequest";
            Size = new Size(474, 287);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
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
        private TextBox txtClinicalInfo;
        private Label lblDiagnosticRequestId;
        private Label label10;
        private LinkLabel linkEdit;
        private Button btnSave;
        private PictureBox pictureBox2;
        private Label label2;
        private PictureBox pbPersonImage;
        private PictureBox pictureBox1;
        private Label label1;
        private ComboBox cbPriority;
        private ComboBox cbStatus;
        private PictureBox pictureBox3;
        private Label label3;
        private ErrorProvider errorProvider1;
        private LinkLabel linkViewDiagnosticItems;
        private LinkLabel linkAddDiagnosticItem;
    }
}
