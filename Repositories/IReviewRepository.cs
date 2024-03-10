using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories;

public interface IReviewRepository
{
    ICollection<Review> GetAll();
    Review GetById(int id);
    ICollection<Review> GetAllByPokemonId(int id);
    bool ReviewExists(int id);
}
