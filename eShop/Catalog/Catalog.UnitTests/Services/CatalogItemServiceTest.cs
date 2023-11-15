using System.Threading;
using Catalog.Host.Data.Entities;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;

namespace Catalog.UnitTests.Services;

public class CatalogItemServiceTest
{
    private readonly ICatalogItemService _catalogItemService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogItemService>> _logger;

    private readonly CatalogItem _testItem = new CatalogItem()
    {
        Name = "Name",
        Description = "Description",
        Price = 1000,
        AvailableStock = 100,
        CatalogBrandId = 1,
        CatalogTypeId = 1,
        PictureFileName = "1.png"
    };

    public CatalogItemServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogItemService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogItemService = new CatalogItemService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object);
    }

    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        var testResult = 1;

        _catalogItemRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogItemService.Add(_testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        int? testResult = null;

        _catalogItemRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogItemService.Add(_testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task DeleteAsync_Success()
    {
        // arrange 
        var itemToDelete = 1;

        _catalogItemRepository.Setup(s => s.DeleteAsync(
            It.IsAny<int>())).ReturnsAsync((CatalogItem?)null);

        // act
        var result = await _catalogItemService.DeleteAsync(itemToDelete);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task DeleteAsync_Failed()
    {
        // arrange
        var itemToDelete = 999;
        
        _catalogItemRepository.Setup(s => s.DeleteAsync(
            It.IsAny<int>())).ReturnsAsync(new CatalogItem { Id = itemToDelete });

        // act
        var result = await _catalogItemService.DeleteAsync(itemToDelete);

        // assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_Success()
    {
        // arrange
        var itemIdToUpdate = 1;
        var updatedItem = new CatalogItem { Id = itemIdToUpdate, Name = "UpdatedName" };

        _catalogItemRepository.Setup(s => s.UpdateAsync(
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(updatedItem);

        // act
        var result = await _catalogItemService.UpdateAsync(_testItem.Id, _testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

        // assert
        result.Should().Be(result);
    }

    [Fact]
    public async Task UpdateAsync_Failed()
    {
        // arrange
        var updatedItem = new CatalogItem { };

        _catalogItemRepository.Setup(s => s.UpdateAsync(
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(updatedItem);

        // act
        var result = await _catalogItemService.UpdateAsync(_testItem.Id, _testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

        // assert
        result.Should().Be(updatedItem);
    }
}