using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Abstractions;
using PokemonReviewApp.Data;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.Filters;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Controllers;

public class CategoriesController : BaseController
{
    public CategoriesController(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
    [ProducesResponseType(400)]
    public IActionResult Index()
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var categories = _mapper.Map<IEnumerable<CategoryDto>>( _unitOfWork.Categories.GetAll());

        return Ok(categories);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(CategoryDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult Show(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var category = _mapper.Map<CategoryDto>(_unitOfWork.Categories.GetById(id));

        if (category is null)
            return NotFound();

        return Ok(category);
    }

    [HttpGet("{id}/pokemons")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<PokemonDto>))]
    [ProducesResponseType(400)]
    public IActionResult GetPokemonByCategoryId(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if(!_unitOfWork.Categories.Exists(id))
            return NotFound();

        var pokemons = _unitOfWork.Pokemons.GetAllByCategoryId(id);
        var pokemonDtos = _mapper.Map<IEnumerable<PokemonDto>>(pokemons);

        return Ok(pokemonDtos);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    public IActionResult Create(CategoryDto categoryDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (_unitOfWork.Categories.Exists(categoryDto.Name))
        {
            ModelState.AddModelError("error", "Category already exists!");
            return Conflict(ModelState);
        }
        
        var category = _mapper.Map<Category>(categoryDto);

        if (!_unitOfWork.Categories.Create(category))
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
    public IActionResult Update(CategoryDto categoryDto, int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (categoryDto.Id != id)
            return BadRequest("The IDs provided don't match!");

        if (!_unitOfWork.Categories.Exists(id))
            return NotFound("The category doesn't exist!");

        var category = _mapper.Map<Category>(categoryDto);

        if (!_unitOfWork.Categories.Update(category))
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

        if (!_unitOfWork.Categories.Exists(id))
            return NotFound("Invalid category ID!");

        _unitOfWork.Categories.Delete(id);

        return NoContent();
    }
}
