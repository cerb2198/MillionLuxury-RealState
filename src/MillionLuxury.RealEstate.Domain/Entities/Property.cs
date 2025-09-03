using MillionLuxury.RealEstate.Domain.Entities.Common;
using MillionLuxury.RealEstate.Domain.ValueObjects;

namespace MillionLuxury.RealEstate.Domain.Entities;
public class Property : FullTrackedEntity
{
    public required string Name { get; set; }
    public Address Address { get; set; } = new();
    public decimal Price { get; set; }
    public int CodeInternal { get; set; }
    public int Year { get; set; }
    public int OwnerId { get; set; }
    public Owner Owner { get; set; } = default!;

    public ICollection<PropertyImage> Images { get; set; } = new List<PropertyImage>();
    public ICollection<PropertyTrace> Traces { get; set; } = new List<PropertyTrace>();
}
