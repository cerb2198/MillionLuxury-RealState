using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Application.Dtos.Responses;
using MillionLuxury.RealEstate.Application.Interfaces.Jobs;
using MillionLuxury.RealEstate.Application.Interfaces.Repositories;
using MillionLuxury.RealEstate.Application.Interfaces.UseCases;
using MillionLuxury.RealEstate.Application.Mappings;
using MillionLuxury.RealEstate.Domain.Entities;
using MillionLuxury.RealEstate.Domain.Exceptions;

namespace MillionLuxury.RealEstate.Application.UseCases;

public class AddPropertyImageUseCase(
    IPropertyRepository propertyRepository,
    IPropertyImageRepository propertyImageRepository,
    IImageCompressionService imageCompressionService,
    IValidator<AddPropertyImageRequest> validator,
    IMapper mapper,
    ILogger<AddPropertyImageUseCase> logger
) : IAddPropertyImageUseCase
{
    private const bool DefaultImageEnabled = true;
    public async Task<AddPropertyImageResponse> ExecuteAsync(AddPropertyImageRequest request, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Starting single image upload for property: {PropertyId}", request.PropertyId);
        
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        await ValidatePropertyExistsAsync(request.PropertyId);

        if (!imageCompressionService.IsSupportedImageFormat(request.Image.ContentType))
        {
            logger.LogWarning("Unsupported image format: {ContentType}", request.Image.ContentType);
            throw new ValidationException($"Unsupported image format: {request.Image.ContentType}");
        }

        logger.LogInformation("Compressing image of type: {ContentType}", request.Image.ContentType);
        byte[] compressedImageData;
        using (var imageStream = request.Image.OpenReadStream())
        {
            compressedImageData = await imageCompressionService.CompressImageAsync(imageStream, request.Image.ContentType);
        }

        var propertyImage = new PropertyImage
        {
            File = compressedImageData,
            FileType = request.Image.ContentType,
            Enabled = DefaultImageEnabled,
            PropertyId = request.PropertyId,
            Property = null!
        };

        logger.LogInformation("Persisting image to database");
        var createdImage = await propertyImageRepository.AddAsync(propertyImage);
        await propertyImageRepository.SaveChangesAsync();

        logger.LogInformation("Image uploaded successfully with ID: {ImageId}", createdImage.Id);
        return createdImage.ToAddPropertyImageResponse(mapper);
    }

    private async Task ValidatePropertyExistsAsync(int propertyId)
    {
        logger.LogInformation("Validating existence of property with ID: {PropertyId}", propertyId);
        var property = await propertyRepository.GetByIdAsync(propertyId);
        if (property == null)
        {
            logger.LogWarning("Property with ID: {PropertyId} not found", propertyId);
            throw new NotFoundException("Property", propertyId.ToString());
        }
    }
}
