using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IReviewerRepository _reviewerRepository;
    private readonly IPokemonRepository _pokemonRepository;
    private readonly IMapper _mapper;

    public ReviewsController(IReviewRepository reviewRepository, IPokemonRepository pokemonRepository, IReviewerRepository reviewerRepository, IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _pokemonRepository = pokemonRepository;
        _reviewerRepository = reviewerRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
    [ProducesResponseType(400)]
    public IActionResult Index()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var reviews = _mapper.Map<IEnumerable<ReviewDto>>(_reviewRepository.GetAll());

        return Ok(reviews);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(ReviewDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult Show(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var review = _mapper.Map<ReviewDto>(_reviewRepository.GetById(id));

        if (review is null)
            return NotFound();

        return Ok(review);
    }

    [HttpGet("/api/pokemon/{id}/reviews")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
    [ProducesResponseType(400)]
    public IActionResult GetPokemonReviews(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var reviews = _mapper.Map<IEnumerable<ReviewDto>>(_reviewRepository.GetAllByPokemonId(id));

        return Ok(reviews);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public IActionResult Create(ReviewDto reviewDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_reviewerRepository.Exists(reviewDto.ReviewerId))
        {
            ModelState.AddModelError("error", "No such reviewer!");
            return BadRequest(ModelState);
        }

        if (!_pokemonRepository.Exists(reviewDto.PokemonId))
        {
            ModelState.AddModelError("error", "No such pokemon!");
            return BadRequest(ModelState);
        }

        var review = _mapper.Map<Review>(reviewDto);

        if (!_reviewRepository.Create(review))
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
    public IActionResult Update(ReviewDto reviewDto, int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if(reviewDto.Id != id)
            return BadRequest("The IDs provided don't match!");

        if (!_reviewRepository.Exists(id))
            return NotFound("Invalid review ID!");
        
        if (!_reviewerRepository.Exists(reviewDto.ReviewerId))
            return BadRequest("Invalid reviewer ID!");

        if (!_pokemonRepository.Exists(reviewDto.PokemonId))
            return BadRequest("Invalid pokemon ID!");

        var review = _mapper.Map<Review>(reviewDto);

        if (!_reviewRepository.Update(review))
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

        if (!_pokemonRepository.Exists(id))
            return NotFound("Invalid pokemon ID!");

        _pokemonRepository.Delete(id);

        return NoContent();
    }
}
