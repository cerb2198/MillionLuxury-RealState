using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MillionLuxury.RealEstate.Domain.Entities;

namespace MillionLuxury.RealEstate.Infrastructure.Persistence.Configurations;
public sealed class PropertyTraceConfiguration : IEntityTypeConfiguration<PropertyTrace>
{
    private const int DecimalPrecision = 18;
    private const int DecimalScale = 2;
    private string ValueColumnType()
        => $"decimal({DecimalPrecision},{DecimalScale})";
    public void Configure(EntityTypeBuilder<PropertyTrace> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.RowVersion)
            .IsRowVersion();

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder
            .Property(x => x.Value)
            .HasColumnType(ValueColumnType());

        builder
            .Property(x => x.Tax)
            .HasColumnType(ValueColumnType());

        builder
            .Property(x => x.DateSale)
            .IsRequired();

        builder
            .HasOne(x => x.Property)
            .WithMany(p => p.Traces)
            .HasForeignKey(x => x.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasIndex(x => x.PropertyId);

        builder
            .HasIndex(x => x.Name);
    }
}
