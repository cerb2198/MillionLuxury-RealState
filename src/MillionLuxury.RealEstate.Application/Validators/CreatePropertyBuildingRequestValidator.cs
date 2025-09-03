using FluentValidation;
using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Application.Validators.Common;

namespace MillionLuxury.RealEstate.Application.Validators;

public class CreatePropertyBuildingRequestValidator : AbstractValidator<CreatePropertyBuildingRequest>
{
    private const int MinNameLength = 3;
    private const int MaxNameLength = 200;
    private const int MaxCountryLength = 100;
    private const int MaxCityLength = 100;
    private const int MaxStreetLength = 200;
    private const int InvalidPrice = 0;
    public CreatePropertyBuildingRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Property name is required.")
            .MinimumLength(MinNameLength)
            .WithMessage($"Property name must be at least {MinNameLength} characters long.")
            .MaximumLength(MaxNameLength)
            .WithMessage($"Property name cannot exceed {MaxNameLength} characters.");

        RuleFor(x => x.Country)
            .NotEmpty()
            .WithMessage("Country is required.")
            .MaximumLength(MaxCountryLength)
            .WithMessage($"Country cannot exceed {MaxCountryLength} characters.");

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("City is required.")
            .MaximumLength(MaxCityLength)
            .WithMessage($"City cannot exceed {MaxCityLength} characters.");

        RuleFor(x => x.Street)
            .NotEmpty()
            .WithMessage("Street is required.")
            .MaximumLength(MaxStreetLength)
            .WithMessage($"Street cannot exceed {MaxStreetLength} characters.");

        RuleFor(x => x.ZipCode)
            .GreaterThan(CommonRestrictions.InvalidZipCode)
            .WithMessage($"Zip code must be greater than {CommonRestrictions.InvalidZipCode}.");

        RuleFor(x => x.Price)
            .GreaterThan(InvalidPrice)
            .WithMessage($"Price must be greater than {InvalidPrice}.");

        RuleFor(x => x.OwnerId)
            .GreaterThan(CommonRestrictions.InvalidId)
            .WithMessage($"Owner ID must be greater than {CommonRestrictions.InvalidId}.");
    }
}
