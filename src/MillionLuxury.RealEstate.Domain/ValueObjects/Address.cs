namespace MillionLuxury.RealEstate.Domain.ValueObjects;
public record Address
{
    public string Country { get; init; } = default!;
    public string City { get; init; } = default!;
    public string Street { get; init; } = default!;
    public int ZipCode { get; init; }

    public Address() { }

    public Address(string country, string city, string street, int zipCode)
    {
        Country = country;
        City = city;
        Street = street;
        ZipCode = zipCode;
    }
}
