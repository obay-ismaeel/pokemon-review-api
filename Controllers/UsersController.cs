using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PokemonReviewApp.Abstractions;
using PokemonReviewApp.Auth;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PokemonReviewApp.Controllers;

public class UsersController : BaseController
{
    private readonly JwtOptions _jwtOptions;
    public UsersController(IMapper mapper, IUnitOfWork unitOfWork, JwtOptions jwtOptions) : base(mapper, unitOfWork)
    {
        _jwtOptions = jwtOptions;
    }

    [HttpPost("login")]
    public IActionResult Login(UserLoginRequest userLogin)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = _unitOfWork.Users.TryLogIn(userLogin.Email, userLogin.Password);

        if (user is null)
            return BadRequest("Invalid Credinatials!");

        var accessToken = IssueJwtToken(user);

        return Ok(accessToken);
    }

    [HttpPost("register")]
    public IActionResult Register(UserRegisterRequest register)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (_unitOfWork.Users.Exists(register.Email))
            return Conflict("Email already exists!");

        var user = _mapper.Map<User>(register);

        if (!_unitOfWork.Users.Create(user))
        {
            ModelState.AddModelError("error", "Something went wrong");
            return StatusCode(500, ModelState);
        }

        var accessToken = IssueJwtToken(_unitOfWork.Users.GetByEmail(user.Email)!);

        return Ok(accessToken);
    }

    private string IssueJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Signingkey)), SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.NameIdentifier, $"{user.Id}"),
                new(ClaimTypes.Email, user.Email)
            })
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        
        var accessToken = tokenHandler.WriteToken(securityToken);

        return accessToken;
    }
}
