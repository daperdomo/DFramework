using System;
using System.Collections.Generic;

namespace DFramework.Domain.Entities
{
    public partial class Language
    {
        public Language()
        {
            LanguageResources = new HashSet<LanguageResource>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Map { get; set; } = null!;
        public bool IsDefault { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<LanguageResource> LanguageResources { get; set; }
    }
}
