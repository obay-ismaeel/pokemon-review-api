namespace PokemonReviewApp.Models;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsAdmin { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
