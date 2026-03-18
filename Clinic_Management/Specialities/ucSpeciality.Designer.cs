namespace Clinic_Management.Specialities
{
    partial class ucSpeciality
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
            txtSpecialityName = new TextBox();
            btnSave = new Button();
            linkEdit = new LinkLabel();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            lblSpecialityID = new Label();
            lbl4 = new Label();
            pbPersonImage = new PictureBox();
            errorProvider1 = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // txtSpecialityName
            // 
            txtSpecialityName.Location = new Point(190, 102);
            txtSpecialityName.Name = "txtSpecialityName";
            txtSpecialityName.Size = new Size(238, 23);
            txtSpecialityName.TabIndex = 100;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(437, 150);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(72, 41);
            btnSave.TabIndex = 99;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // linkEdit
            // 
            linkEdit.AutoSize = true;
            linkEdit.Location = new Point(42, 53);
            linkEdit.Name = "linkEdit";
            linkEdit.Size = new Size(80, 15);
            linkEdit.TabIndex = 98;
            linkEdit.TabStop = true;
            linkEdit.Text = "Edit Speciality";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox2.Location = new Point(147, 103);
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
            label2.Location = new Point(20, 108);
            label2.Name = "label2";
            label2.Size = new Size(105, 15);
            label2.TabIndex = 96;
            label2.Text = "Speciality Name : ";
            // 
            // lblSpecialityID
            // 
            lblSpecialityID.AutoSize = true;
            lblSpecialityID.Location = new Point(103, 8);
            lblSpecialityID.Name = "lblSpecialityID";
            lblSpecialityID.Size = new Size(37, 15);
            lblSpecialityID.TabIndex = 95;
            lblSpecialityID.Text = "[N/A]";
            // 
            // lbl4
            // 
            lbl4.AutoSize = true;
            lbl4.Location = new Point(4, 8);
            lbl4.Name = "lbl4";
            lbl4.Size = new Size(80, 15);
            lbl4.TabIndex = 94;
            lbl4.Text = "Speciality ID : ";
            // 
            // pbPersonImage
            // 
            pbPersonImage.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pbPersonImage.Location = new Point(200, 2);
            pbPersonImage.Name = "pbPersonImage";
            pbPersonImage.Size = new Size(129, 81);
            pbPersonImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbPersonImage.TabIndex = 93;
            pbPersonImage.TabStop = false;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // ucSpeciality
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(txtSpecialityName);
            Controls.Add(btnSave);
            Controls.Add(linkEdit);
            Controls.Add(pictureBox2);
            Controls.Add(label2);
            Controls.Add(lblSpecialityID);
            Controls.Add(lbl4);
            Controls.Add(pbPersonImage);
            Name = "ucSpeciality";
            Size = new Size(512, 194);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtSpecialityName;
        private Button btnSave;
        private LinkLabel linkEdit;
        private PictureBox pictureBox2;
        private Label label2;
        private Label lblSpecialityID;
        private Label lbl4;
        private PictureBox pbPersonImage;
        private ErrorProvider errorProvider1;
    }
}
