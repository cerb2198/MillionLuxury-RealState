using Microsoft.AspNetCore.Http;

namespace MillionLuxury.RealEstate.Application.Dtos.Requests;

public record AddMultiplePropertyImagesRequest(
    int PropertyId,
    IFormFileCollection Images
);
