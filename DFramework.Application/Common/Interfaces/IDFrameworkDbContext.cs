using DFramework.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DFramework.Application.Common.Interfaces
{
    public interface IDFrameworkDbContext : IDbContext
    {
        DbSet<Access> Accesses { get; set; }
        DbSet<Language> Languages { get; set; }
        DbSet<LanguageResource> LanguageResources { get; set; }
        DbSet<Module> Modules { get; set; }
        DbSet<Permission> Permissions { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<RolePermission> RolePermissions { get; set; }
        DbSet<RolePermissionAccess> RolePermissionAccesses { get; set; }
        DbSet<Setting> Settings { get; set; }
        DbSet<User> Users { get; set; }

    }
}
