using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories;

public interface IReviewerRepository
{
    ICollection<Reviewer> GetAll();
    Reviewer GetById(int id);
    ICollection<Review> GetReviewsByReviewerId(int id);
    bool ReviewerExists(int id);
    bool Create(Reviewer reviewer);
}
