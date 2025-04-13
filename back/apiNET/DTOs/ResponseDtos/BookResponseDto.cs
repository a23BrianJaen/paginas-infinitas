namespace apiNET.DTOs;

// DTO | Data Transfer Object
public class BookResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    // public string Genre { get; set; }
    public List<string> SubGenres { get; set; }
    public string ISBN { get; set; }
    public string CoverImage { get; set; }
    public string Publisher { get; set; }
    public string Language { get; set; }
    public int PageCount { get; set; }
    public string Format { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; }
    public int InStock { get; set; }
    public double Rating { get; set; }
    public int ReviewCount { get; set; }
    public string Synopsis { get; set; }
    // public List<string> Tags { get; set; }
    public List<string> Awards { get; set; }
    public string TargetAudience { get; set; }
    public string ReadingTime { get; set; }
    // public string AuthorBio { get; set; }
    // public string AuthorImage { get; set; }
    public string PublicationDate { get; set; }
    public string Edition { get; set; }
    public string Dimensions { get; set; }
    public string Weight { get; set; }
    public int SalesRank { get; set; }
    public string MaturityRating { get; set; }
    public string? Series { get; set; }
    public string? SeriesOrder { get; set; }
    public string TableOfContents { get; set; }
    public string? FileSize { get; set; }
    public int? WordCount { get; set; }
    public AuthorResponseDto Author { get; set; }
    public List<TagsResponseDto> Tags { get; set; } = new();
    
    public List<GenreResponseDto> Genres { get; set; } = new();
}

public class AuthorResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    public string ImageUrl { get; set; }
}

public class TagsResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class GenreResponseDto 
{
    public int Id { get; set; }
    public string Name { get; set; }
}