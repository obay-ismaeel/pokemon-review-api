using Microsoft.EntityFrameworkCore;

namespace PokemonReviewApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}
