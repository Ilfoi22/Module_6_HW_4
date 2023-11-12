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
public class CatalogItemController : ControllerBase
{
    private readonly ILogger<CatalogItemController> _logger;
    private readonly ICatalogItemService _catalogItemService;

    public CatalogItemController(
        ILogger<CatalogItemController> logger,
        ICatalogItemService catalogItemService)
    {
        _logger = logger;
        _catalogItemService = catalogItemService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateProductRequest request)
    {
        var result = await _catalogItemService.Add(request.Name, request.Description, request.Price, request.AvailableStock, request.CatalogBrandId, request.CatalogTypeId, request.PictureFileName);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _catalogItemService.DeleteAsync(id);

        if (result is null)
        {
            return NoContent();
        }

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var result = await _catalogItemService.UpdateAsync(id, name, description, price, availableStock, catalogBrandId, catalogTypeId, pictureFileName);

        if (result is null)
        {
            return NoContent();
        }

        return Ok(result);
    }
}