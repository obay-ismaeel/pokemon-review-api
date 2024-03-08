using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        context.Database.EnsureCreated();
        
        context.Countries.AddRange(LoadCountries());
        context.Reviewers.AddRange(LoadReviewers());
        context.Owners.AddRange(LoadOwners());
        context.Categories.AddRange(LoadCategories());
        context.Pokemons.AddRange(LoadPokemons());
        context.Reviews.AddRange(LoadReviews());
        context.PokemonCategories.AddRange(LoadPokemonCategories());
        context.PokemonOwners.AddRange(LoadPokemonOwners());

        context.SaveChanges();
    }

    public static IEnumerable<Pokemon> LoadPokemons() => new List<Pokemon>()
    {
        new Pokemon{Id=1, Name="Pikachu", BirthDate=DateTime.Now.AddDays(-10)},
        new Pokemon{Id=2, Name="Tawanna", BirthDate=DateTime.Now.AddDays(-20)},
        new Pokemon{Id=3, Name="Gifoof", BirthDate=DateTime.Now.AddDays(-30)},
        new Pokemon{Id=4, Name="Flying Charmander", BirthDate=DateTime.Now.AddDays(-100)},
        new Pokemon{Id=5, Name="Snakey", BirthDate=DateTime.Now.AddDays(-4)},
        new Pokemon{Id=6, Name="Sonic", BirthDate=DateTime.Now.AddDays(-11)},
    };

    public static IEnumerable<Country> LoadCountries() => new List<Country>()
    {
        new Country{Id=1, Name="USA"},
        new Country{Id=2, Name="France"},
        new Country{Id=3, Name="Germany"},
        new Country{Id=4, Name="China"},
        new Country{Id=5, Name="Syria"},
    };

    public static IEnumerable<Category> LoadCategories() => new List<Category>()
    {
        new Category{Id=1,Name="Dragon"},
        new Category{Id=2,Name="Ghost"},
        new Category{Id=3,Name="Fairy"},
        new Category{Id=4,Name="Electric"},
        new Category{Id=5,Name="Fire"},
    };

    public static IEnumerable<Owner> LoadOwners() => new List<Owner>()
    {
        new Owner{Id=1,Name="Obay", CountryId=1 , Gym="What the hell is a gym"},
        new Owner{Id=2,Name="Ahmad", CountryId=3, Gym="What the hell is a gym"},
        new Owner{Id=3,Name="Hasan", CountryId=3, Gym="What the hell is a gym"},
        new Owner{Id=4,Name="Mahmoud", CountryId=2, Gym="What the hell is a gym"},
        new Owner{Id=5,Name="Haidar", CountryId=2, Gym="What the hell is a gym"},
    };

    public static IEnumerable<Review> LoadReviews() => new List<Review>()
    {
        new Review{Id=1, Title="Bad AF", Text="It made me lose a whole game", Rating=1, PokemonId=1, ReviewerId=2},
        new Review{Id=2, Title="Good AF", Text="It saved me at the last sec", Rating=5, PokemonId=1, ReviewerId=2},
        new Review{Id=3, Title="Not bad", Text="It looks like a good one", Rating=3, PokemonId=2, ReviewerId=1},
        new Review{Id=4, Title="A little complex", Text="It's capabilities are confusing", Rating=2, PokemonId=3, ReviewerId=3},
        new Review{Id=5, Title="Bad AF", Text="It made me lose a whole game", Rating=1, PokemonId=4, ReviewerId=4},
        new Review{Id=6, Title="OP AF", Text="Someone should nerf this", Rating=4, PokemonId=5, ReviewerId=5},
        new Review{Id=7, Title="Meh", Text="Average and boring", Rating=3, PokemonId=5, ReviewerId=3},
    };

    public static IEnumerable<Reviewer> LoadReviewers() => new List<Reviewer>()
    {
        new Reviewer{Id=1, FirstName="Bassma", LastName="Gbilie"},
        new Reviewer{Id=2, FirstName="Slma", LastName = "Gbilie"},
        new Reviewer{Id=3, FirstName="Selman", LastName = "Gbilie"},
        new Reviewer{Id=4, FirstName="Ghaitha", LastName = "Gbilie"},
        new Reviewer{Id=5, FirstName="Yesra", LastName = "Gbilie"},
    };

    public static IEnumerable<PokemonCategory> LoadPokemonCategories() => new List<PokemonCategory>()
    {
        new PokemonCategory {CategoryId=1,PokemonId=1 },
        new PokemonCategory {CategoryId=1,PokemonId=2 },
        new PokemonCategory {CategoryId=2,PokemonId=3 },
        new PokemonCategory {CategoryId=3,PokemonId=3 },
        new PokemonCategory {CategoryId=3,PokemonId=5 },
        new PokemonCategory {CategoryId=4,PokemonId=4 },
        new PokemonCategory {CategoryId=5,PokemonId=1 },
    };

    public static IEnumerable<PokemonOwner> LoadPokemonOwners() => new List<PokemonOwner>()
    {
        new PokemonOwner {OwnerId = 1, PokemonId=1 },
        new PokemonOwner {OwnerId = 1, PokemonId=2 },
        new PokemonOwner {OwnerId = 1, PokemonId=3 },
        new PokemonOwner {OwnerId = 2, PokemonId=2 },
        new PokemonOwner {OwnerId = 3, PokemonId=3 },
        new PokemonOwner {OwnerId = 4, PokemonId=4 },
        new PokemonOwner {OwnerId = 5, PokemonId=5 },
        new PokemonOwner {OwnerId = 5, PokemonId=1 },
    };
}
