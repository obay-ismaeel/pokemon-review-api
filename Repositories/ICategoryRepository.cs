using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories;

public interface ICategoryRepository
{
    Category GetById(int id);
    ICollection<Category> GetAll();
    bool Create(Category category);
    bool Update(Category category);
    bool Delete(int id);
    bool Exists(int id);
    bool Exists(string name);
    bool Save();

}
