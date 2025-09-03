using MillionLuxury.RealEstate.Application.Dtos.Internals;

namespace MillionLuxury.RealEstate.Application.Interfaces.UseCases;

public interface IAddMultiplePropertyImagesUseCase
{
    Task ExecuteAsync(AddMultiplePropertyImagesJobInternalDto request, CancellationToken cancellationToken = default);
}
