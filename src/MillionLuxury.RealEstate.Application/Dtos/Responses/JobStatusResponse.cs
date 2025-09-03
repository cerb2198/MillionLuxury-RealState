namespace MillionLuxury.RealEstate.Application.Dtos.Responses;

public record JobStatusResponse(
    string JobId,
    string Status,
    DateTime? CreatedAt,
    IEnumerable<JobInvocationResponse> Invocations
);
