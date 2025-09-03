namespace MillionLuxury.RealEstate.Application.Dtos.Internals;
public record AddMultiplePropertyImagesJobInternalDto(
    int PropertyId,
    IList<ImageFileDataInternalDto> Images
);
