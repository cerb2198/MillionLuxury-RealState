using Hangfire;
using Microsoft.Extensions.Logging;
using MillionLuxury.RealEstate.Application.Dtos.Internals;
using MillionLuxury.RealEstate.Application.Interfaces.Jobs;

namespace MillionLuxury.RealEstate.Infrastructure.Jobs.Services;

public class HangfireJobService(
    IBackgroundJobClient backgroundJobClient,
    ILogger<HangfireJobService> logger
) : IBackgroundJobService
{
    public Task<string> EnqueueImageProcessingJobAsync(AddMultiplePropertyImagesJobInternalDto request)
    {
        logger.LogInformation("Enqueuing Hangfire job for property: {PropertyId}", request.PropertyId);

        var jobId = backgroundJobClient.Enqueue<PropertyImageUploadJob>(
            job => job.ProcessImagesAsync(request, JobCancellationToken.Null)
        );

        logger.LogInformation("Hangfire job enqueued with ID: {JobId} for property: {PropertyId}", 
            jobId, request.PropertyId);

        return Task.FromResult(jobId);
    }
}
