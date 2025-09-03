using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MillionLuxury.RealEstate.API.Constants;
using MillionLuxury.RealEstate.Infrastructure.Healthchecks.Interfaces;

namespace MillionLuxury.RealEstate.API.Endpoints;

public static class HealthCheckEndpoints
{
    public static void MapHealthCheckEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(ApiRoutes.HealthCheckResource)
            .WithTags("Healthchecks")
            .AllowAnonymous();

        group.MapHealthChecks("")
            .WithTags("Health")
            .WithName("Health");

        group.MapHealthChecks("/detailed", new HealthCheckOptions {
            ResponseWriter = WriteDetailedResponseAsync
        })
            .WithTags("Health")
            .WithName("DetailedHealth");

        group.MapHealthChecks("/live", new HealthCheckOptions {
            Predicate = _ => false
        })
            .WithTags("Health")
            .WithName("LiveCheck");
    }

    private static async Task WriteDetailedResponseAsync(HttpContext context, HealthReport result)
    {
        var healthService = context.RequestServices.GetRequiredService<IHealthCheckResponseService>();
        await healthService.WriteDetailedResponseAsync(context, result);
    }
}
