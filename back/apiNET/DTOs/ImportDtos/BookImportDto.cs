namespace apiNET.DTOs.ImportDtos;

public class BookImportDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string AuthorBio { get; set; } = string.Empty;
    public string AuthorImage { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Genre { get; set; } = string.Empty;
    public List<string> SubGenres { get; set; } = new();
    public string ISBN { get; set; } = string.Empty;
    public string CoverImage { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public int PageCount { get; set; }
    public string Format { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Currency { get; set; } = string.Empty;
    public int InStock { get; set; }
    public double Rating { get; set; }
    public int ReviewCount { get; set; }
    public string Synopsis { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public List<string> Awards { get; set; } = new();
    public string TargetAudience { get; set; } = string.Empty;
    public string ReadingTime { get; set; } = string.Empty;
    public string PublicationDate { get; set; } = string.Empty;
    public string Edition { get; set; } = string.Empty;
    public string Dimensions { get; set; } = string.Empty;
    public string Weight { get; set; } = string.Empty;
    public int SalesRank { get; set; }
    public string MaturityRating { get; set; } = string.Empty;
    public string? Series { get; set; }
    public string? SeriesOrder { get; set; }
    public string TableOfContents { get; set; } = string.Empty;
    public string? FileSize { get; set; }
    public int? WordCount { get; set; }
}