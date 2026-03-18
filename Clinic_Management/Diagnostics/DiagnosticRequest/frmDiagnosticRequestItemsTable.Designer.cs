namespace Clinic_Management.Diagnostics.DiagnosticRequest
{
    partial class frmDiagnosticRequestItemsTable
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
            dgvDiagnosticRequestItems = new DataGridView();
            gbFilterBox = new GroupBox();
            button2 = new Button();
            button1 = new Button();
            txtFilterValue = new TextBox();
            label2 = new Label();
            cbFilterBy = new ComboBox();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            cmsDiagnosticRequestItems = new ContextMenuStrip(components);
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            gbDiagnosticRequests.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDiagnosticRequestItems).BeginInit();
            gbFilterBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            cmsDiagnosticRequestItems.SuspendLayout();
            SuspendLayout();
            // 
            // gbDiagnosticRequests
            // 
            gbDiagnosticRequests.Controls.Add(lblRecordNumber);
            gbDiagnosticRequests.Controls.Add(label3);
            gbDiagnosticRequests.Controls.Add(dgvDiagnosticRequestItems);
            gbDiagnosticRequests.Controls.Add(gbFilterBox);
            gbDiagnosticRequests.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbDiagnosticRequests.Location = new Point(11, 58);
            gbDiagnosticRequests.Name = "gbDiagnosticRequests";
            gbDiagnosticRequests.Size = new Size(764, 397);
            gbDiagnosticRequests.TabIndex = 5;
            gbDiagnosticRequests.TabStop = false;
            gbDiagnosticRequests.Text = "Diagnostic Items Request";
            // 
            // lblRecordNumber
            // 
            lblRecordNumber.AutoSize = true;
            lblRecordNumber.Location = new Point(120, 374);
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
            label3.Size = new Size(78, 15);
            label3.TabIndex = 5;
            label3.Text = "Total Items : ";
            // 
            // dgvDiagnosticRequestItems
            // 
            dgvDiagnosticRequestItems.BackgroundColor = SystemColors.Control;
            dgvDiagnosticRequestItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDiagnosticRequestItems.ContextMenuStrip = cmsDiagnosticRequestItems;
            dgvDiagnosticRequestItems.Location = new Point(9, 67);
            dgvDiagnosticRequestItems.Name = "dgvDiagnosticRequestItems";
            dgvDiagnosticRequestItems.Size = new Size(749, 298);
            dgvDiagnosticRequestItems.TabIndex = 1;
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
            pictureBox1.Image = Properties.Resources.checklist;
            pictureBox1.Location = new Point(358, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(110, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // cmsDiagnosticRequestItems
            // 
            cmsDiagnosticRequestItems.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmsDiagnosticRequestItems.ImageScalingSize = new Size(20, 20);
            cmsDiagnosticRequestItems.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem2, toolStripMenuItem4, toolStripMenuItem3 });
            cmsDiagnosticRequestItems.Name = "cmsAppointments";
            cmsDiagnosticRequestItems.Size = new Size(222, 108);
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Image = Properties.Resources.Apathae_Wren_Applications1;
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(221, 26);
            toolStripMenuItem1.Text = "View";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Image = Properties.Resources.Apathae_Wren_Applications1;
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(221, 26);
            toolStripMenuItem2.Text = "Edit";
            toolStripMenuItem2.Click += toolStripMenuItem2_Click;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Image = Properties.Resources.Apathae_Wren_Applications1;
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(221, 26);
            toolStripMenuItem4.Text = "Delete";
            toolStripMenuItem4.Click += toolStripMenuItem4_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Image = Properties.Resources.Apathae_Wren_Applications_512;
            toolStripMenuItem3.ImageAlign = ContentAlignment.MiddleLeft;
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(221, 26);
            toolStripMenuItem3.Text = "Make Diagnostic Result";
            toolStripMenuItem3.Click += toolStripMenuItem3_Click;
            // 
            // frmDiagnosticRequestItemsTable
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 463);
            Controls.Add(gbDiagnosticRequests);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmDiagnosticRequestItemsTable";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmDiagnosticRequestItemsTable";
            Load += frmDiagnosticRequestItemsTable_Load;
            gbDiagnosticRequests.ResumeLayout(false);
            gbDiagnosticRequests.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDiagnosticRequestItems).EndInit();
            gbFilterBox.ResumeLayout(false);
            gbFilterBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            cmsDiagnosticRequestItems.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gbDiagnosticRequests;
        private Label lblRecordNumber;
        private Label label3;
        private DataGridView dgvDiagnosticRequestItems;
        private GroupBox gbFilterBox;
        private Button button2;
        private Button button1;
        private TextBox txtFilterValue;
        private Label label2;
        private ComboBox cbFilterBy;
        private Label label1;
        private PictureBox pictureBox1;
        private ContextMenuStrip cmsDiagnosticRequestItems;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem3;
    }
}