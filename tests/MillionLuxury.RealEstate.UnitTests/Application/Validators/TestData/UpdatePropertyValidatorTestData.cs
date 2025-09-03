using MillionLuxury.RealEstate.Application.Dtos.Requests;

namespace MillionLuxury.RealEstate.UnitTests.Application.Validators.TestData;

public static class UpdatePropertyValidatorTestData
{
    public static UpdatePropertyRequest GetValidRequest() => new(
        PropertyId: 1,
        Name: "Valid Property Name",
        Country: "USA",
        City: "New York",
        Street: "123 Main Street",
        ZipCode: 10001,
        Price: 100000.00m,
        Year: 2020,
        OwnerId: 1
    );

    public static IEnumerable<TestCaseData> GetInvalidPropertyIdTestCases()
    {
        yield return new TestCaseData(0, "PropertyId must be greater than 0");
        yield return new TestCaseData(-1, "PropertyId must be greater than 0");
    }

    public static IEnumerable<TestCaseData> GetInvalidNameTestCases()
    {
        yield return new TestCaseData(new string('x', 201), "Name cannot exceed 200 characters");
    }

    public static IEnumerable<TestCaseData> GetInvalidCountryTestCases()
    {
        yield return new TestCaseData(new string('x', 101), "Country cannot exceed 100 characters");
    }

    public static IEnumerable<TestCaseData> GetInvalidCityTestCases()
    {
        yield return new TestCaseData(new string('x', 101), "City cannot exceed 100 characters");
    }

    public static IEnumerable<TestCaseData> GetInvalidStreetTestCases()
    {
        yield return new TestCaseData(new string('x', 201), "Street cannot exceed 200 characters");
    }

    public static IEnumerable<TestCaseData> GetInvalidPriceTestCases()
    {
        yield return new TestCaseData(0m, "Price must be greater than 0");
        yield return new TestCaseData(-100m, "Price must be greater than 0");
    }

    public static IEnumerable<TestCaseData> GetInvalidYearTestCases()
    {
        yield return new TestCaseData(1799, $"Year must be between 1800 and {DateTime.Now.Year + 10}");
        yield return new TestCaseData(DateTime.Now.Year + 11, $"Year must be between 1800 and {DateTime.Now.Year + 10}");
    }

    public static IEnumerable<TestCaseData> GetInvalidOwnerIdTestCases()
    {
        yield return new TestCaseData(0, "OwnerId must be greater than 0");
        yield return new TestCaseData(-1, "OwnerId must be greater than 0");
    }

    public static IEnumerable<TestCaseData> GetInvalidZipCodeTestCases()
    {
        yield return new TestCaseData(0, "ZipCode must be greater than 0");
        yield return new TestCaseData(-1, "ZipCode must be greater than 0");
    }
}
