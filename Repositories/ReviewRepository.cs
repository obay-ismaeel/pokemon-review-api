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
}
