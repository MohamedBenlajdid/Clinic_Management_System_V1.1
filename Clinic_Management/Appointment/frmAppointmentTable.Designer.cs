namespace Clinic_Management.Appointment
{
    partial class frmAppointmentTable
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
            pictureBox1 = new PictureBox();
            gbAppointmentTable = new GroupBox();
            lblRecordNumber = new Label();
            label3 = new Label();
            dgvAppointments = new DataGridView();
            cmsAppointments = new ContextMenuStrip(components);
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripMenuItem();
            toolStripMenuItem6 = new ToolStripMenuItem();
            toolStripMenuItem7 = new ToolStripMenuItem();
            toolStripMenuItem8 = new ToolStripMenuItem();
            toolStripMenuItem9 = new ToolStripMenuItem();
            toolStripMenuItem10 = new ToolStripMenuItem();
            toolStripMenuItem11 = new ToolStripMenuItem();
            toolStripMenuItem12 = new ToolStripMenuItem();
            gbFilterBox = new GroupBox();
            button2 = new Button();
            button1 = new Button();
            txtFilterValue = new TextBox();
            label2 = new Label();
            cbFilterBy = new ComboBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            gbAppointmentTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAppointments).BeginInit();
            cmsAppointments.SuspendLayout();
            gbFilterBox.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.sign__1_;
            pictureBox1.Location = new Point(350, 1);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // gbAppointmentTable
            // 
            gbAppointmentTable.Controls.Add(lblRecordNumber);
            gbAppointmentTable.Controls.Add(label3);
            gbAppointmentTable.Controls.Add(dgvAppointments);
            gbAppointmentTable.Controls.Add(gbFilterBox);
            gbAppointmentTable.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbAppointmentTable.Location = new Point(3, 57);
            gbAppointmentTable.Name = "gbAppointmentTable";
            gbAppointmentTable.Size = new Size(754, 397);
            gbAppointmentTable.TabIndex = 1;
            gbAppointmentTable.TabStop = false;
            gbAppointmentTable.Text = "Booked Appointment";
            // 
            // lblRecordNumber
            // 
            lblRecordNumber.AutoSize = true;
            lblRecordNumber.Location = new Point(157, 374);
            lblRecordNumber.Name = "lblRecordNumber";
            lblRecordNumber.Size = new Size(30, 15);
            lblRecordNumber.TabIndex = 6;
            lblRecordNumber.Text = "[???]";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(31, 374);
            label3.Name = "label3";
            label3.Size = new Size(120, 15);
            label3.TabIndex = 5;
            label3.Text = "Total Appointment : ";
            // 
            // dgvAppointments
            // 
            dgvAppointments.BackgroundColor = SystemColors.Control;
            dgvAppointments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAppointments.ContextMenuStrip = cmsAppointments;
            dgvAppointments.Location = new Point(9, 67);
            dgvAppointments.Name = "dgvAppointments";
            dgvAppointments.Size = new Size(735, 298);
            dgvAppointments.TabIndex = 1;
            // 
            // cmsAppointments
            // 
            cmsAppointments.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmsAppointments.ImageScalingSize = new Size(20, 20);
            cmsAppointments.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem2, toolStripMenuItem3, toolStripMenuItem4, toolStripMenuItem5, toolStripMenuItem6, toolStripMenuItem7, toolStripMenuItem8, toolStripMenuItem9, toolStripMenuItem10, toolStripMenuItem11, toolStripMenuItem12 });
            cmsAppointments.Name = "cmsAppointments";
            cmsAppointments.Size = new Size(234, 338);
            cmsAppointments.Opening += cmsAppointments_Opening;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Image = Properties.Resources.Apathae_Wren_Applications1;
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(233, 26);
            toolStripMenuItem1.Text = "View";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Image = Properties.Resources.Apathae_Wren_Applications1;
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(233, 26);
            toolStripMenuItem2.Text = "Edit";
            toolStripMenuItem2.Click += toolStripMenuItem2_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Image = Properties.Resources.Apathae_Wren_Applications1;
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(233, 26);
            toolStripMenuItem3.Text = "Cancel";
            toolStripMenuItem3.Click += toolStripMenuItem3_Click;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Image = Properties.Resources.Apathae_Wren_Applications1;
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(233, 26);
            toolStripMenuItem4.Text = "Delete";
            toolStripMenuItem4.Click += toolStripMenuItem4_Click;
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Image = Properties.Resources.Apathae_Wren_Applications1;
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new Size(233, 26);
            toolStripMenuItem5.Text = "No Show";
            toolStripMenuItem5.Click += toolStripMenuItem5_Click;
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.Image = Properties.Resources.Apathae_Wren_Applications1;
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new Size(233, 26);
            toolStripMenuItem6.Text = "In Progress";
            toolStripMenuItem6.Click += toolStripMenuItem6_Click;
            // 
            // toolStripMenuItem7
            // 
            toolStripMenuItem7.Image = Properties.Resources.Apathae_Wren_Applications1;
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            toolStripMenuItem7.Size = new Size(233, 26);
            toolStripMenuItem7.Text = "Complete";
            toolStripMenuItem7.Click += toolStripMenuItem7_Click;
            // 
            // toolStripMenuItem8
            // 
            toolStripMenuItem8.Image = Properties.Resources.Apathae_Wren_Applications_512;
            toolStripMenuItem8.Name = "toolStripMenuItem8";
            toolStripMenuItem8.Size = new Size(233, 26);
            toolStripMenuItem8.Text = "Create Medical Record";
            toolStripMenuItem8.Click += toolStripMenuItem8_Click;
            // 
            // toolStripMenuItem9
            // 
            toolStripMenuItem9.Image = Properties.Resources.Apathae_Wren_Applications_512;
            toolStripMenuItem9.Name = "toolStripMenuItem9";
            toolStripMenuItem9.Size = new Size(233, 26);
            toolStripMenuItem9.Text = "Create Prescription";
            toolStripMenuItem9.Click += toolStripMenuItem9_Click;
            // 
            // toolStripMenuItem10
            // 
            toolStripMenuItem10.Image = Properties.Resources.Apathae_Wren_Applications_512;
            toolStripMenuItem10.Name = "toolStripMenuItem10";
            toolStripMenuItem10.Size = new Size(233, 26);
            toolStripMenuItem10.Text = "Create Diagnostics";
            toolStripMenuItem10.Click += toolStripMenuItem10_Click;
            // 
            // toolStripMenuItem11
            // 
            toolStripMenuItem11.Image = Properties.Resources.Apathae_Wren_Applications_512;
            toolStripMenuItem11.Name = "toolStripMenuItem11";
            toolStripMenuItem11.Size = new Size(233, 26);
            toolStripMenuItem11.Text = "Create Medical Certificate";
            toolStripMenuItem11.Click += toolStripMenuItem11_Click;
            // 
            // toolStripMenuItem12
            // 
            toolStripMenuItem12.Image = Properties.Resources.Apathae_Wren_Applications_512;
            toolStripMenuItem12.Name = "toolStripMenuItem12";
            toolStripMenuItem12.Size = new Size(233, 26);
            toolStripMenuItem12.Text = "Create Invoice";
            toolStripMenuItem12.Click += toolStripMenuItem12_Click;
            // 
            // gbFilterBox
            // 
            gbFilterBox.Controls.Add(button2);
            gbFilterBox.Controls.Add(button1);
            gbFilterBox.Controls.Add(txtFilterValue);
            gbFilterBox.Controls.Add(label2);
            gbFilterBox.Controls.Add(cbFilterBy);
            gbFilterBox.Controls.Add(label1);
            gbFilterBox.Location = new Point(9, 19);
            gbFilterBox.Name = "gbFilterBox";
            gbFilterBox.Size = new Size(739, 42);
            gbFilterBox.TabIndex = 0;
            gbFilterBox.TabStop = false;
            gbFilterBox.Text = "Filter Box : ";
            // 
            // button2
            // 
            button2.BackgroundImage = Properties.Resources.Aniket_Suvarna_Box_Regular_Bx_reset_512;
            button2.BackgroundImageLayout = ImageLayout.Zoom;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(625, 14);
            button2.Name = "button2";
            button2.Size = new Size(51, 28);
            button2.TabIndex = 5;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.BackgroundImage = Properties.Resources.Gakuseisean_Aire_Add_Folder_256;
            button1.BackgroundImageLayout = ImageLayout.Zoom;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(682, 14);
            button1.Name = "button1";
            button1.Size = new Size(51, 28);
            button1.TabIndex = 4;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // txtFilterValue
            // 
            txtFilterValue.Location = new Point(418, 14);
            txtFilterValue.Name = "txtFilterValue";
            txtFilterValue.Size = new Size(164, 23);
            txtFilterValue.TabIndex = 3;
            txtFilterValue.TextChanged += txtFilterValue_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(322, 19);
            label2.Name = "label2";
            label2.Size = new Size(78, 15);
            label2.TabIndex = 2;
            label2.Text = "Filter Value : ";
            // 
            // cbFilterBy
            // 
            cbFilterBy.FormattingEnabled = true;
            cbFilterBy.Location = new Point(121, 16);
            cbFilterBy.Name = "cbFilterBy";
            cbFilterBy.Size = new Size(166, 23);
            cbFilterBy.TabIndex = 1;
            cbFilterBy.SelectedIndexChanged += cbFilterBy_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(53, 19);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 0;
            label1.Text = "Filter By : ";
            // 
            // frmAppointmentTable
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(759, 454);
            Controls.Add(gbAppointmentTable);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmAppointmentTable";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmAppointmentTable";
            Load += frmAppointmentTable_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            gbAppointmentTable.ResumeLayout(false);
            gbAppointmentTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAppointments).EndInit();
            cmsAppointments.ResumeLayout(false);
            gbFilterBox.ResumeLayout(false);
            gbFilterBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private GroupBox gbAppointmentTable;
        private GroupBox gbFilterBox;
        private TextBox txtFilterValue;
        private Label label2;
        private ComboBox cbFilterBy;
        private Label label1;
        private Button button1;
        private Label lblRecordNumber;
        private Label label3;
        private DataGridView dgvAppointments;
        private Button button2;
        private ContextMenuStrip cmsAppointments;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem toolStripMenuItem7;
        private ToolStripMenuItem toolStripMenuItem8;
        private ToolStripMenuItem toolStripMenuItem9;
        private ToolStripMenuItem toolStripMenuItem10;
        private ToolStripMenuItem toolStripMenuItem11;
        private ToolStripMenuItem toolStripMenuItem12;
    }
}