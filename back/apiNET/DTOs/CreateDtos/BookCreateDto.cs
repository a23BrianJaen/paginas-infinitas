using System.ComponentModel.DataAnnotations;

namespace apiNET.DTOs.CreateDtos;

public class BookCreateDto
{
    [Required] public string Title { get; set; } = string.Empty;

    [Required] public int Year { get; set; }

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

    [Required] public int? AuthorId { get; set; }
    public AuthorCreateDto? NewAuthor { get; set; }

    [Required] public int? GenreId { get; set; }
    public GenreCreateDto? NewGenre { get; set; }

    // Many-to-many relations
    public List<int>? SubGenreIds { get; set; }
    public List<int>? TagIds { get; set; }
    public List<int>? AwardIds { get; set; }
}