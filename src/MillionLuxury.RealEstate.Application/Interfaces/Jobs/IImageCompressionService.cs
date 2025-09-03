namespace MillionLuxury.RealEstate.Application.Interfaces.Jobs;
public interface IImageCompressionService
{
    Task<byte[]> CompressImageAsync(Stream imageStream, string contentType);
    bool IsSupportedImageFormat(string contentType);
}
