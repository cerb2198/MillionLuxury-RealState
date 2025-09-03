using FluentValidation;
using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Application.Validators.Common;

namespace MillionLuxury.RealEstate.Application.Validators;

public class UpdatePropertyRequestValidator : AbstractValidator<UpdatePropertyRequest>
{
    private const int MaxNameLength = 200;
    private const int MaxCountryLength = 100;
    private const int MaxCityLength = 100;
    private const int MaxStreetLength = 200;
    private const int MinYear = 1800;
    private const int MaxYearOffset = 10;

    public UpdatePropertyRequestValidator()
    {
        RuleFor(x => x.PropertyId)
            .GreaterThan(CommonRestrictions.InvalidId)
            .WithMessage($"PropertyId must be greater than {CommonRestrictions.InvalidId}");

        When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
        {
            RuleFor(x => x.Name)
                .MaximumLength(MaxNameLength)
                .WithMessage($"Name cannot exceed {MaxNameLength} characters");
        });

        When(x => !string.IsNullOrWhiteSpace(x.Country), () =>
        {
            RuleFor(x => x.Country)
                .MaximumLength(MaxCountryLength)
                .WithMessage($"Country cannot exceed {MaxCountryLength} characters");
        });

        When(x => !string.IsNullOrWhiteSpace(x.City), () =>
        {
            RuleFor(x => x.City)
                .MaximumLength(MaxCityLength)
                .WithMessage($"City cannot exceed {MaxCityLength} characters");
        });

        When(x => !string.IsNullOrWhiteSpace(x.Street), () =>
        {
            RuleFor(x => x.Street)
                .MaximumLength(MaxStreetLength)
                .WithMessage($"Street cannot exceed {MaxStreetLength} characters");
        });

        When(x => x.Price.HasValue, () =>
        {
            RuleFor(x => x.Price)
                .GreaterThan(CommonRestrictions.InvalidPrice)
                .WithMessage($"Price must be greater than {CommonRestrictions.InvalidPrice}");
        });

        When(x => x.Year.HasValue, () =>
        {
            RuleFor(x => x.Year)
                .GreaterThan(MinYear)
                .LessThanOrEqualTo(DateTime.Now.Year + MaxYearOffset)
                .WithMessage($"Year must be between {MinYear} and {DateTime.Now.Year + MaxYearOffset}");
        });

        When(x => x.OwnerId.HasValue, () =>
        {
            RuleFor(x => x.OwnerId)
                .GreaterThan(CommonRestrictions.InvalidId)
                .WithMessage($"OwnerId must be greater than {CommonRestrictions.InvalidId}");
        });

        When(x => x.ZipCode.HasValue, () =>
        {
            RuleFor(x => x.ZipCode)
                .GreaterThan(CommonRestrictions.InvalidZipCode)
                .WithMessage($"ZipCode must be greater than {CommonRestrictions.InvalidZipCode}");
        });
    }
}
