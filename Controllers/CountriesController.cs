using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Abstractions;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.Filters;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Controllers;

public class CountriesController : BaseController
{
    public CountriesController(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CountryDto>))]
    [ProducesResponseType(400)]
    public IActionResult Index()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var countries = _mapper.Map<IEnumerable<CountryDto>>(_unitOfWork.Countries.GetAll());

        return Ok(countries);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(CountryDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult Show(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var country = _mapper.Map<CountryDto>(_unitOfWork.Countries.GetById(id));

        if (country is null)
            return NotFound();

        return Ok(country);
    }

    [HttpGet("/api/owners/{id}/country")]
    [ProducesResponseType(200, Type = typeof(CountryDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetCountryByOwner(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var country = _mapper.Map<CountryDto>(_unitOfWork.Countries.GetByOwnerId(id));

        if (country is null)
            return NotFound();

        return Ok(country);
    }

    [HttpGet("{id}/owners")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDto>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetOwnersByCountry(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if(!_unitOfWork.Countries.Exists(id))
            return NotFound();

        var owners = _mapper.Map<IEnumerable<OwnerDto>>(_unitOfWork.Owners.GetAllByCountryId(id));

        return Ok(owners);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    public IActionResult Create(CountryDto countryDto)
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (_unitOfWork.Countries.Exists(countryDto.Name))
        {
            ModelState.AddModelError("error", "Country already exists!");
            return Conflict(ModelState);
        }

        var country = _mapper.Map<Country>(countryDto);

        if (!_unitOfWork.Countries.Create(country))
        {
            ModelState.AddModelError("error", "Something went wrong!");
            return StatusCode(500, ModelState);
        }

        return Created();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult Update(CountryDto countryDto, int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (countryDto.Id != id)
            return BadRequest("The provided IDs don't match!");

        if (!_unitOfWork.Countries.Exists(id))
            return NotFound("There is no such Id!");

        var country = _mapper.Map<Country>(countryDto);

        if (!_unitOfWork.Countries.Update(country))
        {
            ModelState.AddModelError("error", "Something went wrong!");
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

        if (!_unitOfWork.Countries.Exists(id))
            return NotFound("Invalid country ID!");

        _unitOfWork.Countries.Delete(id);

        return NoContent();
    }
}
