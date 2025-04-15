namespace apiNET.DTOs.ResponseDtos;

public class AuthorOperationResponseDto
{
    public AuthorResponseDto Author { get; set; }
    public string Message { get; set; }
    public bool IsNewAuthor { get; set; }
    public bool Success { get; set; }
}