using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories;

public interface ICountryRepository
{
    Country GetById(int id);
    ICollection<Country> GetAll();
    Country GetByOwnerId(int id);
    bool Create(Country country);
    bool Update(Country country);
    bool Delete(int id);
    bool Exists(int id);
    bool Exists(string name);
    bool Save();
}
