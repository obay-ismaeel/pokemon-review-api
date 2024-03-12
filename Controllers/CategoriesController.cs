using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
    [ProducesResponseType(400)]
    public IActionResult Index()
    {
        var categories = _mapper.Map<IEnumerable<CategoryDto>>( _categoryRepository.GetAll());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(categories);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(CategoryDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult Show(int id)
    {
        var category = _mapper.Map<CategoryDto>(_categoryRepository.GetById(id));

        if (category is null)
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(category);
    }

    [HttpGet("{id}/pokemons")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<PokemonDto>))]
    [ProducesResponseType(400)]
    public IActionResult GetPokemonByCategoryId(int id)
    {
        if(!_categoryRepository.CategoryExists(id))
            return NotFound();

        var pokemons = _categoryRepository.GetPokemonsByCategoryId(id);
        var pokemonDtos = _mapper.Map<IEnumerable<PokemonDto>>(pokemons);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(pokemonDtos);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    public IActionResult Create(CategoryDto categoryDto)
    {
        if (_categoryRepository.CategoryExists(categoryDto.Name))
        {
            ModelState.AddModelError("error", "Category already exists!");
            return Conflict(ModelState);
        }

        var category = _mapper.Map<Category>(categoryDto);
        
        if (!_categoryRepository.Create(category))
        {
            ModelState.AddModelError("error", "Something went wrong");
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
    public IActionResult Update(CategoryDto categoryDto, int id)
    {
        if (categoryDto.Id != id)
            return BadRequest("The IDs provided don't match!");

        if (!_categoryRepository.CategoryExists(id))
            return NotFound("The category doesn't exist!");

        var category = _mapper.Map<Category>(categoryDto);

        if (!_categoryRepository.Update(category))
        {
            ModelState.AddModelError("error", "Something went wrong");
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
        if (!_categoryRepository.CategoryExists(id))
            return NotFound("Invalid category ID!");

        _categoryRepository.Delete(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return NoContent();
    }
}
