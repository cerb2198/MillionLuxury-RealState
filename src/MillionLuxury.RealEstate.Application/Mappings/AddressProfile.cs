using AutoMapper;
using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Domain.ValueObjects;

namespace MillionLuxury.RealEstate.Application.Mappings;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<CreatePropertyBuildingRequest, Address>()
            .ConstructUsing(src => new Address(src.Country, src.City, src.Street, src.ZipCode));
    }
}
