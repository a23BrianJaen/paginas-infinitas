namespace apiNET.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public string? ISBN { get; set; }
    public string? CoverImage { get; set; }
    public string? Publisher { get; set; }
    public string? Language { get; set; }
    public int PageCount { get; set; }
    public string? Format { get; set; }
    public decimal Price { get; set; }
    public string? Currency { get; set; }
    public int InStock { get; set; }
    public double Rating { get; set; }
    public int ReviewCount { get; set; }
    public string? Synopsis { get; set; }
    public string? TargetAudience { get; set; }
    public string? ReadingTime { get; set; }
    // public string? AuthorBio { get; set; }
    // public string? AuthorImage { get; set; }
    public string? PublicationDate { get; set; }
    public string? Edition { get; set; }
    public string? Dimensions { get; set; }
    public string? Weight { get; set; }
    public int SalesRank { get; set; }
    public string? MaturityRating { get; set; }
    public string? Series { get; set; }
    public string? SeriesOrder { get; set; }
    public string? TableOfContents { get; set; }
    public string? FileSize { get; set; }
    public int? WordCount { get; set; }
    
    // Relations
    public int AuthorId { get; set; }
    public Author Author { get; set; }
    
    public int GenreId { get; set; }
    public Genre Genre { get; set; }
    
    // Collection for relations for many-to-many
    public ICollection<BookSubGenre> BookSubGenres { get; set; } = new List<BookSubGenre>();
    public ICollection<BookTag> BookTags { get; set; } = new List<BookTag>();
    public ICollection<BookAward> BookAwards { get; set; } = new List<BookAward>();
}