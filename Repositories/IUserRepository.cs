using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories;

public interface IUserRepository
{
    User? GetById(int id);
    User? GetByEmail(string email);
    ICollection<User> GetAll();
    bool Create(User user);
    bool Update(User user);
    bool Delete(int id);
    bool Exists(int id);
    bool Exists(string email);
    User? TryLogIn(string email, string password);
    bool Save();
}
