using MillionLuxury.RealEstate.Domain.Entities.Common;

namespace MillionLuxury.RealEstate.Domain.Entities;
public class PropertyTrace : FullTrackedEntity
{
    public DateTime DateSale { get; set; }
    public required string Name { get; set; }
    public decimal Value { get; set; }
    public decimal Tax { get; set; }
    public int PropertyId { get; set; }
    public Property? Property { get; set; }
}
