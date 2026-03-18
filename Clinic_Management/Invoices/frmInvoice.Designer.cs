namespace Clinic_Management.Invoices
{
    partial class frmInvoice
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
            ucInvoice1 = new ucInvoice();
            SuspendLayout();
            // 
            // ucInvoice1
            // 
            ucInvoice1.Location = new Point(4, 3);
            ucInvoice1.Name = "ucInvoice1";
            ucInvoice1.Size = new Size(461, 478);
            ucInvoice1.TabIndex = 0;
            // 
            // frmInvoice
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(471, 490);
            Controls.Add(ucInvoice1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmInvoice";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmInvoice";
            ResumeLayout(false);
        }

        #endregion

        private ucInvoice ucInvoice1;
    }
}