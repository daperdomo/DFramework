using System;
using System.Collections.Generic;

namespace DFramework.Domain.Entities
{
    public partial class Module
    {
        public Module()
        {
            InverseParent = new HashSet<Module>();
            Permissions = new HashSet<Permission>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? IconClass { get; set; }
        public int? ParentId { get; set; }
        public short? OrderIndex { get; set; }
        public bool? IsHeader { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Module? Parent { get; set; }
        public virtual ICollection<Module> InverseParent { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
