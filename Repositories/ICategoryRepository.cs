using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories;

public interface ICategoryRepository
{
    ICollection<Category> GetAll();
    Category GetById(int id);
    ICollection<Pokemon> GetPokemonsByCategoryId(int id);
    bool Exists(int id);
    bool Exists(string name);
    bool Create(Category category);
    bool Update(Category category);
    bool Delete(int id);
    bool Save();

}
