using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogItemService : BaseDataService<ApplicationDbContext>, ICatalogItemService
{
    private readonly ICatalogItemRepository _catalogItemRepository;

    public CatalogItemService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository)
        : base(dbContextWrapper, logger)
    {
        _catalogItemRepository = catalogItemRepository;
    }

    public Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.Add(name, description, price, availableStock, catalogBrandId, catalogTypeId, pictureFileName));
    }

    public async Task<CatalogItem?> DeleteAsync(int id)
    {
        var result = await _catalogItemRepository.DeleteAsync(id);
        return result;
    }

    public async Task<CatalogItem?> UpdateAsync(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var result = await _catalogItemRepository.UpdateAsync(id, name, description, price, availableStock,catalogBrandId, catalogTypeId, pictureFileName);
        return result;
    }
}