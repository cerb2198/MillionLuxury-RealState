using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MillionLuxury.RealEstate.Domain.Entities;

namespace MillionLuxury.RealEstate.Infrastructure.Persistence.Configurations;
public sealed class PropertyImageConfiguration : IEntityTypeConfiguration<PropertyImage>
{
    private const string FileColumnType = "VARBINARY(MAX)";
    private const int MaxContentTypeLength = 100;
    public void Configure(EntityTypeBuilder<PropertyImage> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.RowVersion)
            .IsRowVersion();

        builder
            .Property(x => x.File)
            .IsRequired()
            .HasColumnType(FileColumnType);

        builder
            .Property(x => x.FileType)
            .HasMaxLength(MaxContentTypeLength);

        builder
            .Property(x => x.Enabled)
            .IsRequired();

        builder
            .HasOne(x => x.Property)
            .WithMany(p => p.Images)
            .HasForeignKey(x => x.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.PropertyId, x.Enabled });
    }
}
