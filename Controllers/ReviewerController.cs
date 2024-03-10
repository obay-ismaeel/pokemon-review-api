using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewerController : ControllerBase
{
    private readonly IReviewerRepository _reviewerRepository;
    private readonly IMapper _mapper;

    public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
    {
        _reviewerRepository = reviewerRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerDto>))]
    [ProducesResponseType(400)]
    public IActionResult Index()
    {
        var categories = _mapper.Map<IEnumerable<ReviewerDto>>(_reviewerRepository.GetAll());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(categories);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(ReviewerDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult Show(int id)
    {
        var reviewer = _mapper.Map<ReviewerDto>(_reviewerRepository.GetById(id));

        if (reviewer is null)
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(reviewer);
    }

    [HttpGet("{id}/reviews")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetReviewerReviews(int id)
    {
        if (!_reviewerRepository.ReviewerExists(id))
            return NotFound();

        var reviews = _mapper.Map<IEnumerable<ReviewDto>>(_reviewerRepository.GetReviewsByReviewerId(id));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(reviews);
    }

}
