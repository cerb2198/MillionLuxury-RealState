using MillionLuxury.RealEstate.API.Constants;
using MillionLuxury.RealEstate.API.Endpoints;
namespace MillionLuxury.RealEstate.API.Extensions;

public static class EndpointsDependencyInjection
{
    public static void AddEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder.MapGroup(ApiRoutes.Base)
            .WithTags("Real Estate API");

        PropertyEndpoints.MapEndpoint(group);
        JobStatusEndpoints.MapJobEndpoints(group);
        HealthCheckEndpoints.MapHealthCheckEndpoints(endpointRouteBuilder);
    }
}
