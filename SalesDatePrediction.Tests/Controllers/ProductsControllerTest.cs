using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Controllers;
using SalesDatePrediction.Interfaces;
using SalesDatePrediction.Models;

namespace SalesDatePrediction.Tests.Controllers
{
    public class ProductsControllerTest
    {
        private IProductsRepository _productsRepository { get; set; }
        private IMapper _mapper { get; set; }
        private ProductsController _productsController { get; set; }
        public ProductsControllerTest()
        {
            //Dependencies
            _productsRepository = A.Fake<IProductsRepository>();

            //SUT
            _productsController = new ProductsController(_productsRepository, _mapper);
        }

        [Fact]
        public void ProductsController_GetProducts_ReturnsSuccess()
        {
            //Arrange
            var products = A.Fake<Products>();
            A.CallTo(() => _productsRepository.GetProducts());
            //Act
            var result = _productsController.GetProducts();
            //Assert
            result.Should().BeOfType<IActionResult>();
        }
    }
}
