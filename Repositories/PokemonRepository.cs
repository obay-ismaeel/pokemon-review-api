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

    public bool Create(Pokemon pokemon)
    {
        _context.Pokemons.Add(pokemon);
        return _context.SaveChanges() > 0 ? true : false ;
    }

    public bool Delete(int id)
    {
        var item = _context.Pokemons.Find(id);
        if (item != null)
            _context.Pokemons.Remove(item);
        return Save();
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

    public bool Save()
    {
        return _context.SaveChanges() > 0 ? true : false;
    }

    public bool Update(Pokemon pokemon)
    {
        _context.Pokemons.Update(pokemon);
        return Save();
    }
}
