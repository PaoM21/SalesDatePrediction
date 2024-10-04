using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Data;
using SalesDatePrediction.Models;
using SalesDatePrediction.Repository;

namespace SalesDatePrediction.Tests.Repository
{
    public class ProductsRepositoryTest
    {
        private async Task<DataContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Products.CountAsync() < default(int))
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Products.Add(
                        new Products()
                        {
                            ProductId = 1,
                            ProductName = "Product",
                            SupplierId = 1,
                            CategoryId = 1,
                            UnitPrice = 1000,
                            Discontinued = true
                        });

                    await databaseContext.SaveChangesAsync();
                }
            }

            return databaseContext;
        }

        [Fact]
        public async void ProductsRepository_GetProductsByCustom_ReturnsICollection()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var productsRepository = new ProductsRepository(dbContext);

            //Act
            var result = productsRepository.GetProducts();

            //Assert
            result.Should().NotBeNull();
            result.Should().AllBeOfType<Products>();
            result.Should().BeOfType<ICollection<Products>>();
        }

        [Fact]
        public async void ProductsRepository_ProductExists_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var productsRepository = new ProductsRepository(dbContext);
            int productId = 1;

            //Act
            var result = productsRepository.ProductExists(productId);

            //Assert
            result.Should().BeTrue();
        }
    }
}
