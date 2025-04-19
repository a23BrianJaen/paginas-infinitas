namespace apiNET.DTOs.ResponseDtos.Operation;

public class GenreOperationResponseDto
{
    public GenreResponseDto Genre { get; set; }
    public string Message { get; set; }
    public bool IsNewGenre { get; set; }
    public bool Success { get; set; }
}