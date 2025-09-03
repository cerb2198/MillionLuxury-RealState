using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Application.Dtos.Responses;
using MillionLuxury.RealEstate.Application.Interfaces.Repositories;
using MillionLuxury.RealEstate.Application.Interfaces.UseCases;
using MillionLuxury.RealEstate.Application.Mappings;
using MillionLuxury.RealEstate.Domain.Entities;
using MillionLuxury.RealEstate.Domain.Exceptions;
using MillionLuxury.RealEstate.Domain.ValueObjects;

namespace MillionLuxury.RealEstate.Application.UseCases;

public class UpdatePropertyUseCase(
    IPropertyRepository _propertyRepository,
    IOwnerRepository _ownerRepository,
    IValidator<UpdatePropertyRequest> _validator,
    IMapper _mapper,
    ILogger<UpdatePropertyUseCase> _logger
) : IUpdatePropertyUseCase
{
    public async Task<UpdatePropertyResponse> ExecuteAsync(UpdatePropertyRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting update for property ID: {PropertyId}", request.PropertyId);
        
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var property = await ValidateAndGetPropertyAsync(request.PropertyId);

        if (request.OwnerId.HasValue && request.OwnerId != property.OwnerId)
        {
            await ValidateOwnerExistsAsync(request.OwnerId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Name) && request.Name != property.Name)
        {
            var ownerId = request.OwnerId ?? property.OwnerId;
            await ValidateUniquePropertyNameForOwnerAsync(request.Name, ownerId, property.Id);
        }

        _logger.LogInformation("Updating property fields");
        
        if (!string.IsNullOrWhiteSpace(request.Name))
            property.Name = request.Name;

        if (request.Price.HasValue)
            property.Price = request.Price.Value;

        if (request.Year.HasValue)
            property.Year = request.Year.Value;

        if (request.OwnerId.HasValue)
            property.OwnerId = request.OwnerId.Value;

        if (HasAddressFields(request))
        {
            property.Address = new Address(
                request.Country ?? property.Address.Country,
                request.City ?? property.Address.City,
                request.Street ?? property.Address.Street,
                request.ZipCode ?? property.Address.ZipCode
            );
        }

        _logger.LogInformation("Persisting property updates to the database");
        var updatedProperty = await _propertyRepository.UpdateAsync(property);
        await _propertyRepository.SaveChangesAsync();

        _logger.LogInformation("Property updated successfully for ID: {PropertyId}", updatedProperty.Id);
        return updatedProperty.ToUpdatePropertyResponse(_mapper);
    }

    private async Task<Property> ValidateAndGetPropertyAsync(int propertyId)
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

    private async Task ValidateOwnerExistsAsync(int ownerId)
    {
        _logger.LogInformation("Validating existence of owner with ID: {OwnerId}", ownerId);
        var owner = await _ownerRepository.GetByIdAsync(ownerId);
        if (owner == null)
        {
            _logger.LogWarning("Owner with ID: {OwnerId} not found", ownerId);
            throw new NotFoundException("Owner", ownerId.ToString());
        }
    }

    private async Task ValidateUniquePropertyNameForOwnerAsync(string name, int ownerId, int propertyId)
    {
        _logger.LogInformation("Validating uniqueness of property name: {PropertyName} for owner ID: {OwnerId}", name, ownerId);
        var exists = await _propertyRepository.ExistsByNameAndOwnerIdAsync(name, ownerId);
        if (exists)
        {
            var existingProperty = await _propertyRepository.GetByIdAsync(propertyId);
            if (existingProperty?.Name != name)
            {
                _logger.LogWarning("Property name: {PropertyName} already exists for owner ID: {OwnerId}", name, ownerId);
                throw new DuplicateResourceException("Property", $"with name '{name}' already exists for owner {ownerId}");
            }
        }
    }

    private static bool HasAddressFields(UpdatePropertyRequest request) =>
        !string.IsNullOrWhiteSpace(request.Country) ||
        !string.IsNullOrWhiteSpace(request.City) ||
        !string.IsNullOrWhiteSpace(request.Street) ||
        request.ZipCode.HasValue;
}
