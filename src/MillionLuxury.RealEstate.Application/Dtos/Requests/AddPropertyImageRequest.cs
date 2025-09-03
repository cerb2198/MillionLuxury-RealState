using Microsoft.AspNetCore.Http;

namespace MillionLuxury.RealEstate.Application.Dtos.Requests;

public record AddPropertyImageRequest(
    int PropertyId,
    IFormFile Image
);
