using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;

    public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
    [ProducesResponseType(400)]
    public IActionResult Index()
    {
        var reviews = _mapper.Map<IEnumerable<ReviewDto>>(_reviewRepository.GetAll());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(reviews);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(ReviewDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult Show(int id)
    {
        var review = _mapper.Map<ReviewDto>(_reviewRepository.GetById(id));

        if (review is null)
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(review);
    }

    [HttpGet("bypokemonid/{id}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
    [ProducesResponseType(400)]
    public IActionResult GetPokemonReviews(int id)
    {
        var reviews = _mapper.Map<IEnumerable<ReviewDto>>(_reviewRepository.GetAllByPokemonId(id));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(reviews);
    }
}
