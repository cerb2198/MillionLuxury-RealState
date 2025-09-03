using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MillionLuxury.RealEstate.Infrastructure.Configurations;
using MillionLuxury.RealEstate.Infrastructure.Persistence.Databases;
using MillionLuxury.RealEstate.Infrastructure.Persistence.Interceptors;

namespace MillionLuxury.RealEstate.Infrastructure.Persistence.Extensions;
public static class DatabasesDependencyInjection
{
    public static void AddDatabases(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>()
            ?? throw new InvalidOperationException("ConnectionStrings section is missing or invalid.");

        services.AddScoped<AuditableEntityInterceptor>();

        services.AddDbContext<RealEstateDbContext>((serviceProvider, options) => {
            options.UseSqlServer(connectionStrings.RealEstateDb);

            var auditableInterceptor = serviceProvider.GetRequiredService<AuditableEntityInterceptor>()
                ?? throw new InvalidOperationException($"{nameof(AuditableEntityInterceptor)} is not registered.");

            options.AddInterceptors(auditableInterceptor);
        });
    }
}
