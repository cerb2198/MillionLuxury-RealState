namespace MillionLuxury.RealEstate.Application.Dtos.Requests;

public record UpdatePropertyRequest(
    int PropertyId,
    string? Name,
    string? Country,
    string? City,
    string? Street,
    int? ZipCode,
    decimal? Price,
    int? Year,
    int? OwnerId
);
