using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories;

public interface IReviewerRepository
{
    ICollection<Reviewer> GetAll();
    Reviewer GetById(int id);
    ICollection<Review> GetReviewsByReviewerId(int id);
    bool Exists(int id);
    bool Create(Reviewer reviewer);
    bool Update(Reviewer reviewer);
    bool Delete(int id);
    bool Save();
}
