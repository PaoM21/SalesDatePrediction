using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Controllers;
using SalesDatePrediction.Interfaces;
using SalesDatePrediction.Models;

namespace SalesDatePrediction.Tests.Controllers
{
    public class OrdersControllerTest
    {
        private IOrdersRepository _ordersRepository { get; set; }
        private IProductsRepository _productsRepository { get; set; }
        private IMapper _mapper { get; set; }
        private OrdersController _ordersController { get; set; }
        public OrdersControllerTest()
        {
            //Dependencies
            _ordersRepository = A.Fake<IOrdersRepository>();
            _productsRepository = A.Fake<IProductsRepository>();

            //SUT
            _ordersController = new OrdersController(_ordersRepository, _productsRepository, _mapper);
        }

        [Fact]
        public void OrdersController_GetOrdersByCustom_ReturnsSuccess()
        {
            //Arrange
            var orders = A.Fake<Orders>();
            var custId = 1;
            A.CallTo(() => _ordersRepository.GetOrdersByCustom(custId));
            //Act
            var result = _ordersController.GetOrdersByCustom(custId);
            //Assert
            result.Should().BeOfType<IActionResult>();
        }
    }
}