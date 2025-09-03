using MillionLuxury.RealEstate.Domain.Entities.Interfaces;

namespace MillionLuxury.RealEstate.Domain.Entities.Common;
public abstract class FullTrackedEntity : AuditableEntity, ISoftDeletable
{
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
