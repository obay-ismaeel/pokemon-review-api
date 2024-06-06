using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data;

namespace PokemonReviewApp.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SeedController : ControllerBase
{
    private readonly AppDbContext _context;

    public SeedController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Seed()
    {
        DbSeeder.CreateAndSeedDb(_context);
        return Ok();
    }
}
