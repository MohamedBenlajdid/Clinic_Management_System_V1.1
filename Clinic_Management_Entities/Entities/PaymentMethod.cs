using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities.Entities
{
    public sealed class PaymentMethod
    {
        [DbColumn("PaymentMethodId")]
        public byte PaymentMethodId { get; set; }

        [DbColumn("Name")]
        public string Name { get; set; } = null!;
    }

}
