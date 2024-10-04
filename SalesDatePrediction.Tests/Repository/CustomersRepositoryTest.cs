using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Data;
using SalesDatePrediction.Models;
using SalesDatePrediction.Repository;

namespace SalesDatePrediction.Tests.Repository
{
    public class CustomersRepositoryTest
    {
        private async Task<DataContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Customers.CountAsync() < default(int))
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Customers.Add(
                        new Customers()
                        {
                            CustId = 1,
                            CompanyName = "Codifico",
                            ContactName = "Amanda",
                            ContactTitle = "Mobile",
                            Address = "car 1 # 1 - 1",
                            City = "Bogotá",
                            Region = "Colombia",
                            PostalCode = "111111",
                            Country = "Colombia",
                            Phone = "3003003003",
                            Fax = "Fax"
                        });

                    await databaseContext.SaveChangesAsync();
                }
            }

            return databaseContext;
        }

        [Fact]
        public async void CustomersRepository_GetCustomers_ReturnsICollection()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var customersRepository = new CustomersRepository(dbContext);

            //Act
            var result = customersRepository.GetCustomerOrderPredictions();

            //Assert
            result.Should().NotBeNull();
            result.Should().AllBeOfType<Customers>();
            result.Should().BeOfType<ICollection<Customers>>();
        }
    }
}
