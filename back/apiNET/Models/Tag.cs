namespace apiNET.Models;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    // Navigation for many-to-many relations
    public ICollection<BookTag> BookTags { get; set; }
}