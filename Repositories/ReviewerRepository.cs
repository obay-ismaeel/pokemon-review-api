using PokemonReviewApp.Data;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories;

public class ReviewerRepository : IReviewerRepository
{
    private readonly AppDbContext _context;

    public ReviewerRepository(AppDbContext context)
    {
        _context = context;
    }

    public ICollection<Reviewer> GetAll()
    {
        return _context.Reviewers.ToList();
    }

    public Reviewer GetById(int id)
    {
        return _context.Reviewers.Find(id);
    }

    public ICollection<Review> GetReviewsByReviewerId(int id)
    {
        return _context.Reviews.Where(r => r.ReviewerId == id).ToList();
    }

    public bool ReviewerExists(int id)
    {
        return _context.Reviewers.Any(r => r.Id == id);
    }
}
