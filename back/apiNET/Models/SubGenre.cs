namespace apiNET.Models;

public class SubGenre
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    // Navigation to for many-to-many relation
    public ICollection<BookSubGenre> BookSubGenres { get; set; }
}