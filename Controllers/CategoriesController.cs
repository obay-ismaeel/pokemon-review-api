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
    private readonly IPokemonRepository _pokemonRepository;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoryRepository categoryRepository, IPokemonRepository pokemonRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _pokemonRepository = pokemonRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
    [ProducesResponseType(400)]
    public IActionResult Index()
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var categories = _mapper.Map<IEnumerable<CategoryDto>>( _categoryRepository.GetAll());

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

        var category = _mapper.Map<CategoryDto>(_categoryRepository.GetById(id));

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

        if(!_categoryRepository.Exists(id))
            return NotFound();

        var pokemons = _pokemonRepository.GetAllByCategoryId(id);
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

        if (_categoryRepository.Exists(categoryDto.Name))
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

        if (!_categoryRepository.Exists(id))
            return NotFound("The category doesn't exist!");

        var category = _mapper.Map<Category>(categoryDto);

        if (!_categoryRepository.Update(category))
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
    public IActionResult Delete(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_categoryRepository.Exists(id))
            return NotFound("Invalid category ID!");

        _categoryRepository.Delete(id);

        return NoContent();
    }
}
