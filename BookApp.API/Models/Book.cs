namespace BookApp.API.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}