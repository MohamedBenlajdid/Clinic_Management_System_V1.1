using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities.Entities
{
    public sealed class Invoice
    {
        [DbColumn("InvoiceId")]
        public int InvoiceId { get; set; }

        [DbColumn("InvoiceNumber")]
        public string InvoiceNumber { get; set; } = null!;  // INV-2026-00001

        [DbColumn("PatientId")]
        public int PatientId { get; set; }

        [DbColumn("AppointmentId")]
        public int? AppointmentId { get; set; }

        [DbColumn("IssueDate")]
        public DateTime IssueDate { get; set; }

        [DbColumn("DueDate")]
        public DateTime? DueDate { get; set; }

        [DbColumn("SubTotal")]
        public decimal SubTotal { get; set; }

        [DbColumn("DiscountAmount")]
        public decimal DiscountAmount { get; set; }

        [DbColumn("TaxAmount")]
        public decimal TaxAmount { get; set; }

        [DbColumn("TotalAmount")]
        public decimal TotalAmount { get; set; }

        [DbColumn("PaidAmount")]
        public decimal PaidAmount { get; set; }

        [DbColumn("RemainingAmount")]
        public decimal RemainingAmount { get; set; } // computed persisted

        [DbColumn("Status")]
        public byte Status { get; set; } // map to enum in BLL

        [DbColumn("Notes")]
        public string? Notes { get; set; }

        [DbColumn("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [DbColumn("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [DbColumn("CreatedByUserId")]
        public int? CreatedByUserId { get; set; }

        [DbColumn("UpdatedByUserId")]
        public int? UpdatedByUserId { get; set; }

        [DbColumn("IsDeleted")]
        public bool IsDeleted { get; set; }
    }

}
