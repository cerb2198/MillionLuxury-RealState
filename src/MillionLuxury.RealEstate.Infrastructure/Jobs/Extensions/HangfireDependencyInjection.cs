using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.DependencyInjection;

namespace MillionLuxury.RealEstate.Infrastructure.Jobs.Extensions;

public static class HangfireDependencyInjection
{
    public static void AddHangfireServices(this IServiceCollection services)
    {
        services.AddHangfire(config =>
        {
            config.UseSimpleAssemblyNameTypeSerializer()
                  .UseRecommendedSerializerSettings()
                  .UseMemoryStorage();
        });

        services.AddHangfireServer(options =>
        {
            options.WorkerCount = Math.Max(Environment.ProcessorCount, 2);
            options.Queues = new[] { "image-uploads", "default" };
        });
    }
}
