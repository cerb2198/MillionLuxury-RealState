using Microsoft.Extensions.DependencyInjection;
using MillionLuxury.RealEstate.Application.Interfaces.UseCases;
using MillionLuxury.RealEstate.Application.UseCases;

namespace MillionLuxury.RealEstate.Application.Extensions;
public static class UseCaseDependencyInjection
{
    public static void AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreatePropertyBuildingUseCase, CreatePropertyBuildingUseCase>();
        services.AddScoped<IAddPropertyImageUseCase, AddPropertyImageUseCase>();
        services.AddScoped<IAddMultiplePropertyImagesUseCase, AddMultiplePropertyImagesUseCase>();
        services.AddScoped<IEnqueueMultiplePropertyImagesUseCase, EnqueueMultiplePropertyImagesUseCase>();
        services.AddScoped<IChangePropertyPriceUseCase, ChangePropertyPriceUseCase>();
    }
}
