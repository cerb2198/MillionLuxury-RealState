namespace MillionLuxury.RealEstate.Application.Dtos.Responses;

public record ChangePropertyPriceResponse(
    int Id,
    string Name,
    decimal Price
);
