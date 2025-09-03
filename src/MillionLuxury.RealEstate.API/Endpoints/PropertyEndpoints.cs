using MillionLuxury.RealEstate.API.Constants;
using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Application.Dtos.Responses;
using MillionLuxury.RealEstate.Application.Interfaces.UseCases;

namespace MillionLuxury.RealEstate.API.Endpoints;

public static class PropertyEndpoints
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(ApiRoutes.PropertyResource)
            .WithTags("Properties")
            .RequireAuthorization();

        group.MapGet("", ListPropertiesAsync)
            .WithName("ListProperties")
            .Produces<ListPropertiesResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem();

        group.MapPost("", CreatePropertyAsync)
            .WithName("CreateProperty")
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("{propertyId:int}/images", AddPropertyImageAsync)
            .WithName("AddPropertyImage")
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound)
            .DisableAntiforgery();

        group.MapPost("{propertyId:int}/images/batch", AddMultiplePropertyImagesAsync)
            .WithName("AddMultiplePropertyImages")
            .Produces(StatusCodes.Status202Accepted)
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound)
            .DisableAntiforgery();

        group.MapPut("{propertyId:int}/price", ChangePropertyPriceAsync)
            .WithName("ChangePropertyPrice")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPatch("{propertyId:int}", UpdatePropertyAsync)
            .WithName("UpdateProperty")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict);
    }

    public static async Task<IResult> ListPropertiesAsync(
        [AsParameters] ListPropertiesRequest request,
        IListPropertiesUseCase useCase,
        CancellationToken cancellationToken)
    {
        var response = await useCase.ExecuteAsync(request, cancellationToken);
        return Results.Ok(response);
    }

    public static async Task<IResult> CreatePropertyAsync(
        CreatePropertyBuildingRequest request,
        ICreatePropertyBuildingUseCase useCase,
        CancellationToken cancellationToken)
    {
        var response = await useCase.ExecuteAsync(request, cancellationToken);
        return Results.Created($"/{ApiRoutes.PropertyResource}{response.Id}", response);
    }

    public static async Task<IResult> AddPropertyImageAsync(
        int propertyId,
        IFormFile image,
        IAddPropertyImageUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = new AddPropertyImageRequest(propertyId, image);
        var response = await useCase.ExecuteAsync(request, cancellationToken);
        return Results.Created($"/{ApiRoutes.PropertyResource}{propertyId}/images/{response.Id}", response);
    }

    public static async Task<IResult> AddMultiplePropertyImagesAsync(
        int propertyId,
        IFormFileCollection images,
        IEnqueueMultiplePropertyImagesUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = new AddMultiplePropertyImagesRequest(propertyId, images);
        var response = await useCase.ExecuteAsync(request, cancellationToken);

        return Results.Accepted($"{ApiRoutes.Base}{ApiRoutes.JobStatusResource}{response.JobId}", response);
    }

    public static async Task<IResult> ChangePropertyPriceAsync(
        int propertyId,
        ChangePropertyPriceRequest request,
        IChangePropertyPriceUseCase useCase,
        CancellationToken cancellationToken)
    {
        var requestWithId = request with { PropertyId = propertyId };
        var response = await useCase.ExecuteAsync(requestWithId, cancellationToken);
        return Results.Ok(response);
    }

    public static async Task<IResult> UpdatePropertyAsync(
        int propertyId,
        UpdatePropertyRequest request,
        IUpdatePropertyUseCase useCase,
        CancellationToken cancellationToken)
    {
        var requestWithId = request with { PropertyId = propertyId };
        var response = await useCase.ExecuteAsync(requestWithId, cancellationToken);
        return Results.Ok(response);
    }
}
