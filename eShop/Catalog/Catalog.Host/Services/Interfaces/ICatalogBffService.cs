using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogBffService
{
    Task<PaginatedItemsResponse<CatalogItemDto>?> GetCatalogItemsAsync(int pageSize, int pageIndex, Dictionary<CatalogTypeFilter, int>? filters);
    Task<List<CatalogItem>> GetItemsAsync();
    Task<CatalogItem?> GetItemByIdAsync(int id);
    Task<List<CatalogBrand>> GetBrandsAsync();
    Task<CatalogBrand?> GetBrandByIdAsync(int id);
    Task<List<CatalogType>> GetTypesAsync();
    Task<CatalogType?> GetTypeByIdAsync(int id);
}