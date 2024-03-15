using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Models;
using System.Diagnostics.Metrics;

namespace PokemonReviewApp.Repositories;

public class OwnerRepository : IOwnerRepository
{
    private readonly AppDbContext _context;

    public OwnerRepository(AppDbContext context)
    {
        _context = context;
    }

    public Owner GetById(int id)
    {
        return _context.Owners.Find(id);
    }
    
    public ICollection<Owner> GetAll()
    {
        return _context.Owners.ToList();
    }
    
    public ICollection<Owner> GetAllByCountryId(int id)
    {
        return _context.Owners.Where(o => o.CountryId == id).ToList();
    }
    
    public ICollection<Owner> GetAllByPokemonId(int id)
    {
        return _context.Pokemons.Include(p => p.Owners).SingleOrDefault(p => p.Id == id)?.Owners;
    }

    public bool Create(Owner owner)
    {
        owner.Id = null;
        _context.Owners.Add(owner);
        return Save();
    }
    
    public bool Update(Owner owner)
    {
        _context.Owners.Update(owner);
        return Save();
    }

    public bool Delete(int id)
    {
        var item = _context.Owners.Find(id);
        if (item != null)
            _context.Owners.Remove(item);
        return Save();
    }

    public bool Exists(int id)
    {
        return _context.Owners.Any(o => o.Id == id);
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0 ? true : false;
    }

}
