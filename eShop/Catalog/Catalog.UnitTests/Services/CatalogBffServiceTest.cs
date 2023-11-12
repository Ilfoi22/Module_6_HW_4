using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;
using Moq;

namespace Catalog.UnitTests.Services;

public class CatalogBffServiceTest
{
    private readonly ICatalogBffService _catalogBffService;

    private readonly Mock<ICatalogBffRepository> _catalogBffRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogBffService>> _logger;

    public CatalogBffServiceTest()
    {
        _catalogBffRepository = new Mock<ICatalogBffRepository>();
        _mapper = new Mock<IMapper>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogBffService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogBffService = new CatalogBffService(_dbContextWrapper.Object, _logger.Object, _catalogBffRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Success()
    {
        // arrange
        var testPageIndex = 0;
        var testPageSize = 4;
        var testTotalCount = 12;

        var pagingPaginatedItemsSuccess = new PaginatedItems<CatalogItem>()
        {
            Data = new List<CatalogItem>()
            {
                new CatalogItem()
                {
                    Name = "TestName",
                },
            },
            TotalCount = testTotalCount,
        };

        var catalogItemSuccess = new CatalogItem()
        {
            Name = "TestName"
        };

        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            Name = "TestName"
        };

        _catalogBffRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize))).ReturnsAsync(pagingPaginatedItemsSuccess);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogBffService.GetCatalogItemsAsync(testPageSize, testPageIndex);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
        result?.PageIndex.Should().Be(testPageIndex);
        result?.PageSize.Should().Be(testPageSize);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Failed()
    {
        // arrange
        var testPageIndex = 1000;
        var testPageSize = 10000;

        _catalogBffRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize))).Returns((Func<PaginatedItemsResponse<CatalogItemDto>>)null!);

        // act
        var result = await _catalogBffService.GetCatalogItemsAsync(testPageSize, testPageIndex);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetItemAsync_Success()
    {
        var expectedItems = new List<CatalogItem>
        {
            new CatalogItem { Name = "Name1" },
            new CatalogItem { Name = "Name2" },
            new CatalogItem { Name = "Name3" }
        };

        _catalogBffRepository.Setup(s => s.GetItemsAsync())
                         .ReturnsAsync(expectedItems);

        // act
        var result = await _catalogBffService.GetItemsAsync();

        // assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedItems);
    }

    [Fact]
    public async Task GetItemsAsync_Failed()
    {
        // arrange
        _catalogBffRepository.Setup(s => s.GetItemsAsync())
                             .ThrowsAsync(new Exception("Failed to retrieve items"));

        // act
        var result = await _catalogBffService.GetItemsAsync();

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetItemByIdAsync_Success()
    {
        var testItemId = 3;
        var expectedItem = new CatalogItem { Name = "Name1" };

        _catalogBffRepository.Setup(s => s.GetItemByIdAsync(testItemId))
                         .ReturnsAsync(expectedItem);

        // act
        var result = await _catalogBffService.GetItemByIdAsync(testItemId);

        // assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedItem);
    }

    [Fact]
    public async Task GetItemByIdAsync_Failed()
    {
        var testItemId = 999;

        var failedItem = new CatalogItem { Name = "NotFound" };
                
        _catalogBffRepository.Setup(s => s.GetItemByIdAsync(testItemId))
                         .ReturnsAsync(failedItem);

        // act
        var result = await _catalogBffService.GetItemByIdAsync(testItemId);

        // assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(failedItem);
    }

    [Fact]
    public async Task GetBrandsAsync_Success()
    {
        var expectedBrands = new List<CatalogBrand>
        {
            new CatalogBrand { Brand = "Brand1" },
            new CatalogBrand { Brand = "Brand2" },
            new CatalogBrand { Brand = "Brand3" }
        };

        _catalogBffRepository.Setup(s => s.GetBrandsAsync())
                         .ReturnsAsync(expectedBrands);

        // act
        var result = await _catalogBffService.GetBrandsAsync();

        // assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedBrands);
    }

    [Fact]
    public async Task GetBrandsAsync_Failed()
    {
        // arrange
        _catalogBffRepository.Setup(s => s.GetBrandsAsync())
                             .ThrowsAsync(new Exception("Failed to retrieve brands"));

        // act
        var result = await _catalogBffService.GetBrandsAsync();

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetBrandByIdAsync_Success()
    {
        var testBrandId = 3;
        var expectedBrand = new CatalogBrand { Brand = "Brand1" };

        _catalogBffRepository.Setup(s => s.GetBrandByIdAsync(testBrandId))
                         .ReturnsAsync(expectedBrand);

        // act
        var result = await _catalogBffService.GetBrandByIdAsync(testBrandId);

        // assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedBrand);
    }

    [Fact]
    public async Task GetBrandByIdAsync_Failed()
    {
        var testBrandId = 999;
        var expectedBrand = new CatalogBrand { Brand = "Brand1" };

        _catalogBffRepository.Setup(s => s.GetBrandByIdAsync(testBrandId))
                         .ReturnsAsync(expectedBrand);

        // act
        var result = await _catalogBffService.GetBrandByIdAsync(testBrandId);

        // assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedBrand);
    }

    [Fact]
    public async Task GetTypesAsync_Success()
    {
        var expectedTypes = new List<CatalogType>
        {
            new CatalogType { Type = "Type1" },
            new CatalogType { Type = "Type2" },
            new CatalogType { Type = "Type3" }
        };

        _catalogBffRepository.Setup(s => s.GetTypesAsync())
                         .ReturnsAsync(expectedTypes);

        // act
        var result = await _catalogBffService.GetTypesAsync();

        // assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedTypes);
    }

    [Fact]
    public async Task GetTypesAsync_Failed()
    {
        // arrange
        _catalogBffRepository.Setup(s => s.GetTypesAsync())
                             .ThrowsAsync(new Exception("Failed to retrieve types"));

        // act
        var result = await _catalogBffService.GetTypesAsync();

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetTypeByIdAsync_Success()
    {
        var testTypeId = 3;
        var expectedType = new CatalogType { Type = "Type1" };

        _catalogBffRepository.Setup(s => s.GetTypeByIdAsync(testTypeId))
                         .ReturnsAsync(expectedType);

        // act
        var result = await _catalogBffService.GetTypeByIdAsync(testTypeId);

        // assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedType);
    }

    [Fact]
    public async Task GetTypeByIdAsync_Failed()
    {
        var testBrandId = 999;
        var expectedBrand = new CatalogType { Type = "Type1" };

        _catalogBffRepository.Setup(s => s.GetTypeByIdAsync(testBrandId))
                         .ReturnsAsync(expectedBrand);

        // act
        var result = await _catalogBffService.GetTypeByIdAsync(testBrandId);

        // assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedBrand);
    }
}