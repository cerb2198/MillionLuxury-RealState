using FluentValidation;
using MillionLuxury.RealEstate.Application.Dtos.Internals;
using MillionLuxury.RealEstate.Application.Validators.Common;

public class ImageFileDataInternalDtoValidator : AbstractValidator<ImageFileDataInternalDto>
{
    public static readonly string[] AllowedContentTypes = {
        "image/jpeg", "image/jpg", "image/png", "image/gif", "image/bmp", "image/webp"
    };

    public ImageFileDataInternalDtoValidator()
    {
        RuleFor(x => x.Content)
            .NotNull()
            .WithMessage("Image content is required")
            .Must(x => x.Length <= CommonRestrictions.MaxFileSize)
            .WithMessage($"Image file must be less than {CommonRestrictions.MaxFileSize / (1024 * 1024)}MB");

        RuleFor(x => x.ContentType)
            .NotEmpty()
            .WithMessage("Content type is required")
            .Must(x => AllowedContentTypes.Contains(x.ToLower()))
            .WithMessage($"Content type must be one of: {string.Join(", ", AllowedContentTypes)}");

        RuleFor(x => x.FileName)
            .NotEmpty()
            .WithMessage("File name is required");
    }
}
