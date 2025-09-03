namespace MillionLuxury.RealEstate.Application.Dtos.Requests;

public record CreatePropertyBuildingRequest(
    string Name,
    string Country,
    string City, 
    string Street,
    int ZipCode,
    int CodeInternal,
    decimal Price,
    int Year,
    int OwnerId
);
