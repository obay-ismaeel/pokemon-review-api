using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories;

public interface ICountryRepository
{
    ICollection<Country> GetAll();
    Country GetById(int id);
    Country GetByOwnerId(int id);
    ICollection<Owner> GetOwnersByCountry(int id);
    bool Exists(int id);
    bool Exists(string name);
    bool Create(Country country);
    bool Update(Country country);
    bool Delete(int id);
    bool Save();
}
