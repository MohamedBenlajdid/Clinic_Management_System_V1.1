namespace Clinic_Management.Diagnostics.DiagnosticRequest
{
    partial class ucDiagnosticRequestItem
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
            label8 = new Label();
            txtNotes = new TextBox();
            lblDiagnosticRequestItemId = new Label();
            label10 = new Label();
            linkEdit = new LinkLabel();
            btnSave = new Button();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            pbPersonImage = new PictureBox();
            lblDiagnosticRequestId = new Label();
            lblDiagnosticTestId = new Label();
            label4 = new Label();
            errorProvider1 = new ErrorProvider(components);
            pictureBox1 = new PictureBox();
            label1 = new Label();
            linkAddOrFindTest = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 26);
            label8.Name = "label8";
            label8.Size = new Size(90, 15);
            label8.TabIndex = 181;
            label8.Text = "Diag Requst ID :";
            // 
            // txtNotes
            // 
            txtNotes.Location = new Point(141, 125);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new Size(265, 53);
            txtNotes.TabIndex = 180;
            // 
            // lblDiagnosticRequestItemId
            // 
            lblDiagnosticRequestItemId.AutoSize = true;
            lblDiagnosticRequestItemId.Location = new Point(117, 6);
            lblDiagnosticRequestItemId.Name = "lblDiagnosticRequestItemId";
            lblDiagnosticRequestItemId.Size = new Size(37, 15);
            lblDiagnosticRequestItemId.TabIndex = 179;
            lblDiagnosticRequestItemId.Text = "[N/A]";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(12, 6);
            label10.Name = "label10";
            label10.Size = new Size(107, 15);
            label10.TabIndex = 178;
            label10.Text = "D.Request Item ID :";
            // 
            // linkEdit
            // 
            linkEdit.AutoSize = true;
            linkEdit.Location = new Point(358, 22);
            linkEdit.Name = "linkEdit";
            linkEdit.Size = new Size(99, 15);
            linkEdit.TabIndex = 177;
            linkEdit.TabStop = true;
            linkEdit.Text = "Edit Request Item";
            linkEdit.LinkClicked += linkEdit_LinkClicked;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(412, 181);
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
            pictureBox2.Location = new Point(88, 128);
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
            label2.Location = new Point(17, 133);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 174;
            label2.Text = "Notes : ";
            // 
            // pbPersonImage
            // 
            pbPersonImage.Image = Properties.Resources.prescription;
            pbPersonImage.Location = new Point(192, 3);
            pbPersonImage.Name = "pbPersonImage";
            pbPersonImage.Size = new Size(129, 81);
            pbPersonImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbPersonImage.TabIndex = 173;
            pbPersonImage.TabStop = false;
            // 
            // lblDiagnosticRequestId
            // 
            lblDiagnosticRequestId.AutoSize = true;
            lblDiagnosticRequestId.Location = new Point(117, 26);
            lblDiagnosticRequestId.Name = "lblDiagnosticRequestId";
            lblDiagnosticRequestId.Size = new Size(37, 15);
            lblDiagnosticRequestId.TabIndex = 182;
            lblDiagnosticRequestId.Text = "[N/A]";
            // 
            // lblDiagnosticTestId
            // 
            lblDiagnosticTestId.AutoSize = true;
            lblDiagnosticTestId.Location = new Point(117, 46);
            lblDiagnosticTestId.Name = "lblDiagnosticTestId";
            lblDiagnosticTestId.Size = new Size(37, 15);
            lblDiagnosticTestId.TabIndex = 184;
            lblDiagnosticTestId.Text = "[N/A]";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 46);
            label4.Name = "label4";
            label4.Size = new Size(78, 15);
            label4.TabIndex = 183;
            label4.Text = "Diag Test ID : ";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox1.Location = new Point(86, 185);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(28, 22);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 186;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(18, 188);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 185;
            label1.Text = "Test :  ";
            // 
            // linkAddOrFindTest
            // 
            linkAddOrFindTest.AutoSize = true;
            linkAddOrFindTest.Location = new Point(141, 188);
            linkAddOrFindTest.Name = "linkAddOrFindTest";
            linkAddOrFindTest.Size = new Size(87, 15);
            linkAddOrFindTest.TabIndex = 187;
            linkAddOrFindTest.TabStop = true;
            linkAddOrFindTest.Text = "Add / Find Test";
            linkAddOrFindTest.LinkClicked += linkAddOrFindTest_LinkClicked;
            // 
            // ucDiagnosticRequestItem
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(linkAddOrFindTest);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(lblDiagnosticTestId);
            Controls.Add(label4);
            Controls.Add(lblDiagnosticRequestId);
            Controls.Add(label8);
            Controls.Add(txtNotes);
            Controls.Add(lblDiagnosticRequestItemId);
            Controls.Add(label10);
            Controls.Add(linkEdit);
            Controls.Add(btnSave);
            Controls.Add(pictureBox2);
            Controls.Add(label2);
            Controls.Add(pbPersonImage);
            Name = "ucDiagnosticRequestItem";
            Size = new Size(480, 224);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
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
        private Label lblDiagnosticRequestItemId;
        private Label label10;
        private LinkLabel linkEdit;
        private Button btnSave;
        private PictureBox pictureBox2;
        private Label label2;
        private PictureBox pbPersonImage;
        private Label lblDiagnosticRequestId;
        private Label lblDiagnosticTestId;
        private Label label4;
        private ErrorProvider errorProvider1;
        private PictureBox pictureBox1;
        private Label label1;
        private LinkLabel linkAddOrFindTest;
    }
}
