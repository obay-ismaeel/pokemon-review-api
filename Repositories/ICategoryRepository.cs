using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories;

public interface ICategoryRepository
{
    ICollection<Category> GetAll();
    Category GetById(int id);
    ICollection<Pokemon> GetPokemonsByCategoryId(int id);
    bool CategoryExists(int id);

    bool CategoryExists(string name);
    bool Create(Category category);

}
