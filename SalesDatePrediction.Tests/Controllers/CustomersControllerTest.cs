using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Controllers;
using SalesDatePrediction.Interfaces;
using SalesDatePrediction.Models;

namespace SalesDatePrediction.Tests.Controllers
{
    public class CustomersControllerTest
    {
        private ICustomersRepository _customersRepository { get; set; }
        private IMapper _mapper { get; set; }
        private CustomersController _customersController { get; set; }
        public CustomersControllerTest()
        {
            //Dependencies
            _customersRepository = A.Fake<ICustomersRepository>();

            //SUT
            _customersController = new CustomersController(_customersRepository, _mapper);
        }

        [Fact]
        public void CustomersController_GetCustomers_ReturnsSuccess()
        {
            //Arrange
            var customers = A.Fake<IEnumerable<Customers>>();
            A.CallTo(() => _customersRepository.GetCustomerOrderPredictions());
            //Act
            var result = _customersController.GetCustomerOrderPredictions();
            //Assert
            result.Should().BeOfType<IActionResult>();
        }
    }
}
