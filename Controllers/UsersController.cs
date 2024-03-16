using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserRepository _userRepository, IMapper _mapper) : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login(UserLoginRequest userLogin)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_userRepository.TryLogIn(userLogin.Email, userLogin.Password))
            return BadRequest("Invalid Credinatials!");

        return Ok("Welcome back!");
    }

    [HttpPost("register")]
    public IActionResult Register(UserRegisterRequest register)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (_userRepository.Exists(register.Email))
            return Conflict("Email already exists!");

        var user = _mapper.Map<User>(register);

        if (!_userRepository.Create(user))
        {
            ModelState.AddModelError("error", "Something went wrong");
            return StatusCode(500, ModelState);
        }

        return Ok("Created Successfully!");
    }
}
