namespace apiNET.DTOs.CreateDtos;

public class AuthorCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string? ImageUrl { get; set; }
}