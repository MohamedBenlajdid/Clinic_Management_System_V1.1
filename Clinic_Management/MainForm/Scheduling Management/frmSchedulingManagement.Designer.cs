namespace Clinic_Management.MainForm.Scheduling_Management
{
    partial class frmSchedulingManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSchedulingManagement));
            pictureBox1 = new PictureBox();
            pnlSidePanel = new FlowLayoutPanel();
            pictureBox2 = new PictureBox();
            button1 = new Button();
            imageList1 = new ImageList(components);
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlSidePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.doctor_appointment__1_;
            pictureBox1.Location = new Point(335, 110);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(338, 285);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 8;
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
            pnlSidePanel.Controls.Add(button5);
            pnlSidePanel.Controls.Add(button6);
            pnlSidePanel.Dock = DockStyle.Left;
            pnlSidePanel.Location = new Point(0, 0);
            pnlSidePanel.Name = "pnlSidePanel";
            pnlSidePanel.Size = new Size(200, 496);
            pnlSidePanel.TabIndex = 7;
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
            button1.ImageKey = "write.png";
            button1.ImageList = imageList1;
            button1.Location = new Point(3, 75);
            button1.Name = "button1";
            button1.Size = new Size(197, 40);
            button1.TabIndex = 3;
            button1.Text = "       Create \r\n      Doc Schedule";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "write.png");
            // 
            // button2
            // 
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.ImageKey = "write.png";
            button2.ImageList = imageList1;
            button2.Location = new Point(3, 121);
            button2.Name = "button2";
            button2.Size = new Size(197, 40);
            button2.TabIndex = 4;
            button2.Text = "       Find/Update\r\n      Schedule";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.ImageAlign = ContentAlignment.MiddleLeft;
            button3.ImageKey = "write.png";
            button3.ImageList = imageList1;
            button3.Location = new Point(3, 167);
            button3.Name = "button3";
            button3.Size = new Size(197, 40);
            button3.TabIndex = 5;
            button3.Text = "       Create Override Day";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.ImageAlign = ContentAlignment.MiddleLeft;
            button4.ImageKey = "write.png";
            button4.ImageList = imageList1;
            button4.Location = new Point(3, 213);
            button4.Name = "button4";
            button4.Size = new Size(197, 40);
            button4.TabIndex = 6;
            button4.Text = "       Find/Update\r\n      Override Day";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.ImageAlign = ContentAlignment.MiddleLeft;
            button5.ImageKey = "write.png";
            button5.ImageList = imageList1;
            button5.Location = new Point(3, 259);
            button5.Name = "button5";
            button5.Size = new Size(197, 40);
            button5.TabIndex = 7;
            button5.Text = "       Create Override Session";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.ImageAlign = ContentAlignment.MiddleLeft;
            button6.ImageKey = "write.png";
            button6.ImageList = imageList1;
            button6.Location = new Point(3, 305);
            button6.Name = "button6";
            button6.Size = new Size(197, 40);
            button6.TabIndex = 9;
            button6.Text = "       Find/Update\r\n      Override Session";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // frmSchedulingManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(769, 496);
            Controls.Add(pictureBox1);
            Controls.Add(pnlSidePanel);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmSchedulingManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmSchedulingManagement";
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
        private Button button5;
        private Button button6;
    }
}