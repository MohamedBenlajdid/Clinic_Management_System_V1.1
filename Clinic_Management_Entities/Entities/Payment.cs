using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities.Entities
{
    public sealed class Payment
    {
        [DbColumn("PaymentId")]
        public int PaymentId { get; set; }

        [DbColumn("InvoiceId")]
        public int InvoiceId { get; set; }

        [DbColumn("PaymentMethodId")]
        public byte PaymentMethodId { get; set; }

        [DbColumn("Amount")]
        public decimal Amount { get; set; }

        [DbColumn("PaymentDate")]
        public DateTime PaymentDate { get; set; }

        [DbColumn("TransactionReference")]
        public string? TransactionReference { get; set; }

        [DbColumn("Notes")]
        public string? Notes { get; set; }

        [DbColumn("IsRefund")]
        public bool IsRefund { get; set; }

        [DbColumn("CreatedByUserId")]
        public int? CreatedByUserId { get; set; }
    }

}
