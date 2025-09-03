using FluentValidation.TestHelper;
using MillionLuxury.RealEstate.Application.Validators;
using MillionLuxury.RealEstate.UnitTests.Application.Validators.TestData;

namespace MillionLuxury.RealEstate.UnitTests.Application.Validators;

[TestFixture]
public class UpdatePropertyRequestValidatorTests
{
    private UpdatePropertyRequestValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new UpdatePropertyRequestValidator();
    }

    [Test]
    public void Validate_ValidRequest_ShouldNotHaveValidationErrors()
    {
        var request = UpdatePropertyValidatorTestData.GetValidRequest();

        var result = _validator.TestValidate(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public void Validate_AllOptionalFieldsNull_ShouldNotHaveValidationErrors()
    {
        var request = UpdatePropertyValidatorTestData.GetValidRequest() with 
        {
            Name = null,
            Country = null,
            City = null,
            Street = null,
            ZipCode = null,
            Price = null,
            Year = null,
            OwnerId = null
        };

        var result = _validator.TestValidate(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [TestCaseSource(typeof(UpdatePropertyValidatorTestData), nameof(UpdatePropertyValidatorTestData.GetInvalidPropertyIdTestCases))]
    public void Validate_InvalidPropertyId_ShouldHaveValidationError(int propertyId, string expectedError)
    {
        var request = UpdatePropertyValidatorTestData.GetValidRequest() with { PropertyId = propertyId };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.PropertyId)
              .WithErrorMessage(expectedError);
    }

    [TestCaseSource(typeof(UpdatePropertyValidatorTestData), nameof(UpdatePropertyValidatorTestData.GetInvalidNameTestCases))]
    public void Validate_InvalidName_ShouldHaveValidationError(string name, string expectedError)
    {
        var request = UpdatePropertyValidatorTestData.GetValidRequest() with { Name = name };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorMessage(expectedError);
    }

    [TestCaseSource(typeof(UpdatePropertyValidatorTestData), nameof(UpdatePropertyValidatorTestData.GetInvalidCountryTestCases))]
    public void Validate_InvalidCountry_ShouldHaveValidationError(string country, string expectedError)
    {
        var request = UpdatePropertyValidatorTestData.GetValidRequest() with { Country = country };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.Country)
              .WithErrorMessage(expectedError);
    }

    [TestCaseSource(typeof(UpdatePropertyValidatorTestData), nameof(UpdatePropertyValidatorTestData.GetInvalidCityTestCases))]
    public void Validate_InvalidCity_ShouldHaveValidationError(string city, string expectedError)
    {
        var request = UpdatePropertyValidatorTestData.GetValidRequest() with { City = city };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.City)
              .WithErrorMessage(expectedError);
    }

    [TestCaseSource(typeof(UpdatePropertyValidatorTestData), nameof(UpdatePropertyValidatorTestData.GetInvalidStreetTestCases))]
    public void Validate_InvalidStreet_ShouldHaveValidationError(string street, string expectedError)
    {
        var request = UpdatePropertyValidatorTestData.GetValidRequest() with { Street = street };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.Street)
              .WithErrorMessage(expectedError);
    }

    [TestCaseSource(typeof(UpdatePropertyValidatorTestData), nameof(UpdatePropertyValidatorTestData.GetInvalidPriceTestCases))]
    public void Validate_InvalidPrice_ShouldHaveValidationError(decimal price, string expectedError)
    {
        var request = UpdatePropertyValidatorTestData.GetValidRequest() with { Price = price };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.Price)
              .WithErrorMessage(expectedError);
    }

    [TestCaseSource(typeof(UpdatePropertyValidatorTestData), nameof(UpdatePropertyValidatorTestData.GetInvalidYearTestCases))]
    public void Validate_InvalidYear_ShouldHaveValidationError(int year, string expectedError)
    {
        var request = UpdatePropertyValidatorTestData.GetValidRequest() with { Year = year };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.Year);
    }

    [TestCaseSource(typeof(UpdatePropertyValidatorTestData), nameof(UpdatePropertyValidatorTestData.GetInvalidOwnerIdTestCases))]
    public void Validate_InvalidOwnerId_ShouldHaveValidationError(int ownerId, string expectedError)
    {
        var request = UpdatePropertyValidatorTestData.GetValidRequest() with { OwnerId = ownerId };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.OwnerId)
              .WithErrorMessage(expectedError);
    }

    [TestCaseSource(typeof(UpdatePropertyValidatorTestData), nameof(UpdatePropertyValidatorTestData.GetInvalidZipCodeTestCases))]
    public void Validate_InvalidZipCode_ShouldHaveValidationError(int zipCode, string expectedError)
    {
        var request = UpdatePropertyValidatorTestData.GetValidRequest() with { ZipCode = zipCode };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.ZipCode)
              .WithErrorMessage(expectedError);
    }

    [Test]
    public void Validate_EmptyStringFields_ShouldNotHaveValidationErrors()
    {
        var request = UpdatePropertyValidatorTestData.GetValidRequest() with 
        {
            Name = "",
            Country = "",
            City = "",
            Street = ""
        };

        var result = _validator.TestValidate(request);

        result.ShouldNotHaveAnyValidationErrors();
    }
}
