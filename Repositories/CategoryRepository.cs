﻿using Microsoft.EntityFrameworkCore;
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

    public Category GetById(int id)
    {
        return _context.Categories.Find(id);
    }

    public ICollection<Category> GetAll()
    {
        return _context.Categories.ToList();
    }

    public bool Create(Category category)
    {
        category.Id = null;
        _context.Categories.Add(category);
        return Save();
    }

    public bool Update(Category category)
    {
        _context.Categories.Update(category);
        return Save();
    }

    public bool Delete(int id)
    {
        var item = _context.Categories.Find(id);
        if (item != null)
            _context.Categories.Remove(item);
        return Save();
    }

    public bool Exists(int id)
    {
        return _context.Categories.Any(c => c.Id == id);
    }

    public bool Exists(string name)
    {
        name = name.ToLower().Trim();
        var category = _context.Categories.Where(c => c.Name.ToLower().Trim() == name).FirstOrDefault();
        return category is null ? false : true;
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0 ? true : false;
    }
}
