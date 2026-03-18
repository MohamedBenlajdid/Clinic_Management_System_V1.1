using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities
{
    public sealed class UserPermissionOverride
    {
        [DbColumn("UserId")]
        public int UserId { get; set; }

        [DbColumn("PermissionId")]
        public int PermissionId { get; set; }

        /// <summary>
        /// 1 = Grant, 2 = Deny
        /// </summary>
        [DbColumn("OverrideType")]
        public byte OverrideType { get; set; }

        [DbColumn("Reason")]
        public string Reason { get; set; }

        [DbColumn("ExpiresAt")]
        public DateTime? ExpiresAt { get; set; }

        [DbColumn("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [DbColumn("CreatedByUserId")]
        public int? CreatedByUserId { get; set; }
    }

}
