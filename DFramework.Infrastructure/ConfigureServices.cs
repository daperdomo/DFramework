using DFramework.Application.Common.Interfaces;
using DFramework.Application.Common.Interfaces.Caching;
using DFramework.Application.Common.Interfaces.Services;
using DFramework.Contracts.Authentication;
using DFramework.Contracts.Settings;
using DFramework.Infrastructure.Caching;
using DFramework.Infrastructure.Persistence;
using DFramework.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DFramework.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<IDFrameworkDbContext, DFrameworkContext>(options => options.UseInMemoryDatabase("DFrameworkDb"));
        }
        else
        {
            services.AddDbContext<IDFrameworkDbContext, DFrameworkContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")!, builder => builder.MigrationsAssembly(typeof(DFrameworkContext).Assembly.FullName)));
        }

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.Configure<AppSettings>(configuration.GetSection(AppSettings.SectionName));
        services.AddScoped<DFrameworkDbContextInitializer>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddMemoryCache();
        services.AddScoped<ICacheManager, MemoryCacheManager>();
        return services;
    }
}