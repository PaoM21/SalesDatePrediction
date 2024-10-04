using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Interfaces;
using SalesDatePrediction.Models;

namespace SalesDatePrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeesRepository employeesRepository,
            IMapper mapper)
        {
            _employeesRepository = employeesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employees>))]
        public IActionResult GetEmployees()
        {
            var employees = _employeesRepository.GetEmployees();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employees);
        }
    }
}
