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
        return Save();
    }

    public bool Update(Category category)
    {
        _context.Categories.Update(category);
        return Save();
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

    public bool Delete(Category category)
    {
        _context.Categories.Remove(category);
        return Save();
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0 ? true : false;
    }

    public bool Delete(int id)
    {
        var item = _context.Countries.Find(id);
        if (item != null)
            _context.Countries.Remove(item);
        return Save();
    }
}
