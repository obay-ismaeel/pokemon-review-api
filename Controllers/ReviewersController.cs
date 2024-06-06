using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Abstractions;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.Filters;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Controllers;

public class ReviewersController : BaseController
{
    public ReviewersController(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerDto>))]
    [ProducesResponseType(400)]
    public IActionResult Index()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var categories = _mapper.Map<IEnumerable<ReviewerDto>>(_unitOfWork.Reviewers.GetAll());

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

        var reviewer = _mapper.Map<ReviewerDto>(_unitOfWork.Reviewers.GetById(id));

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

        if (!_unitOfWork.Reviewers.Exists(id))
            return NotFound();

        var reviews = _mapper.Map<IEnumerable<ReviewDto>>(_unitOfWork.Reviews.GetAllByReviewerId(id));

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

        if (!_unitOfWork.Reviewers.Create(reviewer))
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

        if (!_unitOfWork.Reviewers.Exists(id))
            return NotFound("There is no such Id!");

        var reviewer = _mapper.Map<Reviewer>(reviewerDto);

        if (!_unitOfWork.Reviewers.Update(reviewer))
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

        if (!_unitOfWork.Reviewers.Exists(id))
            return NotFound("Invalid reviewer ID!");

        _unitOfWork.Reviewers.Delete(id);

        return NoContent();
    }
}
