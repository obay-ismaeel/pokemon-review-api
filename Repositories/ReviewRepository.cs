using PokemonReviewApp.Data;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly AppDbContext _context;

    public ReviewRepository(AppDbContext context)
    {
        _context = context;
    }

    public bool Create(Review review)
    {
        _context.Reviews.Add(review);
        return _context.SaveChanges() > 0 ? true : false;
    }

    public bool Delete(int id)
    {
        var item = _context.Reviews.Find(id);
        if (item != null)
            _context.Reviews.Remove(item);
        return Save();
    }

    public ICollection<Review> GetAll()
    {
        return _context.Reviews.ToList();
    }

    public ICollection<Review> GetAllByPokemonId(int id)
    {
        return _context.Reviews.Where(r => r.PokemonId == id).ToList();
    }

    public Review GetById(int id)
    {
        return _context.Reviews.Find(id);
    }

    public bool ReviewExists(int id)
    {
        return _context.Reviews.Any(r => r.Id == id);
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0 ? true : false;
    }

    public bool Update(Review review)
    {
        _context.Reviews.Update(review);
        return Save();
    }
}
