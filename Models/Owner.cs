namespace PokemonReviewApp.Models;

public class Owner
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Gym { get; set; }
    public Country Country { get; set; }
    public int CountryId { get; set; }
    public ICollection<Pokemon> Pokemons { get; set; }
    public ICollection<PokemonOwner> PokemonOwners { get; set; }
}
