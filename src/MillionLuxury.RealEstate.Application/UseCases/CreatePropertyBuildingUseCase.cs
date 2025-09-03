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

public class CreatePropertyBuildingUseCase(
    IPropertyRepository _propertyRepository,
    IOwnerRepository _ownerRepository,
    IValidator<CreatePropertyBuildingRequest> _validator,
    IMapper _mapper,
    ILogger<CreatePropertyBuildingUseCase> _logger
) : ICreatePropertyBuildingUseCase
{
    public async Task<CreatePropertyBuildingResponse> ExecuteAsync(CreatePropertyBuildingRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting creation of property with name: {PropertyName}", request.Name);
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        await ValidateOwnerExistsAsync(request.OwnerId);
        await ValidateUniquePropertyNameForOwnerAsync(request.Name, request.OwnerId);

        await ValidateUniqueCodeInternalAsync(request);

        _logger.LogInformation("Mapping request to property entity");
        var property = request.ToProperty(_mapper);

        _logger.LogInformation("Persisting new property to the database");
        var createdProperty = await _propertyRepository.AddAsync(property);

        await _propertyRepository.SaveChangesAsync();

        _logger.LogInformation("Property created successfully with ID: {PropertyId}", createdProperty.Id);
        return createdProperty.ToCreatePropertyBuildingResponse(_mapper);
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

    private async Task ValidateUniquePropertyNameForOwnerAsync(string name, int ownerId)
    {
        _logger.LogInformation("Validating uniqueness of property name: {PropertyName} for owner ID: {OwnerId}", name, ownerId);
        var exists = await _propertyRepository.ExistsByNameAndOwnerIdAsync(name, ownerId);
        if (exists)
        {
            _logger.LogWarning("Property name: {PropertyName} already exists for owner ID: {OwnerId}", name, ownerId);
            throw new DuplicateResourceException("Property", $"with name '{name}' already exists for owner {ownerId}");
        }
    }

    private async Task ValidateUniqueCodeInternalAsync(CreatePropertyBuildingRequest request)
    {
        _logger.LogInformation("Validating uniqueness of property code internal: {CodeInternal}", request.CodeInternal);
        var exists = await _propertyRepository.ExistsByCodeInternalAsync(request.CodeInternal);
        if (exists)
        {
            _logger.LogWarning("Property with code internal: {CodeInternal} already exists", request.CodeInternal);
            throw new DuplicateResourceException("Property", $"with code internal '{request.CodeInternal}' already exists");
        }
    }
}
