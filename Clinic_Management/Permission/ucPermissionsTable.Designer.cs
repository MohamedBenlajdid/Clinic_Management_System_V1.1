namespace Clinic_Management.Permission
{
    partial class ucPermissionsTable
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
            gbPermissionTable = new GroupBox();
            dgvPermission = new DataGridView();
            label1 = new Label();
            lblRecordNumbers = new Label();
            btnSave = new Button();
            gbPermissionTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPermission).BeginInit();
            SuspendLayout();
            // 
            // gbPermissionTable
            // 
            gbPermissionTable.Controls.Add(dgvPermission);
            gbPermissionTable.Location = new Point(3, 3);
            gbPermissionTable.Name = "gbPermissionTable";
            gbPermissionTable.Size = new Size(721, 274);
            gbPermissionTable.TabIndex = 0;
            gbPermissionTable.TabStop = false;
            gbPermissionTable.Text = "Permission Table : ";
            // 
            // dgvPermission
            // 
            dgvPermission.BackgroundColor = SystemColors.Control;
            dgvPermission.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPermission.Location = new Point(12, 22);
            dgvPermission.Name = "dgvPermission";
            dgvPermission.Size = new Size(703, 246);
            dgvPermission.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 280);
            label1.Name = "label1";
            label1.Size = new Size(105, 15);
            label1.TabIndex = 0;
            label1.Text = "Records Number : ";
            // 
            // lblRecordNumbers
            // 
            lblRecordNumbers.AutoSize = true;
            lblRecordNumbers.Location = new Point(126, 280);
            lblRecordNumbers.Name = "lblRecordNumbers";
            lblRecordNumbers.Size = new Size(30, 15);
            lblRecordNumbers.TabIndex = 1;
            lblRecordNumbers.Text = "[???]";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(643, 277);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 29);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // ucPermissionsTable
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnSave);
            Controls.Add(lblRecordNumbers);
            Controls.Add(label1);
            Controls.Add(gbPermissionTable);
            Name = "ucPermissionsTable";
            Size = new Size(727, 317);
            gbPermissionTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPermission).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox gbPermissionTable;
        private Label label1;
        private DataGridView dgvPermission;
        private Label lblRecordNumbers;
        private Button btnSave;
    }
}
