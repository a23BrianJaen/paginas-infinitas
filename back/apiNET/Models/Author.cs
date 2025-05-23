namespace apiNET.Models;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    public string ImageUrl { get; set; }

    // Inverse navigation
    public ICollection<Book> Books { get; set; }
}