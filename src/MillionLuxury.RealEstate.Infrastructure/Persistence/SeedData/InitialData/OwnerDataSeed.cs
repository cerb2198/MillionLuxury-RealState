using Microsoft.EntityFrameworkCore;
using MillionLuxury.RealEstate.Domain.Entities;

namespace MillionLuxury.RealEstate.Infrastructure.Persistence.SeedData.InitialData;
public static class OwnerDataSeed
{
    public static void SeedData(ModelBuilder builder)
    {
        builder.Entity<Owner>().HasData(
            new
            {
                Id = 1,
                Name = "John Doe",
                Photo = (byte[]?)null,
                Birthday = "1980-05-15",
                CreatedAt = DataSeedManager.SeedDate,
                LastModifiedAt = DataSeedManager.SeedDate,
                CreatedBy = DataSeedManager.SeedUser,
                IsDeleted = false
            },
            new
            {
                Id = 2,
                Name = "Jane Smith",
                Photo = (byte[]?)null,
                Birthday = "1975-10-30",
                CreatedAt = DataSeedManager.SeedDate,
                LastModifiedAt = DataSeedManager.SeedDate,
                CreatedBy = DataSeedManager.SeedUser,
                IsDeleted = false
            }
        );

        builder.Entity<Owner>().OwnsOne(o => o.Address).HasData(
            new
            {
                OwnerId = 1,
                Street = "123 Main St",
                City = "Anytown",
                Country = "USA",
                ZipCode = 12345
            },
            new
            {
                OwnerId = 2,
                Street = "456 Oak Ave",
                City = "Othertown",
                Country = "USA",
                ZipCode = 67890
            }
        );
    }
}
