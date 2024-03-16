using PokemonReviewApp.Data;
using PokemonReviewApp.Helper;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories;

public class UserRepository(AppDbContext _context) : IUserRepository
{
    public User? GetById(int id)
    {
        return _context.Users.Find(id);
    }
    
    public User? GetByEmail(string email)
    {
        return _context.Users.Where(u => u.Email == email).SingleOrDefault();
    }
    
    public User? TryLogIn(string email, string password)
    {
        var user = _context.Users.Where(u => u.Email == email).SingleOrDefault();
        if ( user is null ) return null;
        return PasswordHasher.VerifyPassword(password, user.Password) ? user : null;
    }
    
    public ICollection<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public bool Create(User user)
    {
        user.Password = PasswordHasher.HashPassword(user.Password);
        user.Id = null;
        _context.Users.Add(user);
        return Save();
    }

    public bool Update(User user)
    {
        _context.Users.Update(user);
        return Save();
    }

    public bool Delete(int id)
    {
        var item = _context.Users.Find(id);
        if(item is not null)
            _context.Users.Remove(item);
        return Save();
    }

    public bool Exists(int id)
    {
        return _context.Users.Find(id) is null ? false : true;
    }

    public bool Exists(string email)
    {
        var user = _context.Users.Where(u => u.Email == email).SingleOrDefault();
        return user is null ? false : true;
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0 ? true : false;
    }
}
