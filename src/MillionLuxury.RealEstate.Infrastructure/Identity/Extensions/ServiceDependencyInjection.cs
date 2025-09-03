using Microsoft.Extensions.DependencyInjection;
using MillionLuxury.RealEstate.Infrastructure.Identity.Interfaces;
using MillionLuxury.RealEstate.Infrastructure.Identity.Services;

namespace MillionLuxury.RealEstate.Infrastructure.Identity.Extensions;
public static class ServiceDependencyInjection
{
    public static void AddIdentityServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
    }
}
