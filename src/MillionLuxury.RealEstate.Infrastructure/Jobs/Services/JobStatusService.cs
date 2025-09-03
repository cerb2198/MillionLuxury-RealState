using Hangfire;
using Microsoft.Extensions.Logging;
using MillionLuxury.RealEstate.Application.Dtos.Responses;
using MillionLuxury.RealEstate.Application.Interfaces.Jobs;

namespace MillionLuxury.RealEstate.Infrastructure.Jobs.Services;

public class JobStatusService(
    ILogger<JobStatusService> logger
) : IJobStatusService
{
    private const string UnknownStatus = "Unknown";
    public Task<JobStatusResponse?> GetJobStatusAsync(string jobId)
    {
        logger.LogInformation("Retrieving job status for job ID: {JobId}", jobId);

        var monitor = JobStorage.Current.GetMonitoringApi();
        var details = monitor.JobDetails(jobId);
        
        if (details == null)
        {
            logger.LogWarning("Job with ID: {JobId} not found", jobId);
            return Task.FromResult<JobStatusResponse?>(null);
        }

        var currentState = details.History?.FirstOrDefault()?.StateName ?? UnknownStatus;
        
        var invocations = details.History?.Select(h => new JobInvocationResponse(
            h.StateName,
            h.CreatedAt,
            h.Reason
        )) ?? Enumerable.Empty<JobInvocationResponse>();

        var response = new JobStatusResponse(
            jobId,
            currentState,
            details.CreatedAt,
            invocations
        );

        logger.LogDebug("Job status retrieved successfully for job ID: {JobId}, Status: {Status}", jobId, currentState);
        
        return Task.FromResult<JobStatusResponse?>(response);
    }
}
