
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities
{
    public sealed class AuditLogDetail
    {
        [DbColumn("AuditId")]
        public long AuditId { get; set; }

        [DbColumn("FieldName")]
        public string FieldName { get; set; }

        [DbColumn("OldValue")]
        public string OldValue { get; set; }

        [DbColumn("NewValue")]
        public string NewValue { get; set; }
    }

}
