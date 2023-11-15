using AutoMapper;
using Catalog.Host.Configurations;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogBffService : BaseDataService<ApplicationDbContext>, ICatalogBffService
{
    private readonly ICatalogBffRepository _catalogBffRepository;
    private readonly IMapper _mapper;

    public CatalogBffService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogBffRepository catalogBffRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogBffRepository = catalogBffRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedItemsResponse<CatalogItemDto>?> GetCatalogItemsAsync(int pageSize, int pageIndex, Dictionary<CatalogTypeFilter, int>? filters)
    {
        return await ExecuteSafeAsync(async () =>
        {
            int? brandFilter = null;
            int? typeFilter = null;

            if (filters != null)
            {
                if (filters.TryGetValue(CatalogTypeFilter.Brand, out var brand))
                {
                    brandFilter = brand;
                }

                if (filters.TryGetValue(CatalogTypeFilter.Type, out var type))
                {
                    typeFilter = type;
                }
            }

            var result = await _catalogBffRepository.GetByPageAsync(pageIndex, pageSize, brandFilter, typeFilter);
            if (result == null)
            {
                return null;
            }

            return new PaginatedItemsResponse<CatalogItemDto>()
            {
                Count = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        });
    }

    public async Task<List<CatalogItem>> GetItemsAsync()
    {
        var result = await _catalogBffRepository.GetItemsAsync();
        return result;
    }

    public async Task<CatalogItem?> GetItemByIdAsync(int id)
    {
        var result = await _catalogBffRepository.GetItemByIdAsync(id);
        return result;
    }

    public async Task<List<CatalogBrand>> GetBrandsAsync()
    {
        var result = await _catalogBffRepository.GetBrandsAsync();
        return result;
    }    

    public async Task<CatalogBrand?> GetBrandByIdAsync(int id)
    {
        var result = await _catalogBffRepository.GetBrandByIdAsync(id);
        return result;
    }

    public async Task<List<CatalogType>> GetTypesAsync()
    {
        var result = await _catalogBffRepository.GetTypesAsync();
        return result;
    }

    public async Task<CatalogType?> GetTypeByIdAsync(int id)
    {
        var result = await _catalogBffRepository.GetTypeByIdAsync(id);
        return result;
    }
}