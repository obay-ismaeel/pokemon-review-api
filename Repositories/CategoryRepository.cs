using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public bool Create(Category category)
    {
        _context.Categories.Add(category);
        return _context.SaveChanges() > 0 ? true : false;
    }

    public bool CategoryExists(int id)
    {
        return _context.Categories.Any(c => c.Id == id);
    }

    public bool CategoryExists(string name)
    {
        name = name.ToLower().Trim();
        var category = _context.Categories.Where(c => c.Name.ToLower().Trim() == name).FirstOrDefault();
        return category is null ? false : true;
    }

    public ICollection<Category> GetAll()
    {
        return _context.Categories.ToList();
    }

    public Category GetById(int id)
    {
        return _context.Categories.Find(id);
    }

    public ICollection<Pokemon> GetPokemonsByCategoryId(int id)
    {
        var category = _context.Categories.Include(c => c.Pokemons).SingleOrDefault(c => c.Id == id);
        
        return category?.Pokemons ?? new List<Pokemon>();
    }
}
