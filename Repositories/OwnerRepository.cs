using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories;

public class OwnerRepository : IOwnerRepository
{
    private readonly AppDbContext _context;

    public OwnerRepository(AppDbContext context)
    {
        _context = context;
    }

    public bool Create(Owner owner)
    {
        _context.Owners.Add(owner);
        return Save();
    }

    public bool Delete(Owner owner)
    {
        _context.Owners.Remove(owner);
        return Save();
    }

    public bool Delete(int id)
    {
        var item = _context.Owners.Find(id);
        if (item != null)
            _context.Owners.Remove(item);
        return Save();
    }

    public ICollection<Owner> GetAll()
    {
        return _context.Owners.ToList();
    }

    public Owner GetById(int id)
    {
        return _context.Owners.Find(id);
    }

    public ICollection<Owner> GetOwnersByCountry(int id)
    {
        return _context.Owners.Where(o => o.CountryId == id).ToList();
    }

    public ICollection<Owner> GetOwnersOfPokemon(int id)
    {
        return _context.Pokemons.Include(p => p.Owners).SingleOrDefault(p => p.Id == id)?.Owners;
    }

    public ICollection<Pokemon> GetPokemonByOwnerId(int id)
    {
        return _context.Owners.Include(o => o.Pokemons).SingleOrDefault(p => p.Id == id)?.Pokemons;
    }


    public bool OwnerExists(int id)
    {
        return _context.Owners.Any(o => o.Id == id);
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0 ? true : false;
    }

    public bool Update(Owner owner)
    {
        _context.Owners.Update(owner);
        return Save();
    }
}
