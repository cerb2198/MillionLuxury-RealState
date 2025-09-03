using MillionLuxury.RealEstate.Domain.Entities.Interfaces;

namespace MillionLuxury.RealEstate.Domain.Entities.Common;
public abstract class AuditableEntity : BaseEntity, IAuditable
{
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public string? LastModifiedBy { get; set; }
}
