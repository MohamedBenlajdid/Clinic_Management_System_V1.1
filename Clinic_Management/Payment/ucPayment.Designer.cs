namespace Clinic_Management.Payment
{
    partial class ucPayment
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
            nudAmount = new NumericUpDown();
            cbPaymentMethodId = new ComboBox();
            txtNotes = new TextBox();
            lblPaymentId = new Label();
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
            label3 = new Label();
            txtTransactionReference = new TextBox();
            dtpPaymentDate = new DateTimePicker();
            errorProvider1 = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)nudAmount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // nudAmount
            // 
            nudAmount.Location = new Point(211, 157);
            nudAmount.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudAmount.Name = "nudAmount";
            nudAmount.Size = new Size(128, 23);
            nudAmount.TabIndex = 335;
            // 
            // cbPaymentMethodId
            // 
            cbPaymentMethodId.FormattingEnabled = true;
            cbPaymentMethodId.Location = new Point(211, 126);
            cbPaymentMethodId.Name = "cbPaymentMethodId";
            cbPaymentMethodId.Size = new Size(207, 23);
            cbPaymentMethodId.TabIndex = 332;
            // 
            // txtNotes
            // 
            txtNotes.Location = new Point(211, 240);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new Size(207, 31);
            txtNotes.TabIndex = 331;
            // 
            // lblPaymentId
            // 
            lblPaymentId.AutoSize = true;
            lblPaymentId.Location = new Point(115, 28);
            lblPaymentId.Name = "lblPaymentId";
            lblPaymentId.Size = new Size(37, 15);
            lblPaymentId.TabIndex = 325;
            lblPaymentId.Text = "[N/A]";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(11, 245);
            label6.Name = "label6";
            label6.Size = new Size(49, 15);
            label6.TabIndex = 323;
            label6.Text = "Notes : ";
            // 
            // pictureBox6
            // 
            pictureBox6.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox6.Location = new Point(157, 240);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(28, 22);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 322;
            pictureBox6.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(11, 217);
            label5.Name = "label5";
            label5.Size = new Size(141, 15);
            label5.TabIndex = 321;
            label5.Text = "Transaction Reference : ";
            // 
            // pictureBox5
            // 
            pictureBox5.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox5.Location = new Point(157, 212);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(28, 22);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 320;
            pictureBox5.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(11, 189);
            label4.Name = "label4";
            label4.Size = new Size(95, 15);
            label4.TabIndex = 319;
            label4.Text = "Payment Date : ";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox1.Location = new Point(157, 184);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(28, 22);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 318;
            pictureBox1.TabStop = false;
            // 
            // lblInvoiceId
            // 
            lblInvoiceId.AutoSize = true;
            lblInvoiceId.Location = new Point(115, 8);
            lblInvoiceId.Name = "lblInvoiceId";
            lblInvoiceId.Size = new Size(37, 15);
            lblInvoiceId.TabIndex = 317;
            lblInvoiceId.Text = "[N/A]";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(11, 8);
            label10.Name = "label10";
            label10.Size = new Size(65, 15);
            label10.TabIndex = 316;
            label10.Text = "Invoice ID :";
            // 
            // linkEdit
            // 
            linkEdit.AutoSize = true;
            linkEdit.Location = new Point(354, 20);
            linkEdit.Name = "linkEdit";
            linkEdit.Size = new Size(77, 15);
            linkEdit.TabIndex = 315;
            linkEdit.TabStop = true;
            linkEdit.Text = "Edit Payment";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(427, 288);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(61, 38);
            btnSave.TabIndex = 314;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox3.Location = new Point(157, 154);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(28, 22);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 313;
            pictureBox3.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(11, 159);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 312;
            label1.Text = "Amount : ";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.Apathae_Wren_Applications_512;
            pictureBox2.Location = new Point(157, 128);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(28, 22);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 311;
            pictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(11, 133);
            label2.Name = "label2";
            label2.Size = new Size(109, 15);
            label2.TabIndex = 310;
            label2.Text = "Payment Method :";
            // 
            // pbPersonImage
            // 
            pbPersonImage.Image = Properties.Resources.cashless_payment__1_;
            pbPersonImage.Location = new Point(190, 3);
            pbPersonImage.Name = "pbPersonImage";
            pbPersonImage.Size = new Size(129, 81);
            pbPersonImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbPersonImage.TabIndex = 309;
            pbPersonImage.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 28);
            label3.Name = "label3";
            label3.Size = new Size(74, 15);
            label3.TabIndex = 336;
            label3.Text = "Payment ID :";
            // 
            // txtTransactionReference
            // 
            txtTransactionReference.Location = new Point(211, 214);
            txtTransactionReference.Name = "txtTransactionReference";
            txtTransactionReference.Size = new Size(207, 23);
            txtTransactionReference.TabIndex = 337;
            // 
            // dtpPaymentDate
            // 
            dtpPaymentDate.Format = DateTimePickerFormat.Short;
            dtpPaymentDate.Location = new Point(211, 185);
            dtpPaymentDate.Name = "dtpPaymentDate";
            dtpPaymentDate.Size = new Size(207, 23);
            dtpPaymentDate.TabIndex = 338;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // ucPayment
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dtpPaymentDate);
            Controls.Add(txtTransactionReference);
            Controls.Add(label3);
            Controls.Add(nudAmount);
            Controls.Add(cbPaymentMethodId);
            Controls.Add(txtNotes);
            Controls.Add(lblPaymentId);
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
            Name = "ucPayment";
            Size = new Size(491, 329);
            ((System.ComponentModel.ISupportInitialize)nudAmount).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown nudAmount;
        private NumericUpDown nudDiscount;
        private NumericUpDown nd;
        private ComboBox cbPaymentMethodId;
        private TextBox txtNotes;
        private Label lblPaymentId;
        private Label label6;
        private PictureBox pictureBox6;
        private Label label5;
        private PictureBox pictureBox5;
        private Label label4;
        private PictureBox pictureBox1;
        private Label lblInvoiceId;
        private Label label10;
        private LinkLabel linkEdit;
        private Button btnSave;
        private PictureBox pictureBox3;
        private Label label1;
        private PictureBox pictureBox2;
        private Label label2;
        private PictureBox pbPersonImage;
        private Label label3;
        private TextBox txtTransactionReference;
        private DateTimePicker dtpPaymentDate;
        private ErrorProvider errorProvider1;
    }
}
