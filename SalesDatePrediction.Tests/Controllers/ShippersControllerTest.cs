using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Controllers;
using SalesDatePrediction.Interfaces;
using SalesDatePrediction.Models;

namespace SalesDatePrediction.Tests.Controllers
{
    public class ShippersControllerTest
    {
        private IShippersRepository _shippersRepository { get; set; }
        private IMapper _mapper { get; set; }
        private ShippersController _shippersController { get; set; }
        public ShippersControllerTest()
        {
            //Dependencies
            _shippersRepository = A.Fake<IShippersRepository>();

            //SUT
            _shippersController = new ShippersController(_shippersRepository, _mapper);
        }

        [Fact]
        public void ShippersController_GetShippers_ReturnsSuccess()
        {
            //Arrange
            var shippers = A.Fake<Shippers>();
            A.CallTo(() => _shippersRepository.GetShippers());
            //Act
            var result = _shippersController.GetShippers();
            //Assert
            result.Should().BeOfType<IActionResult>();
        }
    }
}
