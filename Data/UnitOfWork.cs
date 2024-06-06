using PokemonReviewApp.Abstractions;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Users = new UserRepository(context);
        Pokemons = new PokemonRepository(context);
        Owners = new OwnerRepository(context);
        Reviewers = new ReviewerRepository(context);
        Reviews = new ReviewRepository(context);
        Countries = new CountryRepository(context);
        Categories = new CategoryRepository(context);
    }

    public IUserRepository Users { get; private set; }

    public IPokemonRepository Pokemons { get; private set; }

    public IReviewRepository Reviews { get; private set; }

    public IReviewerRepository Reviewers { get; private set; }

    public ICountryRepository Countries { get; private set; }

    public ICategoryRepository Categories { get; private set; }

    public IOwnerRepository Owners { get; private set; }

    public async Task<int> CompletAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
