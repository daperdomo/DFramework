using System;
using System.Collections.Generic;

namespace DFramework.Domain.Entities
{
    public partial class Access
    {
        public Access()
        {
            RolePermissionAccesses = new HashSet<RolePermissionAccess>();
        }

        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string? Name { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<RolePermissionAccess> RolePermissionAccesses { get; set; }
    }
}
