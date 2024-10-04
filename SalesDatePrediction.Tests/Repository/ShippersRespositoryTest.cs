using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Data;
using SalesDatePrediction.Models;
using SalesDatePrediction.Repository;

namespace SalesDatePrediction.Tests.Repository
{
    public class ShippersRepositoryTest
    {
        private async Task<DataContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Shippers.CountAsync() < default(int))
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Shippers.Add(
                        new Shippers()
                        {
                            ShipperId = 1,
                            CompanyName = "Name",
                            Phone = "3003003003"
                        });

                    await databaseContext.SaveChangesAsync();
                }
            }

            return databaseContext;
        }

        [Fact]
        public async void ShippersRepository_GetShippersByCustom_ReturnsICollection()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var shippersRepository = new ShippersRepository(dbContext);

            //Act
            var result = shippersRepository.GetShippers();

            //Assert
            result.Should().NotBeNull();
            result.Should().AllBeOfType<Shippers>();
            result.Should().BeOfType<ICollection<Shippers>>();
        }
    }
}
