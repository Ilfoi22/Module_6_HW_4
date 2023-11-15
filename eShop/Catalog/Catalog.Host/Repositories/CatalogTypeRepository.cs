using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Catalog.Host.Repositories;

public class CatalogTypeRepository : ICatalogTypeRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogTypeRepository> _logger;

    public CatalogTypeRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogTypeRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<int?> Add(int id, string type)
    {
        var item = await _dbContext.AddAsync(new CatalogType
        {
            Id = id,
            Type = type
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<CatalogType?> DeleteAsync(int id)
    {
        var item = await _dbContext.CatalogTypes.FindAsync(id);

        if (item is null)
        {
            return null;
        }

        _dbContext.CatalogTypes.Remove(item);
        await _dbContext.SaveChangesAsync();

        return item;
    }

    public async Task<CatalogType?> UpdateAsync(int id, string type)
    {
        var catalogType = await _dbContext.CatalogTypes.FindAsync(id);

        if (catalogType is null)
        {
            return null;
        }

        catalogType.Id = id;
        catalogType.Type = type;

        _dbContext.CatalogTypes.Update(catalogType);
        await _dbContext.SaveChangesAsync();

        return catalogType;
    }
}
