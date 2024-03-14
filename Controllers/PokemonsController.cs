using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.Filters;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PokemonsController : ControllerBase
{
    private readonly IPokemonRepository _pokemonRepository;
    private readonly IMapper _mapper;

    public PokemonsController(IPokemonRepository pokemonRepository, IMapper mapper)
    {
        _pokemonRepository = pokemonRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<PokemonDto>) )]
    public IActionResult Index()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var pokemons = _mapper.Map<IEnumerable<PokemonDto>>(_pokemonRepository.GetAll());

        return Ok(pokemons);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200, Type = typeof(PokemonDto))]
    public IActionResult Show(int id)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var pokemon = _pokemonRepository.GetById(id);

        if (pokemon is null)
            return NotFound();

        var pokemonDto = _mapper.Map<PokemonDto>(pokemon);

        return Ok(pokemonDto);
    }

    [HttpGet("{id}/rating")]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200, Type = typeof(decimal))]
    public IActionResult GetPokemonRating(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_pokemonRepository.Exists(id))
            return NotFound();

        var rating = _pokemonRepository.GetRatingById(id);

        return Ok(rating);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public IActionResult Create(PokemonDto pokemonDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var pokemon = _mapper.Map<Pokemon>(pokemonDto);

        if (!_pokemonRepository.Create(pokemon))
        {
            ModelState.AddModelError("error", "Something went wrong");
            return StatusCode(500, ModelState);
        }

        return Created();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult Update(PokemonDto pokemonDto, int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (pokemonDto.Id != id)
            return BadRequest("The IDs provided don't match!");

        if (!_pokemonRepository.Exists(id))
            return NotFound("There is no such ID!");

        var pokemon = _mapper.Map<Pokemon>(pokemonDto);

        if (!_pokemonRepository.Update(pokemon))
        {
            ModelState.AddModelError("error", "Something went wrong");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [LogSensitiveAction]
    public IActionResult Delete(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_pokemonRepository.Exists(id))
            return NotFound("Invalid pokemon ID!");

        _pokemonRepository.Delete(id);

        return NoContent();
    }
}
