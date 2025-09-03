using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Application.Dtos.Responses;
using MillionLuxury.RealEstate.Application.Interfaces.Repositories;
using MillionLuxury.RealEstate.Application.Interfaces.UseCases;
using MillionLuxury.RealEstate.Application.Mappings;
using MillionLuxury.RealEstate.Domain.Exceptions;

namespace MillionLuxury.RealEstate.Application.UseCases;

public class ChangePropertyPriceUseCase(
    IPropertyRepository _propertyRepository,
    IValidator<ChangePropertyPriceRequest> _validator,
    IMapper _mapper,
    ILogger<ChangePropertyPriceUseCase> _logger
) : IChangePropertyPriceUseCase
{
    public async Task<ChangePropertyPriceResponse> ExecuteAsync(ChangePropertyPriceRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting price change for property ID: {PropertyId} to new price: {NewPrice}", 
            request.PropertyId, request.NewPrice);
        
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var property = await ValidateAndGetPropertyAsync(request.PropertyId);

        _logger.LogInformation("Updating property price from {OldPrice} to {NewPrice}", property.Price, request.NewPrice);
        property.Price = request.NewPrice;

        _logger.LogInformation("Persisting property price change to the database");
        var updatedProperty = await _propertyRepository.UpdateAsync(property);
        await _propertyRepository.SaveChangesAsync();

        _logger.LogInformation("Property price changed successfully for ID: {PropertyId}", updatedProperty.Id);
        return updatedProperty.ToChangePropertyPriceResponse(_mapper);
    }

    private async Task<Domain.Entities.Property> ValidateAndGetPropertyAsync(int propertyId)
    {
        _logger.LogInformation("Validating existence of property with ID: {PropertyId}", propertyId);
        var property = await _propertyRepository.GetByIdAsync(propertyId);
        if (property == null)
        {
            _logger.LogWarning("Property with ID: {PropertyId} not found", propertyId);
            throw new NotFoundException("Property", propertyId.ToString());
        }
        return property;
    }
}
