using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Dto;
using SalesDatePrediction.Interfaces;
using SalesDatePrediction.Models;
using SalesDatePrediction.Repository;

namespace SalesDatePrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public OrdersController(
            IOrdersRepository ordersRepository,
            IProductsRepository productsRepository,
            IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        [HttpGet("{custId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Orders>))]
        [ProducesResponseType(400)]
        public IActionResult GetOrdersByCustom(int custId)
        {
            if (!_ordersRepository.CustomerExists(custId))
                return NotFound();

            //var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(orderName));
            var ordersByCustom = _ordersRepository.GetOrdersByCustom(custId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ordersByCustom);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOrderWithProduct([FromBody] OrderProductDto orderProduct)
        {
            if (orderProduct == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var pokemonMap = _mapper.Map<Pokemon>(orderCreate);
            var orderMap = orderProduct;
            var OrderCreated = _ordersRepository.CreateOrderWithProduct(orderMap);

            if (OrderCreated == null || OrderCreated == default(int))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created, orden con Id " + OrderCreated);
        }
    }
}
