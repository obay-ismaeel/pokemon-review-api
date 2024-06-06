using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Abstractions;

namespace PokemonReviewApp.Controllers;

[ApiController]
[Route("api/[controller]")]

public class BaseController : ControllerBase
{
    protected readonly IMapper _mapper;
    protected readonly IUnitOfWork _unitOfWork;

    public BaseController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
}
