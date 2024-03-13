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

    public Pokemon GetById(int id)
    {
        return _context.Pokemons.Find(id);
    }
    
    public Pokemon GetByName(string name)
    {
        return _context.Pokemons.FirstOrDefault(p => p.Name == name);
    }
    
    public ICollection<Pokemon> GetAll()
    {
        return _context.Pokemons.OrderBy(p => p.Name).ToList();
    }
    
    public ICollection<Pokemon> GetAllByCategoryId(int id)
    {
        var category = _context.Categories.Include(c => c.Pokemons).SingleOrDefault(c => c.Id == id);

        return category?.Pokemons ?? new List<Pokemon>();
    }

    public ICollection<Pokemon> GetAllByOwnerId(int id)
    {
        return _context.Owners.Include(o => o.Pokemons).SingleOrDefault(p => p.Id == id)?.Pokemons;
    }
    
    public decimal GetRatingById(int id)
    {
        var reviews = _context.Reviews.Where(r => r.PokemonId == id).ToList();

        if (reviews.Count() <= 0)
            return 0;

        return (decimal)reviews.Average(r => r.Rating);
    }

    public bool Create(Pokemon pokemon)
    {
        _context.Pokemons.Add(pokemon);
        return _context.SaveChanges() > 0 ? true : false ;
    }

    public bool Update(Pokemon pokemon)
    {
        _context.Pokemons.Update(pokemon);
        return Save();
    }

    public bool Delete(int id)
    {
        var item = _context.Pokemons.Find(id);
        if (item != null)
            _context.Pokemons.Remove(item);
        return Save();
    }

    public bool Exists(int id)
    {
        return _context.Pokemons.Any(p => p.Id == id);
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0 ? true : false;
    }

}
