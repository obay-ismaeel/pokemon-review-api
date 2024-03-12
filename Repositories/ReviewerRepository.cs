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

    public bool Create(Reviewer reviewer)
    {
        _context.Reviewers.Add(reviewer);
        return _context.SaveChanges() > 0 ? true : false;
    }

    public bool Delete(int id)
    {
        var item = _context.Reviewers.Find(id);
        if (item != null)
            _context.Reviewers.Remove(item);
        return Save();
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

    public bool Save()
    {
        return _context.SaveChanges() > 0 ? true : false;
    }

    public bool Update(Reviewer reviewer)
    {
        _context.Reviewers.Update(reviewer);
        return Save();
    }
}
