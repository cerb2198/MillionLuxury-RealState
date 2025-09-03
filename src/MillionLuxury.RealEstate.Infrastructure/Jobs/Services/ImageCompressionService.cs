using Microsoft.Extensions.Logging;
using MillionLuxury.RealEstate.Application.Interfaces.Jobs;
using System.IO.Compression;

namespace MillionLuxury.RealEstate.Infrastructure.Jobs.Services;

public class ImageCompressionService(ILogger<ImageCompressionService> logger)
    : IImageCompressionService
{
    private static readonly HashSet<string> _supportedImageFormats = new(StringComparer.OrdinalIgnoreCase)
    {
        "image/jpeg",
        "image/jpg", 
        "image/png",
        "image/webp",
        "image/bmp",
        "image/tiff"
    };

    public bool IsSupportedImageFormat(string contentType)
    {
        return _supportedImageFormats.Contains(contentType);
    }

    public async Task<byte[]> CompressImageAsync(Stream imageStream, string contentType)
    {
        logger.LogInformation("Starting compression for image with content type: {ContentType}", contentType);

        using var memoryStream = new MemoryStream();
        await imageStream.CopyToAsync(memoryStream);
        var imageData = memoryStream.ToArray();

        using var compressedStream = new MemoryStream();
        using (var brotliStream = new BrotliStream(compressedStream, CompressionLevel.Optimal))
        {
            await brotliStream.WriteAsync(imageData);
        }

        var compressedData = compressedStream.ToArray();
        var compressionRatio = (double)compressedData.Length / imageData.Length * 100;

        logger.LogInformation("Compression completed. Original size: {OriginalSize} bytes, Compressed size: {CompressedSize} bytes, Ratio: {Ratio:F2}%",
            imageData.Length, compressedData.Length, compressionRatio);

        return compressedData;
    }
}
