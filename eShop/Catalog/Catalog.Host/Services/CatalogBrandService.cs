using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogBrandService : BaseDataService<ApplicationDbContext>, ICatalogBrandService
{
    private readonly ICatalogBrandRepository _catalogBrandRepository;

    public CatalogBrandService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogBrandRepository catalogBrandRepository)
        : base(dbContextWrapper, logger)
    {
        _catalogBrandRepository = catalogBrandRepository;
    }

    public Task<int?> Add(int id, string brand)
    {
        return ExecuteSafeAsync(() => _catalogBrandRepository.Add(id, brand));
    }

    public async Task<CatalogBrand?> DeleteAsync(int id)
    {
        var result = await _catalogBrandRepository.DeleteAsync(id);
        return result;
    }

    public async Task<CatalogBrand?> UpdateAsync(int id, string brand)
    {
        var result = await _catalogBrandRepository.UpdateAsync(id, brand);
        return result;
    }
}
