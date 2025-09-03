using MillionLuxury.RealEstate.Domain.Entities;

namespace MillionLuxury.RealEstate.Application.Interfaces.Repositories;

public interface IPropertyImageRepository : IBaseGenericRepository<PropertyImage>
{
    Task<IEnumerable<PropertyImage>> GetByPropertyIdAsync(int propertyId);
    Task<bool> ExistsByPropertyIdAsync(int propertyId);
}
