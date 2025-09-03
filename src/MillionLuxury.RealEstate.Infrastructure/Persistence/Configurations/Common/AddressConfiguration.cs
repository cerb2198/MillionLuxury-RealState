using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MillionLuxury.RealEstate.Domain.ValueObjects;

namespace MillionLuxury.RealEstate.Infrastructure.Persistence.Configurations.Common;
public static class AddressConfiguration
{
    private const int MaxCountryLength = 100;
    private const int MaxCityLength = 100;
    private const int MaxStreetLength = 200;

    public static void ConfigureAddress<TEntity>(EntityTypeBuilder<TEntity> builder, string navigationName)
        where TEntity : class
    {
        builder.OwnsOne(typeof(Address), navigationName, addrBuilder
            => {
                addrBuilder
                    .Property("Country")
                    .HasMaxLength(MaxCountryLength)
                    .IsRequired();

                addrBuilder
                    .Property("City")
                    .HasMaxLength(MaxCityLength)
                    .IsRequired();

                addrBuilder
                    .Property("Street")
                    .HasMaxLength(MaxStreetLength)
                    .IsRequired();

                addrBuilder
                    .Property("ZipCode")
                    .IsRequired();
            });
    }
}
