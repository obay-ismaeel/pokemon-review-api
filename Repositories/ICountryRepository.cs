using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories;

public interface ICountryRepository
{
    ICollection<Country> GetAll();
    Country GetById(int id);
    Country GetCountryByOwner(int id);
    ICollection<Owner> GetOwnersByCountry(int id);
    bool CountryExists(int id);
    bool CountryExists(string name);
    bool Create(Country country);
}
