using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Dto;
using SalesDatePrediction.Interfaces;

namespace SalesDatePrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IMapper _mapper;

        public CustomersController(ICustomersRepository customersRepository,
            IMapper mapper)
        {
            _customersRepository = customersRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderPredictionDto>))]
        public IActionResult GetCustomerOrderPredictions()
        {
            var orderPredictions = _customersRepository.GetCustomerOrderPredictions();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orderPredictions);
        }
    }
}
