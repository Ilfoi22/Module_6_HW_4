using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogTypeService : BaseDataService<ApplicationDbContext>, ICatalogTypeService
{
    private readonly ICatalogTypeRepository _catalogTypeRepository;

    public CatalogTypeService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogTypeRepository catalogTypeRepository)
        : base(dbContextWrapper, logger)
    {
        _catalogTypeRepository = catalogTypeRepository;
    }

    public Task<int?> Add(int id, string type)
    {
        return ExecuteSafeAsync(() => _catalogTypeRepository.Add(id, type));
    }

    public async Task<CatalogType?> DeleteAsync(int id)
    {
        var result = await _catalogTypeRepository.DeleteAsync(id);
        return result;
    }

    public async Task<CatalogType?> UpdateAsync(int id, string type)
    {
        var result = await _catalogTypeRepository.UpdateAsync(id, type);
        return result;
    }
}
