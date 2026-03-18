namespace Clinic_Management.MainForm.ManageSystemMembers
{
    partial class frmManageSystemMemebers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManageSystemMemebers));
            pictureBox1 = new PictureBox();
            pnlSidePanel = new FlowLayoutPanel();
            pictureBox2 = new PictureBox();
            button1 = new Button();
            imageList1 = new ImageList(components);
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlSidePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.networking;
            pictureBox1.Location = new Point(321, 122);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(338, 285);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // pnlSidePanel
            // 
            pnlSidePanel.BorderStyle = BorderStyle.FixedSingle;
            pnlSidePanel.Controls.Add(pictureBox2);
            pnlSidePanel.Controls.Add(button1);
            pnlSidePanel.Controls.Add(button2);
            pnlSidePanel.Controls.Add(button3);
            pnlSidePanel.Controls.Add(button4);
            pnlSidePanel.Dock = DockStyle.Left;
            pnlSidePanel.Location = new Point(0, 0);
            pnlSidePanel.Name = "pnlSidePanel";
            pnlSidePanel.Size = new Size(200, 450);
            pnlSidePanel.TabIndex = 5;
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
            button1.ImageKey = "examination (1).png";
            button1.ImageList = imageList1;
            button1.Location = new Point(3, 75);
            button1.Name = "button1";
            button1.Size = new Size(197, 40);
            button1.TabIndex = 3;
            button1.Text = "       Create \r\n      Patient";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "examination (1).png");
            imageList1.Images.SetKeyName(1, "woman.png");
            imageList1.Images.SetKeyName(2, "man (1).png");
            imageList1.Images.SetKeyName(3, "man.png");
            imageList1.Images.SetKeyName(4, "sign (1).png");
            imageList1.Images.SetKeyName(5, "sign.png");
            imageList1.Images.SetKeyName(6, "pngwing.com (1).png");
            imageList1.Images.SetKeyName(7, "examination.png");
            imageList1.Images.SetKeyName(8, "diagnostic (2).png");
            imageList1.Images.SetKeyName(9, "analytics.png");
            imageList1.Images.SetKeyName(10, "diagnostic (1).png");
            imageList1.Images.SetKeyName(11, "diagnostic.png");
            imageList1.Images.SetKeyName(12, "cashless-payment.png");
            imageList1.Images.SetKeyName(13, "atm-card.png");
            imageList1.Images.SetKeyName(14, "pngwing.com.png");
            imageList1.Images.SetKeyName(15, "invoice (1).png");
            imageList1.Images.SetKeyName(16, "invoice.png");
            imageList1.Images.SetKeyName(17, "Medical Services.png");
            imageList1.Images.SetKeyName(18, "Receptionist (2).png");
            imageList1.Images.SetKeyName(19, "Receptionist (1).png");
            imageList1.Images.SetKeyName(20, "receptionist.png");
            imageList1.Images.SetKeyName(21, "medical_13633072.png");
            imageList1.Images.SetKeyName(22, "doctor_9439268.png");
            imageList1.Images.SetKeyName(23, "doctor_2785482.png");
            imageList1.Images.SetKeyName(24, "Saki-NuoveXT-2-Actions-appointment-new.128.png");
            imageList1.Images.SetKeyName(25, "Custom-Icon-Design-Pretty-Office-11-Customer-service.512.png");
            imageList1.Images.SetKeyName(26, "Uriy1966-Steel-System-Search.512.png");
            // 
            // button2
            // 
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.ImageIndex = 26;
            button2.ImageList = imageList1;
            button2.Location = new Point(3, 121);
            button2.Name = "button2";
            button2.Size = new Size(197, 40);
            button2.TabIndex = 4;
            button2.Text = "       Find/Update\r\n      Patient";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.ImageAlign = ContentAlignment.MiddleLeft;
            button3.ImageKey = "medical_13633072.png";
            button3.ImageList = imageList1;
            button3.Location = new Point(3, 167);
            button3.Name = "button3";
            button3.Size = new Size(197, 40);
            button3.TabIndex = 5;
            button3.Text = "       Create \r\n      Doctor";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.ImageAlign = ContentAlignment.MiddleLeft;
            button4.ImageKey = "doctor_9439268.png";
            button4.ImageList = imageList1;
            button4.Location = new Point(3, 213);
            button4.Name = "button4";
            button4.Size = new Size(197, 40);
            button4.TabIndex = 6;
            button4.Text = "       Find/Update \r\n      Doctor";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // frmManageSystemMemebers
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(764, 450);
            Controls.Add(pictureBox1);
            Controls.Add(pnlSidePanel);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmManageSystemMemebers";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmManageSystemMemebers";
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
        private Button button3;
        private Button button4;
    }
}