using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICountryRepository _countryRepository;

    public CountriesController(IMapper mapper, ICountryRepository countryRepository)
    {
        _mapper = mapper;
        _countryRepository = countryRepository;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CountryDto>))]
    [ProducesResponseType(400)]
    public IActionResult Index()
    {
        var countries = _mapper.Map<IEnumerable<CountryDto>>(_countryRepository.GetAll());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(countries);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(CountryDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult Show(int id)
    {
        var country = _mapper.Map<CountryDto>(_countryRepository.GetById(id));

        if (country is null)
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(country);
    }

    [HttpGet("/api/owners/{id}/country")]
    [ProducesResponseType(200, Type = typeof(CountryDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetCountryByOwner(int id)
    {
        var country = _mapper.Map<CountryDto>(_countryRepository.GetCountryByOwner(id));

        if (country is null)
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(country);
    }

    [HttpGet("{id}/owners")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDto>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetOwnersByCountry(int id)
    {
        if(!_countryRepository.CountryExists(id))
            return NotFound();

        var owners = _mapper.Map<IEnumerable<OwnerDto>>(_countryRepository.GetOwnersByCountry(id));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(owners);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    public IActionResult Create(CountryDto countryDto)
    {
        if (_countryRepository.CountryExists(countryDto.Name))
        {
            ModelState.AddModelError("error", "Country already exists!");
            return Conflict(ModelState);
        }

        var country = _mapper.Map<Country>(countryDto);

        if (!_countryRepository.Create(country))
        {
            ModelState.AddModelError("error", "Something went wrong!");
            return StatusCode(500, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Created();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult Update(CountryDto countryDto, int id)
    {
        if (countryDto.Id != id)
            return BadRequest("The provided IDs don't match!");

        if (!_countryRepository.CountryExists(id))
            return NotFound("There is no such Id!");

        var country = _mapper.Map<Country>(countryDto);

        if (!_countryRepository.Update(country))
        {
            ModelState.AddModelError("error", "Something went wrong!");
            return StatusCode(500, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult Delete(int id)
    {
        if (!_countryRepository.CountryExists(id))
            return NotFound("Invalid country ID!");

        _countryRepository.Delete(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return NoContent();
    }
}
