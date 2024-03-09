using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories;

public interface IPokemonRepository
{
    ICollection<Pokemon> All();
    Pokemon GetById(int id);
    Pokemon GetByName(string name);
    decimal GetPokemonRating(int id);
    bool PokemonExists(int id);
}
