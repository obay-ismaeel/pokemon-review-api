using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories;

public interface IReviewRepository
{
    Review GetById(int id);
    ICollection<Review> GetAll();
    ICollection<Review> GetAllByPokemonId(int id);
    ICollection<Review> GetAllByReviewerId(int id);
    bool Create(Review review);
    bool Update(Review review);
    bool Delete(int id);
    bool Exists(int id);
    bool Save();
}
