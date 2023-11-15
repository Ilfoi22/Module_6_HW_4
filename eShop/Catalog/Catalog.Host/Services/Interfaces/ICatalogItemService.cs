using Catalog.Host.Data.Entities;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogItemService
{
    Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<CatalogItem?> DeleteAsync(int id);
    Task<CatalogItem?> UpdateAsync(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
}