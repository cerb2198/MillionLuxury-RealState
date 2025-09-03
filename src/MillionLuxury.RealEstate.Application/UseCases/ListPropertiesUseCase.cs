using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Application.Dtos.Responses;
using MillionLuxury.RealEstate.Application.Interfaces.Repositories;
using MillionLuxury.RealEstate.Application.Interfaces.UseCases;
using MillionLuxury.RealEstate.Application.Mappings;

namespace MillionLuxury.RealEstate.Application.UseCases;

public class ListPropertiesUseCase(
    IPropertyRepository _propertyRepository,
    IValidator<ListPropertiesRequest> _validator,
    IMapper _mapper,
    ILogger<ListPropertiesUseCase> _logger
) : IListPropertiesUseCase
{
    public async Task<ListPropertiesResponse> ExecuteAsync(ListPropertiesRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting properties listing with filters. Page: {PageNumber}, Size: {PageSize}", 
            request.PageNumber, request.PageSize);

        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        _logger.LogInformation("Applying filters and retrieving properties from database");
        var (properties, totalCount) = await _propertyRepository.GetFilteredAsync(request);

        _logger.LogInformation("Retrieved {Count} properties out of {Total} total", 
            properties.Count(), totalCount);

        var propertyListItems = properties.Select(p => p.ToPropertyListItemResponse(_mapper));
        
        var response = new ListPropertiesResponse(propertyListItems, request.PageNumber, request.PageSize, totalCount);
        
        _logger.LogInformation("Properties listing completed successfully");
        return response;
    }
}
