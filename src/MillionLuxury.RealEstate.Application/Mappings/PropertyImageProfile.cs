using AutoMapper;
using MillionLuxury.RealEstate.Application.Dtos.Responses;
using MillionLuxury.RealEstate.Domain.Entities;

namespace MillionLuxury.RealEstate.Application.Mappings;

public class PropertyImageProfile : Profile
{
    public PropertyImageProfile()
    {
        CreateMap<PropertyImage, AddPropertyImageResponse>();
    }
}
