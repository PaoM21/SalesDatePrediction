using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Controllers;
using SalesDatePrediction.Interfaces;
using SalesDatePrediction.Models;

namespace SalesDatePrediction.Tests.Controllers
{
    public class EmployeesControllerTest
    {
        private IEmployeesRepository _employeesRepository { get; set; }
        private IMapper _mapper { get; set; }
        private EmployeesController _employeesController { get; set; }
        public EmployeesControllerTest()
        {
            //Dependencies
            _employeesRepository = A.Fake<IEmployeesRepository>();

            //SUT
            _employeesController = new EmployeesController(_employeesRepository, _mapper);
        }

        [Fact]
        public void EmployeesController_GetEmployees_ReturnsSuccess()
        {
            //Arrange
            var employees = A.Fake<IEnumerable<Employees>>();
            A.CallTo(() => _employeesRepository.GetEmployees());
            //Act
            var result = _employeesController.GetEmployees();
            //Assert
            result.Should().BeOfType<IActionResult>();
        }
    }
}
