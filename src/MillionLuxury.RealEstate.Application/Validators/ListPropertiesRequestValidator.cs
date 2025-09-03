using FluentValidation;
using MillionLuxury.RealEstate.Application.Consts;
using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Application.Validators.Common;

namespace MillionLuxury.RealEstate.Application.Validators;

public class ListPropertiesRequestValidator : AbstractValidator<ListPropertiesRequest>
{
    private const int MaxPageSize = 100;
    private const int MinPageSize = 1;
    private const int MinPageNumber = 1;
    private const int MaxCountryLength = 100;
    private const int MaxCityLength = 100;
    private const int MaxNameLength = 200;
    private const int MinYear = 1800;

    public ListPropertiesRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(MinPageNumber)
            .WithMessage($"PageNumber must be greater than or equal to {MinPageNumber}");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(MinPageSize)
            .WithMessage($"PageSize must be greater than or equal to {MinPageSize}")
            .LessThanOrEqualTo(MaxPageSize)
            .WithMessage($"PageSize cannot exceed {MaxPageSize}");

        When(x => x.MinPrice.HasValue, () =>
        {
            RuleFor(x => x.MinPrice)
                .GreaterThan(CommonRestrictions.InvalidPrice)
                .WithMessage($"MinPrice must be greater than {CommonRestrictions.InvalidPrice}");
        });

        When(x => x.MaxPrice.HasValue, () =>
        {
            RuleFor(x => x.MaxPrice)
                .GreaterThan(CommonRestrictions.InvalidPrice)
                .WithMessage($"MaxPrice must be greater than {CommonRestrictions.InvalidPrice}");
        });

        When(x => x.MinPrice.HasValue && x.MaxPrice.HasValue, () =>
        {
            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(x => x.MinPrice)
                .WithMessage("MaxPrice must be greater than or equal to MinPrice");
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

        When(x => x.MinYear.HasValue, () =>
        {
            RuleFor(x => x.MinYear)
                .GreaterThan(MinYear)
                .WithMessage($"MinYear must be greater than {MinYear}");
        });

        When(x => x.MaxYear.HasValue, () =>
        {
            RuleFor(x => x.MaxYear)
                .GreaterThan(MinYear)
                .LessThanOrEqualTo(DateTime.Now.Year + 10)
                .WithMessage($"MaxYear must be between {MinYear} and {DateTime.Now.Year + 10}");
        });

        When(x => x.MinYear.HasValue && x.MaxYear.HasValue, () =>
        {
            RuleFor(x => x.MaxYear)
                .GreaterThanOrEqualTo(x => x.MinYear)
                .WithMessage("MaxYear must be greater than or equal to MinYear");
        });

        When(x => x.OwnerId.HasValue, () =>
        {
            RuleFor(x => x.OwnerId)
                .GreaterThan(CommonRestrictions.InvalidId)
                .WithMessage($"OwnerId must be greater than {CommonRestrictions.InvalidId}");
        });

        When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
        {
            RuleFor(x => x.Name)
                .MaximumLength(MaxNameLength)
                .WithMessage($"Name cannot exceed {MaxNameLength} characters");
        });

        When(x => !string.IsNullOrWhiteSpace(x.SortBy), () =>
        {
            RuleFor(x => x.SortBy)
                .Must(sort => SortingOptions.All.Contains(sort.ToLower()))
                .WithMessage($"SortBy must be one of: {string.Join(", ", SortingOptions.All)}");
        });
    }
}
