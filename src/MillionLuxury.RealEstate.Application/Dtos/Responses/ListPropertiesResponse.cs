using MillionLuxury.RealEstate.Application.Dtos.Common;

namespace MillionLuxury.RealEstate.Application.Dtos.Responses;

public record ListPropertiesResponse : PagedResponse<PropertyListItemResponse>
{
    public ListPropertiesResponse(IEnumerable<PropertyListItemResponse> data, int pageNumber, int pageSize, int totalRecords)
        : base(data, pageNumber, pageSize, totalRecords)
    {
    }
}
