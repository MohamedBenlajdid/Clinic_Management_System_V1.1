namespace Clinic_Management.Appointment
{
    partial class frmCancelledAppointmentReason
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            label1 = new Label();
            txtReason = new TextBox();
            btnSend = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.sign__1_;
            pictureBox1.Location = new Point(202, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 77);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(24, 119);
            label1.Name = "label1";
            label1.Size = new Size(298, 30);
            label1.TabIndex = 1;
            label1.Text = "Cancell Appointment Reason : ";
            // 
            // txtReason
            // 
            txtReason.Location = new Point(24, 165);
            txtReason.Multiline = true;
            txtReason.Name = "txtReason";
            txtReason.Size = new Size(468, 110);
            txtReason.TabIndex = 2;
            // 
            // btnSend
            // 
            btnSend.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSend.Location = new Point(417, 281);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(75, 60);
            btnSend.TabIndex = 3;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // frmCancelledAppointmentReason
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(504, 348);
            Controls.Add(btnSend);
            Controls.Add(txtReason);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmCancelledAppointmentReason";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmCancelledAppointmentReason";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private TextBox txtReason;
        private Button btnSend;
    }
}