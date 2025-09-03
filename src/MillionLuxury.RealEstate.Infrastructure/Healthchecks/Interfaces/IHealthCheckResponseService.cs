using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MillionLuxury.RealEstate.Infrastructure.Healthchecks.Interfaces;

public interface IHealthCheckResponseService
{
    Task WriteDetailedResponseAsync(HttpContext context, HealthReport result);
}
