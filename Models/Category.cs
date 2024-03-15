namespace PokemonReviewApp.Models;

public class Category
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public ICollection<Pokemon> Pokemons { get; set;} = new List<Pokemon>();
    public ICollection<PokemonCategory> PokemonCategories { get; set; } = new List<PokemonCategory>();
}
