using Microsoft.EntityFrameworkCore;
using MillionLuxury.RealEstate.Domain.Entities;

namespace MillionLuxury.RealEstate.Infrastructure.Persistence.SeedData.InitialData;

public static class PropertyDataSeed
{
    public static void SeedData(ModelBuilder builder)
    {
        builder.Entity<Property>().HasData(
            new
            {
                Id = 1,
                Name = "Luxury Downtown Penthouse",
                Price = 2500000.00m,
                CodeInternal = 1001,
                Year = 2020,
                OwnerId = 1,
                CreatedAt = DataSeedManager.SeedDate,
                LastModifiedAt = DataSeedManager.SeedDate,
                CreatedBy = DataSeedManager.SeedUser,
                IsDeleted = false
            },
            new
            {
                Id = 2,
                Name = "Modern Family Villa",
                Price = 1850000.00m,
                CodeInternal = 1002,
                Year = 2019,
                OwnerId = 1,
                CreatedAt = DataSeedManager.SeedDate,
                LastModifiedAt = DataSeedManager.SeedDate,
                CreatedBy = DataSeedManager.SeedUser,
                IsDeleted = false
            },
            new
            {
                Id = 3,
                Name = "Beachfront Condo",
                Price = 1200000.00m,
                CodeInternal = 1003,
                Year = 2021,
                OwnerId = 2,
                CreatedAt = DataSeedManager.SeedDate,
                LastModifiedAt = DataSeedManager.SeedDate,
                CreatedBy = DataSeedManager.SeedUser,
                IsDeleted = false
            },
            new
            {
                Id = 4,
                Name = "Historic Mansion",
                Price = 4200000.00m,
                CodeInternal = 1004,
                Year = 1895,
                OwnerId = 2,
                CreatedAt = DataSeedManager.SeedDate,
                LastModifiedAt = DataSeedManager.SeedDate,
                CreatedBy = DataSeedManager.SeedUser,
                IsDeleted = false
            },
            new
            {
                Id = 5,
                Name = "Contemporary Loft",
                Price = 975000.00m,
                CodeInternal = 1005,
                Year = 2022,
                OwnerId = 1,
                CreatedAt = DataSeedManager.SeedDate,
                LastModifiedAt = DataSeedManager.SeedDate,
                CreatedBy = DataSeedManager.SeedUser,
                IsDeleted = false
            },
            new
            {
                Id = 6,
                Name = "Suburban Estate",
                Price = 3100000.00m,
                CodeInternal = 1006,
                Year = 2018,
                OwnerId = 2,
                CreatedAt = DataSeedManager.SeedDate,
                LastModifiedAt = DataSeedManager.SeedDate,
                CreatedBy = DataSeedManager.SeedUser,
                IsDeleted = false
            },
            new
            {
                Id = 7,
                Name = "Urban Studio Apartment",
                Price = 450000.00m,
                CodeInternal = 1007,
                Year = 2023,
                OwnerId = 1,
                CreatedAt = DataSeedManager.SeedDate,
                LastModifiedAt = DataSeedManager.SeedDate,
                CreatedBy = DataSeedManager.SeedUser,
                IsDeleted = false
            },
            new
            {
                Id = 8,
                Name = "Mountain Retreat Cabin",
                Price = 875000.00m,
                CodeInternal = 1008,
                Year = 2017,
                OwnerId = 2,
                CreatedAt = DataSeedManager.SeedDate,
                LastModifiedAt = DataSeedManager.SeedDate,
                CreatedBy = DataSeedManager.SeedUser,
                IsDeleted = false
            },
            new
            {
                Id = 9,
                Name = "Waterfront Townhouse",
                Price = 1650000.00m,
                CodeInternal = 1009,
                Year = 2020,
                OwnerId = 1,
                CreatedAt = DataSeedManager.SeedDate,
                LastModifiedAt = DataSeedManager.SeedDate,
                CreatedBy = DataSeedManager.SeedUser,
                IsDeleted = false
            },
            new
            {
                Id = 10,
                Name = "Garden View Cottage",
                Price = 720000.00m,
                CodeInternal = 1010,
                Year = 2016,
                OwnerId = 2,
                CreatedAt = DataSeedManager.SeedDate,
                LastModifiedAt = DataSeedManager.SeedDate,
                CreatedBy = DataSeedManager.SeedUser,
                IsDeleted = false
            }
        );

        builder.Entity<Property>().OwnsOne(p => p.Address).HasData(
            new
            {
                PropertyId = 1,
                Street = "500 Park Avenue",
                City = "New York",
                Country = "USA",
                ZipCode = 10022
            },
            new
            {
                PropertyId = 2,
                Street = "1234 Maple Drive",
                City = "Beverly Hills",
                Country = "USA",
                ZipCode = 90210
            },
            new
            {
                PropertyId = 3,
                Street = "789 Ocean Boulevard",
                City = "Miami",
                Country = "USA",
                ZipCode = 33139
            },
            new
            {
                PropertyId = 4,
                Street = "42 Historic Lane",
                City = "Boston",
                Country = "USA",
                ZipCode = 2108
            },
            new
            {
                PropertyId = 5,
                Street = "888 Industrial Way",
                City = "Seattle",
                Country = "USA",
                ZipCode = 98101
            },
            new
            {
                PropertyId = 6,
                Street = "1500 Country Club Road",
                City = "Atlanta",
                Country = "USA",
                ZipCode = 30309
            },
            new
            {
                PropertyId = 7,
                Street = "101 Downtown Street",
                City = "Chicago",
                Country = "USA",
                ZipCode = 60601
            },
            new
            {
                PropertyId = 8,
                Street = "2000 Mountain View Drive",
                City = "Denver",
                Country = "USA",
                ZipCode = 80202
            },
            new
            {
                PropertyId = 9,
                Street = "75 Harbor Front",
                City = "San Francisco",
                Country = "USA",
                ZipCode = 94105
            },
            new
            {
                PropertyId = 10,
                Street = "350 Garden Lane",
                City = "Portland",
                Country = "USA",
                ZipCode = 97201
            }
        );
    }
}
