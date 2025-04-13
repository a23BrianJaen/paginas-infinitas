namespace apiNET.DTOs.ResponseDtos;

// DTO | Data Transfer Object
public class BookResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public GenreResponseDto Genre { get; set; }
    public List<SubGenresResponseDto> SubGenre { get; set; } = new ();
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
    public List<TagsResponseDto> Tags { get; set; } = new();
    public List<AwardsResponseDto> Awards { get; set; }
    public string TargetAudience { get; set; }
    public string ReadingTime { get; set; }
    public AuthorResponseDto Author { get; set; } 
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
}