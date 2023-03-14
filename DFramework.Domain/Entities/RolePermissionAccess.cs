using System;
using System.Collections.Generic;

namespace DFramework.Domain.Entities
{
    public partial class RolePermissionAccess
    {
        public int Id { get; set; }
        public int RolPermissionId { get; set; }
        public int AccessId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Access Access { get; set; } = null!;
        public virtual RolePermission RolPermission { get; set; } = null!;
    }
}
