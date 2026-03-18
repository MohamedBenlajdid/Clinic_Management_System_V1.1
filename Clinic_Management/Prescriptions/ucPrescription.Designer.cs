namespace Clinic_Management.Prescriptions
{
    partial class ucPrescription
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
            lblPrescriptionId = new Label();
            label10 = new Label();
            linkEdit = new LinkLabel();
            btnSave = new Button();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            pbPersonImage = new PictureBox();
            errorProvider1 = new ErrorProvider(components);
            linkAddItem = new LinkLabel();
            linkShowItems = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // lblDoctorId
            // 
            lblDoctorId.AutoSize = true;
            lblDoctorId.Location = new Point(102, 55);
            lblDoctorId.Name = "lblDoctorId";
            lblDoctorId.Size = new Size(37, 15);
            lblDoctorId.TabIndex = 172;
            lblDoctorId.Text = "[N/A]";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(5, 55);
            label13.Name = "label13";
            label13.Size = new Size(66, 15);
            label13.TabIndex = 171;
            label13.Text = "Doctor ID : ";
            // 
            // lblPatientId
            // 
            lblPatientId.AutoSize = true;
            lblPatientId.Location = new Point(102, 38);
            lblPatientId.Name = "lblPatientId";
            lblPatientId.Size = new Size(37, 15);
            lblPatientId.TabIndex = 170;
            lblPatientId.Text = "[N/A]";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(5, 38);
            label11.Name = "label11";
            label11.Size = new Size(67, 15);
            label11.TabIndex = 169;
            label11.Text = "Patient ID : ";
            // 
            // lblAppointmentId
            // 
            lblAppointmentId.AutoSize = true;
            lblAppointmentId.Location = new Point(101, 22);
            lblAppointmentId.Name = "lblAppointmentId";
            lblAppointmentId.Size = new Size(37, 15);
            lblAppointmentId.TabIndex = 168;
            lblAppointmentId.Text = "[N/A]";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(4, 22);
            label8.Name = "label8";
            label8.Size = new Size(98, 15);
            label8.TabIndex = 167;
            label8.Text = "Appointment ID :";
            // 
            // txtNotes
            // 
            txtNotes.Location = new Point(133, 125);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new Size(265, 53);
            txtNotes.TabIndex = 165;
            // 
            // lblPrescriptionId
            // 
            lblPrescriptionId.AutoSize = true;
            lblPrescriptionId.Location = new Point(101, 6);
            lblPrescriptionId.Name = "lblPrescriptionId";
            lblPrescriptionId.Size = new Size(37, 15);
            lblPrescriptionId.TabIndex = 161;
            lblPrescriptionId.Text = "[N/A]";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(4, 6);
            label10.Name = "label10";
            label10.Size = new Size(90, 15);
            label10.TabIndex = 160;
            label10.Text = "Prescription ID :";
            // 
            // linkEdit
            // 
            linkEdit.AutoSize = true;
            linkEdit.Location = new Point(350, 22);
            linkEdit.Name = "linkEdit";
            linkEdit.Size = new Size(93, 15);
            linkEdit.TabIndex = 159;
            linkEdit.TabStop = true;
            linkEdit.Text = "Edit Prescription";
            linkEdit.LinkClicked += linkEdit_LinkClicked;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(411, 185);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(61, 38);
            btnSave.TabIndex = 158;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox2.Location = new Point(80, 128);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(28, 22);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 152;
            pictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(9, 133);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 151;
            label2.Text = "Notes : ";
            // 
            // pbPersonImage
            // 
            pbPersonImage.Image = Properties.Resources.prescription;
            pbPersonImage.Location = new Point(184, 3);
            pbPersonImage.Name = "pbPersonImage";
            pbPersonImage.Size = new Size(129, 81);
            pbPersonImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbPersonImage.TabIndex = 150;
            pbPersonImage.TabStop = false;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // linkAddItem
            // 
            linkAddItem.AutoSize = true;
            linkAddItem.Location = new Point(372, 55);
            linkAddItem.Name = "linkAddItem";
            linkAddItem.Size = new Size(56, 15);
            linkAddItem.TabIndex = 173;
            linkAddItem.TabStop = true;
            linkAddItem.Text = "Add Item";
            linkAddItem.LinkClicked += linkAddItem_LinkClicked;
            // 
            // linkShowItems
            // 
            linkShowItems.AutoSize = true;
            linkShowItems.Location = new Point(367, 70);
            linkShowItems.Name = "linkShowItems";
            linkShowItems.Size = new Size(68, 15);
            linkShowItems.TabIndex = 174;
            linkShowItems.TabStop = true;
            linkShowItems.Text = "Show Items";
            linkShowItems.LinkClicked += linkShowItems_LinkClicked;
            // 
            // ucPrescription
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(linkShowItems);
            Controls.Add(linkAddItem);
            Controls.Add(lblDoctorId);
            Controls.Add(label13);
            Controls.Add(lblPatientId);
            Controls.Add(label11);
            Controls.Add(lblAppointmentId);
            Controls.Add(label8);
            Controls.Add(txtNotes);
            Controls.Add(lblPrescriptionId);
            Controls.Add(label10);
            Controls.Add(linkEdit);
            Controls.Add(btnSave);
            Controls.Add(pictureBox2);
            Controls.Add(label2);
            Controls.Add(pbPersonImage);
            Name = "ucPrescription";
            Size = new Size(475, 228);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).EndInit();
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
        private Label lblPrescriptionId;
        private Label label10;
        private LinkLabel linkEdit;
        private Button btnSave;
        private PictureBox pictureBox2;
        private Label label2;
        private PictureBox pbPersonImage;
        private ErrorProvider errorProvider1;
        private LinkLabel linkShowItems;
        private LinkLabel linkAddItem;
    }
}
