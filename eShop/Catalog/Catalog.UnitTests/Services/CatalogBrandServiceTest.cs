using Catalog.Host.Data.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.UnitTests.Services
{
    public class CatalogBrandServiceTest
    {
        private readonly ICatalogBrandService _catalogBrandService;

        private readonly Mock<ICatalogBrandRepository> _catalogBrandRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogBrandService>> _logger;

        private readonly CatalogBrand _testItem = new CatalogBrand()
        {
            Id = 20,
            Brand = "New Brand"
        };

        public CatalogBrandServiceTest()
        {
            _catalogBrandRepository = new Mock<ICatalogBrandRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogBrandService>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _catalogBrandService = new CatalogBrandService(_dbContextWrapper.Object, _logger.Object, _catalogBrandRepository.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            var testResult = 1;

            _catalogBrandRepository.Setup(s => s.Add(
                It.IsAny<int>(),
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogBrandService.Add(_testItem.Id, _testItem.Brand);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            // arrange
            int? testResult = null;

            _catalogBrandRepository.Setup(s => s.Add(
                It.IsAny<int>(),
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogBrandService.Add(_testItem.Id, _testItem.Brand);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            // arrange 
            var brandToDelete = 1;

            _catalogBrandRepository.Setup(s => s.DeleteAsync(
                It.IsAny<int>())).ReturnsAsync((CatalogBrand?)null);

            // act
            var result = await _catalogBrandService.DeleteAsync(brandToDelete);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_Failed()
        {
            // arrange
            var brandToDelete = 999;

            _catalogBrandRepository.Setup(s => s.DeleteAsync(
                It.IsAny<int>())).ReturnsAsync(new CatalogBrand { Id = brandToDelete });

            // act
            var result = await _catalogBrandService.DeleteAsync(brandToDelete);

            // assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            // arrange
            var brandIdToUpdate = 1;
            var updatedBrand = new CatalogBrand { Id = brandIdToUpdate, Brand = "UpdatedBrand" };

            _catalogBrandRepository.Setup(s => s.UpdateAsync(
                It.IsAny<int>(),
                It.IsAny<string>())).ReturnsAsync(updatedBrand);

            // act
            var result = await _catalogBrandService.UpdateAsync(_testItem.Id, _testItem.Brand);

            // assert
            result.Should().Be(result);
        }

        [Fact]
        public async Task UpdateAsync_Failed()
        {
            // arrange
            var updatedBrand = new CatalogBrand { };

            _catalogBrandRepository.Setup(s => s.UpdateAsync(
                It.IsAny<int>(),
                It.IsAny<string>())).ReturnsAsync(updatedBrand);

            // act
            var result = await _catalogBrandService.UpdateAsync(_testItem.Id, _testItem.Brand);

            // assert
            result.Should().Be(updatedBrand);
        }
    }
}
