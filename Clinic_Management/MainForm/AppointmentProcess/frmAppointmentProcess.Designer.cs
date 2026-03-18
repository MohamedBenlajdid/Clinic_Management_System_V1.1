namespace Clinic_Management.MainForm.AppointmentProcess
{
    partial class frmAppointmentProcess
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAppointmentProcess));
            pictureBox1 = new PictureBox();
            pnlSidePanel = new FlowLayoutPanel();
            pictureBox2 = new PictureBox();
            button1 = new Button();
            imageList1 = new ImageList(components);
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlSidePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.online_booking;
            pictureBox1.Location = new Point(377, 110);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(338, 285);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            // 
            // pnlSidePanel
            // 
            pnlSidePanel.BorderStyle = BorderStyle.FixedSingle;
            pnlSidePanel.Controls.Add(pictureBox2);
            pnlSidePanel.Controls.Add(button1);
            pnlSidePanel.Controls.Add(button2);
            pnlSidePanel.Dock = DockStyle.Left;
            pnlSidePanel.Location = new Point(0, 0);
            pnlSidePanel.Name = "pnlSidePanel";
            pnlSidePanel.Size = new Size(200, 450);
            pnlSidePanel.TabIndex = 9;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.sign;
            pictureBox2.Location = new Point(3, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(196, 66);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // button1
            // 
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.ImageKey = "click.png";
            button1.ImageList = imageList1;
            button1.Location = new Point(3, 75);
            button1.Name = "button1";
            button1.Size = new Size(197, 40);
            button1.TabIndex = 3;
            button1.Text = "       Booking Appointment";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "click.png");
            // 
            // button2
            // 
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.ImageKey = "click.png";
            button2.ImageList = imageList1;
            button2.Location = new Point(3, 121);
            button2.Name = "button2";
            button2.Size = new Size(197, 40);
            button2.TabIndex = 4;
            button2.Text = "       Appointments Table";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // frmAppointmentProcess
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(756, 450);
            Controls.Add(pictureBox1);
            Controls.Add(pnlSidePanel);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmAppointmentProcess";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmAppointmentProcess";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pnlSidePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private FlowLayoutPanel pnlSidePanel;
        private PictureBox pictureBox2;
        private Button button1;
        private ImageList imageList1;
        private Button button2;
    }
}