using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Catalog.Host.Repositories;

public class CatalogBrandRepository : ICatalogBrandRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogBrandRepository> _logger;

    public CatalogBrandRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogBrandRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<int?> Add(int id, string brand)
    {
        var item = await _dbContext.AddAsync(new CatalogBrand
        {
            Id = id,
            Brand = brand
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<CatalogBrand?> DeleteAsync(int id)
    {
        var item = await _dbContext.CatalogBrands.FindAsync(id);

        if (item is null)
        {
            return null;
        }

        _dbContext.CatalogBrands.Remove(item);
        await _dbContext.SaveChangesAsync();

        return item;
    }

    public async Task<CatalogBrand?> UpdateAsync(int id, string brand)
    {
        var catalogBrand = await _dbContext.CatalogBrands.FindAsync(id);

        if (catalogBrand is null)
        {
            return null;
        }

        catalogBrand.Id = id;
        catalogBrand.Brand = brand;

        _dbContext.CatalogBrands.Update(catalogBrand);
        await _dbContext.SaveChangesAsync();

        return catalogBrand;
    }
}
