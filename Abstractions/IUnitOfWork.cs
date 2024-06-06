using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Abstractions;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IPokemonRepository Pokemons { get; }
    IReviewRepository Reviews { get; }
    IReviewerRepository Reviewers { get; }
    ICountryRepository Countries { get; }
    ICategoryRepository Categories { get; }
    IOwnerRepository Owners { get; }
    Task<int> CompletAsync();
    void Dispose();
}
