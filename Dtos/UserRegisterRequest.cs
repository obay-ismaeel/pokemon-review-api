namespace PokemonReviewApp.Dtos;

public class UserRegisterRequest
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}