using MillionLuxury.RealEstate.Application.Dtos.Requests;

namespace MillionLuxury.RealEstate.UnitTests.Application.Validators.TestData;

public static class CreatePropertyBuildingValidatorTestData
{
    public static CreatePropertyBuildingRequest GetValidRequest() => new(
        Name: "Luxury Downtown Penthouse",
        Country: "USA",
        City: "New York", 
        Street: "500 Park Avenue",
        ZipCode: 10022,
        CodeInternal: 1001,
        Price: 2500000.00m,
        Year: 2020,
        OwnerId: 1
    );

    public static IEnumerable<TestCaseData> GetInvalidNameTestCases()
    {
        yield return new TestCaseData("", "Property name is required.");
        yield return new TestCaseData("AB", "Property name must be at least 3 characters long.");
        yield return new TestCaseData(new string('A', 201), "Property name cannot exceed 200 characters.");
    }

    public static IEnumerable<TestCaseData> GetInvalidCountryTestCases()
    {
        yield return new TestCaseData("", "Country is required.");
        yield return new TestCaseData(new string('A', 101), "Country cannot exceed 100 characters.");
    }

    public static IEnumerable<TestCaseData> GetInvalidCityTestCases()
    {
        yield return new TestCaseData("", "City is required.");
        yield return new TestCaseData(new string('A', 101), "City cannot exceed 100 characters.");
    }

    public static IEnumerable<TestCaseData> GetInvalidStreetTestCases()
    {
        yield return new TestCaseData("", "Street is required.");
        yield return new TestCaseData(new string('A', 201), "Street cannot exceed 200 characters.");
    }

    public static IEnumerable<TestCaseData> GetInvalidZipCodeTestCases()
    {
        yield return new TestCaseData(0, "Zip code must be greater than 0.");
        yield return new TestCaseData(-1, "Zip code must be greater than 0.");
    }

    public static IEnumerable<TestCaseData> GetInvalidPriceTestCases()
    {
        yield return new TestCaseData(0m, "Price must be greater than 0.");
        yield return new TestCaseData(-100m, "Price must be greater than 0.");
    }

    public static IEnumerable<TestCaseData> GetInvalidOwnerIdTestCases()
    {
        yield return new TestCaseData(0, "Owner ID must be greater than 0.");
        yield return new TestCaseData(-1, "Owner ID must be greater than 0.");
    }
}
