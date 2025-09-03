namespace MillionLuxury.RealEstate.Application.Dtos.Responses;

public record CreatePropertyBuildingResponse(
    int Id,
    string Name,
    string Country,
    string City,
    string Street,
    int ZipCode,
    decimal Price,
    int CodeInternal,
    int Year
);
