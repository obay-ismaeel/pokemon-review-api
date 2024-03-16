using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.Filters;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OwnersController : ControllerBase
{
    private readonly IOwnerRepository _ownerRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IPokemonRepository _pokemonRepository;
    private readonly IMapper _mapper;

    public OwnersController(IOwnerRepository ownerRepository, ICountryRepository countryRepository, IPokemonRepository pokemonRepository, IMapper mapper)
    {
        _ownerRepository = ownerRepository;
        _countryRepository = countryRepository;
        _pokemonRepository = pokemonRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDto>))]
    [ProducesResponseType(400)]
    public IActionResult Index()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var owners = _mapper.Map<IEnumerable<OwnerDto>>(_ownerRepository.GetAll());

        return Ok(owners);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(OwnerDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult Show(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetById(id));

        if (owner is null)
            return NotFound();

        return Ok(owner);
    }

    [HttpGet("{id}/pokemons")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<PokemonDto>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetOwnerPokemons(int id)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_ownerRepository.Exists(id))
            return NotFound();
        
        var pokemons = _mapper.Map<IEnumerable<PokemonDto>>(_pokemonRepository.GetAllByOwnerId(id));

        return Ok(pokemons);
    }

    [HttpGet("/api/pokemons/{id}/owners")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDto>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetPokemonOwners(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var owners = _mapper.Map<IEnumerable<OwnerDto>>(_ownerRepository.GetAllByPokemonId(id));

        return Ok(owners);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public IActionResult Create(OwnerDto ownerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_countryRepository.Exists(ownerDto.CountryId))
        {
            ModelState.AddModelError("error", "No such country is found!");
            return BadRequest(ModelState);
        }

        var owner = _mapper.Map<Owner>(ownerDto);

        if (!_ownerRepository.Create(owner))
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
    public IActionResult Update(OwnerDto ownerDto, int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (ownerDto.Id != id)
            return BadRequest("The provided IDs doesn't match!");

        if (!_ownerRepository.Exists(id))
            return NotFound("Invalid owner ID!");

        if (!_countryRepository.Exists(ownerDto.CountryId))
            return BadRequest("Invalid country ID!");

        var owner = _mapper.Map<Owner>(ownerDto);

        if (!_ownerRepository.Update(owner))
        {
            ModelState.AddModelError("error", "Something went wrong");
            return StatusCode(500, ModelState);
        }

        return Created();
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

        if (!_ownerRepository.Exists(id))
            return NotFound("Invalid owner ID!");

        _ownerRepository.Delete(id);

        return NoContent();
    }
}
