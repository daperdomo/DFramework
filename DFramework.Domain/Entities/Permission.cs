using System;
using System.Collections.Generic;

namespace DFramework.Domain.Entities
{
    public partial class Permission
    {
        public Permission()
        {
            RolePermissions = new HashSet<RolePermission>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Path { get; set; } = null!;
        public int? ModuleId { get; set; }
        public short? OrderIndex { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Module? Module { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
