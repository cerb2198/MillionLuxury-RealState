using MillionLuxury.RealEstate.API.Extensions;

namespace MillionLuxury.RealEstate.API;

public static class DependencyInjection
{
    public static void AddApi(this IServiceCollection services, IHostApplicationBuilder host)
    {
        services.AddCorsPolicies(host.Configuration);
        services.AddCustomProblemDetails();
        services.AddSwaggerConfiguration(host.Configuration);
    }
}
