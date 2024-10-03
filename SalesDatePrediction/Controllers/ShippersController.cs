using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Interfaces;
using SalesDatePrediction.Models;

namespace SalesDatePrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippersController : Controller
    {
        private readonly IShippersRepository _shippersRepository;
        private readonly IMapper _mapper;

        public ShippersController(IShippersRepository shippersRepository,
            IMapper mapper)
        {
            _shippersRepository = shippersRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Shippers>))]
        public IActionResult GetShippers()
        {
            //var pokemons = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons());
            var shippers = _shippersRepository.GetShippers();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(shippers);
        }
    }
}
