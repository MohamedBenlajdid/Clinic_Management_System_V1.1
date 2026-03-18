namespace Clinic_Management.Invoices
{
    partial class ucInvoiceItem
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
            components = new System.ComponentModel.Container();
            nudTotal = new NumericUpDown();
            pictureBox8 = new PictureBox();
            label11 = new Label();
            lblInvoiceItemId = new Label();
            label8 = new Label();
            label6 = new Label();
            pictureBox6 = new PictureBox();
            label5 = new Label();
            pictureBox5 = new PictureBox();
            label4 = new Label();
            pictureBox1 = new PictureBox();
            lblInvoiceId = new Label();
            label10 = new Label();
            linkEdit = new LinkLabel();
            btnSave = new Button();
            pictureBox3 = new PictureBox();
            label1 = new Label();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            pbPersonImage = new PictureBox();
            lblReferenceId = new Label();
            label15 = new Label();
            txtDescription = new TextBox();
            cbItemType = new ComboBox();
            nudUnitPrice = new NumericUpDown();
            nudDiscount = new NumericUpDown();
            nudQuantity = new NumericUpDown();
            errorProvider1 = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)nudTotal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudUnitPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDiscount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // nudTotal
            // 
            nudTotal.Location = new Point(188, 267);
            nudTotal.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudTotal.Name = "nudTotal";
            nudTotal.ReadOnly = true;
            nudTotal.Size = new Size(128, 23);
            nudTotal.TabIndex = 295;
            // 
            // pictureBox8
            // 
            pictureBox8.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox8.Location = new Point(135, 267);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new Size(28, 22);
            pictureBox8.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox8.TabIndex = 282;
            pictureBox8.TabStop = false;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.Location = new Point(8, 272);
            label11.Name = "label11";
            label11.Size = new Size(88, 15);
            label11.TabIndex = 281;
            label11.Text = "Total Amount :";
            // 
            // lblInvoiceItemId
            // 
            lblInvoiceItemId.AutoSize = true;
            lblInvoiceItemId.Location = new Point(112, 28);
            lblInvoiceItemId.Name = "lblInvoiceItemId";
            lblInvoiceItemId.Size = new Size(37, 15);
            lblInvoiceItemId.TabIndex = 278;
            lblInvoiceItemId.Text = "[N/A]";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(8, 28);
            label8.Name = "label8";
            label8.Size = new Size(78, 15);
            label8.TabIndex = 277;
            label8.Text = "Invoice Item :";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(8, 245);
            label6.Name = "label6";
            label6.Size = new Size(65, 15);
            label6.TabIndex = 276;
            label6.Text = "Discount : ";
            // 
            // pictureBox6
            // 
            pictureBox6.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox6.Location = new Point(135, 240);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(28, 22);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 275;
            pictureBox6.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(8, 217);
            label5.Name = "label5";
            label5.Size = new Size(68, 15);
            label5.TabIndex = 274;
            label5.Text = "Unit Price :";
            // 
            // pictureBox5
            // 
            pictureBox5.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox5.Location = new Point(135, 212);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(28, 22);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 273;
            pictureBox5.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(8, 189);
            label4.Name = "label4";
            label4.Size = new Size(64, 15);
            label4.TabIndex = 272;
            label4.Text = "Quantity : ";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox1.Location = new Point(135, 184);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(28, 22);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 270;
            pictureBox1.TabStop = false;
            // 
            // lblInvoiceId
            // 
            lblInvoiceId.AutoSize = true;
            lblInvoiceId.Location = new Point(112, 8);
            lblInvoiceId.Name = "lblInvoiceId";
            lblInvoiceId.Size = new Size(37, 15);
            lblInvoiceId.TabIndex = 269;
            lblInvoiceId.Text = "[N/A]";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(8, 8);
            label10.Name = "label10";
            label10.Size = new Size(65, 15);
            label10.TabIndex = 268;
            label10.Text = "Invoice ID :";
            // 
            // linkEdit
            // 
            linkEdit.AutoSize = true;
            linkEdit.Location = new Point(351, 20);
            linkEdit.Name = "linkEdit";
            linkEdit.Size = new Size(95, 15);
            linkEdit.TabIndex = 267;
            linkEdit.TabStop = true;
            linkEdit.Text = "Edit Invoice Item";
            linkEdit.LinkClicked += linkEdit_LinkClicked;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(411, 285);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(61, 38);
            btnSave.TabIndex = 266;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox3.Location = new Point(135, 154);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(28, 22);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 263;
            pictureBox3.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(8, 159);
            label1.Name = "label1";
            label1.Size = new Size(80, 15);
            label1.TabIndex = 262;
            label1.Text = "Description : ";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox2.Location = new Point(135, 128);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(28, 22);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 261;
            pictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(8, 133);
            label2.Name = "label2";
            label2.Size = new Size(69, 15);
            label2.TabIndex = 260;
            label2.Text = "Item Type :";
            // 
            // pbPersonImage
            // 
            pbPersonImage.Image = Properties.Resources.receipt__1_;
            pbPersonImage.Location = new Point(177, 3);
            pbPersonImage.Name = "pbPersonImage";
            pbPersonImage.Size = new Size(129, 81);
            pbPersonImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbPersonImage.TabIndex = 259;
            pbPersonImage.TabStop = false;
            // 
            // lblReferenceId
            // 
            lblReferenceId.AutoSize = true;
            lblReferenceId.Location = new Point(112, 52);
            lblReferenceId.Name = "lblReferenceId";
            lblReferenceId.Size = new Size(37, 15);
            lblReferenceId.TabIndex = 302;
            lblReferenceId.Text = "[N/A]";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(8, 52);
            label15.Name = "label15";
            label15.Size = new Size(82, 15);
            label15.TabIndex = 301;
            label15.Text = "Reference ID : ";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(188, 154);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(207, 23);
            txtDescription.TabIndex = 303;
            // 
            // cbItemType
            // 
            cbItemType.FormattingEnabled = true;
            cbItemType.Location = new Point(188, 125);
            cbItemType.Name = "cbItemType";
            cbItemType.Size = new Size(207, 23);
            cbItemType.TabIndex = 304;
            // 
            // nudUnitPrice
            // 
            nudUnitPrice.Location = new Point(188, 212);
            nudUnitPrice.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudUnitPrice.Name = "nudUnitPrice";
            nudUnitPrice.Size = new Size(128, 23);
            nudUnitPrice.TabIndex = 306;
            // 
            // nudDiscount
            // 
            nudDiscount.Location = new Point(188, 238);
            nudDiscount.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudDiscount.Name = "nudDiscount";
            nudDiscount.Size = new Size(128, 23);
            nudDiscount.TabIndex = 307;
            // 
            // nudQuantity
            // 
            nudQuantity.Location = new Point(188, 183);
            nudQuantity.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudQuantity.Name = "nudQuantity";
            nudQuantity.Size = new Size(128, 23);
            nudQuantity.TabIndex = 308;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // ucInvoiceItem
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(nudQuantity);
            Controls.Add(nudDiscount);
            Controls.Add(nudUnitPrice);
            Controls.Add(cbItemType);
            Controls.Add(txtDescription);
            Controls.Add(lblReferenceId);
            Controls.Add(label15);
            Controls.Add(nudTotal);
            Controls.Add(pictureBox8);
            Controls.Add(label11);
            Controls.Add(lblInvoiceItemId);
            Controls.Add(label8);
            Controls.Add(label6);
            Controls.Add(pictureBox6);
            Controls.Add(label5);
            Controls.Add(pictureBox5);
            Controls.Add(label4);
            Controls.Add(pictureBox1);
            Controls.Add(lblInvoiceId);
            Controls.Add(label10);
            Controls.Add(linkEdit);
            Controls.Add(btnSave);
            Controls.Add(pictureBox3);
            Controls.Add(label1);
            Controls.Add(pictureBox2);
            Controls.Add(label2);
            Controls.Add(pbPersonImage);
            Name = "ucInvoiceItem";
            Size = new Size(475, 326);
            ((System.ComponentModel.ISupportInitialize)nudTotal).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudUnitPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDiscount).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker dtpDueDate;
        private DateTimePicker dtpIssueDate;
        private NumericUpDown nudSubTotal;
        private NumericUpDown nudDiscountAmount;
        private NumericUpDown nudTaxAmount;
        private NumericUpDown nudTotal;
        private NumericUpDown nudPaidAmount;
        private NumericUpDown nudRemainingAmount;
        private ComboBox cbStatus;
        private TextBox txtNotes;
        private PictureBox pictureBox11;
        private Label label14;
        private PictureBox pictureBox9;
        private Label label12;
        private PictureBox pictureBox10;
        private Label label13;
        private PictureBox pictureBox7;
        private Label label7;
        private PictureBox pictureBox8;
        private Label label11;
        private Label lblInvoiceItemId;
        private Label label8;
        private Label label6;
        private PictureBox pictureBox6;
        private Label label5;
        private PictureBox pictureBox5;
        private Label label4;
        private TextBox txtInvoiceNumber;
        private PictureBox pictureBox1;
        private Label lblInvoiceId;
        private Label label10;
        private LinkLabel linkEdit;
        private Button btnSave;
        private PictureBox pictureBox4;
        private Label label3;
        private PictureBox pictureBox3;
        private Label label1;
        private PictureBox pictureBox2;
        private Label label2;
        private PictureBox pbPersonImage;
        private Label lblReferenceId;
        private Label label15;
        private TextBox txtDescription;
        private ComboBox cbItemType;
        private NumericUpDown nudUnitPrice;
        private NumericUpDown nudDiscount;
        private NumericUpDown nudQuantity;
        private ErrorProvider errorProvider1;
    }
}
