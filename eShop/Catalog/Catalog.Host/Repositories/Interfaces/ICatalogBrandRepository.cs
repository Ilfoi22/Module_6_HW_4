using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogBrandRepository
    {
        Task<int?> Add(int id, string brand);
        Task<CatalogBrand?> DeleteAsync(int id);
        Task<CatalogBrand?> UpdateAsync(int id, string brand);
    }
}
