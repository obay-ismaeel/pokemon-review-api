using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Models;
using System.Linq;

namespace PokemonReviewApp.Repositories;

public class PokemonRepository : IPokemonRepository
{
    private readonly AppDbContext _context;

    public PokemonRepository(AppDbContext context)
    {
        _context = context;
    }

    public ICollection<Pokemon> GetAll() => _context.Pokemons.OrderBy(p => p.Name).ToList();

    public Pokemon GetById(int id) => _context.Pokemons.Find(id);

    public Pokemon GetByName(string name)
    {
        return _context.Pokemons.FirstOrDefault(p => p.Name == name);
    }

    public decimal GetPokemonRating(int id)
    {
        var reviews = _context.Reviews.Where(r => r.PokemonId == id).ToList();

        if (reviews.Count() <= 0)
            return 0;

        return (decimal)reviews.Average(r => r.Rating);
    }

    public bool PokemonExists(int id)
    {
        return _context.Pokemons.Any(p => p.Id == id);
    }
}
