namespace apiNET.Models;

public class BookAward
{
    public int BookId { get; set; }
    public Book Book { get; set; }
    public int AwardId { get; set; }
    public Award Award { get; set; }
}