namespace MillionLuxury.RealEstate.Application.Dtos.Responses;
public record JobInvocationResponse(
    string State,
    DateTime CreatedAt,
    string? Reason
);
