using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities
{
    public sealed class User
    {
        [DbColumn("UserId")]
        public int UserId { get; set; }

        [DbColumn("PersonId")]
        public int PersonId { get; set; }

        [DbColumn("Username")]
        public string Username { get; set; } = null!;

        [DbColumn("Email")]
        public string? Email { get; set; }

        [DbColumn("PasswordHash")]
        public byte[] PasswordHash { get; set; } = null!;

        [DbColumn("PasswordSalt")]
        public byte[]? PasswordSalt { get; set; }

        [DbColumn("MustChangePassword")]
        public bool MustChangePassword { get; set; }

        [DbColumn("IsLocked")]
        public bool IsLocked { get; set; }

        [DbColumn("LockoutEndAt")]
        public DateTime? LockoutEndAt { get; set; }

        [DbColumn("FailedLoginCount")]
        public int FailedLoginCount { get; set; }

        [DbColumn("LastLoginAt")]
        public DateTime? LastLoginAt { get; set; }

        [DbColumn("IsActive")]
        public bool IsActive { get; set; }

        [DbColumn("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [DbColumn("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }

}
