namespace Clinic_Management.Patients
{
    partial class ucPatient
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
            txtMedicalRecordNumber = new TextBox();
            pictureBox1 = new PictureBox();
            label7 = new Label();
            cbBloodTypeId = new ComboBox();
            lblPatientID = new Label();
            label8 = new Label();
            linkEdit = new LinkLabel();
            btnSave = new Button();
            pictureBox5 = new PictureBox();
            label4 = new Label();
            pictureBox4 = new PictureBox();
            label3 = new Label();
            txtEmergencyContactName = new TextBox();
            pictureBox3 = new PictureBox();
            label1 = new Label();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            lblPersonId = new Label();
            lbl4 = new Label();
            pbPersonImage = new PictureBox();
            txtEmergencyPhone = new TextBox();
            txtNotes = new TextBox();
            errorProvider1 = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // txtMedicalRecordNumber
            // 
            txtMedicalRecordNumber.Location = new Point(204, 102);
            txtMedicalRecordNumber.Name = "txtMedicalRecordNumber";
            txtMedicalRecordNumber.Size = new Size(207, 23);
            txtMedicalRecordNumber.TabIndex = 123;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox1.Location = new Point(164, 130);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(28, 22);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 122;
            pictureBox1.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(19, 135);
            label7.Name = "label7";
            label7.Size = new Size(77, 15);
            label7.TabIndex = 121;
            label7.Text = "Blood Type : ";
            // 
            // cbBloodTypeId
            // 
            cbBloodTypeId.FormattingEnabled = true;
            cbBloodTypeId.Location = new Point(204, 129);
            cbBloodTypeId.Name = "cbBloodTypeId";
            cbBloodTypeId.Size = new Size(207, 23);
            cbBloodTypeId.TabIndex = 117;
            // 
            // lblPatientID
            // 
            lblPatientID.AutoSize = true;
            lblPatientID.Location = new Point(75, 21);
            lblPatientID.Name = "lblPatientID";
            lblPatientID.Size = new Size(37, 15);
            lblPatientID.TabIndex = 114;
            lblPatientID.Text = "[N/A]";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(3, 21);
            label8.Name = "label8";
            label8.Size = new Size(67, 15);
            label8.TabIndex = 113;
            label8.Text = "Patient ID : ";
            // 
            // linkEdit
            // 
            linkEdit.AutoSize = true;
            linkEdit.Location = new Point(45, 58);
            linkEdit.Name = "linkEdit";
            linkEdit.Size = new Size(67, 15);
            linkEdit.TabIndex = 111;
            linkEdit.TabStop = true;
            linkEdit.Text = "Edit Patient";
            linkEdit.LinkClicked += linkEdit_LinkClicked;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(398, 262);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(65, 35);
            btnSave.TabIndex = 110;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox5.Location = new Point(164, 211);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(28, 22);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 105;
            pictureBox5.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(19, 216);
            label4.Name = "label4";
            label4.Size = new Size(49, 15);
            label4.TabIndex = 104;
            label4.Text = "Notes : ";
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox4.Location = new Point(164, 183);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(28, 22);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 103;
            pictureBox4.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(19, 188);
            label3.Name = "label3";
            label3.Size = new Size(145, 15);
            label3.TabIndex = 102;
            label3.Text = "Emergency Cont.Phone : ";
            // 
            // txtEmergencyContactName
            // 
            txtEmergencyContactName.Location = new Point(204, 154);
            txtEmergencyContactName.Name = "txtEmergencyContactName";
            txtEmergencyContactName.Size = new Size(207, 23);
            txtEmergencyContactName.TabIndex = 101;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox3.Location = new Point(164, 155);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(28, 22);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 100;
            pictureBox3.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(19, 160);
            label1.Name = "label1";
            label1.Size = new Size(143, 15);
            label1.TabIndex = 99;
            label1.Text = "Emergency Cont.Name : ";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox2.Location = new Point(164, 102);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(28, 22);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 98;
            pictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(19, 107);
            label2.Name = "label2";
            label2.Size = new Size(122, 15);
            label2.TabIndex = 97;
            label2.Text = "M. Record Number : ";
            // 
            // lblPersonId
            // 
            lblPersonId.AutoSize = true;
            lblPersonId.Location = new Point(75, 3);
            lblPersonId.Name = "lblPersonId";
            lblPersonId.Size = new Size(37, 15);
            lblPersonId.TabIndex = 96;
            lblPersonId.Text = "[N/A]";
            // 
            // lbl4
            // 
            lbl4.AutoSize = true;
            lbl4.Location = new Point(3, 3);
            lbl4.Name = "lbl4";
            lbl4.Size = new Size(66, 15);
            lbl4.TabIndex = 95;
            lbl4.Text = "Person ID : ";
            // 
            // pbPersonImage
            // 
            pbPersonImage.Image = Properties.Resources.Icons_Land_Medical_People_Patient_Male_256;
            pbPersonImage.Location = new Point(190, 0);
            pbPersonImage.Name = "pbPersonImage";
            pbPersonImage.Size = new Size(129, 81);
            pbPersonImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbPersonImage.TabIndex = 94;
            pbPersonImage.TabStop = false;
            // 
            // txtEmergencyPhone
            // 
            txtEmergencyPhone.Location = new Point(204, 183);
            txtEmergencyPhone.Name = "txtEmergencyPhone";
            txtEmergencyPhone.Size = new Size(207, 23);
            txtEmergencyPhone.TabIndex = 124;
            // 
            // txtNotes
            // 
            txtNotes.Location = new Point(204, 210);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new Size(207, 38);
            txtNotes.TabIndex = 125;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // ucPatient
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(txtNotes);
            Controls.Add(txtEmergencyPhone);
            Controls.Add(txtMedicalRecordNumber);
            Controls.Add(pictureBox1);
            Controls.Add(label7);
            Controls.Add(cbBloodTypeId);
            Controls.Add(lblPatientID);
            Controls.Add(label8);
            Controls.Add(linkEdit);
            Controls.Add(btnSave);
            Controls.Add(pictureBox5);
            Controls.Add(label4);
            Controls.Add(pictureBox4);
            Controls.Add(label3);
            Controls.Add(txtEmergencyContactName);
            Controls.Add(pictureBox3);
            Controls.Add(label1);
            Controls.Add(pictureBox2);
            Controls.Add(label2);
            Controls.Add(lblPersonId);
            Controls.Add(lbl4);
            Controls.Add(pbPersonImage);
            Name = "ucPatient";
            Size = new Size(466, 299);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtMedicalRecordNumber;
        private PictureBox pictureBox1;
        private Label label7;
        private ComboBox cbBloodTypeId;
        private Label lblPatientID;
        private Label label8;
        private LinkLabel linkEdit;
        private Button btnSave;
        private PictureBox pictureBox5;
        private Label label4;
        private PictureBox pictureBox4;
        private Label label3;
        private TextBox txtEmergencyContactName;
        private PictureBox pictureBox3;
        private Label label1;
        private PictureBox pictureBox2;
        private Label label2;
        private Label lblPersonId;
        private Label lbl4;
        private PictureBox pbPersonImage;
        private TextBox txtEmergencyPhone;
        private TextBox txtNotes;
        private ErrorProvider errorProvider1;
    }
}
