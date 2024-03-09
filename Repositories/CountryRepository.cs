using PokemonReviewApp.Data;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly AppDbContext _context;

    public CountryRepository(AppDbContext context)
    {
        _context = context;
    }

    public bool CountryExists(int id)
    {
        return _context.Countries.Any(c => c.Id == id);
    }

    public ICollection<Country> GetAll()
    {
        return _context.Countries.OrderBy(c => c.Name).ToList();
    }

    public Country GetById(int id)
    {
        return _context.Countries.Find(id);
    }

    public Country GetCountryByOwner(int id)
    {
        return _context.Owners.Where(o => o.Id == id).Select(o=>o.Country).FirstOrDefault();
    }

    public ICollection<Owner> GetOwnersByCountry(int id)
    {
        return _context.Owners.Where(o => o.CountryId == id).ToList();
    }
}
