namespace MillionLuxury.RealEstate.Domain.Entities.Common;

public class BaseEntity
{
    public int Id { get; set; }
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
}
