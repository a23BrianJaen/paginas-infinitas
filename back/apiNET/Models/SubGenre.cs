namespace apiNET.Models;

public class SubGenre
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    // Navegación para relación muchos a muchos
    // Navigation to many a many relation
    public ICollection<BookSubGenre> BookSubGenres { get; set; }
}