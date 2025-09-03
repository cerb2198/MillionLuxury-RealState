using MillionLuxury.RealEstate.Domain.Entities.Common;
using MillionLuxury.RealEstate.Domain.ValueObjects;

namespace MillionLuxury.RealEstate.Domain.Entities;
public class Owner : FullTrackedEntity
{
    public required string Name { get; set; }
    // Note: I think owner address can be optional
    public Address? Address { get; set; }
    // Note: I think owner photo can be optional
    public byte[]? Photo { get; set; }
    // Note: I'm threating birthday as string
    // however for a real world application it's better to use DateOnly
    // and setup proper conversion for the database provider.
    // I'm going to handle formatting and validation in the application layer.
    public string? Birthday { get; set; } 
    public ICollection<Property> Properties { get; set; } = new List<Property>();
}
