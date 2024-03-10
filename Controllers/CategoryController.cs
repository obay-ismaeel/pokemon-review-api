using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
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
}
