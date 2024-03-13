using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories;

public interface IOwnerRepository
{
    Owner GetById(int id);
    ICollection<Owner> GetAll();
    ICollection<Owner> GetAllByPokemonId(int id);
    ICollection<Owner> GetAllByCountryId(int id);
    bool Create(Owner owner);
    bool Update(Owner owner);
    bool Delete(int id);
    bool Exists(int id);
    bool Save();
}
