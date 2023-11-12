using Catalog.Host.Models.Response;
using Catalog.Host.Services;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogTypeController : ControllerBase
{
    private readonly ILogger<CatalogTypeController> _logger;
    private readonly ICatalogTypeService _catalogTypeService;

    public CatalogTypeController(
        ILogger<CatalogTypeController> logger,
        ICatalogTypeService catalogTypeService)
    {
        _logger = logger;
        _catalogTypeService = catalogTypeService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(int id, string type)
    {
        var result = await _catalogTypeService.Add(id, type);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _catalogTypeService.DeleteAsync(id);

        if (result is null)
        {
            return NoContent();
        }

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(int id, string type)
    {
        var result = await _catalogTypeService.UpdateAsync(id, type);

        if (result is null)
        {
            return NoContent();
        }

        return Ok(result);
    }
}