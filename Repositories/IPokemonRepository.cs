using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories;

public interface IPokemonRepository
{
    Pokemon GetById(int id);
    Pokemon GetByName(string name);
    ICollection<Pokemon> GetAll();
    ICollection<Pokemon> GetAllByOwnerId(int id);
    ICollection<Pokemon> GetAllByCategoryId(int id);
    decimal GetRatingById(int id);
    bool Create(Pokemon pokemon);
    bool Update(Pokemon pokemon);
    bool Delete(int id);
    bool Exists(int id);
    bool Save();
}
