using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Application.Dtos.Responses;

namespace MillionLuxury.RealEstate.Application.Interfaces.UseCases;

public interface IListPropertiesUseCase
{
    Task<ListPropertiesResponse> ExecuteAsync(ListPropertiesRequest request, CancellationToken cancellationToken = default);
}
