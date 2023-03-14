using System;
using System.Collections.Generic;

namespace DFramework.Domain.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int RolId { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Role Rol { get; set; } = null!;
    }
}
