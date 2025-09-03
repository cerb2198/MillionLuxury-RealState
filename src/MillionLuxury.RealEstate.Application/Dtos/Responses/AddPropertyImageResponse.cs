namespace MillionLuxury.RealEstate.Application.Dtos.Responses;

public record AddPropertyImageResponse(
    int Id,
    string FileType,
    bool Enabled,
    int PropertyId,
    DateTime CreatedAt
);
