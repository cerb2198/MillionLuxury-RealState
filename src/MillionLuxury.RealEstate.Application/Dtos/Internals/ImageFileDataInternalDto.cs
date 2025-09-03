namespace MillionLuxury.RealEstate.Application.Dtos.Internals;
public record ImageFileDataInternalDto(
    byte[] Content,
    string ContentType,
    string FileName
);
