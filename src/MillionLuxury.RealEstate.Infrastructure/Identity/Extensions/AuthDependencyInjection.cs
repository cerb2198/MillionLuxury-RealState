using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MillionLuxury.RealEstate.Infrastructure.Configurations;

namespace MillionLuxury.RealEstate.Infrastructure.Identity.Extensions;

public static class AuthDependencyInjection
{
    public static void AddAuthServices(this IServiceCollection services, IConfiguration configuration)
    {
        var keycloakConfig = configuration
            .GetSection(nameof(KeycloakConfiguration))
            .Get<KeycloakConfiguration>()
            ?? throw new InvalidOperationException("Keycloak configuration is missing");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => {
            options.Authority = keycloakConfig.Authority;
            options.RequireHttpsMetadata = keycloakConfig.RequireHttpsMetadata;
            options.TokenValidationParameters = new TokenValidationParameters {
                ValidateIssuer = keycloakConfig.ValidateIssuer,
                ValidIssuer = keycloakConfig.Authority,
                ValidateAudience = keycloakConfig.ValidateAudience,
                ValidAudience = keycloakConfig.Audience,
                ValidateIssuerSigningKey = keycloakConfig.ValidateIssuerSigningKey,
                ValidateLifetime = keycloakConfig.ValidateLifetime,
                ClockSkew = keycloakConfig.ClockSkew
            };
        });

        services.AddAuthorization();
    }
}
