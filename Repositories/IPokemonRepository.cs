using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories;

public interface IPokemonRepository
{
    ICollection<Pokemon> GetAll();
    Pokemon GetById(int id);
    Pokemon GetByName(string name);
    decimal GetRatingById(int id);
    bool Exists(int id);
    bool Create(Pokemon pokemon);
    bool Update(Pokemon pokemon);
    bool Delete(int id);
    bool Save();
}
