using MillionLuxury.RealEstate.Application.Dtos.Internals;

namespace MillionLuxury.RealEstate.Application.Interfaces.Jobs;

public interface IBackgroundJobService
{
    Task<string> EnqueueImageProcessingJobAsync(AddMultiplePropertyImagesJobInternalDto request);
}
