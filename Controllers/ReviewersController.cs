using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.Filters;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReviewersController : ControllerBase
{
    private readonly IReviewerRepository _reviewerRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;

    public ReviewersController(IReviewerRepository reviewerRepository, IReviewRepository reviewRepository, IMapper mapper)
    {
        _reviewerRepository = reviewerRepository;
        _reviewRepository = reviewRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerDto>))]
    [ProducesResponseType(400)]
    public IActionResult Index()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var categories = _mapper.Map<IEnumerable<ReviewerDto>>(_reviewerRepository.GetAll());

        return Ok(categories);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(ReviewerDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult Show(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var reviewer = _mapper.Map<ReviewerDto>(_reviewerRepository.GetById(id));

        if (reviewer is null)
            return NotFound();

        return Ok(reviewer);
    }

    [HttpGet("{id}/reviews")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetReviewerReviews(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_reviewerRepository.Exists(id))
            return NotFound();

        var reviews = _mapper.Map<IEnumerable<ReviewDto>>(_reviewRepository.GetAllByReviewerId(id));

        return Ok(reviews);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public IActionResult Create(ReviewerDto reviewerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var reviewer = _mapper.Map<Reviewer>(reviewerDto);

        if (!_reviewerRepository.Create(reviewer))
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
    public IActionResult Update(ReviewerDto reviewerDto, int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (reviewerDto.Id != id)
            return BadRequest("The provided IDs don't match!");

        if (!_reviewerRepository.Exists(id))
            return NotFound("There is no such Id!");

        var reviewer = _mapper.Map<Reviewer>(reviewerDto);

        if (!_reviewerRepository.Update(reviewer))
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

        if (!_reviewerRepository.Exists(id))
            return NotFound("Invalid reviewer ID!");

        _reviewerRepository.Delete(id);

        return NoContent();
    }
}
