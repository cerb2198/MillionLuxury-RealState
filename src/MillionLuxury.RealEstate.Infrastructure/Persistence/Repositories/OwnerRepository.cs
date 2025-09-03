using Microsoft.EntityFrameworkCore;
using MillionLuxury.RealEstate.Application.Interfaces.Repositories;
using MillionLuxury.RealEstate.Domain.Entities;
using MillionLuxury.RealEstate.Infrastructure.Persistence.Databases;

namespace MillionLuxury.RealEstate.Infrastructure.Persistence.Repositories;

public class OwnerRepository(RealEstateDbContext dbContext)
    : BaseGenericRepository<Owner>(dbContext), IOwnerRepository
{
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _dbSet
            .AnyAsync(o => o.Name.Equals(name));
    }
}
