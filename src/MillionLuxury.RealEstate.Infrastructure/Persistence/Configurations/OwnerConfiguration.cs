using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MillionLuxury.RealEstate.Domain.Entities;
using MillionLuxury.RealEstate.Infrastructure.Persistence.Configurations.Common;

namespace MillionLuxury.RealEstate.Infrastructure.Persistence.Configurations;
public sealed class OwnerConfiguration : IEntityTypeConfiguration<Owner>
{
    private const string PhotoColumnType = "VARBINARY(MAX)";
    private const int MaxNameLength = 150;

    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.RowVersion)
            .IsRowVersion();

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(MaxNameLength);

        builder
            .Property(x => x.Photo)
            .HasColumnType(PhotoColumnType)
            .IsRequired(false);

        AddressConfiguration
            .ConfigureAddress(builder, nameof(Owner.Address));

        builder
            .Property(x => x.Birthday)
            .IsRequired(false);

        builder.
            HasIndex(x => x.Name);
    }
}
