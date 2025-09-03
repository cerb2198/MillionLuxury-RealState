using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Application.Dtos.Responses;

namespace MillionLuxury.RealEstate.Application.Interfaces.UseCases;

public interface IChangePropertyPriceUseCase
{
    Task<ChangePropertyPriceResponse> ExecuteAsync(ChangePropertyPriceRequest request, CancellationToken cancellationToken = default);
}
