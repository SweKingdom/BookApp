namespace BookApp.API.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    
    public List<Book> Books { get; set; } = new();
    public List<Quote> Quotes { get; set; } = new();
}
