using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Application.Dtos.Responses;

namespace MillionLuxury.RealEstate.Application.Interfaces.UseCases;

public interface IEnqueueMultiplePropertyImagesUseCase
{
    Task<AddMultiplePropertyImagesResponse> ExecuteAsync(AddMultiplePropertyImagesRequest request, CancellationToken cancellationToken = default);
}
