using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MillionLuxury.RealEstate.Application.Dtos.Internals;
using MillionLuxury.RealEstate.Application.Interfaces.UseCases;

namespace MillionLuxury.RealEstate.Infrastructure.Jobs.Services;

public class PropertyImageUploadJob(
    IServiceProvider serviceProvider,
    ILogger<PropertyImageUploadJob> logger
)
{
    [Queue("image-uploads")]
    [AutomaticRetry(Attempts = 3, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
    public async Task ProcessImagesAsync(AddMultiplePropertyImagesJobInternalDto request, IJobCancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Starting Hangfire job for property: {PropertyId}, Images count: {Count}", 
                request.PropertyId, request.Images.Count);

            using var scope = serviceProvider.CreateScope();
            var useCase = scope.ServiceProvider.GetRequiredService<IAddMultiplePropertyImagesUseCase>();

            await useCase.ExecuteAsync(request, cancellationToken.ShutdownToken);

            logger.LogInformation("Hangfire job completed successfully for property: {PropertyId}", request.PropertyId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing Hangfire job for property: {PropertyId}", request.PropertyId);
            throw;
        }
    }
}
