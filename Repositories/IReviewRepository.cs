using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories;

public interface IReviewRepository
{
    ICollection<Review> GetAll();
    Review GetById(int id);
    ICollection<Review> GetAllByPokemonId(int id);
    bool Exists(int id);
    bool Create(Review review);
    bool Update(Review review);
    bool Delete(int id);
    bool Save();
}
