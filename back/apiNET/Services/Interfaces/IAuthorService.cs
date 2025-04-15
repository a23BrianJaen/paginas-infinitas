using apiNET.Models;
using apiNET.DTOs.ResponseDtos;

namespace apiNET.Services.Interfaces;

public interface IAuthorService
{
    // Task<Author> PostAuthorAsync(Author author);
    Task<IEnumerable<AuthorResponseDto>> GetAuthorsAsync();
    Task<AuthorResponseDto> GetAuthorByIdAsync(int id);
    Task<IEnumerable<BookResponseDto>> GetBooksByAuthorAsync(int authorId);
    // Task UpdateAuthorAsync(Author author);
    // Task DeleteAuthorAsync(int id);
}