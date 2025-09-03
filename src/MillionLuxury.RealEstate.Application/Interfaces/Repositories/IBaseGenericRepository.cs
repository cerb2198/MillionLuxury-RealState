using MillionLuxury.RealEstate.Domain.Entities.Common;

namespace MillionLuxury.RealEstate.Application.Interfaces.Repositories;

public interface IBaseGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);
    Task<TEntity> AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteAsync(int id);
    Task SaveChangesAsync();
}
