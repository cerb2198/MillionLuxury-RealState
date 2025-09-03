using MillionLuxury.RealEstate.Domain.Entities.Common;

namespace MillionLuxury.RealEstate.Domain.Entities;
public class PropertyImage : AuditableEntity
{
    public required byte[] File { get; set; }
    public required string FileType { get; set; }
    public required bool Enabled { get; set; }
    public int PropertyId { get; set; }
    public required Property Property { get; set; }
}
