using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Application.Dtos.Responses;

namespace MillionLuxury.RealEstate.Application.Interfaces.UseCases;

public interface ICreatePropertyBuildingUseCase
{
    Task<CreatePropertyBuildingResponse> ExecuteAsync(CreatePropertyBuildingRequest request, CancellationToken cancellationToken = default);
}
