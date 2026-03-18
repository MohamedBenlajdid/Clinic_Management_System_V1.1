namespace Clinic_Management.Invoices
{
    partial class frmInvoiceItem
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
            ucInvoiceItem1 = new ucInvoiceItem();
            SuspendLayout();
            // 
            // ucInvoiceItem1
            // 
            ucInvoiceItem1.Location = new Point(5, 4);
            ucInvoiceItem1.Name = "ucInvoiceItem1";
            ucInvoiceItem1.Size = new Size(475, 326);
            ucInvoiceItem1.TabIndex = 0;
            // 
            // frmInvoiceItem
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(486, 336);
            Controls.Add(ucInvoiceItem1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmInvoiceItem";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmInvoiceItem";
            ResumeLayout(false);
        }

        #endregion

        private ucInvoiceItem ucInvoiceItem1;
    }
}