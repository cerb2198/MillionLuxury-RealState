using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MillionLuxury.RealEstate.Application.Dtos.Internals;
using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Application.Dtos.Responses;
using MillionLuxury.RealEstate.Application.Interfaces.Jobs;
using MillionLuxury.RealEstate.Application.Interfaces.Repositories;
using MillionLuxury.RealEstate.Application.Interfaces.UseCases;
using MillionLuxury.RealEstate.Domain.Exceptions;

namespace MillionLuxury.RealEstate.Application.UseCases;

public class EnqueueMultiplePropertyImagesUseCase(
    IPropertyRepository propertyRepository,
    IBackgroundJobService backgroundJobService,
    IValidator<AddMultiplePropertyImagesRequest> validator,
    ILogger<EnqueueMultiplePropertyImagesUseCase> logger
) : IEnqueueMultiplePropertyImagesUseCase
{
    private const string JobStatusAccepted = "Accepted";
    private const string JobStatusMessageTemplate =
        "Images upload job has been accepted and will be processed in the background. Job ID: {0}";

    public async Task<AddMultiplePropertyImagesResponse> ExecuteAsync(
        AddMultiplePropertyImagesRequest request,
        CancellationToken cancellationToken = default
    )
    {
        LogJobInitiation(request);

        await ValidateRequestAsync(request, cancellationToken);
        await ValidatePropertyExistsAsync(request.PropertyId);

        var imageFiles = await ProcessImageFilesAsync(
            request.Images,
            cancellationToken);

        var jobRequest = CreateJobRequest(request.PropertyId, imageFiles);
        var jobId = await EnqueueJobAsync(jobRequest);

        LogJobSuccess(jobId, request.PropertyId);

        return CreateResponse(jobId);
    }

    private async Task ValidateRequestAsync(AddMultiplePropertyImagesRequest request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
    }

    private async Task ValidatePropertyExistsAsync(int propertyId)
    {
        logger.LogInformation("Validating existence of property with ID: {PropertyId}", propertyId);

        var property = await propertyRepository.GetByIdAsync(propertyId);

        if (property is null)
        {
            logger.LogWarning("Property with ID: {PropertyId} not found", propertyId);
            throw new NotFoundException("Property", propertyId.ToString());
        }
    }

    private async Task<List<ImageFileDataInternalDto>> ProcessImageFilesAsync(
        IFormFileCollection images,
        CancellationToken cancellationToken)
    {
        var imageFiles = new List<ImageFileDataInternalDto>();

        foreach (var image in images)
        {
            logger.LogDebug("Processing image: {FileName}", image.FileName);

            var imageFileData = await ConvertToImageFileDataAsync(image, cancellationToken);
            imageFiles.Add(imageFileData);
        }

        return imageFiles;
    }

    private static async Task<ImageFileDataInternalDto> ConvertToImageFileDataAsync(
        Microsoft.AspNetCore.Http.IFormFile image,
        CancellationToken cancellationToken)
    {
        await using var stream = image.OpenReadStream();
        using var memoryStream = new MemoryStream();

        await stream.CopyToAsync(memoryStream, cancellationToken);

        return new ImageFileDataInternalDto(
            memoryStream.ToArray(),
            image.ContentType,
            image.FileName
        );
    }

    private static AddMultiplePropertyImagesJobInternalDto CreateJobRequest(
        int propertyId,
        List<ImageFileDataInternalDto> imageFiles)
    {
        return new AddMultiplePropertyImagesJobInternalDto(propertyId, imageFiles);
    }

    private async Task<string> EnqueueJobAsync(AddMultiplePropertyImagesJobInternalDto request)
    {
        return await backgroundJobService.EnqueueImageProcessingJobAsync(request);
    }

    private static AddMultiplePropertyImagesResponse CreateResponse(string jobId)
    {
        return new AddMultiplePropertyImagesResponse(
            jobId,
            JobStatusAccepted,
            string.Format(JobStatusMessageTemplate, jobId)
        );
    }

    private void LogJobInitiation(AddMultiplePropertyImagesRequest request)
    {
        logger.LogInformation(
            "Enqueuing multiple images upload for property: {PropertyId}, Images count: {Count}",
            request.PropertyId,
            request.Images.Count);
    }

    private void LogJobSuccess(string jobId, int propertyId)
    {
        logger.LogInformation(
            "Successfully enqueued job: {JobId} for property: {PropertyId}",
            jobId,
            propertyId);
    }
}
