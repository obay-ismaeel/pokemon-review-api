using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories;

public interface IReviewerRepository
{
    Reviewer GetById(int id);
    ICollection<Reviewer> GetAll();
    bool Create(Reviewer reviewer);
    bool Update(Reviewer reviewer);
    bool Delete(int id);
    bool Exists(int id);
    bool Save();
}
