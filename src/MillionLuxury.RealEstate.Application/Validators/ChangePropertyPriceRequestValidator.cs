using FluentValidation;
using MillionLuxury.RealEstate.Application.Dtos.Requests;

namespace MillionLuxury.RealEstate.Application.Validators;

public class ChangePropertyPriceRequestValidator : AbstractValidator<ChangePropertyPriceRequest>
{
    public ChangePropertyPriceRequestValidator()
    {
        RuleFor(x => x.PropertyId)
            .GreaterThan(0)
            .WithMessage("Property ID must be greater than 0.");

        RuleFor(x => x.NewPrice)
            .GreaterThan(0)
            .WithMessage("New price must be greater than 0.")
            .PrecisionScale(18, 2, false)
            .WithMessage("Price must have a maximum of 18 digits with 2 decimal places.");
    }
}
