using FluentValidation.TestHelper;
using MillionLuxury.RealEstate.Application.Validators;
using MillionLuxury.RealEstate.UnitTests.Application.Validators.TestData;

namespace MillionLuxury.RealEstate.UnitTests.Application.Validators;

[TestFixture]
public class CreatePropertyBuildingRequestValidatorTests
{
    private CreatePropertyBuildingRequestValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new CreatePropertyBuildingRequestValidator();
    }

    [Test]
    public void Validate_ValidRequest_ShouldNotHaveValidationErrors()
    {
        var request = CreatePropertyBuildingValidatorTestData.GetValidRequest();

        var result = _validator.TestValidate(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [TestCaseSource(typeof(CreatePropertyBuildingValidatorTestData), nameof(CreatePropertyBuildingValidatorTestData.GetInvalidNameTestCases))]
    public void Validate_InvalidName_ShouldHaveValidationError(string name, string expectedError)
    {
        var request = CreatePropertyBuildingValidatorTestData.GetValidRequest() with { Name = name };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorMessage(expectedError);
    }

    [TestCaseSource(typeof(CreatePropertyBuildingValidatorTestData), nameof(CreatePropertyBuildingValidatorTestData.GetInvalidCountryTestCases))]
    public void Validate_InvalidCountry_ShouldHaveValidationError(string country, string expectedError)
    {
        var request = CreatePropertyBuildingValidatorTestData.GetValidRequest() with { Country = country };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.Country)
              .WithErrorMessage(expectedError);
    }

    [TestCaseSource(typeof(CreatePropertyBuildingValidatorTestData), nameof(CreatePropertyBuildingValidatorTestData.GetInvalidCityTestCases))]
    public void Validate_InvalidCity_ShouldHaveValidationError(string city, string expectedError)
    {
        var request = CreatePropertyBuildingValidatorTestData.GetValidRequest() with { City = city };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.City)
              .WithErrorMessage(expectedError);
    }

    [TestCaseSource(typeof(CreatePropertyBuildingValidatorTestData), nameof(CreatePropertyBuildingValidatorTestData.GetInvalidStreetTestCases))]
    public void Validate_InvalidStreet_ShouldHaveValidationError(string street, string expectedError)
    {
        var request = CreatePropertyBuildingValidatorTestData.GetValidRequest() with { Street = street };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.Street)
              .WithErrorMessage(expectedError);
    }

    [TestCaseSource(typeof(CreatePropertyBuildingValidatorTestData), nameof(CreatePropertyBuildingValidatorTestData.GetInvalidZipCodeTestCases))]
    public void Validate_InvalidZipCode_ShouldHaveValidationError(int zipCode, string expectedError)
    {
        var request = CreatePropertyBuildingValidatorTestData.GetValidRequest() with { ZipCode = zipCode };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.ZipCode)
              .WithErrorMessage(expectedError);
    }

    [TestCaseSource(typeof(CreatePropertyBuildingValidatorTestData), nameof(CreatePropertyBuildingValidatorTestData.GetInvalidPriceTestCases))]
    public void Validate_InvalidPrice_ShouldHaveValidationError(decimal price, string expectedError)
    {
        var request = CreatePropertyBuildingValidatorTestData.GetValidRequest() with { Price = price };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.Price)
              .WithErrorMessage(expectedError);
    }

    [TestCaseSource(typeof(CreatePropertyBuildingValidatorTestData), nameof(CreatePropertyBuildingValidatorTestData.GetInvalidOwnerIdTestCases))]
    public void Validate_InvalidOwnerId_ShouldHaveValidationError(int ownerId, string expectedError)
    {
        var request = CreatePropertyBuildingValidatorTestData.GetValidRequest() with { OwnerId = ownerId };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.OwnerId)
              .WithErrorMessage(expectedError);
    }
}
