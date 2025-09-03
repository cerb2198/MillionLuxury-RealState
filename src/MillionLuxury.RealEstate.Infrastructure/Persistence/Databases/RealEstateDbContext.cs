using Microsoft.EntityFrameworkCore;
using MillionLuxury.RealEstate.Domain.Entities;
using MillionLuxury.RealEstate.Infrastructure.Persistence.Filters;
using MillionLuxury.RealEstate.Infrastructure.Persistence.SeedData;
using System.Reflection;

namespace MillionLuxury.RealEstate.Infrastructure.Persistence.Databases;
public class RealEstateDbContext : DbContext
{
    public DbSet<Property> Property { get; set; }
    public DbSet<PropertyImage> PropertyImage { get; set; }
    public DbSet<PropertyTrace> PropertyTrace { get; set; }
    public DbSet<Owner> Owner { get; set; }
    public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        DataSeedManager.SeedData(builder);
        GlobalQueryFilters.AddSoftDeleteQueryFilter(builder);
    }
}
