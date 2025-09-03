using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Application.Dtos.Responses;
using MillionLuxury.RealEstate.Domain.Entities;
using MillionLuxury.RealEstate.Domain.ValueObjects;

namespace MillionLuxury.RealEstate.UnitTests.Application.UseCases.TestData;

public static class CreatePropertyBuildingTestData
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

    public static Owner GetValidOwner() => new()
    {
        Id = 1,
        Name = "John Doe",
        Birthday = "1980-05-15",
        Address = new Address("123 Main St", "Anytown", "USA", 12345),
        CreatedBy = "System",
        CreatedAt = DateTime.UtcNow
    };

    public static Property GetValidProperty() => new()
    {
        Name = "Luxury Downtown Penthouse",
        Address = new Address("500 Park Avenue", "New York", "USA", 10022),
        CodeInternal = 1001,
        Price = 2500000.00m,
        Year = 2020,
        OwnerId = 1,
        CreatedBy = "System",
        CreatedAt = DateTime.UtcNow
    };

    public static Property GetCreatedProperty() => new()
    {
        Id = 1,
        Name = "Luxury Downtown Penthouse",
        Address = new Address("500 Park Avenue", "New York", "USA", 10022),
        CodeInternal = 1001,
        Price = 2500000.00m,
        Year = 2020,
        OwnerId = 1,
        CreatedBy = "System", 
        CreatedAt = DateTime.UtcNow
    };

    public static CreatePropertyBuildingResponse GetValidResponse() => new(
        Id: 1,
        Name: "Luxury Downtown Penthouse",
        Country: "USA",
        City: "New York",
        Street: "500 Park Avenue", 
        ZipCode: 10022,
        Price: 2500000.00m,
        CodeInternal: 1001,
        Year: 2020
    );
}
