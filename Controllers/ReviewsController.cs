using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Abstractions;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.Filters;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Controllers;

public class ReviewsController : BaseController
{
    public ReviewsController(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
    [ProducesResponseType(400)]
    public IActionResult Index()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var reviews = _mapper.Map<IEnumerable<ReviewDto>>(_unitOfWork.Reviews.GetAll());

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

        var review = _mapper.Map<ReviewDto>(_unitOfWork.Reviews.GetById(id));

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

        var reviews = _mapper.Map<IEnumerable<ReviewDto>>(_unitOfWork.Reviews.GetAllByPokemonId(id));

        return Ok(reviews);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public IActionResult Create(ReviewDto reviewDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_unitOfWork.Reviewers.Exists(reviewDto.ReviewerId))
        {
            ModelState.AddModelError("error", "No such reviewer!");
            return BadRequest(ModelState);
        }

        if (!_unitOfWork.Pokemons.Exists(reviewDto.PokemonId))
        {
            ModelState.AddModelError("error", "No such pokemon!");
            return BadRequest(ModelState);
        }

        var review = _mapper.Map<Review>(reviewDto);

        if (!_unitOfWork.Reviews.Create(review))
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

        if (!_unitOfWork.Reviews.Exists(id))
            return NotFound("Invalid review ID!");
        
        if (!_unitOfWork.Reviewers.Exists(reviewDto.ReviewerId))
            return BadRequest("Invalid reviewer ID!");

        if (!_unitOfWork.Pokemons.Exists(reviewDto.PokemonId))
            return BadRequest("Invalid pokemon ID!");

        var review = _mapper.Map<Review>(reviewDto);

        if (!_unitOfWork.Reviews.Update(review))
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

        if (!_unitOfWork.Pokemons.Exists(id))
            return NotFound("Invalid pokemon ID!");

        _unitOfWork.Pokemons.Delete(id);

        return NoContent();
    }
}
