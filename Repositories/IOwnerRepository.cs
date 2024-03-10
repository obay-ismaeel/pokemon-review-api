using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories;

public interface IOwnerRepository
{
    Owner GetById(int id);
    ICollection<Owner> GetAll();
    ICollection<Pokemon> GetPokemonByOwnerId(int id);
    ICollection<Owner> GetOwnersOfPokemon(int id);
    ICollection<Owner> GetOwnersByCountry(int id);
    bool OwnerExists(int id);
}
