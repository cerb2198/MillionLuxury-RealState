using Microsoft.EntityFrameworkCore;
using MillionLuxury.RealEstate.Application.Interfaces.Repositories;
using MillionLuxury.RealEstate.Domain.Entities;
using MillionLuxury.RealEstate.Infrastructure.Persistence.Databases;

namespace MillionLuxury.RealEstate.Infrastructure.Persistence.Repositories;

public class PropertyImageRepository(RealEstateDbContext dbContext)
    : BaseGenericRepository<PropertyImage>(dbContext), IPropertyImageRepository
{
    public async Task<IEnumerable<PropertyImage>> GetByPropertyIdAsync(int propertyId)
    {
        return await _dbSet
            .Where(pi => pi.PropertyId == propertyId && pi.Enabled)
            .Include(pi => pi.Property)
            .ToListAsync();
    }

    public async Task<bool> ExistsByPropertyIdAsync(int propertyId)
    {
        return await _dbSet
            .AnyAsync(pi => pi.PropertyId == propertyId);
    }
}
