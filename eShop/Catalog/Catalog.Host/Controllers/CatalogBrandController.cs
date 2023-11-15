using System.Net;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBrandController : ControllerBase
{
    private readonly ILogger<CatalogBrandController> _logger;
    private readonly ICatalogBrandService _catalogBrandService;

    public CatalogBrandController(
        ILogger<CatalogBrandController> logger,
        ICatalogBrandService catalogBrandService)
    {
        _logger = logger;
        _catalogBrandService = catalogBrandService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(int id, string brand)
    {
        var result = await _catalogBrandService.Add(id, brand);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _catalogBrandService.DeleteAsync(id);

        if (result is null)
        {
            return NoContent();
        }

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(int id, string brand)
    {
        var result = await _catalogBrandService.UpdateAsync(id, brand);

        if (result is null)
        {
            return NoContent();
        }

        return Ok(result);
    }
}