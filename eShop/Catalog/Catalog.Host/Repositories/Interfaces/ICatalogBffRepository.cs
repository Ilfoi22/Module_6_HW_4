using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogBffRepository
    {
        Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize);
        Task<List<CatalogItem>> GetItemsAsync();
        Task<CatalogItem?> GetItemByIdAsync(int id);
        Task<List<CatalogBrand>> GetBrandsAsync();
        Task<CatalogBrand?> GetBrandByIdAsync(int id);
        Task<List<CatalogType>> GetTypesAsync();
        Task<CatalogType?> GetTypeByIdAsync(int id);
    }
}
