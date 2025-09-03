using Microsoft.EntityFrameworkCore;
using MillionLuxury.RealEstate.Infrastructure.Persistence.SeedData.InitialData;

namespace MillionLuxury.RealEstate.Infrastructure.Persistence.SeedData;
public static class DataSeedManager
{
    public static DateTime SeedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    public static string SeedUser = "DataSeed";

    public static void SeedData(this ModelBuilder builder)
    {
        OwnerDataSeed.SeedData(builder);
        PropertyDataSeed.SeedData(builder);
    }
}
