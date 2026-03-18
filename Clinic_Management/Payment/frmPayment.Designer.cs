namespace Clinic_Management.Payment
{
    partial class frmPayment
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
            ucPayment1 = new ucPayment();
            SuspendLayout();
            // 
            // ucPayment1
            // 
            ucPayment1.Location = new Point(5, 3);
            ucPayment1.Name = "ucPayment1";
            ucPayment1.Size = new Size(491, 329);
            ucPayment1.TabIndex = 0;
            // 
            // frmPayment
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 337);
            Controls.Add(ucPayment1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmPayment";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmPayment";
            ResumeLayout(false);
        }

        #endregion

        private ucPayment ucPayment1;
    }
}