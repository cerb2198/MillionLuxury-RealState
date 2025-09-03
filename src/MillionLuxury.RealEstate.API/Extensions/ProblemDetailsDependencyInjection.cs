using Microsoft.AspNetCore.Http.Features;
using MillionLuxury.RealEstate.API.Infrastructure.Handlers;
using System.Diagnostics;

namespace MillionLuxury.RealEstate.API.Extensions;

public static class ProblemDetailsDependencyInjection
{
    public static void AddCustomProblemDetails(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddProblemDetails(options => {
            options.CustomizeProblemDetails = context => {

                context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

                context
                .ProblemDetails
                .Extensions
                .TryAdd("requestId", context.HttpContext.TraceIdentifier);

                Activity? activity = context
                    .HttpContext
                    .Features.Get<IHttpActivityFeature>()?.Activity;

                context
                    .ProblemDetails
                    .Extensions.TryAdd("traceId", activity?.Id);
            };
        });
    }

    public static IApplicationBuilder UseProblemDetailsHandling(this IApplicationBuilder app)
    {
        app.UseExceptionHandler();
        app.UseStatusCodePages();

        return app;
    }
}
