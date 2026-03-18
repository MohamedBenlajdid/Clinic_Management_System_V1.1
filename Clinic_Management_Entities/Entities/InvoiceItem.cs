using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities.Entities
{
    public sealed class InvoiceItem
    {
        [DbColumn("InvoiceItemId")]
        public int InvoiceItemId { get; set; }

        [DbColumn("InvoiceId")]
        public int InvoiceId { get; set; }

        [DbColumn("ItemType")]
        public byte ItemType { get; set; }   // map to enum in BLL

        [DbColumn("ReferenceId")]
        public int? ReferenceId { get; set; }

        [DbColumn("Description")]
        public string Description { get; set; } = null!;

        [DbColumn("Quantity")]
        public decimal Quantity { get; set; }

        [DbColumn("UnitPrice")]
        public decimal UnitPrice { get; set; }

        [DbColumn("Discount")]
        public decimal Discount { get; set; }

        [DbColumn("Total")]
        public decimal Total { get; set; }   // computed persisted
    }


}
