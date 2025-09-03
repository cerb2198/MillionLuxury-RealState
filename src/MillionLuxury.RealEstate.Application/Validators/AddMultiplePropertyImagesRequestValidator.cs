using FluentValidation;
using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Application.Validators.Common;

namespace MillionLuxury.RealEstate.Application.Validators;

public class AddMultiplePropertyImagesRequestValidator : AbstractValidator<AddMultiplePropertyImagesRequest>
{

    public AddMultiplePropertyImagesRequestValidator()
    {
        RuleFor(x => x.PropertyId)
            .GreaterThan(CommonRestrictions.InvalidId)
            .WithMessage("PropertyId must be greater than 0");

        RuleFor(x => x.Images)
            .NotNull()
            .WithMessage("Images are required")
            .Must(x => x.Count >= CommonRestrictions.MinFilesCount)
            .WithMessage("At least one image is required")
            .Must(x => x.Count <= CommonRestrictions.MaxFilesCount)
            .WithMessage($"Cannot upload more than {CommonRestrictions.MaxFilesCount} images at once")
            .Must(BeValidFilesSizes)
            .WithMessage($"All files must be less than {CommonRestrictions.MaxFileSize / (1024 * 1024)}MB");
    }

    private static bool BeValidFilesSizes(Microsoft.AspNetCore.Http.IFormFileCollection files)
    {
        return files.All(file => file.Length <= CommonRestrictions.MaxFileSize);
    }
}
