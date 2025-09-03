using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Application.Dtos.Responses;

namespace MillionLuxury.RealEstate.Application.Interfaces.UseCases;

public interface IUpdatePropertyUseCase
{
    Task<UpdatePropertyResponse> ExecuteAsync(UpdatePropertyRequest request, CancellationToken cancellationToken = default);
}
