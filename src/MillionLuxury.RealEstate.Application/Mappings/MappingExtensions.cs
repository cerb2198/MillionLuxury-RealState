using AutoMapper;
using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Application.Dtos.Responses;
using MillionLuxury.RealEstate.Domain.Entities;

namespace MillionLuxury.RealEstate.Application.Mappings;

public static class MappingExtensions
{
    public static Property ToProperty(this CreatePropertyBuildingRequest request, IMapper mapper)
    {
        return mapper.Map<Property>(request);
    }

    public static CreatePropertyBuildingResponse ToCreatePropertyBuildingResponse(this Property property, IMapper mapper)
    {
        return mapper.Map<CreatePropertyBuildingResponse>(property);
    }

    public static AddPropertyImageResponse ToAddPropertyImageResponse(this PropertyImage propertyImage, IMapper mapper)
    {
        return mapper.Map<AddPropertyImageResponse>(propertyImage);
    }

    public static ChangePropertyPriceResponse ToChangePropertyPriceResponse(this Property property, IMapper mapper)
    {
        return mapper.Map<ChangePropertyPriceResponse>(property);
    }

    public static UpdatePropertyResponse ToUpdatePropertyResponse(this Property property, IMapper mapper)
    {
        return mapper.Map<UpdatePropertyResponse>(property);
    }

    public static PropertyListItemResponse ToPropertyListItemResponse(this Property property, IMapper mapper)
    {
        return mapper.Map<PropertyListItemResponse>(property);
    }
}
