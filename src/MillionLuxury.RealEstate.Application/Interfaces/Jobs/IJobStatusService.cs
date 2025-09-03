using MillionLuxury.RealEstate.Application.Dtos.Responses;

namespace MillionLuxury.RealEstate.Application.Interfaces.Jobs;

public interface IJobStatusService
{
    Task<JobStatusResponse?> GetJobStatusAsync(string jobId);
}
