namespace apiNET.Models;

public class Award
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    // Navigation for many-to-many relations
    public ICollection<BookAward> BookAwards { get; set; }
}