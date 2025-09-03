using FluentValidation;
using Microsoft.Extensions.Logging;
using MillionLuxury.RealEstate.Application.Dtos.Internals;
using MillionLuxury.RealEstate.Application.Interfaces.Jobs;
using MillionLuxury.RealEstate.Application.Interfaces.Repositories;
using MillionLuxury.RealEstate.Application.Interfaces.UseCases;
using MillionLuxury.RealEstate.Domain.Entities;
using MillionLuxury.RealEstate.Domain.Exceptions;

namespace MillionLuxury.RealEstate.Application.UseCases;

public class AddMultiplePropertyImagesUseCase(
    IPropertyRepository propertyRepository,
    IPropertyImageRepository propertyImageRepository,
    IImageCompressionService imageCompressionService,
    IValidator<AddMultiplePropertyImagesJobInternalDto> validator,
    ILogger<AddMultiplePropertyImagesUseCase> logger
) : IAddMultiplePropertyImagesUseCase
{
    private const bool DefaultEnabled = true;

    public async Task ExecuteAsync(
        AddMultiplePropertyImagesJobInternalDto request,
        CancellationToken cancellationToken = default
    )
    {
        logger.LogInformation("Starting multiple images upload for property: {PropertyId}, Images count: {Count}",
            request.PropertyId, request.Images.Count);

        await validator.ValidateAndThrowAsync(request, cancellationToken);
        await ValidatePropertyExistsAsync(request.PropertyId);

        var propertyImages = new List<PropertyImage>();

        foreach (var image in request.Images)
        {
            if (!imageCompressionService.IsSupportedImageFormat(image.ContentType))
            {
                logger.LogWarning("Skipping unsupported image format: {ContentType}", image.ContentType);
                continue;
            }

            logger.LogInformation("Processing image: {FileName} of type: {ContentType}", image.FileName, image.ContentType);

            byte[] compressedImageData;
            using (var memoryStream = new MemoryStream(image.Content))
            {
                compressedImageData = await imageCompressionService.CompressImageAsync(memoryStream, image.ContentType);
            }

            var propertyImage = new PropertyImage {
                File = compressedImageData,
                FileType = image.ContentType,
                Enabled = DefaultEnabled,
                PropertyId = request.PropertyId,
                Property = null!
            };

            propertyImages.Add(propertyImage);
        }

        if (propertyImages.Count > 0)
        {
            logger.LogInformation("Persisting {Count} images to database", propertyImages.Count);
            await propertyImageRepository.AddRangeAsync(propertyImages);
            await propertyImageRepository.SaveChangesAsync();
            logger.LogInformation("Successfully processed {Count} images for property: {PropertyId}",
                propertyImages.Count, request.PropertyId);
        }
        else
        {
            logger.LogWarning("No valid images to process for property: {PropertyId}", request.PropertyId);
        }
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
