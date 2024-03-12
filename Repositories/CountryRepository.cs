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

    public bool CountryExists(string name)
    {
        name = name.ToLower().Trim();
        var country = _context.Countries.Where(c => c.Name.ToLower().Trim() == name).FirstOrDefault();
        return country is null ? false : true;
    }

    public bool Create(Country country)
    {
        _context.Countries.Add(country);
        return Save();
    }

    public bool Delete(int id)
    {
        var item = _context.Countries.Find(id);
        if (item != null)
            _context.Countries.Remove(item);
        return Save();
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

    public bool Save()
    {
        return _context.SaveChanges() > 0 ? true : false;
    }

    public bool Update(Country country)
    {
        _context.Countries.Update(country);
        return Save();
    }
}
