using System.ComponentModel.DataAnnotations;

namespace apiNET.DTOs.CreateDtos;

public class AuthorCreateDto
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(1000)]
    public string? Bio { get; set; }
    
    [StringLength(1000)]
    public string? ImageUrl { get; set; }
}