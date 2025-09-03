using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Application.Dtos.Responses;
using MillionLuxury.RealEstate.Domain.Entities;
using MillionLuxury.RealEstate.Domain.ValueObjects;

namespace MillionLuxury.RealEstate.UnitTests.Application.UseCases.TestData;

public static class UpdatePropertyTestData
{
    public static UpdatePropertyRequest GetValidUpdateRequest() => new(
        PropertyId: 1,
        Name: "Updated Luxury Penthouse",
        Country: "Canada",
        City: "Toronto",
        Street: "100 Queen Street",
        ZipCode: 12345,
        Price: 3500000.00m,
        Year: 2023,
        OwnerId: 2
    );

    public static UpdatePropertyRequest GetPartialUpdateRequest() => new(
        PropertyId: 1,
        Name: "Updated Name Only",
        Country: null,
        City: null,
        Street: null,
        ZipCode: null,
        Price: null,
        Year: null,
        OwnerId: null
    );

    public static UpdatePropertyRequest GetAddressOnlyUpdateRequest() => new(
        PropertyId: 1,
        Name: null,
        Country: "Spain",
        City: "Madrid",
        Street: "Gran Via",
        ZipCode: 28013,
        Price: null,
        Year: null,
        OwnerId: null
    );

    public static Property GetExistingProperty() => new()
    {
        Id = 1,
        Name = "Original Property Name",
        Address = new Address("500 Park Avenue", "New York", "USA", 10022),
        CodeInternal = 1001,
        Price = 2500000.00m,
        Year = 2020,
        OwnerId = 1,
        CreatedBy = "System",
        CreatedAt = DateTime.UtcNow.AddDays(-30),
        LastModifiedBy = "System",
        LastModifiedAt = DateTime.UtcNow.AddDays(-10)
    };

    public static Property GetUpdatedProperty() => new()
    {
        Id = 1,
        Name = "Updated Luxury Penthouse",
        Address = new Address("100 Queen Street", "Toronto", "Canada", 12345),
        CodeInternal = 1001,
        Price = 3500000.00m,
        Year = 2023,
        OwnerId = 2,
        CreatedBy = "System",
        CreatedAt = DateTime.UtcNow.AddDays(-30),
        LastModifiedBy = "System",
        LastModifiedAt = DateTime.UtcNow
    };

    public static Owner GetValidOwner() => new()
    {
        Id = 2,
        Name = "Jane Smith",
        Birthday = "1985-03-20",
        Address = new Address("456 Oak St", "Toronto", "Canada", 12345),
        CreatedBy = "System",
        CreatedAt = DateTime.UtcNow.AddDays(-5)
    };

    public static UpdatePropertyResponse GetValidResponse() => new()
    {
        Id = 1,
        Name = "Updated Luxury Penthouse",
        Country = "Canada",
        City = "Toronto",
        Street = "100 Queen Street",
        ZipCode = 12345,
        Price = 3500000.00m,
        CodeInternal = 1001,
        Year = 2023,
        OwnerId = 2,
        LastModifiedAt = DateTime.UtcNow
    };

    public static UpdatePropertyRequest GetRequestWithDuplicateName() => new(
        PropertyId: 1,
        Name: "Existing Property Name",
        Country: null,
        City: null,
        Street: null,
        ZipCode: null,
        Price: null,
        Year: null,
        OwnerId: null
    );

    public static UpdatePropertyRequest GetRequestWithNonExistentOwner() => new(
        PropertyId: 1,
        Name: null,
        Country: null,
        City: null,
        Street: null,
        ZipCode: null,
        Price: null,
        Year: null,
        OwnerId: 999
    );

    public static UpdatePropertyRequest GetRequestForNonExistentProperty() => new(
        PropertyId: 999,
        Name: "Some Name",
        Country: null,
        City: null,
        Street: null,
        ZipCode: null,
        Price: null,
        Year: null,
        OwnerId: null
    );
}
