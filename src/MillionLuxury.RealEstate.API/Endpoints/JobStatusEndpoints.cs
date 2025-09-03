using Hangfire;
using MillionLuxury.RealEstate.API.Constants;
using MillionLuxury.RealEstate.Application.Interfaces.Jobs;

namespace MillionLuxury.RealEstate.API.Endpoints;

public static class JobStatusEndpoints
{
    public static void MapJobEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(ApiRoutes.JobStatusResource)
            .WithTags("Jobs")
            .RequireAuthorization();

        group.MapGet("{jobId}", GetJobStatusAsync)
            .WithName("GetJobStatus")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }

    public static async Task<IResult> GetJobStatusAsync(
        string jobId,
        IJobStatusService jobStatusService)
    {
        var jobStatus = await jobStatusService.GetJobStatusAsync(jobId);

        return Results.Ok(jobStatus);
    }
}
