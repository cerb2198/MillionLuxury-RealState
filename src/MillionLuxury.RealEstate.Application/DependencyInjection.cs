using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MillionLuxury.RealEstate.Application.Extensions;
using MillionLuxury.RealEstate.Application.Interfaces.UseCases;
using MillionLuxury.RealEstate.Application.UseCases;
using System.Reflection;

namespace MillionLuxury.RealEstate.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddUseCases();

        services.AddScoped<ICreatePropertyBuildingUseCase, CreatePropertyBuildingUseCase>();
        services.AddScoped<IAddPropertyImageUseCase, AddPropertyImageUseCase>();
        services.AddScoped<IAddMultiplePropertyImagesUseCase, AddMultiplePropertyImagesUseCase>();
        services.AddScoped<IEnqueueMultiplePropertyImagesUseCase, EnqueueMultiplePropertyImagesUseCase>();
        services.AddScoped<IChangePropertyPriceUseCase, ChangePropertyPriceUseCase>();
        services.AddScoped<IUpdatePropertyUseCase, UpdatePropertyUseCase>();
        services.AddScoped<IListPropertiesUseCase, ListPropertiesUseCase>();
    }
}
