using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Catalog.Host.Repositories;

public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize, int? brandFilter, int? typeFilter)
    {
        IQueryable<CatalogItem> query = _dbContext.CatalogItems;

        if (brandFilter.HasValue)
        {
            query = query.Where(w => w.CatalogBrandId == brandFilter.Value);
        }

        if (typeFilter.HasValue)
        {
            query = query.Where(w => w.CatalogTypeId == typeFilter.Value);
        }

        var totalItems = await query.LongCountAsync();

        var itemsOnPage = await query.OrderBy(c => c.Name)
           .Include(i => i.CatalogBrand)
           .Include(i => i.CatalogType)
           .Skip(pageSize * pageIndex)
           .Take(pageSize)
           .ToListAsync();

        return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var item = await _dbContext.AddAsync(new CatalogItem
        {
            CatalogBrandId = catalogBrandId,
            CatalogTypeId = catalogTypeId,
            Description = description,
            Name = name,
            PictureFileName = pictureFileName,
            Price = price
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<CatalogItem?> DeleteAsync(int id)
    {
        var item = await _dbContext.CatalogItems.FindAsync(id);

        if (item is null)
        {
            return null;
        }

        _dbContext.CatalogItems.Remove(item);
        await _dbContext.SaveChangesAsync();

        return item;
    }

    public async Task<CatalogItem?> UpdateAsync(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var catalogItem = await _dbContext.CatalogItems.FindAsync(id);

        if (catalogItem is null)
        {
            return null;
        }

        catalogItem.Name = name;
        catalogItem.Description = description;
        catalogItem.Price = price;
        catalogItem.AvailableStock = availableStock;
        catalogItem.CatalogBrandId = catalogBrandId;
        catalogItem.CatalogTypeId = catalogTypeId;
        catalogItem.PictureFileName = pictureFileName;

        _dbContext.CatalogItems.Update(catalogItem);
        await _dbContext.SaveChangesAsync();

        return catalogItem;
    }
}