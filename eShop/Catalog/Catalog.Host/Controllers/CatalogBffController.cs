using System.Net;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBffController : ControllerBase
{
    private readonly ILogger<CatalogBffController> _logger;
    private readonly ICatalogBffService _catalogBffService;

    public CatalogBffController(
        ILogger<CatalogBffController> logger,
        ICatalogBffService catalogBffService)
    {
        _logger = logger;
        _catalogBffService = catalogBffService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Items(PaginatedItemsRequest<CatalogTypeFilter> request)
    {
        var result = await _catalogBffService.GetCatalogItemsAsync(request.PageSize, request.PageIndex, request.Filters);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetItemsAsync()
    {
        var result = await _catalogBffService.GetItemsAsync();

        if (result is null)
        {
            return NoContent();
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetItemByIdAsync(int id)
    {
        var result = await _catalogBffService.GetItemByIdAsync(id);

        if (result is null)
        {
            return NoContent();
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetBrandsAsync()
    {
        var result = await _catalogBffService.GetBrandsAsync();

        if (result is null)
        {
            return NoContent();
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetBrandByIdAsync(int id)
    {
        var result = await _catalogBffService.GetBrandByIdAsync(id);

        if (result is null)
        {
            return NoContent();
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetTypesAsync()
    {
        var result = await _catalogBffService.GetTypesAsync();

        if (result is null)
        {
            return NoContent();
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetTypeByIdAsyynncc(int id)
    {
        var result = await _catalogBffService.GetTypeByIdAsync(id);

        if (result is null)
        {
            return NoContent();
        }

        return Ok(result);
    }
}