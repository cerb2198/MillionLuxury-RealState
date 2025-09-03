using Microsoft.EntityFrameworkCore;
using MillionLuxury.RealEstate.Application.Consts;
using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Application.Interfaces.Repositories;
using MillionLuxury.RealEstate.Domain.Entities;
using MillionLuxury.RealEstate.Infrastructure.Persistence.Databases;

namespace MillionLuxury.RealEstate.Infrastructure.Persistence.Repositories;

public class PropertyRepository(RealEstateDbContext dbContext)
    : BaseGenericRepository<Property>(dbContext), IPropertyRepository
{
    public async Task<bool> ExistsByNameAndOwnerIdAsync(string name, int ownerId)
    {
        return await _dbSet
            .AnyAsync(p => p.Name.Equals(name) && p.OwnerId == ownerId);
    }

    public async Task<bool> ExistsByCodeInternalAsync(int codeInternal)
    {
        return await _dbSet
            .AnyAsync(p => p.CodeInternal == codeInternal);
    }

    public async Task<IEnumerable<Property>> GetByOwnerIdAsync(int ownerId)
    {
        return await _dbSet
            .Where(p => p.OwnerId == ownerId)
            .Include(p => p.Owner)
            .Include(p => p.Images)
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<(IEnumerable<Property> Properties, int TotalCount)> GetFilteredAsync(ListPropertiesRequest request)
    {
        var query = _dbSet
            .Include(p => p.Owner)
            .Include(p => p.Images.Where(i => i.Enabled))
            .AsSplitQuery()
            .AsQueryable();

        if (request.MinPrice.HasValue)
            query = query.Where(p => p.Price >= request.MinPrice.Value);

        if (request.MaxPrice.HasValue)
            query = query.Where(p => p.Price <= request.MaxPrice.Value);

        if (!string.IsNullOrWhiteSpace(request.Country))
            query = query.Where(p => p.Address.Country.Contains(request.Country));

        if (!string.IsNullOrWhiteSpace(request.City))
            query = query.Where(p => p.Address.City.Contains(request.City));

        if (request.MinYear.HasValue)
            query = query.Where(p => p.Year >= request.MinYear.Value);

        if (request.MaxYear.HasValue)
            query = query.Where(p => p.Year <= request.MaxYear.Value);

        if (request.OwnerId.HasValue)
            query = query.Where(p => p.OwnerId == request.OwnerId.Value);

        if (!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(p => p.Name.Contains(request.Name));

        var totalCount = await query.CountAsync();

        query = ApplySorting(query, request.SortBy, request.SortDescending);

        var properties = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return (properties, totalCount);
    }

    private static IQueryable<Property> ApplySorting(
       IQueryable<Property> query,
       string? sortBy,
       bool descending)
    {
        var key = string.IsNullOrWhiteSpace(sortBy)
            ? SortingOptions.Default
            : sortBy.Trim().ToLowerInvariant();

        return key switch {
            var k when k == SortingOptions.Price =>
                descending ? query.OrderByDescending(p => p.Price)
                           : query.OrderBy(p => p.Price),

            var k when k == SortingOptions.Year =>
                descending ? query.OrderByDescending(p => p.Year)
                           : query.OrderBy(p => p.Year),

            var k when k == SortingOptions.Name =>
                descending ? query.OrderByDescending(p => p.Name)
                           : query.OrderBy(p => p.Name),

            var k when k == SortingOptions.CreatedAt =>
                descending ? query.OrderByDescending(p => p.CreatedAt)
                           : query.OrderBy(p => p.CreatedAt),

            _ => descending ? query.OrderByDescending(p => p.CreatedAt)
                            : query.OrderBy(p => p.CreatedAt),
        };
    }
}
