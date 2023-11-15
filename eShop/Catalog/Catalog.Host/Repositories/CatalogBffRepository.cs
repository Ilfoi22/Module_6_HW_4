﻿using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CatalogBffRepository : ICatalogBffRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogBffRepository> _logger;

        public CatalogBffRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogBffRepository> logger)
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

        public async Task<List<CatalogItem>> GetItemsAsync()
        {
            return await _dbContext.CatalogItems.ToListAsync();
        }

        public async Task<CatalogItem?> GetItemByIdAsync(int id)
        {
            return await _dbContext.CatalogItems
                .Where(item => item.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<CatalogBrand>> GetBrandsAsync()
        {
            return await _dbContext.CatalogBrands.ToListAsync();
        }

        public async Task<CatalogBrand?> GetBrandByIdAsync(int id)
        {
            return await _dbContext.CatalogBrands
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<CatalogType>> GetTypesAsync()
        {
            return await _dbContext.CatalogTypes.ToListAsync();
        }

        public async Task<CatalogType?> GetTypeByIdAsync(int id)
        {
            return await _dbContext.CatalogTypes
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
