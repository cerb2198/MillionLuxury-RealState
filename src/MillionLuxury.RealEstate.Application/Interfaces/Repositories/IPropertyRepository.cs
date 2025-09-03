using MillionLuxury.RealEstate.Application.Dtos.Requests;
using MillionLuxury.RealEstate.Domain.Entities;

namespace MillionLuxury.RealEstate.Application.Interfaces.Repositories;

public interface IPropertyRepository : IBaseGenericRepository<Property>
{
    Task<bool> ExistsByNameAndOwnerIdAsync(string name, int ownerId);
    Task<bool> ExistsByCodeInternalAsync(int codeInternal);
    Task<IEnumerable<Property>> GetByOwnerIdAsync(int ownerId);
    Task<(IEnumerable<Property> Properties, int TotalCount)> GetFilteredAsync(ListPropertiesRequest request);
}
