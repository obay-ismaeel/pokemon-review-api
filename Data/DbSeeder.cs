using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data;

public static class DbSeeder
{
    public static void CreateAndSeedDb(AppDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        context.Users.AddRange(LoadUsers());
        context.Categories.AddRange(LoadCategories());
        context.Countries.AddRange(LoadCountries());
        context.Reviewers.AddRange(LoadReviewers());
        context.Owners.AddRange(LoadOwners());
        context.Pokemons.AddRange(LoadPokemons());
        context.Reviews.AddRange(LoadReviews());
        context.SaveChanges();
        
        context.PokemonOwners.AddRange(LoadPokemonOwners());
        context.PokemonCategories.AddRange(LoadPokemonCategories());
        context.SaveChanges();
    }

    public static IEnumerable<Pokemon> LoadPokemons() => new List<Pokemon>()
    {
        new Pokemon{ Name="Pikachu", BirthDate=DateTime.Now.AddDays(-10)},
        new Pokemon{ Name="Tawanna", BirthDate=DateTime.Now.AddDays(-20)},
        new Pokemon{ Name="Gifoof", BirthDate=DateTime.Now.AddDays(-30)},
        new Pokemon{ Name="Flying Charmander", BirthDate=DateTime.Now.AddDays(-100)},
        new Pokemon{ Name="Snakey", BirthDate=DateTime.Now.AddDays(-4)},
        new Pokemon{ Name="Sonic", BirthDate=DateTime.Now.AddDays(-11)},
    };

    public static IEnumerable<Country> LoadCountries() => new List<Country>()
    {
        new Country{ Name="USA"},
        new Country{ Name="France"},
        new Country{ Name="Germany"},
        new Country{ Name="China"},
        new Country{ Name="Syria"},
    };

    public static IEnumerable<Category> LoadCategories() => new List<Category>()
    {
        new Category{Name="Dragon"},
        new Category{Name="Ghost"},
        new Category{Name="Fairy"},
        new Category{Name="Electric"},
        new Category{Name="Fire"},
    };

    public static IEnumerable<Owner> LoadOwners() => new List<Owner>()
    {
        new Owner{Name="Obay", CountryId=1 , Gym="What the hell is a gym"},
        new Owner{Name="Ahmad", CountryId=3, Gym="What the hell is a gym"},
        new Owner{Name="Hasan", CountryId=3, Gym="What the hell is a gym"},
        new Owner{Name="Mahmoud", CountryId=2, Gym="What the hell is a gym"},
        new Owner{Name="Haidar", CountryId=2, Gym="What the hell is a gym"},
    };

    public static IEnumerable<Review> LoadReviews() => new List<Review>()
    {
        new Review{ Title="Bad AF", Text="It made me lose a whole game", Rating=1, PokemonId=1, ReviewerId=2},
        new Review{ Title="Good AF", Text="It saved me at the last sec", Rating=4, PokemonId=1, ReviewerId=2},
        new Review{ Title="Not bad", Text="It looks like a good one", Rating=3, PokemonId=2, ReviewerId=1},
        new Review{ Title="A little complex", Text="It's capabilities are confusing", Rating=2, PokemonId=3, ReviewerId=3},
        new Review{ Title="Bad AF", Text="It made me lose a whole game", Rating=1, PokemonId=4, ReviewerId=4},
        new Review{ Title="OP AF", Text="Someone should nerf this", Rating=4, PokemonId=5, ReviewerId=5},
        new Review{ Title="Meh", Text="Average and boring", Rating=3, PokemonId=5, ReviewerId=3},
    };

    public static IEnumerable<Reviewer> LoadReviewers() => new List<Reviewer>()
    {
        new Reviewer{ FirstName="Bassma", LastName="Gbilie"},
        new Reviewer{ FirstName="Slma", LastName = "Gbilie"},
        new Reviewer{ FirstName="Selman", LastName = "Gbilie"},
        new Reviewer{ FirstName="Ghaitha", LastName = "Gbilie"},
        new Reviewer{ FirstName="Yesra", LastName = "Gbilie"},
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

    public static IEnumerable<User> LoadUsers() => new List<User>()
    {
        new User{FullName="Obay Ismaeel", Email="obayhany@gmail.com", BirthDate = new DateTime(2001,8,31), IsAdmin = true, Password = "password"},
        new User{FullName = "Ahmad Mo", Email="any@gmail.com", BirthDate = new DateTime(2001,5,3), IsAdmin = false, Password = "password"},
        new User{FullName = "Hasan Ibra", Email="someone@gmail.com", BirthDate = new DateTime(2004,2,1), IsAdmin = false, Password = "password"},
    };
}
