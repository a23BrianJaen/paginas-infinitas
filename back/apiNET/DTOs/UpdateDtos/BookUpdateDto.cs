using apiNET.Models;

namespace apiNET.DTOs.UpdateDtos;

public class BookUpdateDto
{
    public string? Title { get; set; }
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
    public int Author { get; set; }
    public int Genre { get; set; }

    // Update relations many-to-many
    public List<int>? SubGenres { get; set; }
    public List<int>? Tags { get; set; }
    public List<int>? Awards { get; set; }
}