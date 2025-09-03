using MillionLuxury.RealEstate.API.Configurations;

namespace MillionLuxury.RealEstate.API.Extensions;

public static class CorsDependencyInjection
{
    public static IServiceCollection AddCorsPolicies(this IServiceCollection services, IConfiguration configuration)
    {
        var corsSettings = configuration.GetSection(nameof(CorsSettings)).Get<CorsSettings>() ?? new CorsSettings();

        services.AddCors(options => {
            options.AddPolicy(Environments.Development,
                policy => {
                    policy
                        .WithOrigins(corsSettings.DevelopmentConfig.Origins)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetPreflightMaxAge(corsSettings.DevelopmentConfig.PreflightMaxAge);
                });
        });
        return services;
    }
}
