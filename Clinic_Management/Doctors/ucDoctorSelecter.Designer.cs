namespace Clinic_Management.Doctors
{
    partial class ucDoctorSelecter
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
            gbFilterDoctor = new GroupBox();
            cbSpecialities = new ComboBox();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            cbDoctors = new ComboBox();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            gbFilterDoctor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // gbFilterDoctor
            // 
            gbFilterDoctor.Controls.Add(cbDoctors);
            gbFilterDoctor.Controls.Add(pictureBox1);
            gbFilterDoctor.Controls.Add(label1);
            gbFilterDoctor.Controls.Add(cbSpecialities);
            gbFilterDoctor.Controls.Add(pictureBox2);
            gbFilterDoctor.Controls.Add(label2);
            gbFilterDoctor.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbFilterDoctor.Location = new Point(6, 3);
            gbFilterDoctor.Name = "gbFilterDoctor";
            gbFilterDoctor.Size = new Size(517, 104);
            gbFilterDoctor.TabIndex = 0;
            gbFilterDoctor.TabStop = false;
            gbFilterDoctor.Text = "Filter Doctor Box :";
            // 
            // cbSpecialities
            // 
            cbSpecialities.FormattingEnabled = true;
            cbSpecialities.Location = new Point(214, 29);
            cbSpecialities.Name = "cbSpecialities";
            cbSpecialities.Size = new Size(207, 23);
            cbSpecialities.TabIndex = 113;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox2.Location = new Point(161, 29);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(28, 22);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 112;
            pictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(34, 34);
            label2.Name = "label2";
            label2.Size = new Size(69, 15);
            label2.TabIndex = 111;
            label2.Text = "Speciality : ";
            // 
            // cbDoctors
            // 
            cbDoctors.FormattingEnabled = true;
            cbDoctors.Location = new Point(214, 57);
            cbDoctors.Name = "cbDoctors";
            cbDoctors.Size = new Size(207, 23);
            cbDoctors.TabIndex = 116;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox1.Location = new Point(161, 57);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(28, 22);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 115;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(34, 62);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 114;
            label1.Text = "Doctor : ";
            // 
            // ucDoctorSelecter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gbFilterDoctor);
            Name = "ucDoctorSelecter";
            Size = new Size(531, 113);
            gbFilterDoctor.ResumeLayout(false);
            gbFilterDoctor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gbFilterDoctor;
        private ComboBox cbSpecialities;
        private PictureBox pictureBox2;
        private Label label2;
        private ComboBox cbDoctors;
        private PictureBox pictureBox1;
        private Label label1;
    }
}
