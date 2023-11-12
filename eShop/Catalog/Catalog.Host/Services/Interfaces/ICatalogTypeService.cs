using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogTypeService
    {
        Task<int?> Add(int id, string type);
        Task<CatalogType?> DeleteAsync(int id);
        Task<CatalogType?> UpdateAsync(int id, string type);
    }
}
