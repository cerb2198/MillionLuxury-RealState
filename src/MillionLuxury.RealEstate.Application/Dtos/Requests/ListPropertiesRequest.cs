using MillionLuxury.RealEstate.Application.Consts;
using MillionLuxury.RealEstate.Application.Dtos.Common;

namespace MillionLuxury.RealEstate.Application.Dtos.Requests;

public record ListPropertiesRequest : PagedRequest
{
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
    public string? Country { get; init; }
    public string? City { get; init; }
    public int? MinYear { get; init; }
    public int? MaxYear { get; init; }
    public int? OwnerId { get; init; }
    public string? Name { get; init; }
    public string? SortBy { get; init; } = SortingOptions.Default;
    public bool SortDescending { get; init; } = true;
}
