using FluentValidation;
using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Application.Validators.Common;

namespace MillionLuxury.RealEstate.Application.Validators;

public class AddPropertyImageRequestValidator : AbstractValidator<AddPropertyImageRequest>
{
    private const long MaxFileSize = 10 * 1024 * 1024;
    
    public AddPropertyImageRequestValidator()
    {
        RuleFor(x => x.PropertyId)
            .GreaterThan(CommonRestrictions.InvalidId)
            .WithMessage("PropertyId must be greater than 0");

        RuleFor(x => x.Image)
            .NotNull()
            .WithMessage("Image is required")
            .Must(BeValidFileSize)
            .WithMessage($"File size must be less than {MaxFileSize / (1024 * 1024)}MB");
    }

    private static bool BeValidFileSize(Microsoft.AspNetCore.Http.IFormFile file)
    {
        return file?.Length <= MaxFileSize;
    }
}
