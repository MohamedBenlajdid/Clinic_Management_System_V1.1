namespace Clinic_Management.Diagnostics.DiagnosticResult
{
    partial class frmDiagnosticResultsTable
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
            dgvResults = new DataGridView();
            cmsDiagnosticResults = new ContextMenuStrip(components);
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripMenuItem();
            gbFilterBox = new GroupBox();
            btnRefresh = new Button();
            button1 = new Button();
            txtFilterValue = new TextBox();
            label2 = new Label();
            cbFilterBy = new ComboBox();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            gbDiagnosticRequests.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            cmsDiagnosticResults.SuspendLayout();
            gbFilterBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // gbDiagnosticRequests
            // 
            gbDiagnosticRequests.Controls.Add(lblRecordNumber);
            gbDiagnosticRequests.Controls.Add(label3);
            gbDiagnosticRequests.Controls.Add(dgvResults);
            gbDiagnosticRequests.Controls.Add(gbFilterBox);
            gbDiagnosticRequests.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbDiagnosticRequests.Location = new Point(13, 57);
            gbDiagnosticRequests.Name = "gbDiagnosticRequests";
            gbDiagnosticRequests.Size = new Size(764, 397);
            gbDiagnosticRequests.TabIndex = 5;
            gbDiagnosticRequests.TabStop = false;
            gbDiagnosticRequests.Text = "Diagnostic Result";
            // 
            // lblRecordNumber
            // 
            lblRecordNumber.AutoSize = true;
            lblRecordNumber.Location = new Point(180, 374);
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
            label3.Size = new Size(143, 15);
            label3.TabIndex = 5;
            label3.Text = "Total Diagnostics Results";
            // 
            // dgvResults
            // 
            dgvResults.BackgroundColor = SystemColors.Control;
            dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResults.ContextMenuStrip = cmsDiagnosticResults;
            dgvResults.Location = new Point(9, 67);
            dgvResults.Name = "dgvResults";
            dgvResults.Size = new Size(749, 298);
            dgvResults.TabIndex = 1;
            // 
            // cmsDiagnosticResults
            // 
            cmsDiagnosticResults.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmsDiagnosticResults.ImageScalingSize = new Size(20, 20);
            cmsDiagnosticResults.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem2, toolStripMenuItem4 });
            cmsDiagnosticResults.Name = "cmsAppointments";
            cmsDiagnosticResults.Size = new Size(119, 82);
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Image = Properties.Resources.Apathae_Wren_Applications1;
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(118, 26);
            toolStripMenuItem1.Text = "View";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Image = Properties.Resources.Apathae_Wren_Applications1;
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(118, 26);
            toolStripMenuItem2.Text = "Edit";
            toolStripMenuItem2.Click += toolStripMenuItem2_Click;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Image = Properties.Resources.Apathae_Wren_Applications1;
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(118, 26);
            toolStripMenuItem4.Text = "Delete";
            toolStripMenuItem4.Click += toolStripMenuItem4_Click;
            // 
            // gbFilterBox
            // 
            gbFilterBox.Controls.Add(btnRefresh);
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
            // btnRefresh
            // 
            btnRefresh.BackgroundImage = Properties.Resources.Aniket_Suvarna_Box_Regular_Bx_reset_512;
            btnRefresh.BackgroundImageLayout = ImageLayout.Zoom;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Location = new Point(625, 14);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(51, 28);
            btnRefresh.TabIndex = 5;
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
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
            pictureBox1.Image = Properties.Resources.stethoscope;
            pictureBox1.Location = new Point(360, 1);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(110, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // frmDiagnosticResultsTable
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 463);
            Controls.Add(gbDiagnosticRequests);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmDiagnosticResultsTable";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmDiagnosticResultsTable";
            Load += frmDiagnosticResultsTable_Load;
            gbDiagnosticRequests.ResumeLayout(false);
            gbDiagnosticRequests.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
            cmsDiagnosticResults.ResumeLayout(false);
            gbFilterBox.ResumeLayout(false);
            gbFilterBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gbDiagnosticRequests;
        private Label lblRecordNumber;
        private Label label3;
        private DataGridView dgvResults;
        private GroupBox gbFilterBox;
        private Button btnRefresh;
        private Button button1;
        private TextBox txtFilterValue;
        private Label label2;
        private ComboBox cbFilterBy;
        private Label label1;
        private PictureBox pictureBox1;
        private ContextMenuStrip cmsDiagnosticResults;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem4;
    }
}