using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data.Config;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data;

public class AppDbContext : DbContext
{
    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Reviewer> Reviewers { get; set; }
    public DbSet<PokemonCategory> PokemonCategories { get; set; }
    public DbSet<PokemonOwner> PokemonOwners { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PokemonConfiguration).Assembly);
    }
}
