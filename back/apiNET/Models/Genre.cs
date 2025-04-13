namespace apiNET.Models;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    // Inverse navigation
    public ICollection<Book> Books { get; set; }
}