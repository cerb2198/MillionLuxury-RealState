using MillionLuxury.RealEstate.Domain.Entities.Interfaces;

namespace MillionLuxury.RealEstate.Domain.Entities.Common;
public abstract class DeletableEntity : BaseEntity, ISoftDeletable
{
    public abstract bool IsDeleted { get; set; }
    public abstract DateTime? DeletedAt { get; set; }
    public abstract string? DeletedBy { get; set; }
}
