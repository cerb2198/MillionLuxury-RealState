using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Application.Dtos.Responses;

namespace MillionLuxury.RealEstate.Application.Interfaces.UseCases;

public interface IAddPropertyImageUseCase
{
    Task<AddPropertyImageResponse> ExecuteAsync(AddPropertyImageRequest request, CancellationToken cancellationToken = default);
}
