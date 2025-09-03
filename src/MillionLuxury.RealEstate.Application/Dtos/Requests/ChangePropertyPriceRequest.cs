namespace MillionLuxury.RealEstate.Application.Dtos.Requests;

public record ChangePropertyPriceRequest(
    int PropertyId,
    decimal NewPrice
);
