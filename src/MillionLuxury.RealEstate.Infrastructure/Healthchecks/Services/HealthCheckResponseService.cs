using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using MillionLuxury.RealEstate.Infrastructure.Healthchecks.Interfaces;
using System.Text.Json;

namespace MillionLuxury.RealEstate.Infrastructure.Healthchecks.Services;

public sealed class HealthCheckResponseService(
    ILogger<HealthCheckResponseService> logger
) : IHealthCheckResponseService
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task WriteDetailedResponseAsync(HttpContext context, HealthReport result)
    {
        logger.LogDebug("Writing detailed health check response with status: {Status}", result.Status);

        context.Response.ContentType = "application/json; charset=utf-8";
        
        var response = CreateHealthResponse(result);
        var jsonResponse = JsonSerializer.Serialize(response, _jsonOptions);

        await context.Response.WriteAsync(jsonResponse);
    }

    private static object CreateHealthResponse(HealthReport result)
    {
        return new
        {
            status = result.Status.ToString(),
            totalDuration = result.TotalDuration.TotalMilliseconds,
            timestamp = DateTime.UtcNow,
            checks = result.Entries.Select(pair => new
            {
                name = pair.Key,
                status = pair.Value.Status.ToString(),
                duration = pair.Value.Duration.TotalMilliseconds,
                description = pair.Value.Description,
                data = pair.Value.Data,
                exception = pair.Value.Exception?.Message,
                tags = pair.Value.Tags
            }).ToArray()
        };
    }
}
