
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities
{
    public sealed class AuditLog
    {
        [DbColumn("AuditId")]
        public long AuditId { get; set; }

        [DbColumn("At")]
        public DateTime At { get; set; }

        [DbColumn("UserId")]
        public int? UserId { get; set; }

        [DbColumn("Action")]
        public string Action { get; set; }

        [DbColumn("EntityType")]
        public string? EntityType { get; set; }

        [DbColumn("EntityId")]
        public string? EntityId { get; set; }

        [DbColumn("Success")]
        public bool Success { get; set; }

        [DbColumn("FailureReason")]
        public string? FailureReason { get; set; }

        [DbColumn("MachineName")]
        public string? MachineName { get; set; }

        [DbColumn("SessionId")]
        public string? SessionId { get; set; }

        [DbColumn("CorrelationId")]
        public Guid? CorrelationId { get; set; }

        [DbColumn("MetadataJson")]
        public string? MetadataJson { get; set; }
    }

}
