using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MillionLuxury.RealEstate.Infrastructure.Configurations;
using MillionLuxury.RealEstate.Infrastructure.Healthchecks.Interfaces;
using MillionLuxury.RealEstate.Infrastructure.Healthchecks.Services;

namespace MillionLuxury.RealEstate.Infrastructure.Healthchecks.Extensions;

public static class HealthCheckDependencyInjection
{
    public static void AddHealthCheckServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>()
            ?? throw new InvalidOperationException("ConnectionStrings section is missing or invalid.");

        var keycloakConfig = configuration.GetSection(nameof(KeycloakConfiguration)).Get<KeycloakConfiguration>()
            ?? throw new InvalidOperationException("Keycloak configuration is missing");

        services.AddScoped<IHealthCheckResponseService, HealthCheckResponseService>();

        services.AddHealthChecks()
            .AddSqlServer(
                connectionString: connectionStrings.RealEstateDb,
                name: "database",
                failureStatus: HealthStatus.Unhealthy,
                tags: ["database", "sql", "ready"])

            .AddUrlGroup(
                uri: new Uri($"{keycloakConfig.Authority.TrimEnd('/')}/health/ready"),
                name: "keycloak",
                failureStatus: HealthStatus.Degraded,
                tags: ["keycloak", "identity", "ready"],
                timeout: TimeSpan.FromSeconds(10));
    }
}
