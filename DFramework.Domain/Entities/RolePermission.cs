using System;
using System.Collections.Generic;

namespace DFramework.Domain.Entities
{
    public partial class RolePermission
    {
        public RolePermission()
        {
            RolePermissionAccesses = new HashSet<RolePermissionAccess>();
        }

        public int Id { get; set; }
        public int? RolId { get; set; }
        public int? PermissionId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Permission? Permission { get; set; }
        public virtual Role? Rol { get; set; }
        public virtual ICollection<RolePermissionAccess> RolePermissionAccesses { get; set; }
    }
}
