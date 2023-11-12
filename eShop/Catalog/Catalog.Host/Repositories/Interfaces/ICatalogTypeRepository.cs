using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<int?> Add(int id, string type);
        Task<CatalogType?> DeleteAsync(int id);
        Task<CatalogType?> UpdateAsync(int id, string type);
    }
}
