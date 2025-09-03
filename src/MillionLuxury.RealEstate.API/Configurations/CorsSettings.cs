namespace MillionLuxury.RealEstate.API.Configurations;

public record CorsSettings
{
    public CorsBaseInformation DevelopmentConfig { get; init; } = new();
}

public record CorsBaseInformation
{
    public string[] Origins { get; init; } = [];
    public TimeSpan PreflightMaxAge { get; init; } = TimeSpan.FromMinutes(10);
}
