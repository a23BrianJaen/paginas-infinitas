using apiNET.Models;
using apiNET.DTOs.ResponseDtos;
using apiNET.DTOs.CreateDtos;
using apiNET.DTOs.UpdateDtos;

namespace apiNET.Services.Interfaces;

public interface IAuthorService
{
    Task<AuthorOperationResponseDto> CreateAuthorAsync(AuthorCreateDto author);
    Task<IEnumerable<AuthorResponseDto>> GetAuthorsAsync();
    Task<AuthorResponseDto> GetAuthorByIdAsync(int id);
    Task<IEnumerable<BookResponseDto>> GetBooksByAuthorAsync(int authorId);
    Task<IEnumerable<AuthorResponseDto>> UpdateAuthorAsync(int id, AuthorUpdateDto updateAuthor);
    Task<bool> DeleteAuthorAsync(int id);
    Task<IEnumerable<AuthorResponseDto>> SearchByAuthorAsync(string author);
}