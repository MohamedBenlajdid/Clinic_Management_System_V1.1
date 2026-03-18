namespace Clinic_Management.Diagnostics.DiagnosticRequest
{
    partial class frmDiagnosticRequestsTable
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
            gbDiagnosticRequests = new GroupBox();
            lblRecordNumber = new Label();
            label3 = new Label();
            dgvDiagnosticRequests = new DataGridView();
            cmsDiagnosticRequests = new ContextMenuStrip(components);
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            gbFilterBox = new GroupBox();
            button2 = new Button();
            button1 = new Button();
            txtFilterValue = new TextBox();
            label2 = new Label();
            cbFilterBy = new ComboBox();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            gbDiagnosticRequests.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDiagnosticRequests).BeginInit();
            cmsDiagnosticRequests.SuspendLayout();
            gbFilterBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // gbDiagnosticRequests
            // 
            gbDiagnosticRequests.Controls.Add(lblRecordNumber);
            gbDiagnosticRequests.Controls.Add(label3);
            gbDiagnosticRequests.Controls.Add(dgvDiagnosticRequests);
            gbDiagnosticRequests.Controls.Add(gbFilterBox);
            gbDiagnosticRequests.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbDiagnosticRequests.Location = new Point(4, 57);
            gbDiagnosticRequests.Name = "gbDiagnosticRequests";
            gbDiagnosticRequests.Size = new Size(764, 397);
            gbDiagnosticRequests.TabIndex = 3;
            gbDiagnosticRequests.TabStop = false;
            gbDiagnosticRequests.Text = "Diagnostic Requests";
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
            label3.Size = new Size(125, 15);
            label3.TabIndex = 5;
            label3.Text = "Total Diag Requests : ";
            // 
            // dgvDiagnosticRequests
            // 
            dgvDiagnosticRequests.BackgroundColor = SystemColors.Control;
            dgvDiagnosticRequests.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDiagnosticRequests.ContextMenuStrip = cmsDiagnosticRequests;
            dgvDiagnosticRequests.Location = new Point(9, 67);
            dgvDiagnosticRequests.Name = "dgvDiagnosticRequests";
            dgvDiagnosticRequests.Size = new Size(749, 298);
            dgvDiagnosticRequests.TabIndex = 1;
            // 
            // cmsDiagnosticRequests
            // 
            cmsDiagnosticRequests.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmsDiagnosticRequests.ImageScalingSize = new Size(20, 20);
            cmsDiagnosticRequests.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem2, toolStripMenuItem4, toolStripMenuItem3 });
            cmsDiagnosticRequests.Name = "cmsAppointments";
            cmsDiagnosticRequests.Size = new Size(208, 108);
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Image = Properties.Resources.Apathae_Wren_Applications1;
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(207, 26);
            toolStripMenuItem1.Text = "View";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Image = Properties.Resources.Apathae_Wren_Applications1;
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(207, 26);
            toolStripMenuItem2.Text = "Edit";
            toolStripMenuItem2.Click += toolStripMenuItem2_Click;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Image = Properties.Resources.Apathae_Wren_Applications1;
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(207, 26);
            toolStripMenuItem4.Text = "Delete";
            toolStripMenuItem4.Click += toolStripMenuItem4_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Image = Properties.Resources.Apathae_Wren_Applications_512;
            toolStripMenuItem3.ImageAlign = ContentAlignment.MiddleLeft;
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(207, 26);
            toolStripMenuItem3.Text = "Show Request Items.";
            toolStripMenuItem3.Click += toolStripMenuItem3_Click;
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
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.sign__1_;
            pictureBox1.Location = new Point(351, 1);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(110, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // frmDiagnosticRequestsTable
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(793, 462);
            Controls.Add(gbDiagnosticRequests);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmDiagnosticRequestsTable";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmDiagnosticRequestsTable";
            Load += frmDiagnosticRequestsTable_Load;
            gbDiagnosticRequests.ResumeLayout(false);
            gbDiagnosticRequests.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDiagnosticRequests).EndInit();
            cmsDiagnosticRequests.ResumeLayout(false);
            gbFilterBox.ResumeLayout(false);
            gbFilterBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gbDiagnosticRequests;
        private Label lblRecordNumber;
        private Label label3;
        private DataGridView dgvDiagnosticRequests;
        private GroupBox gbFilterBox;
        private Button button2;
        private Button button1;
        private TextBox txtFilterValue;
        private Label label2;
        private ComboBox cbFilterBy;
        private Label label1;
        private PictureBox pictureBox1;
        private ContextMenuStrip cmsDiagnosticRequests;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem3;
    }
}