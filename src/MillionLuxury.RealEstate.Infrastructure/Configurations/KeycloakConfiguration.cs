namespace MillionLuxury.RealEstate.Infrastructure.Configurations;

public record KeycloakConfiguration
{
    public required string Authority { get; init; }
    public required string Audience { get; init; }
    public bool RequireHttpsMetadata { get; init; } = false;
    public bool ValidateIssuer { get; init; } = true;
    public bool ValidateAudience { get; init; } = true;
    public bool ValidateLifetime { get; init; } = true;
    public bool ValidateIssuerSigningKey { get; init; } = true;
    public TimeSpan ClockSkew { get; init; } = TimeSpan.FromMinutes(2);
}
