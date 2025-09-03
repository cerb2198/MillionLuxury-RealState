using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MillionLuxury.RealEstate.Application.Interfaces.Jobs;
using MillionLuxury.RealEstate.Application.Interfaces.Repositories;
using MillionLuxury.RealEstate.Infrastructure.Healthchecks.Extensions;
using MillionLuxury.RealEstate.Infrastructure.Identity.Extensions;
using MillionLuxury.RealEstate.Infrastructure.Jobs.Extensions;
using MillionLuxury.RealEstate.Infrastructure.Jobs.Services;
using MillionLuxury.RealEstate.Infrastructure.Persistence.Extensions;
using MillionLuxury.RealEstate.Infrastructure.Persistence.Repositories;

namespace MillionLuxury.RealEstate.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.AddDatabases(configuration);
        services.AddAuthServices(configuration);
        services.AddIdentityServices();
        services.AddHealthCheckServices(configuration);

        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<IOwnerRepository, OwnerRepository>();
        services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();

        services.AddScoped<IImageCompressionService, ImageCompressionService>();
        services.AddScoped<IJobStatusService, JobStatusService>();

        services.AddHangfireServices();
        services.AddScoped<IBackgroundJobService, HangfireJobService>();
        services.AddScoped<PropertyImageUploadJob>();
    }
}
