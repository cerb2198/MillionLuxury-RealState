using AutoMapper;
using MillionLuxury.RealEstate.Application.Dtos.Responses;
using MillionLuxury.RealEstate.Domain.Entities;

namespace MillionLuxury.RealEstate.Application.Mappings;

public class ResponseProfile : Profile
{
    public ResponseProfile()
    {
        CreateMap<Property, CreatePropertyBuildingResponse>()
            .ConstructUsing(src => new CreatePropertyBuildingResponse(
                src.Id,
                src.Name,
                src.Address.Country,
                src.Address.City,
                src.Address.Street,
                src.Address.ZipCode,
                src.Price,
                src.CodeInternal,
                src.Year
            ));
    }
}
