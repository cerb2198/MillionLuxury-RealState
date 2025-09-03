using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MillionLuxury.RealEstate.Domain.Entities;
using MillionLuxury.RealEstate.Infrastructure.Persistence.Configurations.Common;

namespace MillionLuxury.RealEstate.Infrastructure.Persistence.Configurations;
public sealed class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    private const string PriceColumnType = "decimal(18,2)";
    public void Configure(EntityTypeBuilder<Property> builder)
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
            .Property(x => x.CodeInternal)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.Price)
            .HasColumnType(PriceColumnType);

        builder
            .HasIndex(x => x.CodeInternal);

        builder
            .HasIndex(x => x.Price);

        builder
            .HasIndex(x => x.Year);

        builder
            .HasIndex(x => x.OwnerId);

        AddressConfiguration
            .ConfigureAddress(builder, nameof(Property.Address));

        builder
            .HasOne(x => x.Owner)
            .WithMany(o => o.Properties)
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(x => x.Images)
            .WithOne(i => i.Property)
            .HasForeignKey(i => i.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.Traces)
            .WithOne(t => t.Property)
            .HasForeignKey(t => t.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
