using FluentValidation;
using MillionLuxury.RealEstate.Application.Dtos.Internals;
using MillionLuxury.RealEstate.Application.Validators.Common;

namespace MillionLuxury.RealEstate.Application.Validators;

public class AddMultiplePropertyImagesJobInternalDtoValidator : AbstractValidator<AddMultiplePropertyImagesJobInternalDto>
{
    public AddMultiplePropertyImagesJobInternalDtoValidator()
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
            .WithMessage($"Cannot upload more than {CommonRestrictions.MaxFilesCount} images at once");

        RuleForEach(x => x.Images)
            .SetValidator(new ImageFileDataInternalDtoValidator());
    }
}
