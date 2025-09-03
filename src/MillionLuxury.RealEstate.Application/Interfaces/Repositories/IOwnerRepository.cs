using MillionLuxury.RealEstate.Domain.Entities;

namespace MillionLuxury.RealEstate.Application.Interfaces.Repositories;

public interface IOwnerRepository : IBaseGenericRepository<Owner>
{
    Task<bool> ExistsByNameAsync(string name);
}
